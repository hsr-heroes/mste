using AutoReservation.Dal.Entities;
using AutoReservation.TestEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace AutoReservation.BusinessLayer.Testing
{
    [TestClass]
    public class BusinessLayerTest
    {

        private AutoReservationBusinessComponent target;
        private AutoReservationBusinessComponent Target
        {
            get
            {
                if (target == null)
                {
                    target = new AutoReservationBusinessComponent();
                }
                return target;
            }
        }
        
        [TestInitialize]
        public void InitializeTestData()
        {
            TestEnvironmentHelper.InitializeTestData();
        }
        
        [TestMethod]
        public void UpdateAutoTest()
        {
            Auto a = Target.Autos.First();
            a.Marke = "Fiat";
            Target.UpdateAuto(a);
            var updatedAuto = Target.Autos.First(au => a.Id == au.Id);
            Assert.AreEqual("Fiat", updatedAuto.Marke);
        }

        [TestMethod]
        public void UpdateKundeTest()
        {
            var k = Target.Kunden.First();
            k.Nachname = "*";
            Target.UpdateKunde(k);
            var updatedK = Target.Kunden.First(ku => ku.Id == k.Id);
            Assert.AreEqual("*", updatedK.Nachname);
        }

        [TestMethod]
        public void UpdateReservationTest()
        {
            var r = Target.Reservationen.First();
            var now = new DateTime(2016, 12, 12);
            r.Von = now;
            r.Bis = now + TimeSpan.FromMinutes(60);
            Target.UpdateReservation(r);
            var updatedRes = Target.Reservationen.First(re => re.ReservationsNr == r.ReservationsNr);
            Assert.AreEqual(now, updatedRes.Von);
            Assert.AreEqual(now + TimeSpan.FromMinutes(60), updatedRes.Bis);
        }

    }

}
