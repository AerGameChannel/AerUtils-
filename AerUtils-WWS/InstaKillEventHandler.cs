using System;
using WW_SYSTEM;
using WW_SYSTEM.API;
using WW_SYSTEM.EventHandlers;
using WW_SYSTEM.Events;

namespace AerUtils
{
    class InstaKillEventHandler : IEventHandlerAdminQuery
    {
        public Plugin plugin;
        public InstaKillEventHandler(Plugin plugin)
        {
            this.plugin = plugin;
        }
        public void OnAdminQuery(AdminQueryEvent ev)
        {
            if(ev.Query.ToLower().StartsWith("ik") || ev.Query.ToLower().StartsWith("instakill"))
            {
                var utilsenable = plugin.Config.GetBool("aerutils_enable", true);
                if (!utilsenable) return;
                string[] array = ev.Query.Split();

                if (array.Length <= 1)
                {
                    ev.Output = "AerUtils_InstaKill#Usage: ik <RemoteAdmin player id>";
                    ev.Successful = true;
                    ev.Handled = true;
                    return;
                }
                if (string.IsNullOrEmpty(array[1]))
                {
                    ev.Output = "AerUtils_InstaKill#Usage: ik <RemoteAdmin player id>";
                    ev.Successful = true;
                    ev.Handled = true;
                    return;
                }
                if (array.Length > 1)
                {
                    try
                    {
                        if (!ev.Admin.IsPermitted(PlayerPermissions.PlayersManagement))
                        {
                            ev.Output = "AerUtils_InstaKill#Not enough permissions";
                            ev.Successful = false;
                            ev.Handled = true;
                            return;
                        }
                        if (array.Length > 0)
                        {
                            if (array[1].ToLower().Contains("help"))
                            {
                                ev.Output = "AerUtils_InstaKill#Usage: ik <RemoteAdmin player id>";
                                ev.Successful = true;
                                ev.Handled = true;
                                return;
                            }
                            else
                            {
                                int id = int.Parse(array[1]);

                                Player pl = Server.Round.FindPlayerWithId(id);
                                if (pl != null)
                                {
                                    pl.Kill(DamageTypes.None); // Kill player instantly

                                    ev.Output = "AerUtils_InstaKill#Instantly killed player  " + pl.Nick;
                                    ev.Successful = true;
                                    ev.Handled = true;
                                    return;
                                }
                                else if (pl == null)
                                {
                                    ev.Output = "AerUtils_InstaKill#Please, use valid player id! Id" + pl + " is invalid";
                                    ev.Successful = false;
                                    ev.Handled = true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ev.Output = "AerUtils_InstaKill#Error: " + ex;
                        ev.Successful = false;
                        ev.Handled = true;
                        return;
                    }
                }
            }
        }
    }
}
