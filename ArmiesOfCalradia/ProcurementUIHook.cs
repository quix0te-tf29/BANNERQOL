using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SandBox.GauntletUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.ViewModelCollection.ClanManagement;
using TaleWorlds.Engine.Screens;
using TaleWorlds.GauntletUI;

namespace ArmiesOfCalradia
{
    class ProcurementUIHook : Widget
    {
        public ProcurementUIHook(UIContext context) : base(context)
        { 
            if(ScreenManager.TopScreen is GauntletClanScreen)
        }
    }
}
