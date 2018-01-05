using System;

namespace CyrusBuilt.CyNetTools.Core.Ping
{
    /// <summary>
    /// Ping process error codes.
    /// </summary>
    [Flags]
    public enum PingErrorCodes : int
    {
        /// <summary>
        /// No error. Normal exit.
        /// </summary>
        NoError = 0,

        /// <summary>
        /// Send buffer too small.
        /// </summary>
        BufferTooSmall = 11001,

        /// <summary>
        /// Destination network not reachable.
        /// </summary>
        DestinationNetUnreachable = 11002,

        /// <summary>
        /// Destination host not reachable.
        /// </summary>
        DestinationHostUnreachable = 11003,

        /// <summary>
        /// Destination protocol not reachable.
        /// </summary>
        DestinationProtocolUnreachable = 11004,

        /// <summary>
        /// Destination port not reachable.
        /// </summary>
        DestinationPortUnreachable = 11005,

        /// <summary>
        /// Lack of resources to perform action.
        /// </summary>
        NoResources = 11006,

        /// <summary>
        /// Invalid option or combination of options specified.
        /// </summary>
        BadOption = 11007,

        /// <summary>
        /// A network interface hardware error occurred.
        /// </summary>
        HarwareError = 11008,

        /// <summary>
        /// Packet size too large.
        /// </summary>
        PacketTooBig = 11009,

        /// <summary>
        /// The request timed out.
        /// </summary>
        RequestTimeout = 11010,

        /// <summary>
        /// Bad request.
        /// </summary>
        BadRequest = 11011,

        /// <summary>
        /// Bad route.
        /// </summary>
        BadRoute = 11012,

        /// <summary>
        /// Time-to-live expired in transit.
        /// </summary>
        TTLExpiredTransit = 11013,

        /// <summary>
        /// Time-to-live expired during packet reassembly.
        /// </summary>
        TTLExpiredReassembly = 11014,

        /// <summary>
        /// Bad parameter.
        /// </summary>
        BadParameter = 11015,

        /// <summary>
        /// Source quenched.
        /// </summary>
        SourceQuench = 11016,

        /// <summary>
        /// Option too big.
        /// </summary>
        OptionTooBig = 11017,

        /// <summary>
        /// Bad Destination.
        /// </summary>
        BadDestination = 11018,

        /// <summary>
        /// Failed to negotiate IPSEC with target.
        /// </summary>
        NegotiateIPSEC = 11032,

        /// <summary>
        /// A general failure occurred.
        /// </summary>
        GeneralFailure = 11050
    }
}
