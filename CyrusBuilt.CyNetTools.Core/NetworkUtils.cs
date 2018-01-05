using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace CyrusBuilt.CyNetTools.Core
{
    /// <summary>
    /// Network utility methods.
    /// </summary>
    public static class NetworkUtils
    {
        /// <summary>
        /// Gets a list of network adapters that are up (enabled and have link)
        /// and are IP-enabled (ethernet, VPN, WiFi).
        /// </summary>
        /// <returns>
        /// The list of available network adapters.
        /// </returns>
        public static List<NetworkInterface> GetNetworkAdapters() {
            var ipenabled = new List<NetworkInterface>();
            NetworkInterface[] all = NetworkInterface.GetAllNetworkInterfaces();
            foreach (var adapter in all) {
                if (adapter.OperationalStatus == OperationalStatus.Up) {
                    if ((adapter.NetworkInterfaceType == NetworkInterfaceType.Ethernet) ||
                        (adapter.NetworkInterfaceType == NetworkInterfaceType.FastEthernetT) ||
                        (adapter.NetworkInterfaceType == NetworkInterfaceType.GigabitEthernet) ||
                        (adapter.NetworkInterfaceType == NetworkInterfaceType.Wireless80211) ||
                        (adapter.NetworkInterfaceType == NetworkInterfaceType.Tunnel)) {
                            ipenabled.Add(adapter);
                    }
                }
            }

            Array.Clear(all, 0, all.Length);
            return ipenabled;
        }
    }
}
