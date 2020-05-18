using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem;

using ArmiesOfCalradia.Models;
using ArmiesOfCalradia.Behaviours;
using ArmiesOfCalradia.Classes;

namespace ArmiesOfCalradia
{
    class Main : MBSubModuleBase
    {
        protected override void OnSubModuleLoad()
        {   
            base.OnSubModuleLoad();

        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject)
        {
            base.OnGameStart(game, gameStarterObject);
            AddModels(gameStarterObject as CampaignGameStarter);
            AddBehaviours(gameStarterObject as CampaignGameStarter);
        }

        public override void OnNewGameCreated(Game game, object initializerObject)
        {
            //Dconsole.Instance().Log(this, "ARMIES OF CALRADIA MOD HAS SUCCESSFULLY LOADED");
        }

        private void AddModels(CampaignGameStarter gameStarter)
        {
            if (gameStarter != null)
            {
                gameStarter.AddModel(new ModifiedMarriageModel());
            }
        }

        private void AddBehaviours(CampaignGameStarter gameStarter)
        {
            if (gameStarter != null)
            {
                gameStarter.AddBehavior(new ArmiesOfCalradiaBehaviour());
            }
        }
    }

}
