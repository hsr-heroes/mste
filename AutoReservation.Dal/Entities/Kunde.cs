using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AutoReservation.Dal.Entities
{
    public class Kunde
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Nachname { get; set; }
        [MaxLength(20)]
        public string Vorname { get; set; }
        public DateTime Geburtsdatum { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public int ReservationId { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}
