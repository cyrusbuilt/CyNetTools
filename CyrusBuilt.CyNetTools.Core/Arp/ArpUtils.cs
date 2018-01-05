using System;

namespace CyrusBuilt.CyNetTools.Core.Arp
{
    /// <summary>
    /// ARP utility methods.
    /// </summary>
    public static class ArpUtils
    {
        /// <summary>
        /// Gets the command argument string associated with the specified ARP
        /// command.
        /// </summary>
        /// <param name="command">
        /// The ARP command to get the command string for.
        /// </param>
        /// <returns>
        /// The command string.
        /// </returns>
        public static String GetArpCommandString(ArpCommand command) {
            var cmd = String.Empty;
            switch (command) {
                case ArpCommand.AddHost: cmd = " -s "; break;
                case ArpCommand.DeleteHost: cmd = " -d "; break;
                case ArpCommand.ShowAll: cmd = " -a"; break;
                case ArpCommand.ShowForInterface: cmd = " -N "; break;
                case ArpCommand.None:
                default: break;
            }
            return cmd;
        }
    }
}
