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
        public static void incrementing()
        {
            themeNumber++;
            if (themeNumber > 12)
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
    }
}
