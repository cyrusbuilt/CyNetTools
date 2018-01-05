using System;

namespace CyrusBuilt.CyNetTools.Core.Ping
{
    /// <summary>
    /// Ping utility functions.
    /// </summary>
    public static class PingUtils
    {
        /// <summary>
        /// Gets the description of the specified ping error code.
        /// </summary>
        /// <param name="error">
        /// The error to get the description of.
        /// </param>
        /// <returns>
        /// The description of the specified error.
        /// </returns>
        public static String GetPingErrorMessage(PingErrorCodes error) {
            var reason = String.Empty;
            switch (error) {
                case PingErrorCodes.NoError:
                    reason = "No error.";
                    break;
                case PingErrorCodes.BufferTooSmall:
                    reason = "Buffer too small.";
                    break;
                case PingErrorCodes.DestinationNetUnreachable:
                    reason = "Destination net unreachable.";
                    break;
                case PingErrorCodes.DestinationHostUnreachable:
                    reason = "Destination host unreachable.";
                    break;
                case PingErrorCodes.DestinationProtocolUnreachable:
                    reason = "Destination protocol unreachable.";
                    break;
                case PingErrorCodes.DestinationPortUnreachable:
                    reason = "Destination port unreachable.";
                    break;
                case PingErrorCodes.NoResources:
                    reason = "No resources.";
                    break;
                case PingErrorCodes.BadOption:
                    reason = "Bad option.";
                    break;
                case PingErrorCodes.HarwareError:
                    reason = "Hardware error.";
                    break;
                case PingErrorCodes.PacketTooBig:
                    reason = "Packet too big.";
                    break;
                case PingErrorCodes.RequestTimeout:
                    reason = "Request timed out.";
                    break;
                case PingErrorCodes.BadRequest:
                    reason = "Bad request.";
                    break;
                case PingErrorCodes.BadRoute:
                    reason = "Bad route.";
                    break;
                case PingErrorCodes.TTLExpiredTransit:
                    reason = "Time-to-live expired in transit.";
                    break;
                case PingErrorCodes.TTLExpiredReassembly:
                    reason = "Time-to-live expired during reassembly.";
                    break;
                case PingErrorCodes.BadParameter:
                    reason = "Bad parameter.";
                    break;
                case PingErrorCodes.SourceQuench:
                    reason = "Source quench.";
                    break;
                case PingErrorCodes.OptionTooBig:
                    reason = "Option too big.";
                    break;
                case PingErrorCodes.BadDestination:
                    reason = "Bad destination.";
                    break;
                case PingErrorCodes.NegotiateIPSEC:
                    reason = "Failed to negotiate IPSEC.";
                    break;
                case PingErrorCodes.GeneralFailure:
                    reason = "General failure.";
                    break;
                default:
                    reason = "User cancelled or unknown error.";
                    break;
            }
            return reason;
        }
    }
}
