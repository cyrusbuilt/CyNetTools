using System;
using NUnit.Framework;
using CyrusBuilt.CyNetTools.Core;

namespace CyrusBuilt.CyNetTools.Core.Tests
{
    [TestFixture]
    public class ProcessOutputEventArgsTests
    {
        [Test]
        public void CanConstruct() {
            var args = new ProcessOutputEventArgs("test");
            Assert.True(args.StandardOutput.Equals("test"));
        }
    }
}
