using System;

namespace CyrusBuilt.CyNetTools.Plugins.Events
{
    /// <summary>
    /// Handler delegate for plugin found events.
    /// </summary>
    /// <param name="sender">
    /// A reference to the object firing the event.
    /// </param>
    /// <param name="e">
    /// The event arguments.
    /// </param>
    public delegate void FoundPluginEventHandler(Object sender, FoundPluginEventArgs e);

    /// <summary>
    /// Handler delegate for plugin loaded events.
    /// </summary>
    /// <param name="sender">
    /// A reference to the object firing the event.
    /// </param>
    /// <param name="e">
    /// The event arguments.
    /// </param>
    public delegate void PluginLoadedEventHandler(Object sender, PluginLoadedEventArgs e);
}
