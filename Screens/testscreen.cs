using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.GauntletUI;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Engine.Screens;
using TaleWorlds.Engine.GauntletUI;
using TaleWorlds.GauntletUI.Data;
using TaleWorlds.CampaignSystem.ViewModelCollection.GameMenu;

namespace CommunityQOL.Screens
{
    class testscreen : ScreenBase
    {
        private GauntletLayer _testLayer;
        private GauntletMovie _testMovie;
        private bool _firstRender;

        protected override void OnInitialize()
        {
            base.OnInitialize();
            _testLayer = new GauntletLayer(100);
            _testLayer.IsFocusLayer = true;
            AddLayer(_testLayer);
            _testLayer.InputRestrictions.SetInputRestrictions();
            ScreenManager.TrySetFocus(_testLayer);
            //_testMovie = _testLayer.LoadMovie();
            _firstRender = true;
        }

        protected override void OnActivate()
        {
            base.OnActivate();
            ScreenManager.TrySetFocus(_testLayer);
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();
            _testLayer.IsFocusLayer = false;
            ScreenManager.TryLoseFocus(_testLayer);
        }

        protected override void OnFinalize()
        {
            base.OnFinalize();
            RemoveLayer(_testLayer);
            _testLayer = null;
        }

        protected override void OnFrameTick(float dt)
        {
            base.OnFrameTick(dt);
        }
    }
}
