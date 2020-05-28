using System;
using UnityEngine;
using WW_SYSTEM;
using WW_SYSTEM.API;
using WW_SYSTEM.EventHandlers;
using WW_SYSTEM.Events;

namespace AerUtils
{
    public class SizeChangeEventHandler : IEventHandlerAdminQuery
    {
        public Plugin plugin;
        public Rid rid;
        public Round rd;
        public GameObject gameObject;

        public SizeChangeEventHandler(Plugin plugin)
        {
            this.plugin = plugin;
        }
        public void OnAdminQuery(AdminQueryEvent ev)
        {
            if (ev.Query.ToLower() == "size")
            {
                try
                {
                    var utilsenable = plugin.Config.GetBool("aerutils_enable", true);
                    if (!utilsenable) return;
                    string[] array = ev.Query.Split();

                    if (array.Length <= 1)
                    {
                        ev.Output = "AerUtils_Size#Usage: size <RA player id> <x> <y> <z>";
                        ev.Successful = true;
                        ev.Handled = true;
                        return;
                    }
                    if (string.IsNullOrEmpty(array[1]))
                    {
                        ev.Output = "AerUtils_Size#Usage: size <RA player id> <x> <y> <z>";
                        ev.Successful = true;
                        ev.Handled = true;
                        return;
                    }
                    if (array.Length > 1)
                    {
                        if (!ev.Admin.IsPermitted(PlayerPermissions.PlayersManagement))
                        {
                            ev.Output = "AerUtils_Size#Not enough permissions";
                            ev.Successful = false;
                            ev.Handled = true;
                            return;
                        }
                        if (array.Length > 0)
                        {
                            if (array[1].ToLower() == "help")
                            {
                                ev.Output = "AerUtils_Size#Usage: size <RA player id> <x> <y> <z>";
                                ev.Successful = true;
                                ev.Handled = true;
                                return;
                            }
                            else
                            {
                                var id = int.TryParse(array[1], out int pid);
                                var sx = float.TryParse(array[2], out float size_x);
                                var sy = float.TryParse(array[3], out float size_y);
                                var sz = float.TryParse(array[4], out float size_z);

                                Player pl = Server.Round.FindPlayerWithId(pid);

                                if (pl != null)
                                {
                                    pl.SetPlayerScale(size_x, size_y, size_z); // Set size for player

                                    ev.Output = "AerUtils_Size#Set custom size for player " + pl.Nick;
                                    ev.Successful = true;
                                    ev.Handled = true;
                                    return;
                                }
                                if (string.IsNullOrEmpty(array[1]) && string.IsNullOrEmpty(array[2]) && string.IsNullOrEmpty(array[3]) && string.IsNullOrEmpty(array[4]))
                                {
                                    ev.Output = "AerUtils_Size#Usage: size <RA player id> <x> <y> <z>";
                                    ev.Successful = true;
                                    ev.Handled = true;
                                    return;
                                }
                                if (sx == false || sy == false || sz == false)
                                {
                                    ev.Output = "AerUtils_Size#Error: You have to use only positive integer player id and float size (Example: size 2 1,5 1,5 1,5)";
                                    ev.Successful = false;
                                    ev.Handled = true;
                                    return;
                                }
                                if (id == false) // If player id is invalid
                                {
                                    ev.Output = "Please, enter valid player id!";
                                    ev.Successful = false;
                                    ev.Handled = true;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ev.Output = "AerUtils_Size#Error: " + ex;
                    ev.Successful = false;
                    ev.Handled = true;
                    return;
                }
            }
        }
    }
}
