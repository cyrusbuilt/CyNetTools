using System;
using System.IO;
using NUnit.Framework;
using CyrusBuilt.CyNetTools.Plugins;

namespace CyrusBuilt.CyNetTools.Plugins.Tests
{
    [TestFixture]
    public class PluginConfigurationTests
    {
        [Test]
        public void CanConstructAndDispose()
        {
            var config = new PluginConfiguration();
            Assert.False(config.IsDisposed);
            Assert.True(config.IsEmpty);

            config.Dispose();
            Assert.True(config.IsDisposed);
        }

        [Test]
        public void CanAddSetting()
        {
            var config = new PluginConfiguration();
            config.AddConfigurationSetting("foo", "bar");

            Assert.False(config.IsEmpty);
            Assert.True(config.IsDirty);
            Assert.That(config.GetValue("foo"), Is.EqualTo("bar"));
        }

        [Test]
        public void CanClearConfig()
        {
            var config = new PluginConfiguration();
            config.AddConfigurationSetting("foo", "bar");

            Assert.False(config.IsEmpty);

            config.Clear();
            Assert.True(config.IsEmpty);
        }

        [Test]
        public void CanSetValue() {
            var config = new PluginConfiguration();
            config.AddConfigurationSetting("foo", "bar");
            Assert.That(config.GetValue("foo"), Is.EqualTo("bar"));

            config.SetValue("foo", "none");
            Assert.That(config.GetValue("foo"), Is.EqualTo("none"));
        }

        [Test]
        public void CanClearValues() {
            var config = new PluginConfiguration();
            config.AddConfigurationSetting("foo", "bar");
            Assert.That(config.GetValue("foo"), Is.EqualTo("bar"));

            config.ClearAllValues();
            Assert.Null(config.GetValue("foo"));
        }

        [Test]
        public void CanCopyFromConfig() {
            var config = new PluginConfiguration();
            config.AddConfigurationSetting("foo", "bar");

            var config2 = new PluginConfiguration();
            config2.CopyFromConfig(config);

            Assert.That(config2.GetValue("foo"), Is.EqualTo("bar"));
        }

        [Test]
        public void CanSaveAndLoadConfig() {
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

            // Now create a config and populate it.
            var config = new PluginConfiguration();
            config.AddConfigurationSetting("foo", "bar");
            config.SaveToFile(tempConfig, this.GetType());

            // Now clear it the config in memory. Just to be sure.
            config.Clear();

            // Now load the config from disk, and read back the setting value.
            config.ReadFromFile(tempConfig);
            Assert.That(config.GetValue("foo"), Is.EqualTo("bar"));

            // Delete the temp config file.
            config.Dispose();
            tempConfig.Delete();
        }
    }
}
