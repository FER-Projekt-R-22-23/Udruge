using BaseLibrary;
using UdrugeApp.Commons;

namespace UdrugeApp.Domain.Models
{
    public class Udruge
    {
        private int _IdUdruge;
        private string _OIB;
        private string _Naziv;
        private string _Sjediste;
        private string _BrMob;
        private string _Mail;

        public int IdUdruge { get => _IdUdruge; set => _IdUdruge = value; }
        public string OIB { get => _OIB; set => _OIB = value; }    
        public string Naziv { get => _Naziv; set => _Naziv = value; }
        public string Sjediste { get => _Sjediste; set => _Sjediste = value; }
        public string BrMob { get => _BrMob; set => _BrMob = value; }
        public string Mail { get => _Mail; set => _Mail = value; }

        public Udruge(int idUdruge, string OIB, string Naziv, string Sjediste, string BrMob, string Mail) {
            if (string.isNullOrEmpty(OIB))
            {
                throw new ArgumentException($" '{nameof(OIB)}' cannot be null or empty.", nameof(OIB));
            }

            if (string.isNullOrEmpty(Naziv))
            {
                throw new ArgumentException($" '{nameof(Naziv)}' cannot be null or empty.", nameof(Naziv));
            }

            if (string.isNullOrEmpty(Sjediste))
            {
                throw new ArgumentException($" '{nameof(Sjediste)}' cannot be null or empty.", nameof(Sjediste));
            }

            if (string.isNullOrEmpty(BrMob))
            {
                throw new ArgumentException($" '{nameof(BrMob)}' cannot be null or empty.", nameof(BrMob));
            }

            if (string.isNullOrEmpty(Mail))
            {
                throw new ArgumentException($" '{nameof(Mail)}' cannot be null or empty.", nameof(Mail));
            }

            _IdUdruge = idUdruge;
            _OIB = OIB;
            _Naziv = Naziv;
            _Sjediste = Sjediste;
            _BrMob = BrMob;
            _Mail = Mail;
        }

        public override bool Equals(object? obj)
        {
            return obj is not null &&
                obj is Udruge Udruge &&
                _IdUdruge == Udruge._IdUdruge &&
                _OIB == Udruge._OIB &&
                _Naziv == Udruge._Naziv &&
                _Sjediste == Udruge._Sjediste &&
                _BrMob == Udruge._BrMob &&
                _Mail == Udruge._Mail;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_IdUdruge, _OIB, _Naziv, _Sjediste, _BrMob, _Mail);
        }

        public override Result IsValid()
            => Validation.Validate(
                (() => _OIB.Length == 11, "OIB length must be 11 characters"),
                (() => _Naziv.Length <= 30, "Naziv length must be less than 30 characters")
                (() => _Sjediste.Length <= 60, "Sjediste length must be less than 60 characters")
                (() => _BrMob.Length <= 15, "BrMob length must be less than 15 characters")
                (() => _Mail.Length <= 50, "Mail length must be less than 50 characters")
                );
    }
}
