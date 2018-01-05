using System;

namespace CyrusBuilt.CyNetTools.Core.IPConfig
{
    /// <summary>
    /// Defines command options for ipconfig.
    /// </summary>
    public enum IpConfigCommand : int
    {
        /// <summary>
        /// Show only the basic IP configuration info.
        /// </summary>
        ShowBasic = 0,

        /// <summary>
        /// Show detailed IP configuration info.
        /// </summary>
        ShowAll = 1,

        /// <summary>
        /// Release the IP address for the specified adapter or default if none specified.
        /// </summary>
        Release = 2,

        /// <summary>
        /// Renews the IP address for the specified adapter or default if none specified.
        /// </summary>
        Renew = 3,

        /// <summary>
        /// Purge the DNS resolver cache.
        /// </summary>
        FlushDNS = 4,

        /// <summary>
        /// Refreshes all DHCP leases and re-registers DNS names.
        /// </summary>
        RegisterDNS = 5,

        /// <summary>
        /// Display the contents of the DNS resolver cache.
        /// </summary>
        DisplayDNS = 6,

        /// <summary>
        /// Displays all the DHCP class IDs for adapter or default if none specified.
        /// </summary>
        ShowClassID = 7,

        /// <summary>
        /// Modify the DHCP class ID.
        /// </summary>
        SetClassID = 8
    }
}
