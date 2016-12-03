using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoReservation.Dal.Entities
{
    public class Reservation
    {
        [Column("Id"), Key]
        public int ReservationsNr { get; set; }
        public int AutoId { get; set; }
        public virtual Auto Auto { get; set; }
        public int KundeId { get; set; }
        public virtual Kunde Kunde { get; set; }
        public DateTime Von { get; set; }
        public DateTime Bis { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        
    }
}
