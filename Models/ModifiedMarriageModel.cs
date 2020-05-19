using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using CommunityQOL.Classes;

namespace CommunityQOL.Models {
    class ModifiedMarriageModel : DefaultMarriageModel {

        //As long as everyone's a non-gross age (aka over 18), nobodies already married, siblings etc...
        public override bool IsCoupleSuitableForMarriage(Hero firstHero, Hero secondHero) {
            return (firstHero.Age >= 18 && secondHero.Age >= 18 && firstHero.Spouse == null && secondHero.Spouse == null);
        }

        //I think this is for political marriages other than the player character, weird ass method name tho lol
        public override List<Hero> GetChildrenSuitableForMarriage(Hero hero) {
            return base.GetChildrenSuitableForMarriage(hero);
        }

        //still isn't clear when the game engine calls this
        public override bool IsSuitableForMarriage(Hero maidenOrSuitor) {
            DebugConsole.Log(this, "SUITABILITY CHECK INITIATED with: " + maidenOrSuitor.GetName());
            return (maidenOrSuitor.Age >= 18 && maidenOrSuitor.GetRelationWithPlayer() > 10.0);
        }
    }
}
