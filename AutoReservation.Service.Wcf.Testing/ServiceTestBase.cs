using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace AutoReservation.Service.Wcf.Testing
{
    [TestClass]
    public abstract class ServiceTestBase
    {
        protected abstract IAutoReservationService Target { get; }

        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }

        #region Read all entities

        [TestMethod]
        public void GetAutosTest()
        {
            var alleAutos = Target.Autos.ToList();
            Assert.AreEqual(3, alleAutos.Count);
            AssertAuto(alleAutos[0], 1, AutoKlasse.Standard, 0, 50, "Fiat Punto");
            AssertAuto(alleAutos[1], 2, AutoKlasse.Mittelklasse, 0, 120, "VW Golf");
            AssertAuto(alleAutos[2], 3, AutoKlasse.Luxusklasse, 50, 180, "Audi S6");
        }

        private static void AssertAuto(AutoDto auto, int id,  AutoKlasse autoKlasse, int basistarif, int tagestarif, string marke)
        {
            Assert.AreEqual(id, auto.Id);
            Assert.AreEqual(autoKlasse, auto.AutoKlasse);
            Assert.AreEqual(basistarif, auto.Basistarif);
            Assert.AreEqual(tagestarif, auto.Tagestarif);
            Assert.AreEqual(marke, auto.Marke);
        }

        [TestMethod]
        public void GetKundenTest()
        {
            var kunden = Target.Kunden.ToList();
            Assert.AreEqual(4, kunden.Count);
            AssertKunde(kunden[0], 1, "Nass", "Anna", new DateTime(1981, 05, 05));
            AssertKunde(kunden[1], 2, "Beil", "Timo", new DateTime(1980, 09, 09));
            AssertKunde(kunden[2], 3, "Pfahl", "Martha", new DateTime(1990, 07, 03));
            AssertKunde(kunden[3], 4, "Zufall", "Rainer", new DateTime(1954, 11, 11));
        }

        private void AssertKunde(KundeDto kundeDto, int id, string nachname, string vorname, DateTime geburtsdatum)
        {
            Assert.AreEqual(id, kundeDto.Id);
            Assert.AreEqual(nachname, kundeDto.Nachname);
            Assert.AreEqual(vorname, kundeDto.Vorname);
            Assert.AreEqual(geburtsdatum, kundeDto.Geburtsdatum);
        }

        [TestMethod]
        public void GetReservationenTest()
        {
            var reservationen = Target.Reservationen.ToList();
            Assert.AreEqual(3, reservationen.Count);
            AssertReservation(reservationen[0], 1, new DateTime(2020, 01, 10), new DateTime(2020, 01, 20));
            AssertAuto(reservationen[0].Auto, 1, AutoKlasse.Standard, 0, 50, "Fiat Punto");
            AssertKunde(reservationen[0].Kunde, 1, "Nass", "Anna", new DateTime(1981, 05, 05));
            AssertReservation(reservationen[1], 2, new DateTime(2020, 01, 10), new DateTime(2020, 01, 20));
            AssertAuto(reservationen[1].Auto, 2, AutoKlasse.Mittelklasse, 0, 120, "VW Golf");
            AssertReservation(reservationen[2], 3, new DateTime(2020, 01, 10), new DateTime(2020, 01, 20));
            AssertKunde(reservationen[2].Kunde, 3, "Pfahl", "Martha", new DateTime(1990, 07, 03));

        }

        private void AssertReservation(ReservationDto reservationDto, int id, DateTime von, DateTime bis)
        {
            Assert.AreEqual(id, reservationDto.ReservationsNr);
            Assert.AreEqual(von, reservationDto.Von);
            Assert.AreEqual(bis, reservationDto.Bis);
        }

        #endregion

        #region Get by existing ID

        [TestMethod]
        public void GetAutoByIdTest()
        {
            var auto = Target.Autos.First(a => a.Id == 2);
            AssertAuto(auto, 2, AutoKlasse.Mittelklasse, 0, 120, "VW Golf");
        }

        [TestMethod]
        public void GetKundeByIdTest()
        {
            var kunde = Target.Kunden.First(a => a.Id == 1);
            AssertKunde(kunde, 1, "Nass", "Anna", new DateTime(1981, 05, 05));
        }

        [TestMethod]
        public void GetReservationByNrTest()
        {
            var reservation = Target.Reservationen.First(r => r.ReservationsNr == 1);
            AssertReservation(reservation, 1, new DateTime(2020, 01, 10), new DateTime(2020, 01, 20));
            AssertAuto(reservation.Auto, 1, AutoKlasse.Standard, 0, 50, "Fiat Punto");
            AssertKunde(reservation.Kunde, 1, "Nass", "Anna", new DateTime(1981, 05, 05));
        }

        #endregion

        #region Get by not existing ID

        [TestMethod]
        public void GetAutoByIdWithIllegalIdTest()
        {
            Assert.IsNull(Target.Autos.FirstOrDefault(a => a.Id == 999));
        }

        [TestMethod]
        public void GetKundeByIdWithIllegalIdTest()
        {
            Assert.IsNull(Target.Kunden.FirstOrDefault(a => a.Id == 999));
        }

        [TestMethod]
        public void GetReservationByNrWithIllegalIdTest()
        {
            Assert.IsNull(Target.Reservationen.FirstOrDefault(a => a.ReservationsNr == 999));
        }

        #endregion

        #region Insert

        [TestMethod]
        public void InsertAutoTest()
        {
            var result = Target.InsertAuto(new AutoDto
            {
                AutoKlasse = AutoKlasse.Luxusklasse,
                Basistarif = 55,
                Marke = "Mercedes",
                Tagestarif = 12
            });
            Assert.AreEqual(4, Target.Autos.Count());
            Assert.IsTrue(result.Id > 0);
            AssertAuto(Target.Autos.First(a => a.Id == result.Id), result.Id, AutoKlasse.Luxusklasse, 55, 12, "Mercedes");
        }

        [TestMethod]
        public void InsertKundeTest()
        {
            var kunde = Target.InsertKunde(new KundeDto
            {
                Geburtsdatum = new DateTime(1970, 1, 1),
                Nachname = "Unix",
                Vorname = "UU"
            });
            Assert.AreEqual(5, Target.Kunden.Count());
            Assert.IsTrue(kunde.Id > 0);
            AssertKunde(Target.Kunden.First(k => k.Id == kunde.Id), kunde.Id, "Unix", "UU", new DateTime(1970, 1, 1));
        }

        [TestMethod]
        public void InsertReservationTest()
        {
            var res = Target.InsertReservation(new ReservationDto
            {
                Auto = Target.Autos.First(a => a.Id == 1),
                Von = new DateTime(2016, 12, 3),
                Bis = new DateTime(2016, 12, 4),
                Kunde = Target.Kunden.First(k => k.Id == 1)
            });
            Assert.IsTrue(res.ReservationsNr > 0);
            var savedRes = Target.Reservationen.First(r => r.ReservationsNr == res.ReservationsNr);
            AssertReservation(savedRes, res.ReservationsNr, new DateTime(2016, 12, 3), new DateTime(2016, 12, 4));
            AssertKunde(savedRes.Kunde, 1, "Nass", "Anna", new DateTime(1981, 05, 05));
            AssertAuto(savedRes.Auto, 1, AutoKlasse.Standard, 0, 50, "Fiat Punto");
        }

        #endregion

        #region Delete  

        [TestMethod]
        public void DeleteAutoTest()
        {
            Target.DeleteAuto(new AutoDto {Id = 1});
            var remainingAutos = Target.Autos.ToList();
            Assert.AreEqual(2, remainingAutos.Count);
            AssertAuto(remainingAutos[0], 2, AutoKlasse.Mittelklasse, 0, 120, "VW Golf");
            AssertAuto(remainingAutos[1], 3, AutoKlasse.Luxusklasse, 50, 180, "Audi S6");
            var remainingRes = Target.Reservationen.ToList();
            Assert.AreEqual(2, remainingRes.Count);
            AssertReservation(remainingRes[0], 2, new DateTime(2020, 01, 10), new DateTime(2020, 01, 20));
            AssertReservation(remainingRes[1], 3, new DateTime(2020, 01, 10), new DateTime(2020, 01, 20));
        }

        [TestMethod]
        public void DeleteKundeTest()
        {
            Target.DeleteKunde(new KundeDto {Id = 1});
            var remainingKunden = Target.Kunden.ToList();
            Assert.AreEqual(3, remainingKunden.Count);
            AssertKunde(remainingKunden[0], 2, "Beil", "Timo", new DateTime(1980, 09, 09));
            AssertKunde(remainingKunden[1], 3, "Pfahl", "Martha", new DateTime(1990, 07, 03));
            AssertKunde(remainingKunden[2], 4, "Zufall", "Rainer", new DateTime(1954, 11, 11));
            var remainingRes = Target.Reservationen.ToList();
            Assert.AreEqual(2, remainingRes.Count);
            AssertReservation(remainingRes[0], 2, new DateTime(2020, 01, 10), new DateTime(2020, 01, 20));
            AssertReservation(remainingRes[1], 3, new DateTime(2020, 01, 10), new DateTime(2020, 01, 20));
        }

        [TestMethod]
        public void DeleteReservationTest()
        {
            Target.DeleteReservation(new ReservationDto {ReservationsNr = 1});
            var remainingRes = Target.Reservationen.ToList();
            Assert.AreEqual(2, remainingRes.Count);
            AssertReservation(remainingRes[0], 2, new DateTime(2020, 01, 10), new DateTime(2020, 01, 20));
            AssertReservation(remainingRes[1], 3, new DateTime(2020, 01, 10), new DateTime(2020, 01, 20));
            Assert.AreEqual(3, Target.Autos.Count());
            Assert.AreEqual(4, Target.Kunden.Count());
        }

        #endregion

        #region Update

        [TestMethod]
        public void UpdateAutoTest()
        {
            Target.UpdateAuto(new AutoDto
            {
                Id = 2,
                AutoKlasse = AutoKlasse.Mittelklasse,
                Tagestarif = 150,
                Marke = "Ferarri"
            });
            AssertAuto(Target.Autos.First(a => a.Id == 2), 2, AutoKlasse.Mittelklasse, 0, 150, "Ferarri");
        }

        [TestMethod]
        public void UpdateAutoBasistarifOnlyForLuxusklasseTest()
        {
            Target.UpdateAuto(new AutoDto
            {
                Id = 2,
                AutoKlasse = AutoKlasse.Mittelklasse,
                Basistarif = 10,
                Tagestarif = 150,
                Marke = "Ferarri"
            });
            AssertAuto(Target.Autos.First(a => a.Id == 2), 2, AutoKlasse.Mittelklasse, 0, 150, "Ferarri");
            Target.UpdateAuto(new AutoDto
            {
                Id = 3,
                AutoKlasse = AutoKlasse.Luxusklasse,
                Basistarif = 999,
                Tagestarif = 888,
                Marke = "Ferarri"
            });
            AssertAuto(Target.Autos.First(a => a.Id == 3), 3, AutoKlasse.Luxusklasse, 999, 888, "Ferarri");
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            Target.UpdateKunde(new KundeDto
            {
                Geburtsdatum = new DateTime(1900, 1, 1),
                Id = 1,
                Nachname = "tester",
                Vorname = "testee"
            });
            AssertKunde(Target.Kunden.First(k => k.Id == 1), 1, "tester", "testee", new DateTime(1900, 1, 1));
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            Target.UpdateReservation(new ReservationDto
            {
                Auto = new AutoDto {Id = 2},
                Von = new DateTime(2016, 12, 3),
                Bis = new DateTime(2016, 12, 4),
                Kunde = new KundeDto {Id = 2},
                ReservationsNr = 1
            });
            var res = Target.Reservationen.First(r => r.ReservationsNr == 1);
            AssertReservation(res, 1, new DateTime(2016, 12, 3), new DateTime(2016, 12, 4));
            AssertAuto(res.Auto, 2, AutoKlasse.Mittelklasse, 0, 120, "VW Golf");
            AssertKunde(res.Kunde, 2, "Beil", "Timo", new DateTime(1980, 09, 09));
        }

        #endregion

        #region Update with optimistic concurrency violation

        [TestMethod]
        public void UpdateAutoWithOptimisticConcurrencyTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateKundeWithOptimisticConcurrencyTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        [TestMethod]
        public void UpdateReservationWithOptimisticConcurrencyTest()
        {
            Assert.Inconclusive("Test not implemented.");
        }

        #endregion
    }
}
