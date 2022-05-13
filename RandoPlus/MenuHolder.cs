﻿using MenuChanger;
using MenuChanger.MenuElements;
using MenuChanger.MenuPanels;
using MenuChanger.Extensions;
using RandomizerMod.Menu;
using UnityEngine.SceneManagement;

namespace RandoPlus
{
    public class MenuHolder
    {
        internal MenuPage RandoPlus;
        internal MenuElementFactory<GlobalSettings> rpMEF;
        internal VerticalItemPanel rpVIP;

        internal SmallButton JumpToRPButton;

        private static MenuHolder _instance = null;
        internal static MenuHolder Instance => _instance ?? (_instance = new MenuHolder());

        public static void OnExitMenu()
        {
            _instance = null;
        }

        public static void Hook()
        {
            RandomizerMenuAPI.AddMenuPage(Instance.ConstructMenu, Instance.HandleButton);
            MenuChangerMod.OnExitMainMenu += OnExitMenu;
        }

        private bool HandleButton(MenuPage landingPage, out SmallButton button)
        {
            JumpToRPButton = new(landingPage, "RandoPlus");
            JumpToRPButton.AddHideAndShowEvent(landingPage, RandoPlus);
            button = JumpToRPButton;
            return true;
        }

        private void ConstructMenu(MenuPage landingPage)
        {
            RandoPlus = new MenuPage("RandoPlus", landingPage);
            rpMEF = new(RandoPlus, global::RandoPlus.RandoPlus.GS);
            rpVIP = new(RandoPlus, new(0, 300), 50f, false, rpMEF.Elements);
            Modding.Logger.Log(rpVIP.Items[rpVIP.Items.Count - 1].ToString());
            rpVIP.Items[rpVIP.Items.Count - 1].Hide();
            //rpVIP.Items.Find(x=> x.ToString().ToLower() == "UpgradesTaken".ToLower()).Hide();
        }
    }
}