using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Financije;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test1()
        {
            Banka banka = new Banka();

            var racun = banka.DohvatiRacun("HR12");

            Assert.IsNull(racun);
        }

        [TestMethod]
        public void Test2()
        {
            Banka banka = new Banka();

            var racun = banka.DohvatiRacun("HR11");

            Assert.IsTrue(racun.IBAN == "HR11");
        }

        [TestMethod]
        public void Test3()
        {
            Banka banka = new Banka();

            Assert.ThrowsException<BankAccountBlockedException>(() => banka.DohvatiRacun("HR44"));
        }

        [TestMethod]
        public void Test4()
        {
            Banka banka = new Banka();
            var racun = banka.DohvatiRacun("HR11");
            var primatelj = banka.DohvatiRacun("HR21");

            Assert.ThrowsException<TransactionFailedException>(() => racun.Isplati(primatelj, 30000));
        }

        [TestMethod]
        public void Test5()
        {
            Banka banka = new Banka();
            var racun = banka.DohvatiRacun("HR11");
            var primatelj = banka.DohvatiRacun("HR22");

            racun.Isplati(primatelj, 30000);

            Assert.IsTrue(racun.Stanje == 70000 && primatelj.Stanje == 80000);
        }

        [TestMethod]
        public void Test6()
        {
            Banka banka = new Banka();
            var racun = banka.DohvatiRacun("HR11");
            var primatelj = banka.DohvatiRacun("HR22");

            var isplata = racun.Isplati(primatelj, 30000);

            Assert.IsTrue(
                isplata.Primatelj == "HR22" &&
                isplata.Platitelj == "HR11" &&
                isplata.Iznos == 30000
            );
        }

        [TestMethod]
        public void Test7()
        {
            Banka banka = new Banka();
            var racun = banka.DohvatiRacun("HR66");
            var primatelj = banka.DohvatiRacun("HR55");

            Assert.ThrowsException<TransactionFailedException>(() => racun.Isplati(primatelj, 130000));
        }

        [TestMethod]
        public void Test8()
        {
            Banka banka = new Banka();
            var racun = banka.DohvatiRacun("HR66");
            var primatelj = banka.DohvatiRacun("HR55");

            var isplata = racun.Isplati(primatelj, 3000);

            Assert.IsTrue(
                racun.Stanje == -1000 &&
                primatelj.Stanje == 11000 &&
                isplata.Iznos == 3000
            );
        }

        [TestMethod]
        public void Test9()
        {
            Banka banka = new Banka();
            var racun = banka.DohvatiRacun("HR66");
            var primatelj = banka.DohvatiRacun("HR55");

            Assert.ThrowsException<TransactionFailedException>(() => racun.Isplati(primatelj, 5500));

        }

        [TestMethod]
        public void Test10()
        {
            Banka banka = new Banka();
            var racun = banka.DohvatiRacun("HR11");
            var primatelj = banka.DohvatiRacun("HR22");

            Assert.ThrowsException<TransactionFailedException>(() => racun.Isplati(primatelj, -5));

        }
    }
}
