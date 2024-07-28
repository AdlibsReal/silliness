using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.XR;
using static silliness.Menu.Main;
using static silliness.Menu.Settings;

namespace silliness.Mods
{
    internal class Global
    {
        public static void ReturnHome()
        {
            pageName = "home ";
            buttonsType = 0;
        }
        public static void EnabledMods()
        {
            pageName = "enabled mods ";
            buttonsType = 1;
        }
        public static void MovementMods()
        {
            pageName = "movement ";
            buttonsType = 6;
        }
        public static void MiscellaneousMods()
        {
            pageName = "miscellaneous ";
            buttonsType = 7;
        }
        public static void RigMods()
        {
            pageName = "rig ";
            buttonsType = 8;
        }
        public static void SafetyMods()
        {
            pageName = "safety ";
            buttonsType = 9;
        }
        public static void VisualMods()
        {
            pageName = "visual ";
            buttonsType = 10;
        }
        public static void incrementing()
        {
            themeNumber++;
            if (themeNumber > 15)
            {
                themeNumber = 1;
            }
            ChangeTheme();
        }
        public static void pageincrementing()
        {
            pageButtonType++;
            if (pageButtonType > 2)
            {
                pageButtonType = 1;
            }
        }
        public static void FreezeRigInMenu()
        {
            if (menu != null)
            {
                if (blahhhhhhh == Vector3.zero)
                {
                    blahhhhhhh = GorillaTagger.Instance.rigidbody.transform.position;
                }
                else
                {
                    GorillaTagger.Instance.rigidbody.transform.position = blahhhhhhh;
                }
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            }
            else
            {
                blahhhhhhh = Vector3.zero;
            }
        }
        public static void Discord()
        {
            Application.OpenURL("https://discord.gg/5ySdQqDWFV");
        }
        public static void ZeroGravityMenu()
        {
            menu.GetComponent<Rigidbody>().velocity -= Vector3.down * 9.81f * Time.deltaTime;
        }
        public static void FixRigColors()
        {
            foreach (VRRig rig in GorillaParent.instance.vrrigs)
            {
                if (rig.mainSkin.material.name.Contains("gorilla_body"))
                {
                    rig.mainSkin.material.color = rig.playerColor;
                }
            }
        }
        public static void ThinMenuEnable()
        {
            thinMenu = true;
        }
        public static void ThinMenuDisable()
        {
            thinMenu = false;
        }
    }
}
