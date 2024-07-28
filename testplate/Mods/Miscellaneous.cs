using BepInEx;
using ExitGames.Client.Photon;
using GorillaNetworking;
using GorillaTag.GuidedRefs;
using Photon.Pun;
using Photon.Realtime;
using PlayFab.ClientModels;
using silliness.Classes;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using static silliness.Classes.RigManager;
using static silliness.Menu.Main;
using static silliness.Mods.Global;
using static UnityEngine.Object;
using static UnityEngine.UI.CanvasScaler;

namespace silliness.Mods
{
    internal class Miscellaneous
    {
        public static bool lastthing = false;
        public static bool lastthing2 = false;
        public static void materializesnowballballoon()
        {
            bool thing = rightGrab;
            if (thing && !lastthing)
            {
                ControllerInputPoller.instance.rightControllerGripFloat = 1f;
                GameObject lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(lhelp, 0.1f);
                lhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                lhelp.transform.position = GorillaTagger.Instance.rightHandTransform.position + new Vector3(0f, 0.1f, 0f);
                lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 32;
                lhelp.GetComponent<Renderer>().enabled = false;

                lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(lhelp, 0.1f);
                lhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                lhelp.transform.position = GorillaTagger.Instance.rightHandTransform.position - new Vector3(0f, 0.1f, 0f);
                lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 32;
                lhelp.GetComponent<Renderer>().enabled = false;

                GorillaTagger.Instance.rightHandTransform.position += new Vector3(0f, 0.1f, 0f);
            }
            lastthing = thing;
            bool thing2 = leftGrab;
            if (thing2 && !lastthing2)
            {
                ControllerInputPoller.instance.leftControllerGripFloat = 1f;
                GameObject rhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(rhelp, 0.1f);
                rhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                rhelp.transform.position = GorillaTagger.Instance.leftHandTransform.position + new Vector3(0f, 0.1f, 0f);
                rhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 32;
                rhelp.GetComponent<Renderer>().enabled = false;

                rhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(rhelp, 0.1f);
                rhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                rhelp.transform.position = GorillaTagger.Instance.leftHandTransform.position - new Vector3(0f, 0.1f, 0f);
                rhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 32;
                rhelp.GetComponent<Renderer>().enabled = false;

                GorillaTagger.Instance.leftHandTransform.position += new Vector3(0f, 0.1f, 0f);
            }
            lastthing2 = thing2;
        }
        public static void materializewaterballoon()
        {
            bool thing = rightGrab;
            if (thing && !lastthing)
            {
                ControllerInputPoller.instance.rightControllerGripFloat = 1f;
                GameObject lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(lhelp, 0.1f);
                lhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                lhelp.transform.position = GorillaTagger.Instance.rightHandTransform.position + new Vector3(0f, 0.1f, 0f);
                lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 204;
                lhelp.GetComponent<Renderer>().enabled = false;

                lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(lhelp, 0.1f);
                lhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                lhelp.transform.position = GorillaTagger.Instance.rightHandTransform.position - new Vector3(0f, 0.1f, 0f);
                lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 204;
                lhelp.GetComponent<Renderer>().enabled = false;

                GorillaTagger.Instance.rightHandTransform.position += new Vector3(0f, 0.1f, 0f);
            }
            lastthing = thing;
            bool thing2 = leftGrab;
            if (thing2 && !lastthing2)
            {
                ControllerInputPoller.instance.leftControllerGripFloat = 1f;
                GameObject rhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(rhelp, 0.1f);
                rhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                rhelp.transform.position = GorillaTagger.Instance.leftHandTransform.position + new Vector3(0f, 0.1f, 0f);
                rhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 204;
                rhelp.GetComponent<Renderer>().enabled = false;

                rhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(rhelp, 0.1f);
                rhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                rhelp.transform.position = GorillaTagger.Instance.leftHandTransform.position - new Vector3(0f, 0.1f, 0f);
                rhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 204;
                rhelp.GetComponent<Renderer>().enabled = false;

                GorillaTagger.Instance.leftHandTransform.position += new Vector3(0f, 0.1f, 0f);
            }
            lastthing2 = thing2;
        }
        public static void materializerockballoon()
        {
            bool thing = rightGrab;
            if (thing && !lastthing)
            {
                ControllerInputPoller.instance.rightControllerGripFloat = 1f;
                GameObject lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(lhelp, 0.1f);
                lhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                lhelp.transform.position = GorillaTagger.Instance.rightHandTransform.position + new Vector3(0f, 0.1f, 0f);
                lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 231;
                lhelp.GetComponent<Renderer>().enabled = false;

                lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(lhelp, 0.1f);
                lhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                lhelp.transform.position = GorillaTagger.Instance.rightHandTransform.position - new Vector3(0f, 0.1f, 0f);
                lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 231;
                lhelp.GetComponent<Renderer>().enabled = false;

                GorillaTagger.Instance.rightHandTransform.position += new Vector3(0f, 0.1f, 0f);
            }
            lastthing = thing;
            bool thing2 = leftGrab;
            if (thing2 && !lastthing2)
            {
                ControllerInputPoller.instance.leftControllerGripFloat = 1f;
                GameObject rhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(rhelp, 0.1f);
                rhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                rhelp.transform.position = GorillaTagger.Instance.leftHandTransform.position + new Vector3(0f, 0.1f, 0f);
                rhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 231;
                rhelp.GetComponent<Renderer>().enabled = false;

                rhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(rhelp, 0.1f);
                rhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                rhelp.transform.position = GorillaTagger.Instance.leftHandTransform.position - new Vector3(0f, 0.1f, 0f);
                rhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 231;
                rhelp.GetComponent<Renderer>().enabled = false;

                GorillaTagger.Instance.leftHandTransform.position += new Vector3(0f, 0.1f, 0f);
            }
            lastthing2 = thing2;
        }
        public static void materializepresentballoon()
        {
            bool thing = rightGrab;
            if (thing && !lastthing)
            {
                ControllerInputPoller.instance.rightControllerGripFloat = 1f;
                GameObject lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(lhelp, 0.1f);
                lhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                lhelp.transform.position = GorillaTagger.Instance.rightHandTransform.position + new Vector3(0f, 0.1f, 0f);
                lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 240;
                lhelp.GetComponent<Renderer>().enabled = false;

                lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(lhelp, 0.1f);
                lhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                lhelp.transform.position = GorillaTagger.Instance.rightHandTransform.position - new Vector3(0f, 0.1f, 0f);
                lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 240;
                lhelp.GetComponent<Renderer>().enabled = false;

                GorillaTagger.Instance.rightHandTransform.position += new Vector3(0f, 0.1f, 0f);
            }
            lastthing = thing;
            bool thing2 = leftGrab;
            if (thing2 && !lastthing2)
            {
                ControllerInputPoller.instance.leftControllerGripFloat = 1f;
                GameObject rhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(rhelp, 0.1f);
                rhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                rhelp.transform.position = GorillaTagger.Instance.leftHandTransform.position + new Vector3(0f, 0.1f, 0f);
                rhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 240;
                rhelp.GetComponent<Renderer>().enabled = false;

                rhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(rhelp, 0.1f);
                rhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                rhelp.transform.position = GorillaTagger.Instance.leftHandTransform.position - new Vector3(0f, 0.1f, 0f);
                rhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 240;
                rhelp.GetComponent<Renderer>().enabled = false;

                GorillaTagger.Instance.leftHandTransform.position += new Vector3(0f, 0.1f, 0f);
            }
            lastthing2 = thing2;
        }
        public static void materializementosballoon()
        {
            bool thing = rightGrab;
            if (thing && !lastthing)
            {
                ControllerInputPoller.instance.rightControllerGripFloat = 1f;
                GameObject lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(lhelp, 0.1f);
                lhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                lhelp.transform.position = GorillaTagger.Instance.rightHandTransform.position + new Vector3(0f, 0.1f, 0f);
                lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 249;
                lhelp.GetComponent<Renderer>().enabled = false;

                lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(lhelp, 0.1f);
                lhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                lhelp.transform.position = GorillaTagger.Instance.rightHandTransform.position - new Vector3(0f, 0.1f, 0f);
                lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 249;
                lhelp.GetComponent<Renderer>().enabled = false;

                GorillaTagger.Instance.rightHandTransform.position += new Vector3(0f, 0.1f, 0f);
            }
            lastthing = thing;
            bool thing2 = leftGrab;
            if (thing2 && !lastthing2)
            {
                ControllerInputPoller.instance.leftControllerGripFloat = 1f;
                GameObject rhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(rhelp, 0.1f);
                rhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                rhelp.transform.position = GorillaTagger.Instance.leftHandTransform.position + new Vector3(0f, 0.1f, 0f);
                rhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 249;
                rhelp.GetComponent<Renderer>().enabled = false;

                rhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(rhelp, 0.1f);
                rhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                rhelp.transform.position = GorillaTagger.Instance.leftHandTransform.position - new Vector3(0f, 0.1f, 0f);
                rhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 249;
                rhelp.GetComponent<Renderer>().enabled = false;

                GorillaTagger.Instance.leftHandTransform.position += new Vector3(0f, 0.1f, 0f);
            }
            lastthing2 = thing2;
        }
        public static void materializegishfoodballoon()
        {
            bool thing = rightGrab;
            if (thing && !lastthing)
            {
                ControllerInputPoller.instance.rightControllerGripFloat = 1f;
                GameObject lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(lhelp, 0.1f);
                lhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                lhelp.transform.position = GorillaTagger.Instance.rightHandTransform.position + new Vector3(0f, 0.1f, 0f);
                lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 252;
                lhelp.GetComponent<Renderer>().enabled = false;

                lhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(lhelp, 0.1f);
                lhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                lhelp.transform.position = GorillaTagger.Instance.rightHandTransform.position - new Vector3(0f, 0.1f, 0f);
                lhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 252;
                lhelp.GetComponent<Renderer>().enabled = false;

                GorillaTagger.Instance.rightHandTransform.position += new Vector3(0f, 0.1f, 0f);
            }
            lastthing = thing;
            bool thing2 = leftGrab;
            if (thing2 && !lastthing2)
            {
                ControllerInputPoller.instance.leftControllerGripFloat = 1f;
                GameObject rhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(rhelp, 0.1f);
                rhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                rhelp.transform.position = GorillaTagger.Instance.leftHandTransform.position + new Vector3(0f, 0.1f, 0f);
                rhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 252;
                rhelp.GetComponent<Renderer>().enabled = false;

                rhelp = GameObject.CreatePrimitive(PrimitiveType.Cube);
                UnityEngine.Object.Destroy(rhelp, 0.1f);
                rhelp.transform.localScale = new Vector3(0.25f, 0.05f, 0.25f);
                rhelp.transform.position = GorillaTagger.Instance.leftHandTransform.position - new Vector3(0f, 0.1f, 0f);
                rhelp.AddComponent<GorillaSurfaceOverride>().overrideIndex = 252;
                rhelp.GetComponent<Renderer>().enabled = false;

                GorillaTagger.Instance.leftHandTransform.position += new Vector3(0f, 0.1f, 0f);
            }
            lastthing2 = thing2;
        }
        public static void FixHandTaps()
        {
            GorillaTagger.Instance.handTapVolume = 0.1f;
        }

        public static void LoudHandTaps()
        {
            GorillaTagger.Instance.handTapVolume = int.MaxValue;
        }

        public static void SilentHandTaps()
        {
            GorillaTagger.Instance.handTapVolume = 0;
        }

        public static void EnableInstantHandTaps()
        {
            GorillaTagger.Instance.tapCoolDown = 0f;
        }

        public static void DisableInstantHandTaps()
        {
            GorillaTagger.Instance.tapCoolDown = 0.33f;
        }
        public static void jointheroom()
        {
            PhotonNetworkController.Instance.AttemptToJoinSpecificRoom("<$II_4.3.0>", JoinType.Solo);
        }//<$II_4.2.0>
        public static void SolidPlayers()
        {

        }
    }
}
