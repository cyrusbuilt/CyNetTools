using System;
using System.Net;
using System.Threading;
using NUnit.Framework;
using CyrusBuilt.CyNetTools.Core.GetMac;

namespace CyrusBuilt.CyNetTools.Core.Tests.GetMac
{
    [TestFixture]
    public class GetMacModuleTests
    {
        [Test]
        public void CanConstructAndDestruct() {
            var mod = new GetMacModule();
            Assert.False(mod.IsDisposed);

            mod.Dispose();
            Assert.True(mod.IsDisposed);
        }

        [Test]
        public void TestProperties() {
            var mod = new GetMacModule();
            Assert.True(mod.ColumnHeaderEnabled);

            mod.ColumnHeaderEnabled = false;
            Assert.False(mod.ColumnHeaderEnabled);

            Assert.False(mod.Verbose);
            mod.Verbose = true;
            Assert.True(mod.Verbose);

            Assert.AreEqual(0, mod.ExitCode);
            Assert.False(mod.IsRunning);

            Assert.AreEqual(GetMacOutputFormat.Table, mod.OutputFormat);
            mod.OutputFormat = GetMacOutputFormat.CSV;
            Assert.AreEqual(GetMacOutputFormat.CSV, mod.OutputFormat);

            Assert.Null(mod.RemoteHost);
            mod.RemoteHost = new IPHostEntry();
            Assert.NotNull(mod.RemoteHost);

            Assert.Null(mod.RemoteHostCredentials);
            mod.RemoteHostCredentials = new NetworkCredential();
            Assert.NotNull(mod.RemoteHostCredentials);

            Assert.False(mod.WasCancelled);
            Assert.False(mod.IsRunning);
            Assert.AreEqual(0, mod.ExitCode);

            mod.Dispose();
        }

        [Test]
        public void CanStart() {
            var mod = new GetMacModule();
            mod.Start();
            Assert.True(mod.IsRunning);

            //mod.Dispose();
        }

        [Test]
        public void CanCancel() {
            var mod = new GetMacModule();
            mod.Start();
            Assert.True(mod.IsRunning);

            Thread.Sleep(50);
            mod.Cancel();
            Assert.True(mod.WasCancelled);
            Assert.False(mod.IsRunning);

            //mod.Dispose();
        }

        [Test]
        public void TestEvents() {
            var mod = new GetMacModule();
            var outputFired = false;
            var startFired = false;
            var cancelFired = false;
            var finishFired = false;

            mod.ProcessStarted += (o, e) => {
                startFired = true;
            };

            mod.OutputReceived += (o, e) => {
                outputFired = true;
            };

            mod.ProcessCancelled += (o, e) => {
                cancelFired = true;
            };

            mod.ProcessFinished += (o, e) => {
                finishFired = true;
            };

            mod.Start();
            Thread.Sleep(850);
            mod.Cancel();
            
            Thread.Sleep(2000);
            Assert.True(startFired);
            Assert.True(outputFired);
            
            // TODO gotta work out cancel and finish event tests.

            //Assert.True(cancelFired);

            //mod.Start();
            //Thread.Sleep(2000);
            //Assert.True(finishFired);
        }
    }
}
