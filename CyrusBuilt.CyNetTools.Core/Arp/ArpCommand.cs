using System;

namespace CyrusBuilt.CyNetTools.Core.Arp
{
    /// <summary>
    /// Possible ARP commands.
    /// </summary>
    public enum ArpCommand : int
    {
        /// <summary>
        /// No ARP command. Just shows basic ARP table info.
        /// </summary>
        None = 0,

        /// <summary>
        /// Displays current ARP entries by interrogating the current protocol
        /// data. If an IP address is specified, the IP and Physical addresses
        /// for only the specified host are displayed. If more than one network
        /// interface uses ARP, entries for each ARP table are displayed.
        /// </summary>
        ShowAll = 1,

        /// <summary>
        /// Displays the ARP entries for a specified network interface.
        /// </summary>
        ShowForInterface = 2,

        /// <summary>
        /// Adds a specified host and associates the IP address with a specified
        /// Physical address. The Physical address is given as 6 hexadecimal bytes
        /// separated by hyphens. The entry is permanent.
        /// </summary>
        AddHost = 3,

        /// <summary>
        /// Deletes a host entry specified by a given IP address. Wildcards may
        /// be used. Use "*" to delete all host entries.
        /// </summary>
        DeleteHost = 4
    }
}
