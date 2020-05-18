using System;
using System.Collections.Generic;
using System.Linq;
using WW_SYSTEM;
using WW_SYSTEM.API;
using WW_SYSTEM.EventHandlers;
using WW_SYSTEM.Events;

namespace AerUtils
{
    class PersonalBCEventHandler : IEventHandlerAdminQuery
    {
        public Plugin plugin;

        public PersonalBCEventHandler(Plugin plugin)
        {
            this.plugin = plugin;
        }
        public void OnAdminQuery(AdminQueryEvent ev)
        {
            var utilsenable = plugin.Config.GetBool("aerutils_enable", true);
            if (!utilsenable) return;
            string[] array = ev.Query.Split();

            if (ev.Query.ToLower().StartsWith("pbc"))
            {
                if (array.Length <= 1)
                {
                    ev.Output = "AerUtils_PersonalBC#Usage: pbc <RA player id> <time in seconds> <text>";
                    ev.Successful = true;
                    ev.Handled = true;
                    return;
                }
                if (string.IsNullOrEmpty(array[1]))
                {
                    ev.Output = "AerUtils_PersonalBC#Usage: pbc <RA player id> <time in seconds> <text>";
                    ev.Successful = true;
                    ev.Handled = true;
                    return;
                }
                if (array.Length > 1)
                {
                    try
                    {
                        if (!ev.Admin.IsPermitted(PlayerPermissions.Broadcasting))
                        {
                            ev.Output = "AerUtils_PersonalBC#Not enough permissions";
                            ev.Successful = false;
                            ev.Handled = true;
                            return;
                        }
                        if (array.Length > 0)
                        {
                            if (array.Length > 0)
                            {
                                ev.Output = "AerUtils_PersonalBC#Usage: pbc <RA player id> <time in seconds> <text>";
                                ev.Successful = true;
                                ev.Handled = true;
                                return;
                            }
                            if (array[1].ToLower().Contains("help"))
                            {
                                ev.Output = "AerUtils_PersonalBC#Usage: pbc <RA player id> <time in seconds> <text>";
                                ev.Successful = true;
                                ev.Handled = true;
                                return;
                            }
                            else
                            {
                                int.TryParse(array[1], out int id);

                                Player pl = Server.Round.FindPlayerWithId(id);
                                if (pl != null)
                                {
                                    IEnumerable<string> thing = array.Skip(3);
                                    string msg = "";
                                    foreach (string s in thing) msg += $"{s} ";
                                    pl.PersonalBroadcast(msg, Convert.ToUInt32(array[2]), false); // Send personal broadcast to player

                                    ev.Output = "AerUtils_PersonalBC#Done!";
                                    ev.Successful = true;
                                    ev.Handled = true;
                                    return;
                                }
                                else
                                {
                                    ev.Output = "AerUtils_PersonalBC# Please enter valid player id!";
                                    ev.Successful = false;
                                    ev.Handled = true;
                                }
                            }
                        }
                        else
                        {
                            ev.Output = "AerUtils_PersonalBC#Usage: pbc <RA player id> <time in seconds> <text>";
                            ev.Successful = true;
                            ev.Handled = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        ev.Output = "AerUtils_PersonalBC#Error: " + ex;
                        ev.Successful = false;
                        ev.Handled = true;
                    }
                }
            }
        }
    }
}
