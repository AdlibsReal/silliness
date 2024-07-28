using BepInEx;
using ExitGames.Client.Photon;
using GorillaTag.GuidedRefs;
using Photon.Pun;
using Photon.Realtime;
using PlayFab.ClientModels;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using static silliness.Classes.RigManager;
using static silliness.Menu.Main;
using static silliness.Menu.Settings;
using static silliness.Mods.Global;
using static UnityEngine.Object;
using static UnityEngine.UI.CanvasScaler;
using silliness.Classes;
using silliness.Notifications;
using Oculus.Platform;
using UnityEngine.Animations.Rigging;

namespace silliness.Mods
{
    internal class Visual
    {
        public static void EnableFullBright()
        {
            LightmapSettings.lightmaps = null;
        }
        public static void SetNight()
        {
            BetterDayNightManager.instance.SetTimeOfDay(0);
        }

        public static void SetAfternoon()
        {
            BetterDayNightManager.instance.SetTimeOfDay(1);
        }

        public static void SetDay()
        {
            BetterDayNightManager.instance.SetTimeOfDay(3);
        }
        public static void CasualModeChams()
        {
            foreach (VRRig stupid in GorillaParent.instance.vrrigs)
            {
                if (stupid != GorillaTagger.Instance.offlineVRRig)
                {
                    stupid.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                    stupid.mainSkin.material.color = stupid.playerColor;
                }
            }
        }
        public static void DisableChams()
        {
            foreach (VRRig stupid in GorillaParent.instance.vrrigs)
            {
                if (stupid != GorillaTagger.Instance.offlineVRRig)
                {
                    stupid.mainSkin.material.shader = Shader.Find("GorillaTag/UberShader");
                }
            }
        }
        public static void CasualModeTracers()
        {
            foreach (VRRig stupid in GorillaParent.instance.vrrigs)
            {
                if (stupid != GorillaTagger.Instance.offlineVRRig)
                {
                        GameObject line = new GameObject("Line");
                        LineRenderer PointerLine = line.AddComponent<LineRenderer>();
                        PointerLine.startWidth = 0.015f; PointerLine.endWidth = 0.015f; PointerLine.positionCount = 2; PointerLine.useWorldSpace = true;
                        PointerLine.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                        PointerLine.SetPosition(1, stupid.transform.position);
                        PointerLine.material.shader = Shader.Find("GUI/Text Shader");
                        PointerLine.material.color = stupid.playerColor;
                        UnityEngine.Object.Destroy(line, Time.deltaTime);
                }
            }
        }
    }
}
