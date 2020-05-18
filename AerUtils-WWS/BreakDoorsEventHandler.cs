using System;
using WW_SYSTEM;
using WW_SYSTEM.API;
using WW_SYSTEM.EventHandlers;
using WW_SYSTEM.Events;

namespace AerUtils
{
    class BreakDoorsEventHandler : IEventHandlerAdminQuery
    {
        public Plugin plugin;
        public BreakDoorsEventHandler(Plugin plugin)
        {
            this.plugin = plugin;
        }
        public void OnAdminQuery(AdminQueryEvent ev)
        {
            var utilsenable = plugin.Config.GetBool("aerutils_enable", true);
            if (!utilsenable) return;
            string[] array = ev.Query.Split();

            if (ev.Query.ToLower().StartsWith("bd"))
            {
                if (!utilsenable) return;
                var aerutils_bd = plugin.Config.GetBool("aerutils_breakdoors_enable", true);
                if (!aerutils_bd) return;
                if (array.Length <= 1)
                {
                    ev.Output = "AerUtils_BreakDoors#Usage: bd <RemoteAdmin player id>";
                    ev.Successful = true;
                    ev.Handled = true;
                    return;
                }
                if (string.IsNullOrEmpty(array[1]))
                {
                    ev.Output = "AerUtils_BreakDoors#Usage: bd <RemoteAdmin player id>";
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
                            ev.Output = "AerUtils_BreakDoors#Not enough permissions";
                            ev.Successful = false;
                            ev.Handled = true;
                            return;
                        }
                        if (array.Length > 0)
                        {
                            if (array[1].ToLower().Contains("help"))
                            {
                                ev.Output = "AerUtils_BreakDoors#Usage: bd <RemoteAdmin player id>";
                                ev.Successful = true;
                                ev.Handled = true;
                                return;
                            }
                            else
                            {
                                int id = int.Parse(array[1]);

                                Player pl = Server.Round.FindPlayerWithId(id);
                                if (pl != null && !pl.BreakDoors) // If player isn't null and he doesn't have turned on breakdoors
                                {
                                    pl.BreakDoors = true; // Enable breakdoors

                                    ev.Output = "AerUtils_BreakDoors#Enabled BreakDoors for player " + pl.Nick;
                                    ev.Successful = true;
                                    ev.Handled = true;
                                    return;
                                }
                                else if (pl != null && pl.BreakDoors) // If player isn't null and he have turned on breakdoors
                                {
                                    pl.BreakDoors = false; // Disable breakdoors

                                    ev.Output = "AerUtils_BreakDoors#Disabled BreakDoors for player " + pl.Nick;
                                    ev.Successful = true;
                                    ev.Handled = true;
                                    return;
                                }
                                else if(pl == null)
                                {
                                    ev.Output = "AerUtils_BreakDoors#Please, use valid player id! Id" + pl + " is invalid";
                                    ev.Successful = false;
                                    ev.Handled = true;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ev.Output = "AerUtils_BreakDoors#Error: " + ex;
                        ev.Successful = false;
                        ev.Handled = true;
                        return;
                    }
                }
            }
        }
    }
}
