using System;
using System.Collections.Generic;
using System.Linq;
using WW_SYSTEM;
using WW_SYSTEM.API;
using WW_SYSTEM.EventHandlers;
using WW_SYSTEM.Events;

namespace AerUtils
{
    class EventHandler : IEventHandlerAdminQuery, IEventHandlerPlayerJoin
    {
        public Plugin plugin;

        public EventHandler(Plugin plugin)
        {
            this.plugin = plugin;
        }
        public void OnPlayerJoin(PlayerJoinEvent ev)
        {
            bool utilsenable = plugin.Config.GetBool("aerutils_enable", true);
            if (!utilsenable) return;
            bool aerutils_jbc = plugin.Config.GetBool("aerutils_jbc_enable", true);
            if (!aerutils_jbc) return;

            uint bctime = plugin.Config.GetUInt("aerutils_jbc_time", 15);
            string bcmsg = plugin.Config.GetString("aerutils_jbc_message", "Welcome to the server!");
            ev.Player.PersonalBroadcast(bcmsg, bctime, false);
        }
        public void OnAdminQuery(AdminQueryEvent ev)
        {
            bool utilsenable = plugin.Config.GetBool("aerutils_enable", true);
            if (!utilsenable) return;
            string[] array = ev.Query.Split(new char[]{' '});

            if (ev.Query.ToLower().StartsWith("lo") || ev.Query.ToLower().StartsWith("lights"))
            {
                bool aerutils_lo = plugin.Config.GetBool("aerutils_lo_enable", true);
                if (!aerutils_lo) return;
                if (array.Length <= 1)
                {
                    ev.Output = "AerUtils_LightsOff#Usage: lo/lights <time in seconds>";
                    ev.Successful = true;
                    ev.Handled = true;
                    return;
                }
                if (string.IsNullOrEmpty(array[1]))
                {
                    ev.Output = "AerUtils_LightsOff#Usage: lo/lights <time in seconds>";
                    ev.Successful = true;
                    ev.Handled = true;
                    return;
                }
                if (array.Length > 1)
                {
                    try
                    {
                        if (!ev.Admin.IsPermitted(PlayerPermissions.FacilityManagement))
                        {
                            ev.Output = "AerUtils_LightsOff#Not enough permissions";
                            ev.Successful = false;
                            ev.Handled = true;
                            return;
                        }
                        if (array.Length > 1)
                        {
                            if (array[1].ToLower().Contains("help"))
                            {
                                ev.Output = "AerUtils_LightsOff#Usage: lo <time in seconds>";
                                ev.Successful = true;
                                ev.Handled = true;
                                return;
                            }
                            else
                            {
                                int lotime = int.Parse(array[1]);

                                Server.Round.EnableFlickering(lotime, false);

                                ev.Output = "AerUtils_LightsOff#Turned off the lights ";
                                ev.Successful = true;
                                ev.Handled = true;
                                return;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ev.Output = "LightsOff#Error: " + ex;
                        ev.Successful = false;
                        ev.Handled = true;
                        return;
                    }
                }
            }
            if (ev.Query.ToLower().StartsWith("bd"))
            {
                if (!utilsenable) return;
                bool aerutils_bd = plugin.Config.GetBool("aerutils_breakdoors_enable", true);
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
                                if (pl != null && !pl.BreakDoors)
                                {
                                    pl.BreakDoors = true;

                                    ev.Output = "AerUtils_BreakDoors#Enabled BreakDoors for player " + pl.Nick;
                                    ev.Successful = true;
                                    ev.Handled = true;
                                    return;
                                }
                                else if (pl != null && pl.BreakDoors)
                                {
                                    pl.BreakDoors = false;

                                    ev.Output = "AerUtils_BreakDoors#Disabled BreakDoors for player " + pl.Nick;
                                    ev.Successful = true;
                                    ev.Handled = true;
                                    return;
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