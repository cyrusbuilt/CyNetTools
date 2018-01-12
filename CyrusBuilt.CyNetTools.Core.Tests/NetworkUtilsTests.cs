using System;
using NUnit.Framework;
using CyrusBuilt.CyNetTools.Core;

namespace CyrusBuilt.CyNetTools.Core.Tests
{
    [TestFixture]
    public class NetworkUtilsTests
    {
        [Test]
        public void CanGetAdapters() {
            var result = NetworkUtils.GetNetworkAdapters();
            Assert.True(result.Count > 0);
        }
    }
}
