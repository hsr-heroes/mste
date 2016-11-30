using System.Text;
using AutoReservation.Common.DataTransferObjects.Core;

namespace AutoReservation.Common.DataTransferObjects
{
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

        public AutoKlasse AutoKlasse { get; set; }
        public int Id { get; set; }
        public string Marke { get; set; }
        public int Tagestarif { get; set; }
        public byte[] RowVersion { get; set; }
        public int Basistarif { get; set; }
    }
}
