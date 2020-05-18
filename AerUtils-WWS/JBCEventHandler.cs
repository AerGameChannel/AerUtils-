using WW_SYSTEM;
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
            var utilsenable = plugin.Config.GetBool("aerutils_enable", true);
            if (!utilsenable) return;
            var aerutils_jbc = plugin.Config.GetBool("aerutils_jbc_enable", true);
            if (!aerutils_jbc) return;
            var bctime = plugin.Config.GetUInt("aerutils_jbc_time", 15);
            var bcmsg = plugin.Config.GetString("aerutils_jbc_message", "Welcome to the server!"); 
            ev.Player.PersonalBroadcast(bcmsg, bctime, false); // Send join broadcast to player
        }
    }
}
