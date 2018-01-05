using System;

namespace CyrusBuilt.CyNetTools.Core.Netstat
{
    /// <summary>
    /// Netstat utility methods.
    /// </summary>
    public static class NetstatUtils
    {
        /// <summary>
        /// Gets the name of the specified connection protocol.
        /// </summary>
        /// <param name="protocol">
        /// The protocol to get the name of.
        /// </param>
        /// <returns>
        /// The name of the specified protocol.
        /// </returns>
        public static String GetConnectionProtocolName(ConnectionProtocol protocol) {
            if (protocol == ConnectionProtocol.None) {
                return String.Empty;
            }
            return Enum.GetName(typeof(ConnectionProtocol), protocol);
        }

        /// <summary>
        /// Gets the name of the specified stats protocol.
        /// </summary>
        /// <param name="protocol">
        /// The protocol to get the name of.
        /// </param>
        /// <returns>
        /// The name of the specified protocol.
        /// </returns>
        public static String GetStatsProtocolName(StatsProtocol protocol) {
            if (protocol == StatsProtocol.None) {
                return String.Empty;
            }
            return Enum.GetName(typeof(StatsProtocol), protocol);
        }
    }
}
