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
            using (var ctx = new AutoReservationContext())
            {
                ctx.Reservationen.Add(reservation);
                ctx.SaveChanges();
                return reservation;
            }
        }

        public Reservation UpdateReservation(Reservation reservation)
        {
            using (var ctx = new AutoReservationContext())
            {
                ctx.Entry(reservation).State = EntityState.Modified;
                ctx.SaveChanges();
                return reservation;
            }
        }

        public Auto InsertAuto(Auto auto)
        {
            using (var ctx = new AutoReservationContext())
            {
                ctx.Autos.Add(auto);
                ctx.SaveChanges();
                return auto;
            }
        }

        public Auto UpdateAuto(Auto auto)
        {
            using (var ctx = new AutoReservationContext())
            {
                ctx.Entry(auto).State = EntityState.Modified;
                ctx.SaveChanges();
                return auto;
            }
        }

        public void DeleteAuto(Auto auto)
        {
            using (var ctx = new AutoReservationContext())
            {
                ctx.Autos.Remove(ctx.Autos.FirstOrDefault(e => e.Id == auto.Id));
                ctx.SaveChanges();
            }
        }

        public Kunde InsertKunde(Kunde kunde)
        {
            using (var ctx = new AutoReservationContext())
            {
                ctx.Kunden.Add(kunde);
                ctx.SaveChanges();
                return kunde;
            }
        }

        public Kunde UpdateKunde(Kunde kunde)
        {
            using (var ctx = new AutoReservationContext())
            {
                ctx.Entry(kunde).State = EntityState.Modified;
                ctx.SaveChanges();
                return kunde;
            }
        }

        public void DeleteKunde(Kunde kunde)
        {
            using (var ctx = new AutoReservationContext())
            {
                ctx.Kunden.Remove(ctx.Kunden.FirstOrDefault(e => e.Id == kunde.Id));
                ctx.SaveChanges();
            }
        }

        public void DeleteReservation(Reservation reservation)
        {
            using (var ctx = new AutoReservationContext())
            {
                ctx.Reservationen.Remove(ctx.Reservationen.FirstOrDefault(e => e.ReservationsNr == reservation.ReservationsNr));
                ctx.SaveChanges();
            }
        }
    }
}