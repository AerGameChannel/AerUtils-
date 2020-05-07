using System;
using System.Collections.Generic;
using System.Linq;
using WW_SYSTEM;
using WW_SYSTEM.API;
using WW_SYSTEM.EventHandlers;
using WW_SYSTEM.Events;

namespace AerUtils
{
    class JBCEventHandler : IEventHandlerPlayerJoin
    {
        public Plugin plugin;

        public JBCEventHandler(Plugin plugin)
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
    }
}