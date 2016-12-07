using System.Runtime.Serialization;
using System.Text;
using AutoReservation.Common.DataTransferObjects.Core;

namespace AutoReservation.Common.DataTransferObjects
{
    [DataContract]
    public class AutoDto : DtoBase<AutoDto>
    {

        public override string Validate()
        {
            StringBuilder error = new StringBuilder();
            if (string.IsNullOrEmpty(Marke))
            {
                error.AppendLine("- Marke ist nicht gesetzt.");
            }
            if (Tagestarif <= 0)
            {
                error.AppendLine("- Tagestarif muss grösser als 0 sein.");
            }
            if (AutoKlasse == AutoKlasse.Luxusklasse && Basistarif <= 0)
            {
                error.AppendLine("- Basistarif eines Luxusautos muss grösser als 0 sein.");
            }

            if (error.Length == 0) { return null; }

            return error.ToString();
        }

        public override string ToString()
            => $"{Id}; {Marke}; {Tagestarif}; {Basistarif}; {AutoKlasse}";
        [DataMember]
        public AutoKlasse AutoKlasse { get; set; }
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Marke { get; set; }
        [DataMember]
        public int Tagestarif { get; set; }
        [DataMember]
        public byte[] RowVersion { get; set; }
        [DataMember]
        public int Basistarif { get; set; }
    }
}
