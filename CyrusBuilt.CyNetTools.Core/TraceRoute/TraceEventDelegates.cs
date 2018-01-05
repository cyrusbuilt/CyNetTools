using System;

namespace CyrusBuilt.CyNetTools.Core.TraceRoute
{
    /// <summary>
    /// Handler delegate for trace progress events.
    /// </summary>
    /// <param name="sender">
    /// A reference to the object that raised the event.
    /// </param>
    /// <param name="e">
    /// The event arguments.
    /// </param>
    public delegate void TraceProgressEventHandler(Object sender, TraceProgressEventArgs e);
}
