using System.Collections.Generic;
using System.Linq;

namespace ZeMenu
{
    internal static class CommandStates
    {
        internal static Dictionary<string, bool> States = new Dictionary<string, bool>()
        {
            {"WantedIgnored", false},
            {"LockControls", true },
            {"IsSmoking", false },
            {"IsInvincible", false },
            {"IsVehicleInvincible", false },
            {"VehicleNeverDirty", false },
            {"InfiniteAmmo", false },
            {"NoReload", false }
        };
        internal static string ToDebugString()
        {
            return "{" + string.Join(",", States.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
        }
    }
}
