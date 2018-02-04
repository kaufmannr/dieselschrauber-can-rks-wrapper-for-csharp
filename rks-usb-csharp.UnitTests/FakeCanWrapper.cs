using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace rks_usb_csharp.UnitTests
{
    public class FakeCanWrapper : CanWrapper
    {
        public readonly List<Event> Events = new List<Event>();
        public FakeCanWrapper(string libraryPath, string dllName = "Fake-RKS-USB.dll") : base(libraryPath, dllName)
        {
        }

        private FakeNativeCanEntryPoints.UniversalCallback _universalCallbackPtr;
        

        public virtual void SetupTestEventHandler()
        {
            _universalCallbackPtr = ParameterWasSet;
            Procedure<FakeNativeCanEntryPoints.SetUpModel>(FakeNativeCanEntryPoints._SetUpModel)(_universalCallbackPtr);
        }

        private int ParameterWasSet(string name, string value)
        {
            Events.Add(new Event(){Name = name, Value = value});
            return 0;
        }

        public void AssertEvent(string name, string value)
        {
            if (!Events.Exists(@event => @event.Name.Equals(name) && @event.Value.Equals(value)))
            {
                var events = Events.Aggregate("", (s, @event) => s + $"[{@event.Name}::{@event.Value}]");

                throw new Exception($"Event name:{name}, value:{value} does not exist. But {events}");
            }

        }

    }
}