using System;
using System.Runtime.Serialization;
using System.Text;
using AutoReservation.Common.DataTransferObjects.Core;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class KundeDto : DtoBase<KundeDto>
    {

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(Nachname))
            {
                error.AppendLine("- Nachname ist nicht gesetzt.");
            }
            if (string.IsNullOrEmpty(Vorname))
            {
                error.AppendLine("- Vorname ist nicht gesetzt.");
            }
            if (Geburtsdatum == DateTime.MinValue)
            {
                error.AppendLine("- Geburtsdatum ist nicht gesetzt.");
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override string ToString()
            => $"{Id}; {Nachname}; {Vorname}; {Geburtsdatum}";
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Nachname { get; set; }
        [DataMember]
        public string Vorname { get; set; }
        [DataMember]
        public DateTime Geburtsdatum { get; set; }
        [DataMember]
        public byte[] RowVersion { get; set; }
    }
}
