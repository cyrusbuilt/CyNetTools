using System;

namespace CyrusBuilt.CyNetTools.Core.GetMac
{
    /// <summary>
    /// Possible output formats.
    /// </summary>
    public enum GetMacOutputFormat : int
    {
        /// <summary>
        /// Display results in table format (physical address, transport name).
        /// </summary>
        Table = 0,

        /// <summary>
        /// Display results in list format.
        /// </summary>
        List = 1,

        /// <summary>
        /// Display results in CSV format.
        /// </summary>
        CSV = 2
    }
}
