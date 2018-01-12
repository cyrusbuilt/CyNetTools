using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using NUnit.Framework;
using CyrusBuilt.CyNetTools.Plugins;

namespace CyrusBuilt.CyNetTools.Plugins.Tests
{
    [TestFixture]
    public class AvailablePluginTests
    {
        private class TestPlugin : PluginBase
        {
            private bool _isBusy = false;
            private PluginConfiguration _config = new PluginConfiguration();

            public TestPlugin() : base() {
                this._config.AddConfigurationSetting("foo", "bar");
            }

            public override string Author {
                get {
                    return "John Doe";
                }
            }

            public override string Copyright {
                get {
                    return "(c) Acme Co";
                }
            }

            public override string Description {
                get {
                    return "A fake plugin";
                }
            }

            public override bool IsBusy {
                get {
                    return this._isBusy;
                }
            }

            public override string Name {
                get {
                    return "TestPlugin";
                }
            }

            public override Version Version {
                get {
                    return new Version(1, 0, 0, 0);
                }
            }

            public override void Dispose() {
                base.Dispose();
                this._isBusy = false;

            }

            public override PluginConfiguration GetConfiguration() {
                return this._config;
            }

            public override void SaveConfiguration(PluginConfiguration config) {
                // First, create a temp config file. 
                var tempFileName = Path.GetTempPath() + Guid.NewGuid().ToString() + ".config";
                var tempConfig = new FileInfo(tempFileName);
                using (var tempStream = tempConfig.CreateText()) {
                    tempStream.WriteLine(@"<?xml version=""1.0"" encoding=""utf-8"" ?>");
                    tempStream.WriteLine("<configuration>");
                    tempStream.WriteLine("\t<configSections>");
                    tempStream.Write("\t\t<sectionGroup name=");
                    tempStream.WriteLine(@"""applicationSettings"" type=""System.Configuration.ApplicationSettingsGroup, System, Version = 4.0.0.0, Culture = neutral, PublicKeyToken = b77a5c561934e089"" >");
                    tempStream.WriteLine("\t\t</sectionGroup>");
                    tempStream.WriteLine("\t</configSections>");
                    tempStream.WriteLine("\t<applicationSettings>");
                    tempStream.WriteLine("\t\t<" + this.GetType().Namespace + ">");
                    tempStream.Write("\t\t\t<setting ");
                    tempStream.WriteLine(@"name =""foo"" serializeAs=""String"">");
                    tempStream.WriteLine("\t\t\t\t<value>none</value>");
                    tempStream.WriteLine("\t\t\t</setting>");
                    tempStream.WriteLine("\t\t</" + this.GetType().Namespace + ">");
                    tempStream.WriteLine("\t</applicationSettings>");
                    tempStream.WriteLine("</configuration>");
                    tempStream.Flush();
                }

                this._config.SaveToFile(tempConfig, this.GetType());
            }

            public override Form ShowUI(Form mdiParent) {
                throw new NotImplementedException();
            }
        }

        private String AssemblyPath {
            get {
                return Assembly.GetExecutingAssembly().Location;
            }
        }

        [Test]
        public void CanConstruct() {
            var testPlugin = new TestPlugin();
            var ap = new AvailablePlugin(testPlugin, this.AssemblyPath);
            Assert.That(ap.Instance, Is.InstanceOf(testPlugin.GetType()));
            Assert.True(ap.AssemblyPath.Equals(this.AssemblyPath));

            var hash = ap.GetHashCode();
            Assert.True(hash != 0);
            Assert.True(ap.ToString().Equals(testPlugin.Name));

            var apClone = new AvailablePlugin(ap.Instance, ap.AssemblyPath);
            Assert.True(ap.Equals(apClone));
        }
    }
}
