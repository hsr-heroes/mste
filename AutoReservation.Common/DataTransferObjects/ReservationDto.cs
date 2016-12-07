using System;
using System.Runtime.Serialization;
using System.Text;
using AutoReservation.Common.DataTransferObjects.Core;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class ReservationDto : DtoBase<ReservationDto>
    {

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (Von == DateTime.MinValue)
            {
                error.AppendLine("- Von-Datum ist nicht gesetzt.");
            }
            if (Bis == DateTime.MinValue)
            {
                error.AppendLine("- Bis-Datum ist nicht gesetzt.");
            }
            if (Von > Bis)
            {
                error.AppendLine("- Von-Datum ist grösser als Bis-Datum.");
            }
            if (Auto == null)
            {
                error.AppendLine("- Auto ist nicht zugewiesen.");
            }
            else
            {
                string autoError = Auto.Validate();
                if (!string.IsNullOrEmpty(autoError))
                {
                    error.AppendLine(autoError);
                }
            }
            if (Kunde == null)
            {
                error.AppendLine("- Kunde ist nicht zugewiesen.");
            }
            else
            {
                string kundeError = Kunde.Validate();
                if (!string.IsNullOrEmpty(kundeError))
                {
                    error.AppendLine(kundeError);
                }
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override string ToString()
            => $"{ReservationsNr}; {Von}; {Bis}; {Auto}; {Kunde}";

        [DataMember]
        public int ReservationsNr { get; set; }
        [DataMember]
        public DateTime Von { get; set; }
        [DataMember]
        public DateTime Bis { get; set; }
        [DataMember]
        public AutoDto Auto { get; set; }
        [DataMember]
        public KundeDto Kunde { get; set; }
        [DataMember]
        public byte[] RowVersion { get; set; }
    }
}
