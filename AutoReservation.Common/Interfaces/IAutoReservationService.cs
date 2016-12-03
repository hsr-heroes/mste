﻿using System.Collections.Generic;
using System.ServiceModel;
using AutoReservation.Common.DataTransferObjects;

namespace AutoReservation.Common.Interfaces
{
    [ServiceContract]
    public interface IAutoReservationService
    {
        IEnumerable<KundeDto> Kunden { [OperationContract]get; }
        IEnumerable<AutoDto> Autos { [OperationContract] get; }
        IEnumerable<ReservationDto> Reservationen { [OperationContract]get; }
        [OperationContract]
        ReservationDto InsertReservation(ReservationDto reservation);
        [OperationContract]
        ReservationDto UpdateReservation(ReservationDto reservation);


        [OperationContract]
        AutoDto InsertAuto(AutoDto auto);
        [OperationContract]
        AutoDto UpdateAuto(AutoDto auto);
        [OperationContract]
        void DeleteAuto(AutoDto selectedAuto);
        [OperationContract]
        KundeDto InsertKunde(KundeDto kunde);
        [OperationContract]
        KundeDto UpdateKunde(KundeDto kunde);
        [OperationContract]
        void DeleteKunde(KundeDto selectedKunde);
        [OperationContract]
        void DeleteReservation(ReservationDto selectedReservation);
    }
}
