﻿using silliness.Classes;
using silliness.Menu;
using silliness.Notifications;
using System.IO;
using static silliness.Menu.Main;
using static silliness.Menu.Settings;
using static UnityEngine.ParticleSystem;

namespace silliness.Mods
{
    internal class SettingsMods
    {
        public static void EnterSettings()
        {
            pageName = "settings ";
            buttonsType = 1;
        }

        public static void MenuSettings()
        {
            pageName = "menu settings ";
            buttonsType = 2;
        }

        public static void MovementSettings()
        {
            pageName = "movement settings ";
            buttonsType = 3;
        }

        public static void ProjectileSettings()
        {
            pageName = "projectile settings ";
            buttonsType = 4;
        }

        public static void RightHand()
        {
            rightHanded = true;
        }

        public static void LeftHand()
        {
            rightHanded = false;
        }

        public static void EnableFPSCounter()
        {
            fpsCounter = true;
        }

        public static void DisableFPSCounter()
        {
            fpsCounter = false;
        }

        public static void EnableNotifications()
        {
            disableNotifications = false;
        }

        public static void DisableNotifications()
        {
            disableNotifications = true;
        }

        public static void EnableDisconnectButton()
        {
            disconnectButton = true;
        }

        public static void DisableDisconnectButton()
        {
            disconnectButton = false;
        }
        public static void EnableOutlines()
        {
            outlines = true;
        }

        public static void DisableOutlines()
        {
            outlines = false;
        }
        public static void EnableMiddleHand()
        {
            middleHanded = true;
        }

        public static void DisableMiddleHand()
        {
            middleHanded = false;
        }
        public static void SavePreferences()
        {
            string text = "";
            foreach (ButtonInfo[] buttonlist in Buttons.buttons)
            {
                foreach (ButtonInfo v in buttonlist)
                {
                    if (v.enabled && v.buttonText != "save preferences")
                    {
                        if (text == "")
                        {
                            text += v.buttonText;
                        }
                        else
                        {
                            text += "\n" + v.buttonText;
                        }
                    }
                }
            }

            if (!Directory.Exists("silliness"))
            {
                Directory.CreateDirectory("silliness");
            }
            File.WriteAllText("silliness/enabledmods.txt", text);
            File.WriteAllText("silliness/theme.txt", themeNumber.ToString());
        }
        public static void LoadPreferences()
        {
            if (Directory.Exists("silliness"))
            {
                TurnOffAllMods();
                try
                {
                    string config = File.ReadAllText("silliness/enabledmods.txt");
                    string[] activebuttons = config.Split("\n");
                    for (int index = 0; index < activebuttons.Length; index++)
                    {
                        Toggle(activebuttons[index]);
                    }
                }
                catch { }
                string themer = File.ReadAllText("silliness/theme.txt");

                themeNumber = int.Parse(themer) - 1;
                Toggle("change theme [pink]");
            }
        }
        public static void TurnOffAllMods()
        {
            foreach (ButtonInfo[] buttonlist in Buttons.buttons)
            {
                foreach (ButtonInfo v in buttonlist)
                {
                    if (v.enabled)
                    {
                        Toggle(v.buttonText);
                    }
                }
            }
            NotifiLib.ClearAllNotifications();
        }
    }
}
