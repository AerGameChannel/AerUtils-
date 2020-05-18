using System;
using WW_SYSTEM;
using WW_SYSTEM.EventHandlers;
using WW_SYSTEM.Events;
using MEC;
using System.Collections.Generic;

namespace AerUtils
{
    public class LightsOffEventHandler : IEventHandlerAdminQuery, IEventHandlerRoundStart
    {
        public Plugin plugin;
        public Random random;

        public LightsOffEventHandler(Plugin plugin)
        {
            this.plugin = plugin;
        }

        public void OnAdminQuery(AdminQueryEvent ev)
        {
            var utilsenable = plugin.Config.GetBool("aerutils_enable", true);
            if (!utilsenable) return;
            var aerutils_lo = plugin.Config.GetBool("aerutils_lo_enable", true);
            if (!aerutils_lo) return;
            string[] array = ev.Query.Split();

            if (ev.Query.ToLower().StartsWith("lo") || ev.Query.ToLower().StartsWith("lights"))
            {
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
                                int.TryParse(array[1], out int lotime);

                                Server.Round.EnableFlickering(lotime, false);

                                ev.Output = "AerUtils_LightsOff#Turned off the lights ";
                                ev.Successful = true;
                                ev.Handled = true;
                                if (int.TryParse(array[1], out int lotime2) == false)
                                {
                                    ev.Output = "AerUtils_LightsOff#Error! used" + lotime2 + "instead of positive integer";
                                    ev.Successful = false;
                                    ev.Handled = true;
                                }
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
        }
        public void OnRoundStart(RoundStartEvent ev)
        {
            var randomlo = plugin.Config.GetBool("aerutils_randomlo_enable", true);
            if (randomlo)
            {
                var utilsenable = plugin.Config.GetBool("aerutils_enable", true);
                var aerutils_lo = plugin.Config.GetBool("aerutils_lo_enable", true);
                var lo_cassie_msg = plugin.Config.GetString("aerutils_lo_cassie", "Warning . Generator malfunction detected");
                if (!utilsenable) return;
                if (!aerutils_lo) return;
                var randomlo_time = plugin.Config.GetInt("aerutils_randomlo_time", 20);
                var randomlo_delay_min = plugin.Config.GetInt("aerutils_randomlo_delay_min", 30);
                var randomlo_delay_max = plugin.Config.GetInt("aerutils_randomlo_delay_min", 60);
                var randomlo_onlyhcz = plugin.Config.GetBool("aerutils_randomlo_onlyhcz", true);
                if (randomlo_time <= 0) return;
                if (randomlo_delay_min <= 0 || randomlo_delay_max <= 0) return;
                if (randomlo == true && randomlo_time >= 1 && randomlo_delay_min >= 1 && randomlo_delay_max >= 1)
                {
                    Timing.RunCoroutine(RandomLODelay());

                    IEnumerator<float> RandomLODelay()
                    {
                        while(true)
                        {
                            var rdelay = random.Next(randomlo_delay_min, randomlo_delay_max + 1);
                            Server.Round.EnableFlickering(randomlo_time, randomlo_onlyhcz);
                            yield return Timing.WaitForSeconds(rdelay);
                            Server.Round.CustomCassieSL(lo_cassie_msg);
                        }
                    }
                }
            }
        }
    }
}
