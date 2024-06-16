using BepInEx;
using HarmonyLib;
using Photon.Pun;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR;
using static silliness.Menu.Buttons;
using static silliness.Menu.Settings;
using static silliness.Mods.SettingsMods;
using System.Reflection;
using silliness.Classes;
using silliness;
using silliness.Notifications;
using silliness.Mods;

namespace silliness.Menu
{
    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("LateUpdate", MethodType.Normal)]
    public class Main : MonoBehaviour
    {
        // Constant
        public static void Prefix()
        {
            // Initialize Menu
            try
            {
                bool toOpen = !rightHanded && ControllerInputPoller.instance.leftControllerSecondaryButton || rightHanded && ControllerInputPoller.instance.rightControllerSecondaryButton;
                bool keyboardOpen = UnityInput.Current.GetKey(keyboardButton);

                if (menu == null)
                {
                    if (toOpen || keyboardOpen)
                    {
                        CreateMenu();
                        RecenterMenu(rightHanded, keyboardOpen);
                        if (reference == null)
                        {
                            CreateReference(rightHanded);
                        }
                    }
                }
                else
                {
                    if (toOpen || keyboardOpen)
                    {
                        RecenterMenu(rightHanded, keyboardOpen);
                    }
                    else
                    {
                        Rigidbody comp = menu.AddComponent(typeof(Rigidbody)) as Rigidbody;
                        if (rightHanded)
                        {
                            comp.velocity = GorillaLocomotion.Player.Instance.rightHandCenterVelocityTracker.GetAverageVelocity(true, 0);
                        }
                        else
                        {
                            comp.velocity = GorillaLocomotion.Player.Instance.leftHandCenterVelocityTracker.GetAverageVelocity(true, 0);
                        }

                        Destroy(menu, 2);
                        menu = null;

                        Destroy(reference);
                        reference = null;
                    }
                }
            }
            catch (Exception exc)
            {
                Debug.LogError(string.Format("{0} // Error initializing at {1}: {2}", PluginInfo.Name, exc.StackTrace, exc.Message));
            }

            // Constant
            try
            {
                rightPrimary = ControllerInputPoller.instance.rightControllerPrimaryButton;
                rightSecondary = ControllerInputPoller.instance.rightControllerSecondaryButton;
                leftPrimary = ControllerInputPoller.instance.leftControllerPrimaryButton;
                leftSecondary = ControllerInputPoller.instance.leftControllerSecondaryButton;
                leftGrab = ControllerInputPoller.instance.leftGrab;
                rightGrab = ControllerInputPoller.instance.rightGrab;
                leftTrigger = ControllerInputPoller.TriggerFloat(XRNode.LeftHand);
                rightTrigger = ControllerInputPoller.TriggerFloat(XRNode.RightHand);
                // Pre-Execution
                if (fpsObject != null)
                {
                    fpsObject.text = "FPS: " + Mathf.Ceil(1f / Time.unscaledDeltaTime).ToString();
                }

                // Execute Enabled mods
                foreach (ButtonInfo[] buttonlist in buttons)
                {
                    foreach (ButtonInfo v in buttonlist)
                    {
                        if (v.enabled)
                        {
                            if (v.method != null)
                            {
                                try
                                {
                                    v.method.Invoke();
                                }
                                catch (Exception exc)
                                {
                                    Debug.LogError(string.Format("{0} // Error with mod {1} at {2}: {3}", PluginInfo.Name, v.buttonText, exc.StackTrace, exc.Message));
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                Debug.LogError(string.Format("{0} // Error with executing mods at {1}: {2}", PluginInfo.Name, exc.StackTrace, exc.Message));
            }
            if (!HasLoaded)
            {
                HasLoaded = true;
                OnLaunch();
            }
            try
            {
                GameObject.Find("motdtext").GetComponent<Text>().text = "HEY THANKS FOR USING <color=magenta>" + PluginInfo.Name + " V:" + PluginInfo.Version + "</color> THE SILLIEST MENU IN THE WORLD!\n\nTHIS MENU IS NOT FOR OP SHIT, WE ONLY FOCUS ON THE SILLY SIDE OF THINGS";
                GameObject.Find("COC Text").GetComponent<Text>().text = "PLEASE KEEP IN MIND THIS IS AN ILLEGAL MOD MENU, BEING SILLY COMES AT A PRICE PEOPLE. I AM NOT RESPONSIBLE FOR ANY BANS YOU GET, WE HAVE ANTI REPORT FOR A REASON (ANTI REPORT IN SAFETY MODS)";//COC writing
                GameObject.Find("CodeOfConduct").GetComponent<Text>().text = "SILLINESS";//COC Title
            }
            catch { }
        }

        public static int themeNumber = 1;
        public static void ChangeTheme()
        {
            if (themeNumber == 1)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [pink]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(new Color(0.996f, 0.486f, 0.890f)) };
                mainBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(1f, 0.584f, 0.918f)) };
                buttonBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(1f, 0.584f, 0.918f)) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(0.996f, 0.486f, 0.890f)) }, // Disabled
                    new ExtGradient{colors = GetSolidGradient(new Color(0.859f, 0.416f, 0.773f))} // Enabled
                };
                textColors = new Color[]
                {
                    Color.white, // Disabled
                    Color.white // Enabled
                };
            }
            if (themeNumber == 2)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [dark]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(new Color(0.05f, 0.05f, 0.05f)) };
                mainBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(0.643f, 0.306f, 1f)) };
                buttonBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(0.643f, 0.306f, 1f)) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(0.05f, 0.05f, 0.05f)) }, // Disabled
                    new ExtGradient{colors = GetSolidGradient(new Color(0.15f, 0.15f, 0.15f)) }, // Enabled
                };
                textColors = new Color[]
                {
                    Color.white, // Disabled
                    Color.white // Enabled
                };
            }
            if (themeNumber == 3)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [blue]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(new Color(0.357f, 0.349f, 1f)) };
                mainBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(0.518f, 0.51f, 1f)) };
                buttonBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(0.518f, 0.51f, 1f)) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(0.357f, 0.349f, 1f)) }, // Disabled
                    new ExtGradient{colors = GetSolidGradient(new Color(0.306f, 0.298f, 0.851f)) }, // Enabled
                };
                textColors = new Color[]
                {
                    Color.white, // Disabled
                    Color.white // Enabled
                };
            }
            if (themeNumber == 4)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [orange]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(new Color(1f, 0.58f, 0.271f)) };
                mainBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(1f, 0.671f, 0.439f)) };
                buttonBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(1f, 0.671f, 0.439f)) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(1f, 0.58f, 0.271f)) }, // Disabled
                    new ExtGradient{colors = GetSolidGradient(new Color(0.839f, 0.482f, 0.224f)) }, // Enabled
                };
                textColors = new Color[]
                {
                    Color.white, // Disabled
                    Color.white // Enabled
                };
            }
            if (themeNumber == 5)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [purple]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(new Color(0.702f, 0.357f, 1f)) };
                mainBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(0.753f, 0.471f, 1f)) };
                buttonBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(0.753f, 0.471f, 1f)) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(0.702f, 0.357f, 1f)) }, // Disabled
                    new ExtGradient{colors = GetSolidGradient(new Color(0.561f, 0.286f, 0.8f)) }, // Enabled
                };
                textColors = new Color[]
                {
                    Color.white, // Disabled
                    Color.white // Enabled
                };
            }
            if (themeNumber == 6)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [red]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(new Color(1f, 0.271f, 0.271f)) };
                mainBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(1f, 0.467f, 0.467f)) };
                buttonBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(1f, 0.467f, 0.467f)) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(1f, 0.271f, 0.271f)) }, // Disabled
                    new ExtGradient{colors = GetSolidGradient(new Color(0.82f, 0.282f, 0.282f)) }, // Enabled
                };
                textColors = new Color[]
                {
                    Color.white, // Disabled
                    Color.white // Enabled
                };
            }
            if (themeNumber == 7)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [green]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(new Color(0.416f, 1f, 0.416f)) };
                mainBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(0.329f, 0.859f, 0.329f)) };
                buttonBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(0.329f, 0.859f, 0.329f)) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(0.416f, 1f, 0.416f)) }, // Disabled
                    new ExtGradient{colors = GetSolidGradient(new Color(0.337f, 0.812f, 0.337f)) }, // Enabled
                };
                textColors = new Color[]
                {
                    Color.black, // Disabled
                    Color.black // Enabled
                };
            }
            if (themeNumber == 8)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [femboy 1]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(Color.white) };
                mainBorderColors = new ExtGradient { colors = GetSolidGradient(Color.black) };
                buttonBorderColors = new ExtGradient { colors = GetSolidGradient(Color.black) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(0.996f, 0.62f, 0.624f)) }, // Disabled
                    new ExtGradient{colors = GetSolidGradient(Color.white) }, // Enabled
                };
                textColors = new Color[]
                {
                    Color.black, // Disabled
                    new Color(0.851f, 0.851f, 0.851f) // Enabled
                };
            }
            if (themeNumber == 9)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [femboy 2]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(Color.white) };
                mainBorderColors = new ExtGradient { colors = GetSolidGradient(Color.black) };
                buttonBorderColors = new ExtGradient { colors = GetSolidGradient(Color.black) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(0.353f, 0.353f, 0.353f)) }, // Disabled
                    new ExtGradient{colors = GetSolidGradient(Color.white) }, // Enabled
                };
                textColors = new Color[]
                {
                    Color.black, // Disabled
                    new Color(0.851f, 0.851f, 0.851f) // Enabled
                };
            }
            if (themeNumber == 10)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [femboy 3]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(Color.white) };
                mainBorderColors = new ExtGradient { colors = GetSolidGradient(Color.black) };
                buttonBorderColors = new ExtGradient { colors = GetSolidGradient(Color.black) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(Color.white) }, // Disabled
                    new ExtGradient{colors = GetSolidGradient(Color.white) }, // Enabled
                };
                textColors = new Color[]
                {
                    Color.black, // Disabled
                    new Color(0.851f, 0.851f, 0.851f) // Enabled
                };
            }
            if (themeNumber == 11)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [gay]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(Color.white) };
                mainBorderColors = new ExtGradient { colors = GetSolidGradient(Color.black) };
                buttonBorderColors = new ExtGradient { colors = GetSolidGradient(Color.black) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(Color.white) }, // Disabled
                    new ExtGradient{colors = GetSolidGradient(Color.white) }, // Enabled
                };
                textColors = new Color[]
                {
                    Color.black, // Disabled
                    Color.gray // Enabled
                };
            }
            if (themeNumber == 12)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [pan]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(Color.white) };
                mainBorderColors = new ExtGradient { colors = GetSolidGradient(Color.black) };
                buttonBorderColors = new ExtGradient { colors = GetSolidGradient(Color.black) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(Color.white) }, // Disabled
                    new ExtGradient{colors = GetSolidGradient(Color.white) }, // Enabled
                };
                textColors = new Color[]
                {
                    Color.black, // Disabled
                    Color.gray // Enabled
                };
            }
            if (themeNumber == 13)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [trans]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(Color.white) };
                mainBorderColors = new ExtGradient { colors = GetSolidGradient(Color.black) };
                buttonBorderColors = new ExtGradient { colors = GetSolidGradient(Color.black) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(Color.white) }, // Disabled
                    new ExtGradient{colors = GetSolidGradient(Color.white) }, // Enabled
                };
                textColors = new Color[]
                {
                    Color.black, // Disabled
                    Color.gray // Enabled
                };
            }
            if (themeNumber == 14)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [bisexual]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(Color.white) };
                mainBorderColors = new ExtGradient { colors = GetSolidGradient(Color.black) };
                buttonBorderColors = new ExtGradient { colors = GetSolidGradient(Color.black) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(Color.white) }, // Disabled
                    new ExtGradient{colors = GetSolidGradient(Color.white) }, // Enabled
                };
                textColors = new Color[]
                {
                    Color.black, // Disabled
                    Color.gray // Enabled
                };
            }
            if (themeNumber == 15)
            {
                GetIndex("change theme [pink]").overlapText = "change theme [lesbian]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(Color.white) };
                mainBorderColors = new ExtGradient { colors = GetSolidGradient(Color.black) };
                buttonBorderColors = new ExtGradient { colors = GetSolidGradient(Color.black) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(Color.white) }, // Disabled
                    new ExtGradient{colors = GetSolidGradient(Color.white) }, // Enabled
                };
                textColors = new Color[]
                {
                    Color.black, // Disabled
                    Color.gray // Enabled
                };
            }
            if (themeNumber == 16)// racism
            {
                GetIndex("change theme [pink]").overlapText = "change theme [midnight blue]";

                backgroundColor = new ExtGradient { colors = GetSolidGradient(new Color(0.09f, 0.09f, 0.43f)) };
                mainBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(0.094f, 0.094f, 0.612f)) };
                buttonBorderColors = new ExtGradient { colors = GetSolidGradient(new Color(0.094f, 0.094f, 0.612f)) };
                buttonColors = new ExtGradient[]
                {
                    new ExtGradient{colors = GetSolidGradient(new Color(0.09f, 0.09f, 0.43f)) }, // Disabled
                    new ExtGradient{colors = GetSolidGradient(new Color(0.031f, 0.031f, 0.239f)) }, // Enabled
                };
                textColors = new Color[]
                {
                    Color.white, // Disabled
                    Color.white // Enabled
                };
            }
        }

        // Functions 0.09f, 0.09f, 0.43f
        public static void CreateMenu()
        {

            // Menu Holder
            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(menu.GetComponent<Rigidbody>());
            Destroy(menu.GetComponent<BoxCollider>());
            Destroy(menu.GetComponent<Renderer>());
            menu.transform.localScale = new Vector3(0.1f, 0.3f, 0.39f);

            // Menu Background
            menuBackground = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(menuBackground.GetComponent<Rigidbody>());
            Destroy(menuBackground.GetComponent<BoxCollider>());
            menuBackground.transform.parent = menu.transform;
            menuBackground.transform.rotation = Quaternion.identity;
            menuBackground.transform.localScale = menuSize;
            menuBackground.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
            menuBackground.transform.position = new Vector3(0.05f, 0f, -0.0075f);

            ColorChanger colorChanger = menuBackground.AddComponent<ColorChanger>();
            colorChanger.colorInfo = backgroundColor;
            colorChanger.Start();
            if (themeNumber == 11)
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.color = Color.white;
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/gayflagmiddle.png", "gayflagmiddle.png");
            }
            if (themeNumber == 12)
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.color = Color.white;
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/panflagmiddle.png", "panflagmiddle.png");
            }
            if (themeNumber == 13)
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.color = Color.white;
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/transflagmiddle.png", "transflagmiddle.png");
            }
            if (themeNumber == 14)
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.color = Color.white;
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/bisexualflagmiddle.png", "bisexualflagmiddle.png");
            }
            if (themeNumber == 15)
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.color = Color.white;
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/lesbianflagmiddle.png", "lesbianflagmiddle.png");
            }
            if (themeNumber == 8)
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.color = Color.white;
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/femboyImage1.jpg", "femboyImage1.jpg");
            }
            if (themeNumber == 9)
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.color = Color.white;
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/femboyImage2.png", "femboyImage2.png");
            }
            if (themeNumber == 10)
            {
                menuBackground.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                menuBackground.GetComponent<Renderer>().material.color = Color.white;
                menuBackground.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/femboyImage3.png", "femboyImage3.png");
            }

            if (outlines)
            {
                // Menu Border
                menuBorder = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(menuBorder.GetComponent<Rigidbody>());
                Destroy(menuBorder.GetComponent<BoxCollider>());
                menuBorder.transform.parent = menu.transform;
                menuBorder.transform.rotation = Quaternion.identity;
                menuBorder.transform.localScale = new Vector3(0.098f, 1.0125f, 1.0625f);
                menuBorder.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
                menuBorder.transform.position = new Vector3(0.05f, 0f, -0.0075f);

                colorChanger = menuBorder.AddComponent<ColorChanger>();
                colorChanger.colorInfo = mainBorderColors;
                colorChanger.Start();
            }

            // Canvas
            canvasObject = new GameObject();
            canvasObject.transform.parent = menu.transform;
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            CanvasScaler canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = 1000f;

            // _            _   
            //| |_ _____  _| |_ 
            //| __/ _ \ \/ / __|
            //| ||  __/>  <| |_ 
            // \__\___/_/\_\\__|
            Text text = new GameObject
            {
                transform =
                    {
                        parent = canvasObject.transform
                    }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = pageName + "<color=grey>[</color><color=white>" + (pageNumber + 1).ToString() + "</color><color=grey>]</color>";
            text.fontSize = 1;
            text.color = textColors[0];
            text.supportRichText = true;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.28f, 0.02f);
            component.position = new Vector3(0.06f, -0.0025f, 0.1875f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            if (themeNumber == 8)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 9)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 10)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 11)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 12)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 13)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 14)
            {
                text.fontStyle = FontStyle.Bold;
            }

            Text discontext2 = new GameObject
            {
                transform =
                            {
                                parent = canvasObject.transform
                            }
            }.AddComponent<Text>();
            discontext2.text = PluginInfo.Name;
            discontext2.fontStyle = FontStyle.Bold;
            discontext2.font = currentFont;
            discontext2.fontSize = 1;
            discontext2.color = textColors[0];
            discontext2.alignment = TextAnchor.MiddleCenter;
            discontext2.resizeTextForBestFit = true;
            discontext2.resizeTextMinSize = 0;

            RectTransform rectt = discontext2.GetComponent<RectTransform>();
            rectt.localPosition = Vector3.zero;
            rectt.sizeDelta = new Vector2(0.28f, 0.05f);
            rectt.localPosition = new Vector3(0.06f, 0f, 0.1645f);
            rectt.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            if (themeNumber == 8)
            {
                discontext2.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 9)
            {
                discontext2.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 10)
            {
                discontext2.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 11)
            {
                discontext2.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 12)
            {
                discontext2.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 13)
            {
                discontext2.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 14)
            {
                discontext2.fontStyle = FontStyle.Bold;
            }

            if (fpsCounter)
            {
                fpsObject = new GameObject
                {
                    transform =
                    {
                        parent = canvasObject.transform
                    }
                }.AddComponent<Text>();
                fpsObject.font = currentFont;
                fpsObject.text = "FPS: " + Mathf.Ceil(1f / Time.unscaledDeltaTime).ToString();
                fpsObject.color = textColors[0];
                fpsObject.fontSize = 1;
                fpsObject.supportRichText = true;
                fpsObject.alignment = TextAnchor.MiddleCenter;
                fpsObject.horizontalOverflow = HorizontalWrapMode.Overflow;
                fpsObject.resizeTextForBestFit = true;
                fpsObject.resizeTextMinSize = 0;
                RectTransform component2 = fpsObject.GetComponent<RectTransform>();
                component2.localPosition = Vector3.zero;
                component2.sizeDelta = new Vector2(0.28f, 0.01f);
                component2.position = new Vector3(0.06f, 0f, -0.203f);
                component2.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
                if (themeNumber == 8)
                {
                    fpsObject.fontStyle = FontStyle.Bold;
                }
                if (themeNumber == 9)
                {
                    fpsObject.fontStyle = FontStyle.Bold;
                }
                if (themeNumber == 10)
                {
                    fpsObject.fontStyle = FontStyle.Bold;
                }
                if (themeNumber == 11)
                {
                    fpsObject.fontStyle = FontStyle.Bold;
                }
                if (themeNumber == 12)
                {
                    fpsObject.fontStyle = FontStyle.Bold;
                }
                if (themeNumber == 13)
                {
                    fpsObject.fontStyle = FontStyle.Bold;
                }
                if (themeNumber == 14)
                {
                    fpsObject.fontStyle = FontStyle.Bold;
                }
            }
            Text discontext3 = new GameObject
            {
                transform =
                            {
                                parent = canvasObject.transform
                            }
            }.AddComponent<Text>();
            discontext3.text = PluginInfo.Description;
            discontext3.font = currentFont;
            discontext3.fontSize = 1;
            discontext3.color = textColors[0];
            discontext3.alignment = TextAnchor.MiddleCenter;
            discontext3.resizeTextForBestFit = true;
            discontext3.resizeTextMinSize = 0;

            RectTransform rectttt = discontext3.GetComponent<RectTransform>();
            rectttt.localPosition = Vector3.zero;
            rectttt.sizeDelta = new Vector2(0.28f, 0.023f);
            rectttt.localPosition = new Vector3(0.06f, 0f, -0.19f);
            rectttt.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            if (themeNumber == 8)
            {
                discontext3.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 9)
            {
                discontext3.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 10)
            {
                discontext3.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 11)
            {
                discontext3.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 12)
            {
                discontext3.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 13)
            {
                discontext3.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 14)
            {
                discontext3.fontStyle = FontStyle.Bold;
            }

            Text discontext4 = new GameObject
            {
                transform =
                            {
                                parent = canvasObject.transform
                            }
            }.AddComponent<Text>();
            discontext4.text = PluginInfo.Version;
            discontext4.font = currentFont;
            discontext4.fontSize = 1;
            discontext4.color = textColors[0];
            discontext4.alignment = TextAnchor.MiddleCenter;
            discontext4.resizeTextForBestFit = true;
            discontext4.resizeTextMinSize = 0;

            RectTransform recttttt = discontext4.GetComponent<RectTransform>();
            recttttt.localPosition = Vector3.zero;
            recttttt.sizeDelta = new Vector2(0.28f, 0.015f);
            recttttt.localPosition = new Vector3(0.06f, 0f, 0.1325f);
            recttttt.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            if (themeNumber == 8)
            {
                discontext4.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 9)
            {
                discontext4.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 10)
            {
                discontext4.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 11)
            {
                discontext4.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 12)
            {
                discontext4.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 13)
            {
                discontext4.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 14)
            {
                discontext4.fontStyle = FontStyle.Bold;
            }

            if (disconnectButton)
            {
                Text discontext = new GameObject
                {
                    transform =
                            {
                                parent = canvasObject.transform
                            }
                }.AddComponent<Text>();
                discontext.text = "disconnect";
                discontext.font = currentFont;
                discontext.fontSize = 1;
                discontext.color = textColors[0];
                discontext.alignment = TextAnchor.MiddleCenter;
                discontext.resizeTextForBestFit = true;
                discontext.resizeTextMinSize = 0;

                RectTransform recttt = discontext.GetComponent<RectTransform>();
                recttt.localPosition = Vector3.zero;
                recttt.sizeDelta = new Vector2(0.2f, 0.06f);
                recttt.localPosition = new Vector3(0.064f, 0f, 0.24f);
                recttt.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
                if (themeNumber == 8)
                {
                    discontext.fontStyle = FontStyle.Bold;
                }
                if (themeNumber == 9)
                {
                    discontext.fontStyle = FontStyle.Bold;
                }
                if (themeNumber == 10)
                {
                    discontext.fontStyle = FontStyle.Bold;
                }
                if (themeNumber == 11)
                {
                    discontext.fontStyle = FontStyle.Bold;
                }
                if (themeNumber == 12)
                {
                    discontext.fontStyle = FontStyle.Bold;
                }
                if (themeNumber == 13)
                {
                    discontext.fontStyle = FontStyle.Bold;
                }
                if (themeNumber == 14)
                {
                    discontext.fontStyle = FontStyle.Bold;
                }
            }

            text = new GameObject
            {
                transform =
                            {
                                parent = canvasObject.transform
                            }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = "<";
            text.fontSize = 1;
            text.color = textColors[0];
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.2f, 0.03f);
            component.localPosition = new Vector3(0.064f, 0.195f, 0f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            if (themeNumber == 8)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 9)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 10)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 11)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 12)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 13)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 14)
            {
                text.fontStyle = FontStyle.Bold;
            }

            text = new GameObject
            {
                transform =
                            {
                                parent = canvasObject.transform
                            }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = ">";
            text.fontSize = 1;
            text.color = textColors[0];
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.2f, 0.03f);
            component.localPosition = new Vector3(0.064f, -0.195f, 0f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            if (themeNumber == 8)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 9)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 10)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 11)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 12)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 13)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 14)
            {
                text.fontStyle = FontStyle.Bold;
            }

            // Buttons
            // Disconnect
            if (disconnectButton)
            {
                GameObject disconnectbutton = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    disconnectbutton.layer = 2;
                }
                Destroy(disconnectbutton.GetComponent<Rigidbody>());
                disconnectbutton.GetComponent<BoxCollider>().isTrigger = true;
                disconnectbutton.transform.parent = menu.transform;
                disconnectbutton.transform.rotation = Quaternion.identity;
                disconnectbutton.transform.localScale = new Vector3(0.09f, 1f, 0.13f);
                disconnectbutton.transform.localPosition = new Vector3(0.5f, 0f, 0.6f);
                disconnectbutton.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
                disconnectbutton.AddComponent<Classes.Button>().relatedText = "disconnect";

                colorChanger = disconnectbutton.AddComponent<ColorChanger>();
                colorChanger.colorInfo = buttonColors[0];
                colorChanger.Start();

                if (themeNumber == 11)
                {
                    disconnectbutton.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                    disconnectbutton.GetComponent<Renderer>().material.color = Color.white;
                    disconnectbutton.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/gayflagmiddle.png", "gayflagmiddle.png");
                }
                if (themeNumber == 12)
                {
                    disconnectbutton.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                    disconnectbutton.GetComponent<Renderer>().material.color = Color.white;
                    disconnectbutton.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/panflagmiddle.png", "panflagmiddle.png");
                }
                if (themeNumber == 13)
                {
                    disconnectbutton.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                    disconnectbutton.GetComponent<Renderer>().material.color = Color.white;
                    disconnectbutton.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/transflagmiddle.png", "transflagmiddle.png");
                }
                if (themeNumber == 14)
                {
                    disconnectbutton.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                    disconnectbutton.GetComponent<Renderer>().material.color = Color.white;
                    disconnectbutton.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/bisexualflagmiddle.png", "bisexualflagmiddle.png");
                }
                if (themeNumber == 15)
                {
                    disconnectbutton.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                    disconnectbutton.GetComponent<Renderer>().material.color = Color.white;
                    disconnectbutton.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/lesbianflagmiddle.png", "lesbianflagmiddle.png");
                }

                if (outlines)
                {
                    GameObject gameObject2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    if (!UnityInput.Current.GetKey(KeyCode.Q))
                    {
                        gameObject2.layer = 2;
                    }
                    Destroy(gameObject2.GetComponent<Rigidbody>());
                    gameObject2.GetComponent<BoxCollider>().isTrigger = true;
                    gameObject2.transform.parent = menu.transform;
                    gameObject2.transform.rotation = Quaternion.identity;
                    gameObject2.transform.localScale = new Vector3(0.089f, 1.0065f, 0.1365f);
                    gameObject2.transform.localPosition = new Vector3(0.5f, 0f, 0.6f);

                    colorChanger = gameObject2.AddComponent<ColorChanger>();
                    colorChanger.colorInfo = buttonBorderColors;
                    colorChanger.Start();
                }
            }

            // Page Buttons
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject.layer = 2;
            }
            Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.1f, 0.225f, 1.05f);// 0.1f, 0.3f, 0.39f
            gameObject.transform.localPosition = new Vector3(0.5f, 0.65f, -0.02f);
            gameObject.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
            gameObject.AddComponent<Classes.Button>().relatedText = "PreviousPage";

            colorChanger = gameObject.AddComponent<ColorChanger>();
            colorChanger.colorInfo = buttonColors[0];
            colorChanger.Start();

            if (themeNumber == 11)
            {
                gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                gameObject.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/gayflagleft.png", "gayflagleft.png");
            }
            if (themeNumber == 12)
            {
                gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                gameObject.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/panflagleft.png", "panflagleft.png");
            }
            if (themeNumber == 13)
            {
                gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                gameObject.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/transflagleft.png", "transflagleft.png");
            }
            if (themeNumber == 14)
            {
                gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                gameObject.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/bisexualflagleft.png", "bisexualflagleft.png");
            }
            if (themeNumber == 15)
            {
                gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                gameObject.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/lesbianflagleft.png", "lesbianflagleft.png");
            }

            if (outlines)
            {
                GameObject gameObject2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    gameObject.layer = 2;
                }
                Destroy(gameObject2.GetComponent<Rigidbody>());
                gameObject2.GetComponent<BoxCollider>().isTrigger = true;
                gameObject2.transform.parent = menu.transform;
                gameObject2.transform.rotation = Quaternion.identity;
                gameObject2.transform.localScale = new Vector3(0.099f, 0.235f, 1.065f);
                gameObject2.transform.localPosition = new Vector3(0.5f, 0.65f, -0.02f);

                colorChanger = gameObject2.AddComponent<ColorChanger>();
                colorChanger.colorInfo = buttonBorderColors;
                colorChanger.Start();
            }

            gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject.layer = 2;
            }
            Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.1f, 0.225f, 1.05f);// 0.1f, 0.3f, 0.39f
            gameObject.transform.localPosition = new Vector3(0.5f, -0.65f, -0.02f);
            gameObject.GetComponent<Renderer>().material.color = buttonColors[0].colors[0].color;
            gameObject.AddComponent<Classes.Button>().relatedText = "NextPage";

            colorChanger = gameObject.AddComponent<ColorChanger>();
            colorChanger.colorInfo = buttonColors[0];
            colorChanger.Start();

            if (themeNumber == 11)
            {
                gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                gameObject.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/gayflagright.png", "gayflagright.png");
            }
            if (themeNumber == 12)
            {
                gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                gameObject.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/panflagright.png", "panflagright.png");
            }
            if (themeNumber == 13)
            {
                gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                gameObject.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/transflagright.png", "transflagright.png");
            }
            if (themeNumber == 14)
            {
                gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                gameObject.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/bisexualflagright.png", "bisexualflagright.png");
            }
            if (themeNumber == 15)
            {
                gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Universal Render Pipeline/Lit");
                gameObject.GetComponent<Renderer>().material.color = Color.white;
                gameObject.GetComponent<Renderer>().material.mainTexture = LoadTextureFromURL("https://adlibsreal.github.io/lesbianflagright.png", "lesbianflagright.png");
            }

            if (outlines)
            {
                GameObject gameObject2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    gameObject.layer = 2;
                }
                Destroy(gameObject2.GetComponent<Rigidbody>());
                gameObject2.GetComponent<BoxCollider>().isTrigger = true;
                gameObject2.transform.parent = menu.transform;
                gameObject2.transform.rotation = Quaternion.identity;
                gameObject2.transform.localScale = new Vector3(0.099f, 0.235f, 1.065f);
                gameObject2.transform.localPosition = new Vector3(0.5f, -0.65f, -0.02f);

                colorChanger = gameObject2.AddComponent<ColorChanger>();
                colorChanger.colorInfo = buttonBorderColors;
                colorChanger.Start();
            }

            // Mod Buttons
            ButtonInfo[] activeButtons = buttons[buttonsType].Skip(pageNumber * buttonsPerPage).Take(buttonsPerPage).ToArray();
            for (int i = 0; i < activeButtons.Length; i++)
            {
                CreateButton(i * 0.072f, activeButtons[i]);
            }
        }

        public static void CreateButton(float offset, ButtonInfo method)
        {
            GameObject gameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(KeyCode.Q))
            {
                gameObject.layer = 2;
            }
            Destroy(gameObject.GetComponent<Rigidbody>());
            gameObject.GetComponent<BoxCollider>().isTrigger = true;
            gameObject.transform.parent = menu.transform;
            gameObject.transform.rotation = Quaternion.identity;
            gameObject.transform.localScale = new Vector3(0.09f, 0.95f, 0.06f);
            gameObject.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - offset);
            gameObject.AddComponent<Classes.Button>().relatedText = method.buttonText;

            ColorChanger colorChanger = gameObject.AddComponent<ColorChanger>();
            if (method.enabled)
            {
                colorChanger.colorInfo = buttonColors[1];
            }
            else
            {
                colorChanger.colorInfo = buttonColors[0];
            }
            colorChanger.Start();

            if (outlines)
            {
                GameObject buttonOutline = GameObject.CreatePrimitive(PrimitiveType.Cube);
                if (!UnityInput.Current.GetKey(KeyCode.Q))
                {
                    gameObject.layer = 2;
                }
                Destroy(buttonOutline.GetComponent<Rigidbody>());
                buttonOutline.GetComponent<BoxCollider>().isTrigger = true;
                buttonOutline.transform.parent = menu.transform;
                buttonOutline.transform.rotation = Quaternion.identity;
                buttonOutline.transform.localScale = new Vector3(0.08f, 0.9545f, 0.0645f);
                buttonOutline.transform.localPosition = new Vector3(0.56f, 0f, 0.28f - offset);

                colorChanger = buttonOutline.AddComponent<ColorChanger>();
                colorChanger.colorInfo = buttonBorderColors;
                colorChanger.Start();
                if (themeNumber == 8)
                {
                    buttonOutline.GetComponent<Renderer>().enabled = false;
                }
                if (themeNumber == 9)
                {
                    buttonOutline.GetComponent<Renderer>().enabled = false;
                }
                if (themeNumber == 10)
                {
                    buttonOutline.GetComponent<Renderer>().enabled = false;
                }
                if (themeNumber == 11)
                {
                    buttonOutline.GetComponent<Renderer>().enabled = false;
                }
                if (themeNumber == 12)
                {
                    buttonOutline.GetComponent<Renderer>().enabled = false;
                }
                if (themeNumber == 13)
                {
                    buttonOutline.GetComponent<Renderer>().enabled = false;
                }
                if (themeNumber == 14)
                {
                    buttonOutline.GetComponent<Renderer>().enabled = false;
                }
                if (themeNumber == 15)
                {
                    buttonOutline.GetComponent<Renderer>().enabled = false;
                }
            }
            if (themeNumber == 8)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            }
            if (themeNumber == 9)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            }
            if (themeNumber == 10)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            }
            if (themeNumber == 11)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            }
            if (themeNumber == 12)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            }
            if (themeNumber == 13)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            }
            if (themeNumber == 14)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            }
            if (themeNumber == 15)
            {
                gameObject.GetComponent<Renderer>().enabled = false;
            }

            Text text = new GameObject
            {
                transform =
                {
                    parent = canvasObject.transform
                }
            }.AddComponent<Text>();
            text.font = currentFont;
            text.text = method.buttonText;
            if (method.overlapText != null)
            {
                text.text = method.overlapText;
            }
            text.supportRichText = true;
            text.fontSize = 1;
            if (method.enabled)
            {
                text.color = textColors[1];
            }
            else
            {
                text.color = textColors[0];
            }
            text.alignment = TextAnchor.MiddleCenter;
            text.fontStyle = FontStyle.Italic;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(.2f, .02f);
            component.localPosition = new Vector3(.064f, 0, .111f - offset / 2.6f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
            if (themeNumber == 8)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 9)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 10)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 11)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 12)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 13)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 14)
            {
                text.fontStyle = FontStyle.Bold;
            }
            if (themeNumber == 15)
            {
                text.fontStyle = FontStyle.Bold;
            }
        }

        public static void RecreateMenu()
        {
            if (menu != null)
            {
                Destroy(menu);
                menu = null;

                CreateMenu();
                RecenterMenu(rightHanded, UnityInput.Current.GetKey(keyboardButton));
            }
        }

        public static void RecenterMenu(bool isRightHanded, bool isKeyboardCondition)
        {
            if (!isKeyboardCondition)
            {
                if (!isRightHanded)
                {
                    menu.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                    menu.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                }
                else
                {
                    menu.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    Vector3 rotation = GorillaTagger.Instance.rightHandTransform.rotation.eulerAngles;
                    rotation += new Vector3(0f, 0f, 180f);
                    menu.transform.rotation = Quaternion.Euler(rotation);
                }
            }
            else
            {
                try
                {
                    TPC = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera").GetComponent<Camera>();
                }
                catch { }
                if (TPC != null)
                {
                    TPC.transform.position = new Vector3(-999f, -999f, -999f);
                    TPC.transform.rotation = Quaternion.identity;
                    menu.transform.parent = TPC.transform;
                    menu.transform.position = TPC.transform.position + Vector3.Scale(TPC.transform.forward, new Vector3(0.5f, 0.5f, 0.5f)) + Vector3.Scale(TPC.transform.up, new Vector3(-0.02f, -0.02f, -0.02f));
                    Vector3 rot = TPC.transform.rotation.eulerAngles;
                    rot = new Vector3(rot.x - 90, rot.y + 90, rot.z);
                    menu.transform.rotation = Quaternion.Euler(rot);

                    if (reference != null)
                    {
                        if (Mouse.current.leftButton.isPressed)
                        {
                            Ray ray = TPC.ScreenPointToRay(Mouse.current.position.ReadValue());
                            RaycastHit hit;
                            bool worked = Physics.Raycast(ray, out hit, 100);
                            if (worked)
                            {
                                Classes.Button collide = hit.transform.gameObject.GetComponent<Classes.Button>();
                                if (collide != null)
                                {
                                    collide.OnTriggerEnter(buttonCollider);
                                }
                            }
                        }
                        else
                        {
                            reference.transform.position = new Vector3(999f, -999f, -999f);
                        }
                    }
                }
            }
        }

        public static void CreateReference(bool isRightHanded)
        {
            reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            if (isRightHanded)
            {
                reference.transform.parent = GorillaTagger.Instance.leftHandTransform;
            }
            else
            {
                reference.transform.parent = GorillaTagger.Instance.rightHandTransform;
            }
            reference.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
            reference.transform.localPosition = new Vector3(0f, -0.101f, 0.019f);
            reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            buttonCollider = reference.GetComponent<SphereCollider>();

            ColorChanger colorChanger = reference.AddComponent<ColorChanger>();
            colorChanger.colorInfo = backgroundColor;
            colorChanger.Start();
        }

        public static void Toggle(string buttonText)
        {
            int lastPage = (buttons[buttonsType].Length + buttonsPerPage - 1) / buttonsPerPage - 1;
            if (buttonText == "PreviousPage")
            {
                pageNumber--;
                if (pageNumber < 0)
                {
                    pageNumber = lastPage;
                }
            }
            else
            {
                if (buttonText == "NextPage")
                {
                    pageNumber++;
                    if (pageNumber > lastPage)
                    {
                        pageNumber = 0;
                    }
                }
                else
                {
                    ButtonInfo target = GetIndex(buttonText);
                    if (target != null)
                    {
                        if (target.isTogglable)
                        {
                            target.enabled = !target.enabled;
                            if (target.enabled)
                            {
                                NotifiLib.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);
                                if (target.enableMethod != null)
                                {
                                    try { target.enableMethod.Invoke(); } catch { }
                                }
                            }
                            else
                            {
                                NotifiLib.SendNotification("<color=grey>[</color><color=red>DISABLE</color><color=grey>]</color> " + target.toolTip);
                                if (target.disableMethod != null)
                                {
                                    try { target.disableMethod.Invoke(); } catch { }
                                }
                            }
                        }
                        else
                        {
                            NotifiLib.SendNotification("<color=grey>[</color><color=green>ENABLE</color><color=grey>]</color> " + target.toolTip);
                            if (target.method != null)
                            {
                                try { target.method.Invoke(); } catch { }
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError(buttonText + " does not exist");
                    }
                }
            }
            RecreateMenu();
        }

        public static GradientColorKey[] GetSolidGradient(Color color)
        {
            return new GradientColorKey[] { new GradientColorKey(color, 0f), new GradientColorKey(color, 1f) };
        }

        public static ButtonInfo GetIndex(string buttonText)
        {
            foreach (ButtonInfo[] buttons in buttons)
            {
                foreach (ButtonInfo button in buttons)
                {
                    if (button.buttonText == buttonText)
                    {
                        return button;
                    }
                }
            }

            return null;
        }
        public static void OnLaunch()
        {
            if (File.Exists("silliness/enabledmods.txt"))
            {
                try
                {
                    LoadPreferences();
                }
                catch
                {
                    Task.Delay(1000).ContinueWith(t => LoadPreferences());
                }
            }
        }
        public static Texture2D LoadTextureFromURL(string resourcePath, string fileName)
        {
            Texture2D texture = new Texture2D(2, 2);

            if (!Directory.Exists("silliness"))
            {
                Directory.CreateDirectory("silliness");
            }
            if (!File.Exists("silliness/" + fileName))
            {
                Debug.Log("Downloading " + fileName);
                WebClient stream = new WebClient();
                stream.DownloadFile(resourcePath, "silliness/" + fileName);
            }

            byte[] bytes = File.ReadAllBytes("silliness/" + fileName);
            texture.LoadImage(bytes);

            return texture;
        }
        public static AudioClip LoadSoundFromResource(string resourcePath)
        {
            AudioClip sound = null;

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("silliness.Resources.silliness");
            if (stream != null)
            {
                if (assetBundle == null)
                {
                    assetBundle = AssetBundle.LoadFromStream(stream);
                }
                sound = assetBundle.LoadAsset(resourcePath) as AudioClip;
            }
            else
            {
                Debug.LogError("Failed to load sound from resource: " + resourcePath);
            }

            return sound;
        }
        public static void disconnect()
        {
            PhotonNetwork.Disconnect();
        }

        // Variables
        // Important
        // Objects
        public static GameObject menu;
        public static GameObject menuBackground;
        public static GameObject menuBorder;
        public static GameObject menuBorder2;
        public static GameObject pageBackground;
        public static GameObject reference;
        public static GameObject canvasObject;
        public static GameObject buttonOutline;

        public static SphereCollider buttonCollider;
        public static Camera TPC;
        public static Text fpsObject;
        // Data
        public static bool shouldSaveMods = false;
        public static int pageNumber = 0;
        public static string pageName = "home ";
        public static int buttonsType = 0;
        public static bool rightPrimary = false;
        public static bool rightSecondary = false;
        public static bool leftPrimary = false;
        public static bool leftSecondary = false;
        public static bool leftGrab = false;
        public static bool rightGrab = false;
        public static float leftTrigger = 0f;
        public static float rightTrigger = 0f;
        public static bool EverythingSlippery = false;
        public static bool EverythingGrippy = false;
        public static bool HasLoaded = false;
        public static Vector3 blahhhhhhh;
        public static AssetBundle assetBundle = null;
        static public GorillaScoreBoard[] allleaderboards;
        public static GameObject motd = null;
        public static GameObject motdText = null;
    }
}
