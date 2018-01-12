using System;
using NUnit.Framework;
using CyrusBuilt.CyNetTools.Core;

namespace CyrusBuilt.CyNetTools.Core.Tests
{
    [TestFixture]
    public class ProcessDoneEventArgsTests
    {
        [Test]
        public void CanConstruct() {
            var args = new ProccessDoneEventArgs(1, true);
            Assert.True(args.ExitCode == 1);
            Assert.True(args.Cancelled);
        }
    }
}
