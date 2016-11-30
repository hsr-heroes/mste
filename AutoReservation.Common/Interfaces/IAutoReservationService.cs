using System.Collections.Generic;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{
    public interface IAutoReservationService
    {
        ReservationDto InsertReservation(ReservationDto reservation);
        ReservationDto UpdateReservation(ReservationDto reservation);
        IEnumerable<KundeDto> Kunden { get; }
        IEnumerable<AutoDto> Autos { get; }
        IEnumerable<ReservationDto> Reservationen { get; }
        AutoDto InsertAuto(AutoDto auto);
        AutoDto UpdateAuto(AutoDto auto);
        void DeleteAuto(AutoDto selectedAuto);
        KundeDto InsertKunde(KundeDto kunde);
        KundeDto UpdateKunde(KundeDto kunde);
        void DeleteKunde(KundeDto selectedKunde);
        void DeleteReservation(ReservationDto selectedReservation);
    }
}
