using System;
using NUnit.Framework;
using CyrusBuilt.CyNetTools.Core;

namespace CyrusBuilt.CyNetTools.Core.Tests
{
    [TestFixture]
    public class ProcessCancelledEventArgsTests
    {
        [Test]
        public void CanConstruct() {
            var ex = new Exception("test exception");
            var args = new ProcessCancelledEventArgs(ex);
            Assert.True(args.CancelCause == ex);

            args = ProcessCancelledEventArgs.Empty;
            Assert.Null(args.CancelCause);
        }
    }
}
