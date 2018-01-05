using System;

namespace CyrusBuilt.CyNetTools.Core.Ping
{
    /// <summary>
    /// Routing modes (for host lists).
    /// </summary>
    public enum RouteMode : int
    {
        /// <summary>
        /// Loose source routing.
        /// </summary>
        Loose = 1,

        /// <summary>
        /// String source routing.
        /// </summary>
        Strict = 2,

        /// <summary>
        /// Do not use source routing.
        /// </summary>
        None = 0
    }
}
