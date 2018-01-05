using System;

namespace CyrusBuilt.CyNetTools.Core.Netstat
{
    /// <summary>
    /// Defines the protocols to show connections for.
    /// </summary>
    public enum ConnectionProtocol : int
    {
        /// <summary>
        /// Will not show connections for a given protocol (disable).
        /// </summary>
        None = 0,

        /// <summary>
        /// Shows TCP (Transmission Control Protocol v4) connections.
        /// </summary>
        TCP,

        /// <summary>
        /// Shows UDP (User Datagram Protocol v4) connections.
        /// </summary>
        UDP,

        /// <summary>
        /// Shows TCPv6 (Transmission Control Protocol v6) connections.
        /// </summary>
        TCPv6,

        /// <summary>
        /// Shows UDPv6 (User Datagram Protocol v6) connections.
        /// </summary>
        UDPv6
    }
}
