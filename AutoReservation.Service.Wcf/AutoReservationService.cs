using System;
using System.Collections.Generic;
using System.Diagnostics;
using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;

namespace AutoReservation.Service.Wcf
{
    public class AutoReservationService : IAutoReservationService
    {

        private static void WriteActualMethod()
        {
            Console.WriteLine($"Calling: {new StackTrace().GetFrame(1).GetMethod().Name}");
        }

        public ReservationDto InsertReservation(ReservationDto reservation)
        {
            throw new NotImplementedException();
        }

        public ReservationDto UpdateReservation(ReservationDto reservation)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<KundeDto> Kunden { get; }
        public IEnumerable<AutoDto> Autos { get; }
        public IEnumerable<ReservationDto> Reservationen { get; }
        public AutoDto InsertAuto(AutoDto auto)
        {
            throw new NotImplementedException();
        }

        public AutoDto UpdateAuto(AutoDto auto)
        {
            throw new NotImplementedException();
        }

        public void DeleteAuto(AutoDto selectedAuto)
        {
            throw new NotImplementedException();
        }

        public KundeDto InsertKunde(KundeDto kunde)
        {
            throw new NotImplementedException();
        }

        public KundeDto UpdateKunde(KundeDto kunde)
        {
            throw new NotImplementedException();
        }

        public void DeleteKunde(KundeDto selectedKunde)
        {
            throw new NotImplementedException();
        }

        public void DeleteReservation(ReservationDto selectedReservation)
        {
            throw new NotImplementedException();
        }
    }
}