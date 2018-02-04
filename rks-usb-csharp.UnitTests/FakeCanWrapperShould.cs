using System;
using NUnit.Framework;

namespace rks_usb_csharp.UnitTests
{
    [TestFixture]
    public class FakeCanWrapperShould
    {
        private FakeCanWrapper FakeCanWrapper;

        [SetUp]
        public void SetUp()
        {
            FakeCanWrapper = new FakeCanWrapper(Environment.CurrentDirectory);
            FakeCanWrapper.SetUp();

            FakeCanWrapper.SetupTestEventHandler();
        }

        [Test]
        public void RKSInitialize()
        {
            FakeCanWrapper.RKSInitialize();
            FakeCanWrapper.AssertEvent("RKSInitialize", "void");
        }

        [Test]
        public void RKSSetTimeouts()
        {
            FakeCanWrapper.RKSSetTimeouts(2000, 1000);
            FakeCanWrapper.AssertEvent("RKSSetTimeouts", "2000,1000");
        }

        [Test]
        public void RKSDeviceConnected()
        {
            FakeCanWrapper.RKSDeviceConnected(out string id);
            Assert.AreEqual("1234", id);
            FakeCanWrapper.AssertEvent("RKSDeviceConnected", ",39"); // Guid returned {...} with 38 visible signs
        }

        [Test]
        public void RKSReadPipe()
        {
            var result = FakeCanWrapper.RKSReadPipe(out NativeCanEntryPoints.LPOVERLAPPED overlapped);
            Assert.AreEqual("1234", result);
            Assert.AreEqual(10,overlapped.Internal);
            Assert.AreEqual(20,overlapped.InternalHigh);
            Assert.AreEqual(30,overlapped.Offset);
            Assert.AreEqual(40,overlapped.OffsetHigh);
            FakeCanWrapper.AssertEvent("RKSReadPipe", "128,0");
        }

        [Test]
        public void RKSWritePipe()
        {
            NativeCanEntryPoints.LPOVERLAPPED pOverlapped = new NativeCanEntryPoints.LPOVERLAPPED
            {
                Internal = 10,
                InternalHigh = 20,
                Offset = 30,
                OffsetHigh = 40
            };
            var result = FakeCanWrapper.RKSWritePipe(pOverlapped);
            Assert.AreEqual("1234", result);
            FakeCanWrapper.AssertEvent("RKSWritePipe", "128,0,10,20,30,40");
        }

        [Test]
        public void RKSRead()
        {
            var result = FakeCanWrapper.RKSRead();
            Assert.AreEqual("1234", result);
            FakeCanWrapper.AssertEvent("RKSRead", "128,0");
        }

        [Test]
        public void RKSWrite()
        {
            var result = FakeCanWrapper.RKSWrite("write-msg");
            Assert.AreEqual(true, result);
            FakeCanWrapper.AssertEvent("RKSWrite", "write-msg,128,0");
        }

        [Test]
        public void RKSGetVersion()
        {
            var result = FakeCanWrapper.RKSGetVersion();
            Assert.AreEqual("1070", result);
            FakeCanWrapper.AssertEvent("RKSGetVersion", "32");
        }

        [Test]
        public void RKSGetSerial()
        {
            var result = FakeCanWrapper.RKSGetSerial();
            Assert.AreEqual("{1-2-3-4}", result);
            FakeCanWrapper.AssertEvent("RKSGetSerial", "32");
        }

        [Test]
        public void RKSGetTimeSinceInit()
        {
            var result = FakeCanWrapper.RKSGetTimeSinceInit();
            Assert.AreEqual(1.0d, result);
            FakeCanWrapper.AssertEvent("RKSGetTimeSinceInit", "0");

            FakeCanWrapper.RKSGetTimeSinceInit(true);
            FakeCanWrapper.AssertEvent("RKSGetTimeSinceInit", "1");
        }

        [Test]
        public void RKSCANGetLastStatus()
        {
            var status = FakeCanWrapper.RKSCANGetLastStatus();
            Assert.AreEqual(4, status);
            FakeCanWrapper.AssertEvent("RKSCANGetLastStatus", "0");
        }

        [Test]
        public void RKSCANSetListenOnly()
        {
            var result = FakeCanWrapper.RKSCANSetListenOnly();
            Assert.AreEqual(true, result);
            FakeCanWrapper.AssertEvent("RKSCANSetListenOnly", "0");

            FakeCanWrapper.RKSCANSetListenOnly(true);
            FakeCanWrapper.AssertEvent("RKSCANSetListenOnly", "1");
        }

        [Test]
        public void RKSCANSetTimeStamp()
        {
            var result = FakeCanWrapper.RKSCANSetTimeStamp(2);
            Assert.AreEqual(true, result);
            FakeCanWrapper.AssertEvent("RKSCANSetTimeStamp", "2");
        }

        [Test]
        public void RKSCANGetTimeStamp()
        {
            var result = FakeCanWrapper.RKSCANGetTimeStamp();
            Assert.AreEqual(1, result);
            FakeCanWrapper.AssertEvent("RKSCANGetTimeStamp", "0");
        }

        [Test]
        public void RKSCANGetUb()
        {
            var result = FakeCanWrapper.RKSCANGetUb();
            Assert.AreEqual(12, result);
            FakeCanWrapper.AssertEvent("RKSCANGetUb", "0");
        }

        [Test]
        public void RKSCANSetFilter()
        {
            var result = FakeCanWrapper.RKSCANSetFilter(1, 2);
            Assert.AreEqual(true, result);
            FakeCanWrapper.AssertEvent("RKSCANSetFilter", "1,2");
        }

        [Test]
        public void RKSCANOpen()
        {
            var result = FakeCanWrapper.RKSCANOpen(2000);
            Assert.AreEqual(true, result);
            FakeCanWrapper.AssertEvent("RKSCANOpen", "2000");
        }

        [Test]
        public void RKSCANClose()
        {
            var result = FakeCanWrapper.RKSCANClose();
            Assert.AreEqual(true, result);
            FakeCanWrapper.AssertEvent("RKSCANClose", "void");
        }

        [Test]
        public void RKSCANRx()
        {
            var result = FakeCanWrapper.RKSCANRx();
            Assert.AreEqual(10, result.dwID);
            Assert.AreEqual(new byte[]{90,91,92,93,94,95,96,97}, result.abyData);
            Assert.AreEqual(30, result.byDLC);
            Assert.AreEqual(40, result.byType);
            Assert.AreEqual(50, result.dwTimeStamp);
            Assert.AreEqual(10, result.byError);

            FakeCanWrapper.AssertEvent("RKSCANRx", "0,0,0");
        }

        [Test]
        public void RKSCANTx()
        {
            var canMessage = new CanWrapper.CanMessage
            {
                byType = 10,
                dwTimeStamp = 20,
                dwID = 30,
                byDLC = 40,
                abyData = new byte[]
                {
                    50,
                    51,
                    52,
                    53,
                    54,
                    55,
                    56,
                    57
                }
            };
            var result = FakeCanWrapper.RKSCANTx(canMessage);
            Assert.AreEqual(true, result);
            FakeCanWrapper.AssertEvent("RKSCANTx", "10,20,30,40,50,51,52,53,54,55,56,57");
        }
    }
}