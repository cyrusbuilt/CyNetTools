using System;

namespace CyrusBuilt.CyNetTools.Core.IPConfig
{
    /// <summary>
    /// IP Config utility methods.
    /// </summary>
    public static class IpConfigUtils
    {
        /// <summary>
        /// Gets command (argument) string associated with the specified command.
        /// </summary>
        /// <param name="command">
        /// The command to get the related argument string for.
        /// </param>
        /// <param name="version">
        /// The IP protocol version to use, which affects the command version.
        /// </param>
        /// <returns>
        /// The argument string associated with the specified command.
        /// </returns>
        public static String GetCommandString(IpConfigCommand command, IPVersion version) {
            var cmd = String.Empty;
            switch (command) {
                case IpConfigCommand.DisplayDNS: cmd = " /displaydns"; break;
                case IpConfigCommand.FlushDNS: cmd = " /flushdns"; break;
                case IpConfigCommand.RegisterDNS: cmd = " /registerdns"; break;
                case IpConfigCommand.Release: cmd = " /release"; break;
                case IpConfigCommand.Renew: cmd = " /renew"; break;
                case IpConfigCommand.SetClassID: cmd = " /setclassid"; break;
                case IpConfigCommand.ShowAll: cmd = " /all"; break;
                case IpConfigCommand.ShowClassID: cmd = " /showclassid"; break;
                case IpConfigCommand.ShowBasic:
                default: break;
            }

            if (version == IPVersion.IPv6) {
                cmd += "6";
            }
            return cmd;
        }

        /// <summary>
        /// Gets command (argument) string associated with the specified command.
        /// This overload assumes the IP protocol to use is IPv4.
        /// </summary>
        /// <param name="command">
        /// The command to get the related argument string for.
        /// </param>
        /// <returns>
        /// The argument string associated with the specified command.
        /// </returns>
        public static String GetCommandString(IpConfigCommand command) {
            return GetCommandString(command, IPVersion.IPv4);
        }
    }
}
