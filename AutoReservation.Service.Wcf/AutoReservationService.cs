using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoReservation.BusinessLayer;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {
        private AutoReservationBusinessComponent service = new AutoReservationBusinessComponent();

        private static void WriteActualMethod()
        {
            Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");
        }

        public IEnumerable<KundeDto> Kunden
        {
            get
            {
                WriteActualMethod();
                return service.Kunden.ConvertToDtos();
            }
        }

        public IEnumerable<AutoDto> Autos { get
        {
                WriteActualMethod();
            return service.Autos.ConvertToDtos();
            }
        }
        public IEnumerable<ReservationDto> Reservationen { get
            {
                WriteActualMethod();
                return service.Reservationen.ConvertToDtos();
            }
        }

        public ReservationDto InsertReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            return service.InsertReservation(reservation.ConvertToEntity()).ConvertToDto();
        }

        public ReservationDto UpdateReservation(ReservationDto reservation)
        {
            WriteActualMethod();
            try
            {
                return service.UpdateReservation(reservation.ConvertToEntity()).ConvertToDto();
            }
            catch (Exception)
            {
                throw new LocalOptimisticConcurrencyException<ReservationDto>("concurrent modification");
            }
           
        }

        public AutoDto InsertAuto(AutoDto auto)
        {
            WriteActualMethod();
            return service.InsertAuto(auto.ConvertToEntity()).ConvertToDto();
        }

        public AutoDto UpdateAuto(AutoDto auto)
        {
            WriteActualMethod();
            try
            {
                return service.UpdateAuto(auto.ConvertToEntity()).ConvertToDto();
            }
            catch (Exception)
            {
               throw new LocalOptimisticConcurrencyException<AutoDto>("concurrent modification");
            }
        }

        public void DeleteAuto(AutoDto selectedAuto)
        {
            WriteActualMethod();
            service.DeleteAuto(selectedAuto.ConvertToEntity());
        }

        public KundeDto InsertKunde(KundeDto kunde)
        {
            WriteActualMethod();
            return service.InsertKunde(kunde.ConvertToEntity()).ConvertToDto();
        }

        public KundeDto UpdateKunde(KundeDto kunde)
        {
            WriteActualMethod();
            try
            {
                return service.UpdateKunde(kunde.ConvertToEntity()).ConvertToDto();
            }
            catch (Exception)
            {
                throw new LocalOptimisticConcurrencyException<KundeDto>("");
            }
        }

        public void DeleteKunde(KundeDto selectedKunde)
        {
            WriteActualMethod();
            service.DeleteKunde(selectedKunde.ConvertToEntity());
        }

        public void DeleteReservation(ReservationDto selectedReservation)
        {
            WriteActualMethod();
            service.DeleteReservation(selectedReservation.ConvertToEntity());
        }
    }
}