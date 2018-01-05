using System;

namespace CyrusBuilt.CyNetTools.Core.Ping
{
    /// <summary>
    /// Event handler delegate for ping progress events.
    /// </summary>
    /// <param name="sender">
    /// The object that fired the event.
    /// </param>
    /// <param name="e">
    /// The event arguments.
    /// </param>
    public delegate void PingProgressEventHandler(Object sender, PingProgressEventArgs e);
}
