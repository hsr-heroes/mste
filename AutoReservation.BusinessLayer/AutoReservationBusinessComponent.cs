using System;
using AutoReservation.Dal;
using AutoReservation.Dal.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;

namespace AutoReservation.BusinessLayer
{
    public class AutoReservationBusinessComponent
    {
        private static LocalOptimisticConcurrencyException<T> CreateLocalOptimisticConcurrencyException<T>(AutoReservationContext context, T entity)
            where T : class
        {
            var dbEntity = (T)context.Entry(entity)
                .GetDatabaseValues()
                .ToObject();

            return new LocalOptimisticConcurrencyException<T>($"Update {typeof(T).Name}: Concurrency-Fehler", dbEntity);
        }

        public IEnumerable<Kunde> Kunden
        {
            get
            {
                using (var ctx = new AutoReservationContext())
                {
                    return ctx.Kunden.ToList();
                }
            }
        }

        public IEnumerable<Auto> Autos
        {
            get
            {
                using (var ctx = new AutoReservationContext())
                {
                    return ctx.Autos.ToList();
                }
            }
        }

        public IEnumerable<Reservation> Reservationen
        {
            get
            {
                using (var ctx = new AutoReservationContext())
                {
                    return ctx.Reservationen.Include(r => r.Auto).Include(r => r.Kunde).ToList();
                }
            }
        }

        public Reservation InsertReservation(Reservation reservation)
        {
            return InsertEntity(reservation, ctx => ctx.Reservationen);
        }

        public Reservation UpdateReservation(Reservation reservation)
        {
            return UpdateEntity(reservation);
        }

        public Auto InsertAuto(Auto auto)
        {
            return InsertEntity(auto, ctx => ctx.Autos);
        }

        public Auto UpdateAuto(Auto auto)
        {
            return UpdateEntity(auto);
        }

        public void DeleteAuto(Auto auto)
        {
            DeleteEntity(ctx => ctx.Autos, e => e.Id == auto.Id);
        }

        public Kunde InsertKunde(Kunde kunde)
        {
            return InsertEntity(kunde, ctx => ctx.Kunden);
        }

        public Kunde UpdateKunde(Kunde kunde)
        {
            return UpdateEntity(kunde);
        }

        public void DeleteKunde(Kunde kunde)
        {
            DeleteEntity(ctx => ctx.Kunden, e => e.Id == kunde.Id);
        }

        public void DeleteReservation(Reservation reservation)
        {
            DeleteEntity(ctx => ctx.Reservationen, e => e.ReservationsNr == reservation.ReservationsNr);
        }

        private static void DeleteEntity<T>(Func<AutoReservationContext, DbSet<T>> setSelector, Func<T, bool> pred ) where T : class
        {
            using (var ctx = new AutoReservationContext())
            {
                var dbSet = setSelector(ctx);
                var toRemove = dbSet.FirstOrDefault(pred);
                if (toRemove != null)
                {
                    dbSet.Remove(toRemove);
                    ctx.SaveChanges();
                }
            }
        }

        private static T InsertEntity<T>(T entity, Func<AutoReservationContext, DbSet<T>> setSelector) where T : class
        {
            using (var ctx = new AutoReservationContext())
            {
                setSelector(ctx).Add(entity);
                ctx.SaveChanges();
                return entity;
            }
        }

        private static T UpdateEntity<T>(T entity) where T : class
        {
            using (var ctx = new AutoReservationContext())
            {
                try
                {
                    ctx.Entry(entity).State = EntityState.Modified;
                    ctx.SaveChanges();
                    return entity;
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw CreateLocalOptimisticConcurrencyException(ctx, entity);
                }
            }
        }
    }
}