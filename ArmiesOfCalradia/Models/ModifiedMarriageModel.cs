using TaleWorlds.CampaignSystem;
using TaleWorlds.CampaignSystem.SandBox.CampaignBehaviors;
using TaleWorlds.CampaignSystem.SandBox.GameComponents;
using System.Collections.Generic;
using TaleWorlds.Core;
using ArmiesOfCalradia.Classes;

namespace ArmiesOfCalradia.Models
{
    class ModifiedMarriageModel : DefaultMarriageModel
    {
        //As long as everyone's a non-gross age (aka over 18), nobodies already married, siblings etc...
        public override bool IsCoupleSuitableForMarriage(Hero firstHero, Hero secondHero)
        {
            if (firstHero.Age >= 18 && secondHero.Age >= 18 && firstHero.Spouse == null && secondHero.Spouse == null)
            {
                    return true;
            }
                else
                {
                    return false;
                } 
        }

        //I think this is for political marriages other than the player character, weird ass method name tho lol
        public override List<Hero> GetChildrenSuitableForMarriage(Hero hero)
        {
            return base.GetChildrenSuitableForMarriage(hero);
        }

        //still isn't clear when the game engine calls this
        public override bool IsSuitableForMarriage(Hero maidenOrSuitor)
        {
            Dconsole.Instance().Log(this, "SUITABILITY CHECK INITIATED with: " + maidenOrSuitor.GetName() + ".");
            if (maidenOrSuitor.Age >= 18 && maidenOrSuitor.GetRelationWithPlayer() > 10.0)
            {
                return true;
            }
                else
                {
                    return false;
                }
        }
    }
}
