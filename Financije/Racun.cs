using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financije
{
    public class TransactionFailedException : ApplicationException
    {

    }

    public class Racun
    {
        public string IBAN { get; set; }
        public double Stanje { get; set; }
        public bool Blokiran { get; set; }
        public double OdobreniMinus { get; set; }

        public Isplata Isplati(Racun primatelj, double iznos)
        {
            Isplata isplata = null;

            if (primatelj == null)
            {
                throw new TransactionFailedException();
            }

            if ((iznos > Stanje && iznos > this.OdobreniMinus) || iznos < 0)
            {
                throw new TransactionFailedException();
            }

            primatelj.Stanje += iznos;
            Stanje -= iznos;

            isplata = new Isplata
            {
                Platitelj = IBAN,
                Primatelj = primatelj.IBAN,
                Iznos = iznos
            };

            return isplata;
        }
    }
}
