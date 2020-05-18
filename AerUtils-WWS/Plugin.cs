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
        version = "2.0.0",
        WWSYSTEMMajor = 6,
        WWSYSTEMMinor = 3,
        WWSYSTEMRevision = 1
        )]
    public class AerUtils : Plugin
    {
        public override void OnDisable()
        {
            this.Info(this.Details.name + " was disabled");
        }

        public override void OnEnable()
        {
            var utilsenable = this.Config.GetBool("aerutils_enable", true);
            if (!utilsenable) return;
            this.Info(this.Details.name + " has loaded");
        }
        public override void Register()
        {
            AddEventHandlers(new PersonalBCEventHandler(this));
            AddEventHandlers(new LightsOffEventHandler(this));
            AddEventHandlers(new JBCEventHandler(this));
            AddEventHandlers(new BreakDoorsEventHandler(this));
            AddEventHandlers(new OtherFunctions(this));
            AddEventHandlers(new SizeChangeEventHandler(this));
            AddEventHandlers(new CleanupEventHandler(this));
            AddEventHandlers(new InstaKillEventHandler(this));
        }
    }
}
