using System;
using System.Linq;
using WW_SYSTEM;
using WW_SYSTEM.API;
using WW_SYSTEM.EventHandlers;
using WW_SYSTEM.Events;

namespace AerUtils
{
    public class OtherFunctions : IEventHandlerAdminQuery
    {
        public Plugin plugin;

        public OtherFunctions(Plugin plugin)
        {
            this.plugin = plugin;
        }
        
        public void OnAdminQuery(AdminQueryEvent ev)
        {
            var utilsenable = plugin.Config.GetBool("aerutils_enable", true);
            if (!utilsenable) return;
            string[] array = ev.Query.Split();

            if (ev.Query.ToLower().StartsWith("killall"))
            {
                try
                {
                    if (!ev.Admin.IsPermitted(PlayerPermissions.ForceclassToSpectator))
                    {
                        ev.Output = "AerUtils_KillAll#Not enough permissions";
                        ev.Successful = false;
                        ev.Handled = true;
                        return;
                    }
                    else
                    {
                        foreach (Player player in Server.Round.GetPlayers().Where(player => player != null || player.GetClassName != "Spectator"))
                        {
                            player.Kill(DamageTypes.None);
                        }
                        ev.Output = "AerUtils_KillAll#Killed everything alive (and dead) in the facility";
                        ev.Successful = true;
                        ev.Handled = true;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    ev.Output = "AerUtils_KillAll#Error: " + ex;
                    ev.Successful = false;
                    ev.Handled = true;
                    return;
                }
            }
        }
    }
}
