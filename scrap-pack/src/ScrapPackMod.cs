using System.IO;
using BepInEx;
using UnityEngine;
using LethalLib.Modules;
using HarmonyLib;
using System;
using Unity.Netcode;
using NetworkPrefabs = LethalLib.Modules.NetworkPrefabs;

namespace ScrapPack
{
    [BepInDependency("evaisa.lethallib", "0.15.1")]
    [BepInPlugin(modGUID, modName, modVersion)]
    public class ScrapPackMod : BaseUnityPlugin
    {
        private const string modGUID = "locochoco.scrappack";
        private const string modName = "ScrapPack";
        private const string modVersion = "0.1.0";

        private static readonly Harmony harmony = new Harmony(modGUID);
        private static AssetBundle scrappackBundle;
        private static GameObject scrappackPrefab;
        private static Sprite scrappackIcon;
        private static Item scrappackItem;

        private static TerminalNode CreateInfoNode(string name, string description)
        {
            TerminalNode node = ScriptableObject.CreateInstance<TerminalNode>();
            DontDestroyOnLoad(node);
            node.clearPreviousText = true;
            node.name = name + "InfoNode";
            node.displayText = description + "\n\n";
            return node;
        }

        private static Item MakeItem(GameObject prefab, Sprite icon)
        {
            Item item = ScriptableObject.CreateInstance<Item>();
            DontDestroyOnLoad(item);
            item.name = "Scrap Pack";
            item.itemName = "Scrap Pack";
            item.itemId = 13;
            item.isScrap = false;
            item.creditsWorth = 0;
            item.canBeGrabbedBeforeGameStart = false;
            item.automaticallySetUsingPower = true;
            item.automaticallySetUsingPower = false;
            item.allowDroppingAheadOfPlayer = true;
            item.batteryUsage = 1f/3f;
            item.weight = 1.5f;
            item.canBeInspected = false;
            item.itemIsTrigger = true;
            item.isDefensiveWeapon = false;
            item.isConductiveMetal = true;
            item.saveItemVariable = false;
            item.requiresBattery = true;
            item.twoHandedAnimation = true;
            item.syncGrabFunction = true;
            item.syncInteractLRFunction = true;
            item.syncUseFunction = true;
            item.syncDiscardFunction = true;
            item.verticalOffset = 0.2f;
            item.grabAnim = "HoldJetpack";
            item.positionOffset = new Vector3(0, -0.2f, -1);
            item.rotationOffset = new Vector3(0, 0, 0);
            item.restingRotation = new Vector3(90, 0, 90);
            item.spawnPrefab = prefab;
            item.itemIcon = icon;
            prefab.GetComponent<GrabbableObject>().itemProperties = item;
            prefab.GetComponent<GrabbableObject>().insertedBattery = new Battery(false, 1);
            prefab.GetComponent<GrabbableObject>().useCooldown = 1.25f;
            return item;
        }

        public void Awake()
        {
            harmony.PatchAll();

            scrappackBundle = AssetBundle.LoadFromFile(Info.Location.Replace("scrappack.dll", "scrappack"));
            scrappackPrefab = scrappackBundle.LoadAsset<GameObject>("scrappack");
            scrappackIcon = scrappackBundle.LoadAsset<Sprite>("ScrapPack");
            NetworkPrefabs.RegisterNetworkPrefab(scrappackPrefab);
            scrappackItem = MakeItem(scrappackPrefab, scrappackIcon);
            Items.RegisterShopItem(scrappackItem, price: 0, itemInfo: CreateInfoNode("Scrap Pack", "A jetpack made from scrap."));

            Logger.LogInfo($"Plugin {modName} is loaded with version {modVersion}!");
        }
    }
}
