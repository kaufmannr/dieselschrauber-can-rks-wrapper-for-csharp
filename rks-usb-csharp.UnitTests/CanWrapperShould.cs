using System;
using NUnit.Framework;

namespace rks_usb_csharp.UnitTests
{
    [TestFixture]
    public class CanWrapperShould
    {
        private CanWrapper CanWrapper;

        [SetUp]
        public void SetUp()
        {
            CanWrapper = new CanWrapper(Environment.CurrentDirectory);
        }

        [Test]
        public void RKSInitialize_RKSFree()
        {
            CanWrapper.SetUp();

            CanWrapper.RKSInitialize();
            CanWrapper.RKSFree();
            
        } [Test]
        public void RKSGetVersion()
        {
            CanWrapper.SetUp();

            CanWrapper.RKSInitialize();
            CanWrapper.RKSSetTimeouts(2000, 2000);

            var version = CanWrapper.RKSGetVersion();

            CanWrapper.RKSFree();

            Assert.AreEqual("1070", version);
         
        } [Test]
        public void RKSGetSerial()
        {
            CanWrapper.SetUp();

            CanWrapper.RKSInitialize();
            CanWrapper.RKSSetTimeouts(2000, 2000);
            
            var serial = CanWrapper.RKSGetSerial();

            CanWrapper.RKSFree();

            Assert.AreEqual("7641170426110", serial);
         
        } [Test]
        public void RKSDeviceConnected()
        {
            CanWrapper.SetUp();

            CanWrapper.RKSInitialize();
            CanWrapper.RKSSetTimeouts(2000, 2000);
            
            var connected = CanWrapper.RKSDeviceConnected(out var guid);
           
            CanWrapper.RKSFree();
            
            Assert.IsTrue(connected);
           
        } [Test]
        public void RKSGetTimeSinceInit()
        {
            CanWrapper.SetUp();

            CanWrapper.RKSInitialize();
            CanWrapper.RKSSetTimeouts(2000, 2000);
            
            var time = CanWrapper.RKSGetTimeSinceInit();
            CanWrapper.RKSFree();

          
            Assert.Greater(0, time);
        }[Test]
        public void RKSCANSetListenOnly()
        {
            CanWrapper.SetUp();

            CanWrapper.RKSInitialize();
            CanWrapper.RKSSetTimeouts(2000, 2000);
            
            var result = CanWrapper.RKSCANSetListenOnly();
            CanWrapper.RKSFree();

          
            Assert.AreEqual(true, result);
        }

        [Test]
        public void RKSCANSetTimeStamp()
        {
            CanWrapper.SetUp();

            CanWrapper.RKSInitialize();
            CanWrapper.RKSSetTimeouts(2000, 2000);

            var time = CanWrapper.RKSCANSetTimeStamp(0);
            CanWrapper.RKSFree();


            Assert.Greater(true, time);
        }
        [Test]
        public void RKSCANGetUb()
        {
            CanWrapper.SetUp();

            CanWrapper.RKSInitialize();
            CanWrapper.RKSSetTimeouts(2000, 2000);

            var ub = CanWrapper.RKSCANGetUb();
            CanWrapper.RKSFree();


            Assert.Greater(0, ub);
        }

        [Test]
        public void RKSCANRx()
        {
            CanWrapper.SetUp();

            CanWrapper.RKSInitialize();
            CanWrapper.RKSSetTimeouts(2000, 2000);
            
            CanWrapper.RKSCANOpen(2000);
            CanWrapper.RKSCANRx();
            CanWrapper.RKSCANClose();


            CanWrapper.RKSFree();
        }
    }
}