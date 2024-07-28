using BepInEx;
using ExitGames.Client.Photon;
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

namespace silliness.Mods
{
    internal class Rig
    {
        public static void GrabRig()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
                GorillaTagger.Instance.offlineVRRig.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void RigGun()
        {
            RaycastHit PointerPos;
            GameObject Pointer;
            GameObject line = new GameObject("Line");
            LineRenderer PointerLine = line.AddComponent<LineRenderer>();
            Physics.Raycast(GorillaLocomotion.Player.Instance.rightControllerTransform.position, GorillaLocomotion.Player.Instance.rightControllerTransform.forward, out PointerPos);
            if (ControllerInputPoller.instance.rightGrab)
            {
                Pointer = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                Pointer.transform.position = PointerPos.point;
                Pointer.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
                Destroy(Pointer.GetComponent<Collider>());
                Destroy(Pointer.GetComponent<Rigidbody>());
                Pointer.GetComponent<Renderer>().material.shader = Shader.Find("GorillaTag/UberShader");
                Destroy(Pointer, Time.deltaTime);

                ColorChanger colorChanger = Pointer.AddComponent<ColorChanger>();
                colorChanger.colorInfo = backgroundColor;
                colorChanger.Start();

                PointerLine.startWidth = 0.025f; PointerLine.endWidth = 0.025f; PointerLine.positionCount = 2; PointerLine.useWorldSpace = true;
                PointerLine.SetPosition(0, GorillaLocomotion.Player.Instance.rightControllerTransform.position);
                PointerLine.SetPosition(1, Pointer.transform.localPosition);
                PointerLine.material.shader = Shader.Find("GUI/Text Shader");
                Destroy(line, Time.deltaTime);

                colorChanger = PointerLine.AddComponent<ColorChanger>();
                colorChanger.colorInfo = backgroundColor;
                colorChanger.Start();

                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = Pointer.transform.position - new Vector3(-0.1f, -1f, 0f);
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
        public static void HeadSpinZAxis()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z += 10f;
        }
        public static void HeadSpinXAxis()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x += 10f;
        }
        public static void HeadSpinYAxis()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y += 10f;
        }
        public static Vector3 stupid1 = Vector3.zero;
        public static Vector3 stupid2 = Vector3.zero;
        public static Vector3 stupid3 = Vector3.zero;
        public static void SpazRig()
        {
            stupid1 = GorillaTagger.Instance.offlineVRRig.leftHand.trackingPositionOffset;
            stupid2 = GorillaTagger.Instance.offlineVRRig.rightHand.trackingPositionOffset;
            stupid3 = GorillaTagger.Instance.offlineVRRig.head.trackingPositionOffset;
            float spaz = 0.1f;
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaTagger.Instance.offlineVRRig.leftHand.trackingPositionOffset = stupid1 + new Vector3(UnityEngine.Random.Range(-spaz, spaz), UnityEngine.Random.Range(-spaz, spaz), UnityEngine.Random.Range(-spaz, spaz));
                GorillaTagger.Instance.offlineVRRig.rightHand.trackingPositionOffset = stupid2 + new Vector3(UnityEngine.Random.Range(-spaz, spaz), UnityEngine.Random.Range(-spaz, spaz), UnityEngine.Random.Range(-spaz, spaz));
                GorillaTagger.Instance.offlineVRRig.head.trackingPositionOffset = stupid3 + new Vector3(UnityEngine.Random.Range(-spaz, spaz), UnityEngine.Random.Range(-spaz, spaz), UnityEngine.Random.Range(-spaz, spaz));

            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.leftHand.trackingPositionOffset = stupid1;
                GorillaTagger.Instance.offlineVRRig.rightHand.trackingPositionOffset = stupid2;
                GorillaTagger.Instance.offlineVRRig.head.trackingPositionOffset = stupid3;
            }
        }
        public static void SpazHands()
        {
            stupid1 = GorillaTagger.Instance.offlineVRRig.leftHand.trackingPositionOffset;
            stupid2 = GorillaTagger.Instance.offlineVRRig.rightHand.trackingPositionOffset;
            float spaz = 0.1f;
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaTagger.Instance.offlineVRRig.leftHand.trackingPositionOffset = stupid1 + new Vector3(UnityEngine.Random.Range(-spaz, spaz), UnityEngine.Random.Range(-spaz, spaz), UnityEngine.Random.Range(-spaz, spaz));
                GorillaTagger.Instance.offlineVRRig.rightHand.trackingPositionOffset = stupid2 + new Vector3(UnityEngine.Random.Range(-spaz, spaz), UnityEngine.Random.Range(-spaz, spaz), UnityEngine.Random.Range(-spaz, spaz));

            }
            else
            {
                stupid1 = GorillaTagger.Instance.offlineVRRig.leftHand.trackingPositionOffset = new Vector3(spaz, 0f);
                stupid2 = GorillaTagger.Instance.offlineVRRig.rightHand.trackingPositionOffset = new Vector3(spaz, 0f);
                GorillaTagger.Instance.offlineVRRig.leftHand.trackingPositionOffset = stupid1;
                GorillaTagger.Instance.offlineVRRig.rightHand.trackingPositionOffset = stupid2;
            }
        }
        public static void SpazHead()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x = UnityEngine.Random.Range(0f, 360f);
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = UnityEngine.Random.Range(0f, 360f);
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = UnityEngine.Random.Range(0f, 360f);
        }
        public static void FreezeRig()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaTagger.Instance.offlineVRRig.headBodyOffset = new Vector3(999999f, 999999f, 999999f);
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.headBodyOffset = new Vector3(0f, 0f, 0f);
            }
        }
        public static void GhostMonke()
        {
            if (ControllerInputPoller.instance.rightGrab)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }
    }
}
