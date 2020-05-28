using Mirror;
using System;
using UnityEngine;
using WW_SYSTEM;
using WW_SYSTEM.API;
using WW_SYSTEM.EventHandlers;
using WW_SYSTEM.Events;
using Object = UnityEngine.Object;

namespace AerUtils
{
    public class CleanupEventHandler : IEventHandlerAdminQuery
    {
        public Plugin plugin;
        public Rid rid;
        public Round rd;
        public GameObject gameObject;

        public CleanupEventHandler(Plugin plugin)
        {
            this.plugin = plugin;
        }
        public void OnAdminQuery(AdminQueryEvent ev)
        {
            var utilsenable = plugin.Config.GetBool("aerutils_enable", true);
            if (!utilsenable) return;
            string[] array = ev.Query.Split();

            if (ev.Query.ToLower() == "cleanup")
            {
                if (array.Length <= 1)
                {
                    ev.Output = "AerUtils_Cleanup#Usage: cleanup <ragdolls/items/all>";
                    ev.Successful = true;
                    ev.Handled = true;
                    return;
                }
                if (string.IsNullOrEmpty(array[1]))
                {
                    ev.Output = "AerUtils_Cleanup#Usage: cleanup <ragdolls/items/all>";
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

                            ev.Output = "AerUtils_Cleanup#Not enough permissions";
                            ev.Successful = false;
                            ev.Handled = true;
                            return;
                        }
                        else
                        {
                            if (array.Length > 1)
                            {
                                if (array[1].ToLower() == "help")
                                {
                                    ev.Output = "AerUtils_Cleanup#Usage: cleanup <ragdolls/items/all>";
                                    ev.Successful = true;
                                    ev.Handled = true;
                                    return;
                                }
                                if (array[1].ToLower() == "ragdoll" || array[1].ToLower() == "ragdolls" || array[1].ToLower() == "r")
                                {
                                    foreach (Ragdoll doll in Object.FindObjectsOfType<Ragdoll>())
                                    {
                                        NetworkServer.Destroy(doll.gameObject); // Destroy all ragdolls
                                    }

                                    ev.Output = "AerUtils_Cleanup#Done! Cleaned up ragdolls";
                                    ev.Successful = true;
                                    ev.Handled = true;
                                    return;

                                }
                                if (array[1].ToLower() == "item" || array[1].ToLower() == "items" || array[1].ToLower() == "i")
                                {
                                    foreach (Pickup item in Object.FindObjectsOfType<Pickup>())
                                    {
                                        NetworkServer.Destroy(item.gameObject); // Destroy all items
                                    }

                                    ev.Output = "AerUtils_Cleanup#Done! Cleaned up items";
                                    ev.Successful = true;
                                    ev.Handled = true;
                                    return;
                                }
                                if (array[1].ToLower().Contains("all") || array[1].ToLower().Contains("everything") || array[1].ToLower().Contains("a") || array[1].ToLower().Contains("e"))
                                {
                                    foreach (Pickup item in Object.FindObjectsOfType<Pickup>())
                                    {
                                        NetworkServer.Destroy(item.gameObject); // 
                                    }
                                    foreach (Ragdoll doll in Object.FindObjectsOfType<Ragdoll>())
                                    {
                                        NetworkServer.Destroy(doll.gameObject);
                                    }
                                    // ^ Destroy all items and ragdolls
                                    ev.Output = "AerUtils_Cleanup#Done! Cleaned up all";
                                    ev.Successful = true;
                                    ev.Handled = true;
                                    return;
                                }

                            }
                            else
                            {
                                ev.Output = "AerUtils_Cleanup#Usage: cleanup <ragdolls/items/all>";
                                ev.Successful = true;
                                ev.Handled = true;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        ev.Output = "AerUtils_Cleanup#Error: " + ex;
                        ev.Successful = false;
                        ev.Handled = true;
                        return;
                    }
                }
                else
                {
                    ev.Output = "AerUtils_Cleanup#Usage: cleanup <ragdolls/items/all>";
                    ev.Successful = true;
                    ev.Handled = true;
                }
            }
        }
    }
}
