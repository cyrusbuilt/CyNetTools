using System;

namespace CyrusBuilt.CyNetTools.Core.Netstat
{
    /// <summary>
    /// Defines the protocols to show statistics for.
    /// </summary>
    public enum StatsProtocol : int
    {
        /// <summary>
        /// Will not show statistics for a specific protocol (disable).
        /// </summary>
        None = 0,

        /// <summary>
        /// Shows statistics for the IP (Internet Protocol v4) protocol.
        /// </summary>
        IP = 1,

        /// <summary>
        /// Shows statistics for the IPv6 (Internet Protocol v6) protocol.
        /// </summary>
        IPv6 = 2,

        /// <summary>
        /// Shows statistics for the ICMP (Internet Control Message Protocol v4) protocol.
        /// </summary>
        ICMP = 3,

        /// <summary>
        /// Shows statistics for the ICMPv6 (Internet Control Message Protocol v6) protocol.
        /// </summary>
        ICMPv6 = 4,

        /// <summary>
        /// Shows statistics for the TCP (Transmission Control Protocol v4) protocol.
        /// </summary>
        TCP = 5,

        /// <summary>
        /// Shows statistics for the TCPv6 (Transmission Control Protocol v6) protocol.
        /// </summary>
        TCPv6 = 6,

        /// <summary>
        /// Shows statistics for the UDP (User Datagram Protocol v4) protocol.
        /// </summary>
        UDP = 7,

        /// <summary>
        /// Shows statistics for the UDPv6 (User Datagram Protocol v6) protocol.
        /// </summary>
        UDPv6 = 8
    }
}
