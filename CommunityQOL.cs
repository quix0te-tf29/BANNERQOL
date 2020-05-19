using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade;
using TaleWorlds.CampaignSystem;
using CommunityQOL.Behaviours;
using CommunityQOL.Classes;
using CommunityQOL.Models;
using System.Diagnostics;

namespace CommunityQOL
{
    public class CommunityQOL : MBSubModuleBase {

        private DebugConsole debugConsole = null;

        protected override void OnSubModuleLoad() {
            base.OnSubModuleLoad();
            if (debugConsole == null) {
                debugConsole = DebugConsole.Instance();
            }
            DebugConsole.Log(this, "SubModule loaded");
        }

        public override void OnNewGameCreated(Game game, object initializerObject) {
            base.OnNewGameCreated(game, initializerObject);
            DebugConsole.Log(this, "New Game Created");
            InformationManager.DisplayMessage(new InformationMessage("CommunityQOL Loaded"));
        }

        protected override void OnGameStart(Game game, IGameStarter gameStarterObject) {
            base.OnGameStart(game, gameStarterObject);
            DebugConsole.Log(this, "Game Started");
            if (game.GameType is Campaign) {
                DebugConsole.Log(this, "Game Type is Campaign");
                CampaignGameStarter campaignGameStarter = (CampaignGameStarter)gameStarterObject;
                try {
                    campaignGameStarter.AddModel(new ModifiedMarriageModel());
                    campaignGameStarter.AddBehavior(new CommunityQOLBehaviour());
                    DebugConsole.Log(this, "Mod Behavior added");
                } catch (Exception ex) {
                    DebugConsole.Log(this, "Cast of [IGameStarter] object \"gameStarterObject\" to [CampaignGameStarter] object failed");
                    DebugConsole.Log(ex, ex.Message);
                }
            }
        }
    }
}
