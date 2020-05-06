using WW_SYSTEM;
using WW_SYSTEM.Attributes;

namespace AerUtils
{
    [PluginDetails(
        author = "Aer",
        name = "AerUtils",
        description = "AerUtils",
        id = "aerutils.plugin",
        configPrefix = "AerUtils",
        version = "1.0",
        WWSYSTEMMajor = 6,
        WWSYSTEMMinor = 3,
        WWSYSTEMRevision = 1
        )]
    public class AerUtils : Plugin
    {
        public override void OnDisable()
        {
            this.Info(this.Details.name + "AerUtils was disabled");
        }

        public override void OnEnable()
        {
            bool utilsenable = this.Config.GetBool("aerutils_enable", true);
            if (!utilsenable) return;
            this.Info(this.Details.name + " has loaded");
        }

        public override void Register()
        {
            AddEventHandlers(new EventHandler(this));
        }
    }
}
