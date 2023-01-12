using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Financije
{
    public class BankAccountBlockedException : ApplicationException
    {
        public BankAccountBlockedException()
        {

        }
    }

    public class Banka
    {
        private List<Racun> Racuni { get; set; }

        public Banka()
        {
            Racuni = new List<Racun>();

            Racuni.Add(new Racun { IBAN = "HR11", Stanje = 100000, Blokiran = false });
            Racuni.Add(new Racun { IBAN = "HR22", Stanje = 50000, Blokiran = false });
            Racuni.Add(new Racun { IBAN = "HR33", Stanje = 12000, Blokiran = false });
            Racuni.Add(new Racun { IBAN = "HR44", Stanje = 36000, Blokiran = true });
            Racuni.Add(new Racun { IBAN = "HR55", Stanje = 8000, Blokiran = false });
            Racuni.Add(new Racun { IBAN = "HR66", Stanje = 2000, Blokiran = false, OdobreniMinus = 3000 });
        }

        public Racun DohvatiRacun(string iban)
        {
            var racun = Racuni.FirstOrDefault(r => r.IBAN == iban);

            if (racun != null && racun.Blokiran)
                throw new BankAccountBlockedException();

            return racun;
        }
    }
}
