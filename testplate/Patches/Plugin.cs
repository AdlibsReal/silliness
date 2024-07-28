using BepInEx;
using GorillaNetworking;
using silliness.Patches;
using System.ComponentModel;
using UnityEngine;

namespace silliness.Patches
{
    [Description(PluginInfo.Description)]
    [BepInPlugin(PluginInfo.GUID, PluginInfo.Name, PluginInfo.Version)]
    public class HarmonyPatches : BaseUnityPlugin
    {
        private void OnEnable()
        {
            Menu.ApplyHarmonyPatches();
        }

        private void OnDisable()
        {
            Menu.RemoveHarmonyPatches();
        }
        private string whatshappeningimscared;
        private void OnGUI()
        {
            whatshappeningimscared = GUI.TextField(new Rect(Screen.width - 200, 20, 180, 20), whatshappeningimscared);
            if (GUI.Button(new Rect(Screen.width - 200, 50, 180, 50), "join this FUCKING room"))
            {
                PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(whatshappeningimscared, JoinType.Solo);
            }
            
        }
    }
}
