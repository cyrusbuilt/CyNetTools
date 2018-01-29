using System;
using NUnit.Framework;
using CyrusBuilt.CyNetTools.Core.GetMac;

namespace CyrusBuilt.CyNetTools.Core.Tests.GetMac
{
    [TestFixture]
    public class GetMacUtilsTests
    {
        [Test]
        public void GetOutputFormatStringTest() {
            var result = GetMacUtils.GetOutputFormatString(GetMacOutputFormat.Table);
            Assert.True(result.Equals("table"));
        }
    }
}
