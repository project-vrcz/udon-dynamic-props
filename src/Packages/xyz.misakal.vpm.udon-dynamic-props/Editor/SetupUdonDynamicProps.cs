using System.Collections.Generic;
using System.Linq;
using UdonDynamicProps.Runtime;
using UdonDynamicProps.Runtime.Components;
using UdonDynamicProps.Runtime.Components.Toggle;
using UdonDynamicProps.Runtime.ObjectPool;
using UdonDynamicProps.Runtime.ObjectPool.PlayerObjectExtensions;
using UdonSharp;
using UdonSharpEditor;
using UnityEditor;
using UnityEngine;
using VRC.SDK3.Dynamics.PhysBone.Components;
using VRC.Udon;

namespace UdonDynamicProps.Editor
{
    public static class SetupUdonDynamicProps
    {
        private static IEnumerable<T> GetComponentsInScene<T>() where T : UdonSharpBehaviour
        {
            return Object.FindObjectsOfType<UdonBehaviour>()
                .Where(UdonSharpEditorUtility.IsUdonSharpBehaviour)
                .Select(UdonSharpEditorUtility.GetProxyBehaviour)
                .Select(u => u as T)
                .Where(u => u != null);
        }

        public static void Setup()
        {
            var setup = Object.FindObjectOfType<UdonDynamicPropsSetup>();
            Setup(setup);
        }

        public static void Setup(UdonDynamicPropsSetup setupComponent)
        {
            var propsManager = GetComponentsInScene<UdonDynamicPropsManager>()
                .FirstOrDefault();
            if (!propsManager)
            {
                Debug.LogError("You must have a UdonDynamicPropsManager in the scene.");
                return;
            }

            var playerColliders = SetupPool(propsManager, setupComponent);
            var collidersToAdd = setupComponent.additionalPhysBoneColliders
                .Concat(playerColliders)
                .ToArray();

            var doors = Object.FindObjectsOfType<UdonDynamicPropsSetColliders>();
            foreach (var door in doors)
            {
                door.Setup(collidersToAdd);
            }

            var swingChairs = Object.FindObjectsOfType<UdonDynamicPropsToggleCollidersStationBinding>();
            foreach (var swingChair in swingChairs)
            {
                swingChair.udonDynamicPropsManager = propsManager;
                EditorUtility.SetDirty(swingChair);
            }
        }

        private static VRCPhysBoneCollider[] SetupPool(
            UdonDynamicPropsManager manager,
            UdonDynamicPropsSetup setupComponent
        )
        {
            var maxPlayerCount = setupComponent.maxPlayerCount;
            var colliders = new List<VRCPhysBoneCollider>();

            var playerObjectPool = manager.playerObjectPool;
            playerObjectPool.SetupPool(index =>
            {
                var go = Object.Instantiate(setupComponent.playerObjectTemplate, playerObjectPool.transform);
                go.name = $"[{index}] {go.name}";

                var propsExt = go.GetComponentInChildren<UdonDynamicPropsPlayerObjectExtension>();
                colliders.AddRange(propsExt.GetComponentsInChildren<VRCPhysBoneCollider>(true));

                return go.GetComponent<MisakaLocalPlayerObject>();
            }, maxPlayerCount);

            return colliders.ToArray();
        }
    }
}