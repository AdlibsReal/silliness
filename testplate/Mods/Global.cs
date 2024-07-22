using UnityEngine;
using UnityEngine.XR;
using static silliness.Menu.Main;

namespace silliness.Mods
{
    internal class Global
    {
        public static void ReturnHome()
        {
            pageName = "home ";
            buttonsType = 0;
        }
        public static void MovementMods()
        {
            pageName = "movement ";
            buttonsType = 5;
        }
        public static void MiscellaneousMods()
        {
            pageName = "miscellaneous ";
            buttonsType = 6;
        }
        public static void RigMods()
        {
            pageName = "rig ";
            buttonsType = 7;
        }
        public static void SafetyMods()
        {
            pageName = "safety ";
            buttonsType = 8;
        }
        public static void VisualMods()
        {
            pageName = "visual ";
            buttonsType = 9;
        }
        public static void incrementing()
        {
            themeNumber++;
            if (themeNumber > 13)
            {
                themeNumber = 1;
            }
            ChangeTheme();
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
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (vrrig.mainSkin.material.name.Contains("gorilla_body"))
                {
                    vrrig.mainSkin.material.color = vrrig.playerColor;
                }
            }
        }
    }
}
