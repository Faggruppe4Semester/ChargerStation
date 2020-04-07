using System;
using System.IO;
using ChargerStation;
using ChargerStation.Interfaces;
using NUnit.Framework;

namespace ChargerStationTest
{
    [TestFixture]
    public class DisplayTest
    {
        private IDisplay _uut;
        [SetUp]
        public void SetUp()
        {
            _uut = new Display();
        }

        [Test]
        public void MessageWrittenIsMessageReceived()
        {
            //Sætter consollen til at 
            StringWriter sW = new StringWriter();
            Console.SetOut(sW);
            _uut.DisplayMessage("Made For Coverage");

            Assert.That(sW.ToString(),Is.EqualTo("Made For Coverage\r\n"));
            
        }
    }
}