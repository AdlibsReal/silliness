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

namespace silliness.Mods
{
    internal class Movement
    {
        public static void WASDFly()
        {
            GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0.06f, 0f);

            if (UnityInput.Current.GetKey(KeyCode.LeftControl))
            {
                GorillaTagger.Instance.rigidbody.transform.position += GorillaTagger.Instance.rigidbody.transform.up * Time.deltaTime * -5f;
            }
            if (UnityInput.Current.GetKey(KeyCode.Space))
            {
                GorillaTagger.Instance.rigidbody.transform.position += GorillaTagger.Instance.rigidbody.transform.up * Time.deltaTime * 5f;
            }
            if (UnityInput.Current.GetKey(KeyCode.W))
            {
                GorillaTagger.Instance.rigidbody.transform.position += GorillaTagger.Instance.rigidbody.transform.right * Time.deltaTime * -5f;
            }
            if (UnityInput.Current.GetKey(KeyCode.A))
            {
                GorillaTagger.Instance.rigidbody.transform.position += GorillaTagger.Instance.rigidbody.transform.forward * Time.deltaTime * -5f;
            }
            if (UnityInput.Current.GetKey(KeyCode.S))
            {
                GorillaTagger.Instance.rigidbody.transform.position += GorillaTagger.Instance.rigidbody.transform.right * Time.deltaTime * 5f;
            }
            if (UnityInput.Current.GetKey(KeyCode.D))
            {
                GorillaTagger.Instance.rigidbody.transform.position += GorillaTagger.Instance.rigidbody.transform.forward * Time.deltaTime * 5f;
            }
        }
        public static GameObject LeftPlatform;
        public static GameObject RightPlatform;
        public static GameObject LeftPlatformBorder;
        public static GameObject RightPlatformBorder;
        public static void StickyPlatforms()
        {
            if (leftGrab)
            {
                if (LeftPlatform == null)
                {
                    LeftPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    LeftPlatform.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    LeftPlatform.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                    LeftPlatform.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;

                    ColorChanger colorChanger = LeftPlatform.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = backgroundColor;
                    colorChanger.Start();

                    LeftPlatformBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    LeftPlatformBorder.transform.localScale = new Vector3(0.024f, 0.315f, 0.415f);
                    LeftPlatformBorder.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                    LeftPlatformBorder.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;

                    colorChanger = LeftPlatformBorder.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = mainBorderColors;
                    colorChanger.Start();
                }
            }
            else
            {
                if (LeftPlatform != null)
                {

                    Destroy(LeftPlatform);
                    Destroy(LeftPlatformBorder);
                }
            }
            if (rightGrab)
            {
                if (RightPlatform == null)
                {
                    RightPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    RightPlatform.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    RightPlatform.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    RightPlatform.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;

                    ColorChanger colorChanger = RightPlatform.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = backgroundColor;
                    colorChanger.Start();

                    RightPlatformBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    RightPlatformBorder.transform.localScale = new Vector3(0.024f, 0.315f, 0.415f);
                    RightPlatformBorder.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    RightPlatformBorder.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;

                    colorChanger = RightPlatformBorder.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = mainBorderColors;
                    colorChanger.Start();
                }
            }
            else
            {
                if (RightPlatform != null)
                {
                    Destroy(RightPlatform);
                    Destroy(RightPlatformBorder);
                }
            }
        }
        public static void TriggerStickyPlatforms()
        {
            bool leftTriggerBool = leftGrab;
            bool rightTriggerBool = rightGrab;
            leftGrab = leftTrigger > 0.5f;
            rightGrab = rightTrigger > 0.5f;
            StickyPlatforms();
            leftGrab = leftTriggerBool;
            rightGrab = rightTriggerBool;
        }
        public static void Platforms()
        {
            if (leftGrab)
            {
                if (LeftPlatform == null)
                {
                    LeftPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    LeftPlatform.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    LeftPlatform.transform.position = GorillaTagger.Instance.leftHandTransform.position - new Vector3(0f, 0.05f, 0f);
                    LeftPlatform.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;

                    ColorChanger colorChanger = LeftPlatform.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = backgroundColor;
                    colorChanger.Start();

                    LeftPlatformBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    LeftPlatformBorder.transform.localScale = new Vector3(0.024f, 0.315f, 0.415f);
                    LeftPlatformBorder.transform.position = GorillaTagger.Instance.leftHandTransform.position - new Vector3(0f, 0.05f, 0f);
                    LeftPlatformBorder.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;

                    colorChanger = LeftPlatformBorder.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = mainBorderColors;
                    colorChanger.Start();
                }
            }
            else
            {
                if (LeftPlatform != null)
                {

                    Destroy(LeftPlatform);
                    Destroy(LeftPlatformBorder);
                }
            }
            if (rightGrab)
            {
                if (RightPlatform == null)
                {
                    RightPlatform = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    RightPlatform.transform.localScale = new Vector3(0.025f, 0.3f, 0.4f);
                    RightPlatform.transform.position = GorillaTagger.Instance.rightHandTransform.position - new Vector3(0f, 0.05f, 0f);
                    RightPlatform.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;

                    ColorChanger colorChanger = RightPlatform.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = backgroundColor;
                    colorChanger.Start();

                    RightPlatformBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    RightPlatformBorder.transform.localScale = new Vector3(0.024f, 0.315f, 0.415f);
                    RightPlatformBorder.transform.position = GorillaTagger.Instance.rightHandTransform.position - new Vector3(0f, 0.05f, 0f);
                    RightPlatformBorder.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;

                    colorChanger = RightPlatformBorder.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = mainBorderColors;
                    colorChanger.Start();
                }
            }
            else
            {
                if (RightPlatform != null)
                {
                    Destroy(RightPlatform);
                    Destroy(RightPlatformBorder);
                }
            }
        }
        public static void TriggerPlatforms()
        {
            bool leftTriggerBool = leftGrab;
            bool rightTriggerBool = rightGrab;
            leftGrab = leftTrigger > 0.5f;
            rightGrab = rightTrigger > 0.5f;
            Platforms();
            leftGrab = leftTriggerBool;
            rightGrab = rightTriggerBool;
        }
        public static void Speedboost()
        {
            GorillaLocomotion.Player.Instance.jumpMultiplier = 15;
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 15;
        }
        public static void ZeroGravity()
        {
            GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity -= Vector3.down * 9.81f * Time.deltaTime;
        }
        public static void EnableSlipperyHands()
        {
            EverythingSlippery = true;
        }
        public static void DisableSlipperyHands()
        {
            EverythingSlippery = false;
        }
        public static void EnableGrippyHands()
        {
            EverythingGrippy = true;
        }
        public static void DisableGrippyHands()
        {
            EverythingGrippy = false;
        }
        public static void Strafe()
        {
            Vector3 stupiddumbidiot = GorillaTagger.Instance.bodyCollider.transform.forward * GorillaLocomotion.Player.Instance.maxJumpSpeed;
            GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity = new Vector3(stupiddumbidiot.x, GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity.y, stupiddumbidiot.z);
        }
        public static void GripStrafe()
        {
            if (rightGrab)
            {
                Vector3 stupiddumbidiot = GorillaTagger.Instance.bodyCollider.transform.forward * GorillaLocomotion.Player.Instance.maxJumpSpeed;
                GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity = new Vector3(stupiddumbidiot.x, GorillaTagger.Instance.bodyCollider.attachedRigidbody.velocity.y, stupiddumbidiot.z);
            }
        }
        public static void Fly()
        {
            if (rightGrab)
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * 15f;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
        public static void TriggerFly()
        {
            if (rightTrigger > 0.5f)
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * 15f;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }
    }
}
