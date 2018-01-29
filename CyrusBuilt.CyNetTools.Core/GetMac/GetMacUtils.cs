using System;

namespace CyrusBuilt.CyNetTools.Core.GetMac
{
    /// <summary>
    /// Getmac utility methods.
    /// </summary>
    public static class GetMacUtils
    {
        /// <summary>
        /// Gets the name string of the specified output format.
        /// </summary>
        /// <param name="format">
        /// The output format to get the name string for.
        /// </param>
        /// <returns>
        /// The name string of the specified format.
        /// </returns>
        public static String GetOutputFormatString(GetMacOutputFormat format) {
            return Enum.GetName(typeof(GetMacOutputFormat), format).ToLower();
        }
    }
}
