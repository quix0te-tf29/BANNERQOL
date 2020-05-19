using CommunityQOL.Classes;
using StoryMode;
using System;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.GameMenus;
using TaleWorlds.CampaignSystem.Overlay;
using TaleWorlds.Core;
using TaleWorlds.Library;
using TaleWorlds.Localization;
using TaleWorlds.SaveSystem;
using TaleWorlds.Engine.Screens;
using TaleWorlds.CampaignSystem.Actions;
using TaleWorlds.CampaignSystem.SandBox.GameComponents.Party;
using System.Collections.Generic;

namespace CommunityQOL.Behaviours {
    class CommunityQOLBehaviour : CampaignBehaviorBase {
        /// <summary>
        /// Used to access events fired by the game itself, register listeners here to control game behaviour (for example, when a hero is created, do X)
        /// </summary>
        public override void RegisterEvents() {
            //When the game first gets launched, this gets called to do some initial setup
            CampaignEvents.OnSessionLaunchedEvent.AddNonSerializedListener(this, new Action<CampaignGameStarter>(this.OnSessionLaunched));
            //For the time being, this seems to be the only event that can be reliably hooked into after the player object is created to give the player starting gold
            StoryModeEvents.OnBannerPieceCollectedEvent.AddNonSerializedListener(this, new Action(this.AddGoldOneTimeOnly));
            //The daily tick event, at this point used so I can give the player a bit of passive income to bypass the earlygame grind
            CampaignEvents.DailyTickEvent.AddNonSerializedListener(this, new Action(this.BenefactorSupport));
        }

        /// <summary>
        /// used to pass save data
        /// </summary>
        /// <param name="dataStore"></param>
        public override void SyncData(IDataStore dataStore) {

        }

        /// <summary>
        /// Gets called when the session is first launched, does some setup
        /// </summary>
        /// <param name="obj">Takes a CampaignGameStarter Object</param>
        private void OnSessionLaunched(CampaignGameStarter obj) {
            //Dconsole.Instance().Log(this, "attempting to hook menu");
            try {
                //attempt to add the recruiter menu
                this.AddRecruiterMenu(obj);
                this.AddProcurementMenu(obj);
            } catch (Exception ex) {
                //Dconsole.Instance().Log(this, ex.ToString());
            }
        }

        private bool addLeaveConditional(MenuCallbackArgs args) {
            args.optionLeaveType = GameMenuOption.LeaveType.Leave;
            return true;
        }

        private void switchToVillageMenu(MenuCallbackArgs args) {
            GameMenu.SwitchToMenu("castle");
        }

        /// <summary>
        /// The Recruiter menu gets spliced into the game here
        /// </summary>
        /// <param name="obj"></param>
        public void AddRecruiterMenu(CampaignGameStarter obj) {
            GameMenuOption.OnConditionDelegate recruiterDelegate = delegate (MenuCallbackArgs args) {
                args.optionLeaveType = GameMenuOption.LeaveType.Recruit;
                return Settlement.CurrentSettlement.OwnerClan == Clan.PlayerClan;
            };
            GameMenuOption.OnConsequenceDelegate recruiterConsequencesDelegate = delegate (MenuCallbackArgs args) {
                GameMenu.SwitchToMenu("recruiter_hire_menu");
            };

            obj.AddGameMenu("recruiter_hire_menu", "Select a faction to recruit from.", null, GameOverlays.MenuOverlayType.None, GameMenu.MenuFlags.none);

            //Adds "Hire A recruiter" option to town keep and castle menus
            obj.AddGameMenuOption("town_keep", "recruiter_buy_recruiter", "Hire a recruiter.", recruiterDelegate, recruiterConsequencesDelegate, false, 4, false);
            obj.AddGameMenuOption("castle", "recruiter_buy_recruiter", "Hire a Recruiter", recruiterDelegate, recruiterConsequencesDelegate, false, 4, false);

            //This adds the pay menu option to the faction menu
            obj.AddGameMenuOption("recruiter_hire_menu", "recruiter_pay_small", "Pay 500.", delegate (MenuCallbackArgs args)
            {
                args.optionLeaveType = GameMenuOption.LeaveType.Recruit;
                string stringId = Settlement.CurrentSettlement.StringId;
                int cost = 500;
                bool flag = cost >= Hero.MainHero.Gold;
                return !flag;
            }, delegate (MenuCallbackArgs args)
            {
                string stringId = Settlement.CurrentSettlement.StringId;
                int cost = 500;
                bool flag = cost <= Hero.MainHero.Gold;
                if (flag) {
                    GiveGoldAction.ApplyForCharacterToSettlement(Hero.MainHero, Settlement.CurrentSettlement, cost, false);
                }
                GameMenu.SwitchToMenu("castle");
            }, false, -1, false);

            obj.AddGameMenuOption("recruiter_hire_menu", "recruiter_leave", "Nevermind.", new GameMenuOption.OnConditionDelegate(this.addLeaveConditional), new GameMenuOption.OnConsequenceDelegate(this.switchToVillageMenu), false, -1, false);
        }

        public void AddProcurementMenu(CampaignGameStarter obj) {
            GameMenuOption.OnConditionDelegate procurementDelegate = delegate (MenuCallbackArgs args) {
                args.optionLeaveType = GameMenuOption.LeaveType.Trade;
                return Settlement.CurrentSettlement.OwnerClan == Clan.PlayerClan;
            };
            GameMenuOption.OnConsequenceDelegate procurementConsequencesDelegate = delegate (MenuCallbackArgs args) {
                GameMenu.SwitchToMenu("procurement_menu");
            };

            obj.AddGameMenu("procurement_menu", "Select Items to procure", null, GameOverlays.MenuOverlayType.None, GameMenu.MenuFlags.none);

            //Adds "Procure War Materials" option to town keep
            obj.AddGameMenuOption("town_keep", "procurement_menu", "Procure War Materials.", procurementDelegate, procurementConsequencesDelegate, false, 4, false);

            obj.AddGameMenuOption("procurement_menu", "recruiter_leave", "Nevermind.", new GameMenuOption.OnConditionDelegate(this.addLeaveConditional), new GameMenuOption.OnConsequenceDelegate(this.switchToVillageMenu), false, -1, false);
        }

        private void AddGoldOneTimeOnly() {
            InformationManager.DisplayMessage(new InformationMessage("Your Clan's benefactors supplied you with some money to aid in completing your mission", TaleWorlds.Library.Color.White, ""));
            ChangeOwnerOfSettlementAction.ApplyByRevolt(Hero.MainHero, Settlement.Find("town_A6"));
            Hero.MainHero.ChangeHeroGold(30000);
        }

        private void BenefactorSupport() {
            Hero.MainHero.ChangeHeroGold(600);
        }

        /// <summary>
        /// May Need Fixing
        /// </summary>
        /// <returns></returns>
        public List<CultureObject> getPossibleCultures() {
            IEnumerable<Settlement> settlements = Settlement.All;

            List<CultureObject> returnList = new List<CultureObject>();
            foreach (Settlement settlement in settlements) {
                if (!returnList.Contains(settlement.Culture)) {
                    returnList.Add(settlement.Culture);
                }
            }
            return returnList;
        }
    }
}

