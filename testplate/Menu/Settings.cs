using silliness.Classes;
using UnityEngine;
using static silliness.Menu.Main;

namespace silliness.Menu
{
    internal class Settings
    {
        public static ExtGradient backgroundColor = new ExtGradient { colors = GetSolidGradient(new Color(0.996f, 0.486f, 0.890f)) };
        public static ExtGradient mainBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(1f, 0.584f, 0.918f)) };
        public static ExtGradient buttonBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(1f, 0.592f, 0.918f)) };
        public static ExtGradient[] buttonColors = new ExtGradient[]
        {
            new ExtGradient{colors = GetSolidGradient(new Color(1f, 0.439f, 0.886f)) }, // Disabled
            new ExtGradient{colors = GetSolidGradient(Color.black)} // Enabled
        };
        public static Color[] textColors = new Color[]
        {
            Color.white, // Disabled
            Color.white // Enabled
        };


        public static Font currentFont = Font.CreateDynamicFontFromOSFont("Comic Sans MS", 24);

        public static bool fpsCounter = true;
        public static bool disconnectButton = true;
        public static bool rightHanded = false;
        public static bool middleHanded = false;
        public static bool disableNotifications = false;
        public static bool outlines = false;

        public static KeyCode keyboardButton = KeyCode.Q;

        public static Vector3 menuSize = new Vector3(0.1f, 1f, 1.05f); // Depth, Width, Height
        public static int buttonsPerPage = 11;
    }
}
