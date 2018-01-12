using System;
using NUnit.Framework;
using CyrusBuilt.CyNetTools.Core;

namespace CyrusBuilt.CyNetTools.Core.Tests
{
    [TestFixture]
    public class ProcessStartedEventArgsTests
    {
        [Test]
        public void CanConstruct() {
            var args = new ProcessStartedEventArgs(20);
            Assert.True(args.ProcessID == 20);
        }
    }
}
