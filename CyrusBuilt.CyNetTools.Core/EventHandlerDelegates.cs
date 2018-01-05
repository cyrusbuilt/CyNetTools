using System;

namespace CyrusBuilt.CyNetTools.Core
{
    /// <summary>
    /// Process cancelled event handler delegate.
    /// </summary>
    /// <param name="sender">
    /// The object that fired the event.
    /// </param>
    /// <param name="e">
    /// The event arguments.
    /// </param>
    public delegate void ProcessCancelledEventHandler(Object sender, ProcessCancelledEventArgs e);
 
    /// <summary>
    /// Process running event handler delegate.
    /// </summary>
    /// <param name="sender">
    /// The object that fired the event.
    /// </param>
    /// <param name="e">
    /// The event arguments.
    /// </param>
    public delegate void ProcessRunningEventHandler(Object sender, ProcessStartedEventArgs e);

    /// <summary>
    /// Process done event handler delegate.
    /// </summary>
    /// <param name="sender">
    /// The object that fired the event.
    /// </param>
    /// <param name="e">
    /// The event arguments.
    /// </param>
    public delegate void ProcessDoneEventHandler(Object sender, ProccessDoneEventArgs e);

    /// <summary>
    /// Process output event handler delegate.
    /// </summary>
    /// <param name="sender">
    /// The object that fired the event.
    /// </param>
    /// <param name="e">
    /// The event arguments.
    /// </param>
    public delegate void ProcessOutputEventHandler(Object sender, ProcessOutputEventArgs e);
}
