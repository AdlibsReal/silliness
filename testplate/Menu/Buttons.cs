using silliness.Classes;
using silliness.Mods;
using static silliness.Menu.Settings;

namespace silliness.Menu
{
    internal class Buttons
    {
        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            //                 _                             _
            // _ __ ___   __ _(_)_ __    _ __ ___   ___   __| |___
            //| '_ ` _ \ / _` | | '_ \  | '_ ` _ \ / _ \ / _` / __|
            //| | | | | | (_| | | | | | | | | | | | (_) | (_| \__ \
            //|_| |_| |_|\__,_|_|_| |_| |_| |_| |_|\___/ \__,_|___/
            new ButtonInfo[] {
                new ButtonInfo { buttonText = "settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "breakfast"},
                new ButtonInfo { buttonText = "join the discord", method =() => Global.Discord(), isTogglable = false, toolTip = "makes you join the discord"},
                new ButtonInfo { buttonText = "movement mods", method =() => Global.MovementMods(), isTogglable = false, toolTip = "opens up the movement mods page"},
                new ButtonInfo { buttonText = "miscellaneous mods", method =() => Global.MiscellaneousMods(), isTogglable = false, toolTip = "opens up the miscellaneous mods page"},
                new ButtonInfo { buttonText = "rig mods", method =() => Global.RigMods(), isTogglable = false, toolTip = "opens up the rig mods page"},
                new ButtonInfo { buttonText = "safety mods", method =() => Global.SafetyMods(), isTogglable = false, toolTip = "opens up the safety mods page"},
                new ButtonInfo { buttonText = "visual mods", method =() => Global.VisualMods(), isTogglable = false, toolTip = "opens up the visual mods page"},
            },


            //          _   _   _
            // ___  ___| |_| |_(_)_ __   __ _ ___
            /// __|/ _ \ __| __| | '_ \ / _` / __|
            //\__ \  __/ |_| |_| | | | | (_| \__ \
            //|___/\___|\__|\__|_|_| |_|\__, |___/
            //                          |___/
            new ButtonInfo[] {
                new ButtonInfo { buttonText = "return to main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "go back to the main page of the menu"},
                new ButtonInfo { buttonText = "menu", method =() => SettingsMods.MenuSettings(), isTogglable = false, toolTip = "your only here to turn on like 5 things"},
            },


            //                                       _   _   _
            // _ __ ___   ___ _ __  _   _   ___  ___| |_| |_(_)_ __   __ _ ___
            //| '_ ` _ \ / _ \ '_ \| | | | / __|/ _ \ __| __| | '_ \ / _` / __|
            //| | | | | |  __/ | | | |_| | \__ \  __/ |_| |_| | | | | (_| \__ \
            //|_| |_| |_|\___|_| |_|\__,_| |___/\___|\__|\__|_|_| |_|\__, |___/
            //                                                       |___/
            new ButtonInfo[] {
                new ButtonInfo { buttonText = "return to settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "returns to the main settings page for the menu"},
                new ButtonInfo { buttonText = "save preferences", method =() => SettingsMods.SavePreferences(), isTogglable = false, toolTip = "saves your preferences"},
                new ButtonInfo { buttonText = "change theme [pink]", method =() => Global.incrementing(), isTogglable = false, toolTip = "switches the theme"},
                new ButtonInfo { buttonText = "outlines", enableMethod =() => SettingsMods.EnableOutlines(), disableMethod =() => SettingsMods.DisableOutlines(), toolTip = "turns on outlines" },
                new ButtonInfo { buttonText = "freeze player in menu", method =() => Global.FreezeRigInMenu(), toolTip = "freezes you in the menu"},
                new ButtonInfo { buttonText = "zero gravity menu", method =() => Global.ZeroGravityMenu(), toolTip = "makes the menu float away when you let go of it"},
                new ButtonInfo { buttonText = "fix rig colors", method =() => Global.FixRigColors(), toolTip = "fixes rig colors"},
                new ButtonInfo { buttonText = "right hand", enableMethod =() => SettingsMods.RightHand(), disableMethod =() => SettingsMods.LeftHand(), toolTip = "puts the menu on your right hand"},
                new ButtonInfo { buttonText = "notifications", enableMethod =() => SettingsMods.EnableNotifications(), disableMethod =() => SettingsMods.DisableNotifications(), enabled = !disableNotifications, toolTip = "toggles the notifications"},
            //  new ButtonInfo { buttonText = "FPS counter", enableMethod =() => SettingsMods.EnableFPSCounter(), disableMethod =() => SettingsMods.DisableFPSCounter(), enabled = fpsCounter, toolTip = "toggles the FPS counter"},
                new ButtonInfo { buttonText = "disconnect button", enableMethod =() => SettingsMods.EnableDisconnectButton(), disableMethod =() => SettingsMods.DisableDisconnectButton(), enabled = disconnectButton, toolTip = "toggles the disconnect button"},
            },


            //                                               _              _   _   _                 
            // _ __ ___   _____   _____ _ __ ___   ___ _ __ | |_   ___  ___| |_| |_(_)_ __   __ _ ___ 
            //| '_ ` _ \ / _ \ \ / / _ \ '_ ` _ \ / _ \ '_ \| __| / __|/ _ \ __| __| | '_ \ / _` / __|
            //| | | | | | (_) \ V /  __/ | | | | |  __/ | | | |_  \__ \  __/ |_| |_| | | | | (_| \__ \
            //|_| |_| |_|\___/ \_/ \___|_| |_| |_|\___|_| |_|\__| |___/\___|\__|\__|_|_| |_|\__, |___/
            //                                                                              |___/     
            new ButtonInfo[] {
                new ButtonInfo { buttonText = "return to settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "returns to the main settings page"},
            },


            //                 _           _   _ _                             _     
            // _ __  _ __ ___ (_) ___  ___| |_(_) | ___    _ __ ___   ___   __| |___ 
            //| '_ \| '__/ _ \| |/ _ \/ __| __| | |/ _ \  | '_ ` _ \ / _ \ / _` / __|
            //| |_) | | | (_) | |  __/ (__| |_| | |  __/  | | | | | | (_) | (_| \__ \
            //| .__/|_|  \___// |\___|\___|\__|_|_|\___|  |_| |_| |_|\___/ \__,_|___/
            //|_|           |__/                          
            new ButtonInfo[] {
                new ButtonInfo { buttonText = "return to settings", method =() => SettingsMods.MenuSettings(), isTogglable = false, toolTip = "opens the settings"},
            },


            //                                               _                          _     
            // _ __ ___   _____   _____ _ __ ___   ___ _ __ | |_    _ __ ___   ___   __| |___ 
            //| '_ ` _ \ / _ \ \ / / _ \ '_ ` _ \ / _ \ '_ \| __|  | '_ ` _ \ / _ \ / _` / __|
            //| | | | | | (_) \ V /  __/ | | | | |  __/ | | | |_   | | | | | | (_) | (_| \__ \
            //|_| |_| |_|\___/ \_/ \___|_| |_| |_|\___|_| |_|\__|  |_| |_| |_|\___/ \__,_|___/
            new ButtonInfo[] {
                new ButtonInfo { buttonText = "return to main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "go back to the main page of the menu"},
                new ButtonInfo { buttonText = "wasd fly", method =() => Movement.WASDFly(), toolTip = "use wasd to move around (mouse movement not included with this product)"},
                new ButtonInfo { buttonText = "speedboost", method =() => Movement.Speedboost(), toolTip = "makes you go slightly faster, i swear"},
                new ButtonInfo { buttonText = "platforms", method =() => Movement.Platforms(), toolTip = "spawns a shape of your choice under your hands"},
                new ButtonInfo { buttonText = "trigger platforms", method =() => Movement.TriggerPlatforms(), toolTip = "makes platforms use trigger instead of grip"},
                //new ButtonInfo { buttonText = "sticky platforms", method =() => Movement.StickyPlatforms(), toolTip = "spawns a shape of your choice under your hands and makes you stick to it"},
                //new ButtonInfo { buttonText = "trigger sticky platforms", method =() => Movement.TriggerStickyPlatforms(), toolTip = "makes platforms use trigger instead of grip and makes you stick to it"},
                new ButtonInfo { buttonText = "zero gravity", method =() => Movement.ZeroGravity(), toolTip = "makes it so you just float away"},
                new ButtonInfo { buttonText = "slippery hands", enableMethod =() => Movement.EnableSlipperyHands(), disableMethod =() => Movement.DisableSlipperyHands(), toolTip = "man it sure is cold out"},
                new ButtonInfo { buttonText = "grippy hands", enableMethod =() => Movement.EnableGrippyHands(), disableMethod =() => Movement.DisableGrippyHands(), toolTip = "man it sure is warm out"},
                new ButtonInfo { buttonText = "strafe", method =() => Movement.Strafe(), toolTip = "csgo in gorilla tag real"},
                new ButtonInfo { buttonText = "grip strafe", method =() => Movement.GripStrafe(), toolTip = "csgo in gorilla tag real but using grip"},
                new ButtonInfo { buttonText = "trigger strafe", method =() => Movement.TriggerStrafe(), toolTip = "csgo in gorilla tag real but using trigger"},
                new ButtonInfo { buttonText = "fly", method =() => Movement.Fly(), toolTip = "hold down grip to fly"},
                new ButtonInfo { buttonText = "trigger fly", method =() => Movement.TriggerFly(), toolTip = "think the normal but you use trigger"},
                new ButtonInfo { buttonText = "noclip fly", method =() => Movement.NoclipFly(), toolTip = "hold down grip to fly whilst phasing through stuff"},
                new ButtonInfo { buttonText = "trigger noclip fly", method =() => Movement.NoclipTriggerFly(), toolTip = "think the normal but you use trigger whilst phasing through stuff"},
                new ButtonInfo { buttonText = "noclip", enableMethod =() => Movement.Noclip(), disableMethod =() => Movement.DisableNoclip(), toolTip = "allows you to go through stuff"},
            },


            //           _              _ _                                                         _     
            // _ __ ___ (_)___  ___ ___| | | __ _ _ __   ___  ___  _   _ ___    _ __ ___   ___   __| |___ 
            //| '_ ` _ \| / __|/ __/ _ \ | |/ _` | '_ \ / _ \/ _ \| | | / __|  | '_ ` _ \ / _ \ / _` / __|
            //| | | | | | \__ \ (_|  __/ | | (_| | | | |  __/ (_) | |_| \__ \  | | | | | | (_) | (_| \__ \
            //|_| |_| |_|_|___/\___\___|_|_|\__,_|_| |_|\___|\___/ \__,_|___/  |_| |_| |_|\___/ \__,_|___/
            new ButtonInfo[] {
                new ButtonInfo { buttonText = "return to main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "go back to the main page of the menu"},
                new ButtonInfo { buttonText = "materialize snowball", method =() => Miscellaneous.materializesnowballballoon(), toolTip = "spawns a snowball in your right hand using grip"},
                new ButtonInfo { buttonText = "materialize water balloon", method =() => Miscellaneous.materializewaterballoon(), toolTip = "spawns a water balloon in your right hand using grip"},
                new ButtonInfo { buttonText = "materialize rock", method =() => Miscellaneous.materializerockballoon(), toolTip = "spawns a rock in your right hand using grip"},
                new ButtonInfo { buttonText = "materialize present", method =() => Miscellaneous.materializepresentballoon(), toolTip = "spawns a present in your right hand using grip"},
                new ButtonInfo { buttonText = "materialize mentos", method =() => Miscellaneous.materializementosballoon(), toolTip = "spawns mentos in your right hand using grip"},
                new ButtonInfo { buttonText = "materialize fish food", method =() => Miscellaneous.materializegishfoodballoon(), toolTip = "spawns fish food in your right hand using grip"},
                new ButtonInfo { buttonText = "silent hand taps", enableMethod =() => Miscellaneous.SilentHandTaps(), disableMethod =() => Miscellaneous.FixHandTaps(), toolTip = "makes your handtaps silent"},
                new ButtonInfo { buttonText = "loud hand taps", enableMethod =() => Miscellaneous.LoudHandTaps(), disableMethod =() => Miscellaneous.FixHandTaps(), toolTip = "makes your handtaps loud"},
                new ButtonInfo { buttonText = "instant hand taps", enableMethod =() => Miscellaneous.EnableInstantHandTaps(), disableMethod =() => Miscellaneous.DisableInstantHandTaps(), toolTip = "makes your handtaps go quick"},
            },


            //      _                             _
            // _ __(_) __ _   _ __ ___   ___   __| |___
            //| '__| |/ _` | | '_ ` _ \ / _ \ / _` / __|
            //| |  | | (_| | | | | | | | (_) | (_| \__ \
            //|_|  |_|\__, | |_| |_| |_|\___/ \__,_|___/
            //        |___/
            new ButtonInfo[] {
                new ButtonInfo { buttonText = "return to main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "go back to the main page of the menu"},
                new ButtonInfo { buttonText = "grab rig", method =() => Rig.GrabRig(), toolTip = "AHHHHHHHHHHHHH"},
                new ButtonInfo { buttonText = "rig gun", method =() => Rig.RigGun(), toolTip = "makes your rig go to where you point"},
                new ButtonInfo { buttonText = "head spin x", method =() => Rig.HeadSpinXAxis(), toolTip = "makes your head spin on the x axis"},
                new ButtonInfo { buttonText = "head spin y", method =() => Rig.HeadSpinYAxis(), toolTip = "makes your head spin on the y axis"},
                new ButtonInfo { buttonText = "head spin z", method =() => Rig.HeadSpinZAxis(), toolTip = "makes your head spin on the z axis"},
                new ButtonInfo { buttonText = "spaz rig", method =() => Rig.SpazRig(), toolTip = "makes your rig freak out"},
                new ButtonInfo { buttonText = "spaz hands", method =() => Rig.SpazHands(), toolTip = "makes your hands freak out"},
                new ButtonInfo { buttonText = "invisible monke", method =() => Rig.InvisibleMonke(), toolTip = "makes you invisible to other players"},
                new ButtonInfo { buttonText = "ghost monke", method =() => Rig.GhostMonke(), toolTip = "makes you invisible to other players"},
            },


            //            __      _                                _
            // ___  __ _ / _| ___| |_ _   _    _ __ ___   ___   __| |___
            /// __|/ _` | |_ / _ \ __| | | |  | '_ ` _ \ / _ \ / _` / __|
            //\__ \ (_| |  _|  __/ |_| |_| |  | | | | | | (_) | (_| \__ \
            //|___/\__,_|_|  \___|\__|\__, |  |_| |_| |_|\___/ \__,_|___/
            //                        |___/
            new ButtonInfo[] {
                new ButtonInfo { buttonText = "return to main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "go back to the main page of the menu"},
                new ButtonInfo { buttonText = "anti report disconnect", method =() => Safety.AntiReportDisconnect(), toolTip = "disconnects you when someone tries reporting you"},
                new ButtonInfo { buttonText = "anti report reconnect", method =() => Safety.AntiReportReconnect(), toolTip = "reconnects you when someone tries reporting you"},
                new ButtonInfo { buttonText = "disable network triggers", enableMethod =() => Safety.EnableNetworkTriggers(), disableMethod =() => Safety.DisableNetworkTriggers(), toolTip = "makes it so you dont join a new room when you go to a different area"},
            },


            new ButtonInfo[] {
                new ButtonInfo { buttonText = "return to main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "go back to the main page of the menu"},
                new ButtonInfo { buttonText = "full bright", method =() => Visual.EnableFullBright(), toolTip = "disables the lighting"},
                new ButtonInfo { buttonText = "set time to night", method =() => Visual.SetNight(), toolTip = "sets the time to night"},
                new ButtonInfo { buttonText = "set time to afternoon", method =() => Visual.SetAfternoon(), toolTip = "sets the time to afternoon"},
                new ButtonInfo { buttonText = "set time to day", method =() => Visual.SetDay(), toolTip = "sets the time to day"},
                new ButtonInfo { buttonText = "casual mode chams", enableMethod =() => Visual.CasualModeChams(), disableMethod =() => Visual.DisableChams(), toolTip = "allows you to see monkes from anywhere by making them appear through walls"},
                new ButtonInfo { buttonText = "casual mode tracers", method =() => Visual.CasualModeTracers(), toolTip = "makes lines from your hands go to monkes in your lobby"},
            },

            new ButtonInfo[]
            {
                new ButtonInfo { buttonText = "disconnect", method =() => Main.disconnect(), isTogglable = false, toolTip = "fuck this lobby"},
            }
        };
    }
}
