using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.Dal;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {

        private static void WriteActualMethod()
        {
            Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");
        }

        public IEnumerable<KundeDto> Kunden
        {
            get
            {
                using (var ctx = new AutoReservationContext())
                {
                    WriteActualMethod();
                    return ctx.Kunden.ToList().Select(k => k.ConvertToDto());
                }
            }
        }

        public IEnumerable<AutoDto> Autos { get
            {
                using (var ctx = new AutoReservationContext())
                {
                    WriteActualMethod();
                    return ctx.Autos.ToList().Select(k => k.ConvertToDto());
                }
            }
        }
        public IEnumerable<ReservationDto> Reservationen { get
            {
                using (var ctx = new AutoReservationContext())
                {
                    WriteActualMethod();
                    return ctx.Reservationen.Include(r => r.Auto).Include(r => r.Kunde).ToList().Select(k => k.ConvertToDto());
                }
            }
        }

        public ReservationDto InsertReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            using (var ctx = new AutoReservationContext())
            {
                var entity = reservation.ConvertToEntity();
                ctx.Reservationen.AddOrUpdate(entity);
                ctx.SaveChanges();
                return entity.ConvertToDto();
            }
        }

        public ReservationDto UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            using (var ctx = new AutoReservationContext())
            {
                var entity = reservation.ConvertToEntity();
                ctx.Reservationen.Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
                return entity.ConvertToDto();
            }
        }

        public AutoDto InsertAuto(AutoDto auto)
        {
            WriteActualMethod();
            using (var ctx = new AutoReservationContext())
            {
                var dbAuto = auto.ConvertToEntity();
                ctx.Autos.AddOrUpdate(dbAuto);
                ctx.SaveChanges();
                return dbAuto.ConvertToDto();
            }
        }

        public AutoDto UpdateAuto(AutoDto auto)
        {
            WriteActualMethod();
            using (var ctx = new AutoReservationContext())
            {
                var entity = auto.ConvertToEntity();
                ctx.Autos.AddOrUpdate(entity);
                ctx.SaveChanges();
                return entity.ConvertToDto();
            }
        }

        public void DeleteAuto(AutoDto selectedAuto)
        {
            WriteActualMethod();
            using (var ctx = new AutoReservationContext())
            {
                ctx.Autos.Remove(ctx.Autos.FirstOrDefault(e => e.Id == selectedAuto.Id));
                ctx.SaveChanges();
            }
        }

        public KundeDto InsertKunde(KundeDto kunde)
        {
            WriteActualMethod();
            using (var ctx = new AutoReservationContext())
            {
                var entity = kunde.ConvertToEntity();
                ctx.Kunden.Add(entity);
                ctx.SaveChanges();
                return entity.ConvertToDto();
            }
        }

        public KundeDto UpdateKunde(KundeDto kunde)
        {
            WriteActualMethod();
            using (var ctx = new AutoReservationContext())
            {
                var entity = kunde.ConvertToEntity();
                ctx.Kunden.Attach(entity);
                ctx.Entry(entity).State = EntityState.Modified;
                ctx.SaveChanges();
                return entity.ConvertToDto();
            }
        }

        public void DeleteKunde(KundeDto selectedKunde)
        {
            WriteActualMethod();
            using (var ctx = new AutoReservationContext())
            {
                ctx.Kunden.Remove(ctx.Kunden.FirstOrDefault(e => e.Id == selectedKunde.Id));
                ctx.SaveChanges();
            }
        }

        public void DeleteReservation(ReservationDto selectedReservation)
        {
            WriteActualMethod();
            using (var ctx = new AutoReservationContext())
            {
                ctx.Reservationen.Remove(ctx.Reservationen.FirstOrDefault(e => e.ReservationsNr == selectedReservation.ReservationsNr));
                ctx.SaveChanges();
            }
        }
    }
}