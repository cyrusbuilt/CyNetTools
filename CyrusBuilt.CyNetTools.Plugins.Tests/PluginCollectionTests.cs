using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using NUnit.Framework;
using CyrusBuilt.CyNetTools.Plugins;

namespace CyrusBuilt.CyNetTools.Plugins.Tests
{
    [TestFixture]
    public class PluginCollectionTests
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
                throw new NotImplementedException();
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
            var plugins = new AvailablePluginCollection();
            Assert.True(plugins.Count == 0);
        }
        
        [Test]
        public void CanAdd() {
            var tp = new TestPlugin();
            var ap = new AvailablePlugin(tp, this.AssemblyPath);
            var plugins = new AvailablePluginCollection();
            Assert.True(plugins.Add(ap) == 0);
            Assert.True(plugins.Count > 0);
            Assert.True(plugins.Add(ap) == -1);
        }

        [Test]
        public void CanGetFromIndexer() {
            var tp = new TestPlugin();
            var ap = new AvailablePlugin(tp, this.AssemblyPath);
            var plugins = new AvailablePluginCollection();
            plugins.Add(ap);

            Assert.True(plugins[0].Equals(ap));
        }

        [Test]
        public void CanInsert() {
            var tp = new TestPlugin();
            var ap = new AvailablePlugin(tp, this.AssemblyPath);
            var plugins = new AvailablePluginCollection();
            plugins.Add(ap);

            var apPath2 = Path.Combine(Path.GetDirectoryName(this.AssemblyPath), Guid.NewGuid().ToString() + ".dll");
            var ap2 = new AvailablePlugin(tp, apPath2);
            plugins.Add(ap2);

            var apPath3 = Path.Combine(Path.GetDirectoryName(this.AssemblyPath), Guid.NewGuid().ToString() + ".dll");
            var ap3 = new AvailablePlugin(tp, apPath3);

            plugins.Insert(1, ap3);

            // At this point, ap should be at index 0. ap2 should be at index 2, and ap3 should be at index 1.
            Assert.True(plugins[0].Equals(ap));
            Assert.True(plugins[1].Equals(ap3));
            Assert.True(plugins[2].Equals(ap2));
        }

        [Test]
        public void CanRemove() {
            var tp = new TestPlugin();
            var ap = new AvailablePlugin(tp, this.AssemblyPath);
            var plugins = new AvailablePluginCollection();
            plugins.Add(ap);

            // So we should have 1 plugin in the collection.
            Assert.True(plugins.Count == 1);

            // Now remove it and verify that the count is zero.
            plugins.Remove(ap);
            Assert.True(plugins.Count == 0);
        }

        [Test]
        public void DoesContain() {
            var tp = new TestPlugin();
            var ap = new AvailablePlugin(tp, this.AssemblyPath);
            var plugins = new AvailablePluginCollection();
            plugins.Add(ap);
            Assert.True(plugins.Contains(ap));
        }

        [Test]
        public void CanGetIndex() {
            var tp = new TestPlugin();
            var ap = new AvailablePlugin(tp, this.AssemblyPath);
            var plugins = new AvailablePluginCollection();
            plugins.Add(ap);

            Assert.True(plugins.IndexOf(ap) == 0);
        }

        [Test]
        public void CanCopyToArray() {
            var tp = new TestPlugin();
            var ap = new AvailablePlugin(tp, this.AssemblyPath);
            var plugins = new AvailablePluginCollection();
            plugins.Add(ap);

            AvailablePlugin[] pluginArray = new AvailablePlugin[plugins.Count];
            plugins.CopyTo(pluginArray, 0);

            Assert.True(pluginArray.Length == 1);
        }

        [Test]
        public void CanFind() {
            var tp = new TestPlugin();
            var ap = new AvailablePlugin(tp, this.AssemblyPath);
            var plugins = new AvailablePluginCollection();
            plugins.Add(ap);

            var result = plugins.Find(ap.Instance.Name);
            Assert.NotNull(result);

            result = plugins.Find(ap.AssemblyPath);
            Assert.NotNull(result);
            Assert.True(result.Equals(ap));

            plugins.Remove(ap);
            result = plugins.Find(ap.Instance.Name);
            Assert.Null(result);
        }

        [Test]
        public void CanTestNullOrEmpty() {
            Assert.True(AvailablePluginCollection.IsNullOrEmpty(null));

            var plugins = new AvailablePluginCollection();
            Assert.True(AvailablePluginCollection.IsNullOrEmpty(plugins));

            var tp = new TestPlugin();
            var ap = new AvailablePlugin(tp, this.AssemblyPath);
            plugins.Add(ap);

            Assert.False(AvailablePluginCollection.IsNullOrEmpty(plugins));
        }
    }
}
