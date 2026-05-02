using System;
using System.Linq;
using UdonDynamicProps.Runtime.ObjectPool;
using UnityEditor;
using UnityEngine;
using VRC.Dynamics;
using VRC.SDKBase;
using VRC.SDKBase.Editor.BuildPipeline;

namespace UdonDynamicProps.Runtime.InteractiveDoor
{
    public sealed class InteractiveDoorManager : MonoBehaviour, IEditorOnly
    {
        public MisakaLocalPlayerObjectPool playerObjectPool;
        public int maxPlayerCount = 80;

        public VRCPhysBoneColliderBase[] additionalPhysBoneColliders = Array.Empty<VRCPhysBoneColliderBase>();

        [SerializeField] private GameObject interactiveDoorPlayerObjectTemplate;

#if !COMPILER_UDONSHARP && UNITY_EDITOR
        private void Setup()
        {
            var doorColliders = SetupPool();
            var collidersToAdd = additionalPhysBoneColliders
                .Concat(doorColliders)
                .ToArray();

            var doors = FindObjectsOfType<InteractiveDoor>();
            foreach (var door in doors)
            {
                door.Setup(collidersToAdd);
            }

            var swingChairs = FindObjectsOfType<InteractiveSwingChair>();
            foreach (var swingChair in swingChairs)
            {
                swingChair.playerObjectPool = playerObjectPool;
                EditorUtility.SetDirty(swingChair);
            }
        }

        private VRCPhysBoneColliderBase[] SetupPool()
        {
            var colliders = new VRCPhysBoneColliderBase[maxPlayerCount];

            playerObjectPool.SetupPool(index =>
            {
                var go = Instantiate(interactiveDoorPlayerObjectTemplate, playerObjectPool.transform);
                go.name = $"[{index}] {go.name}";

                colliders[index] = go.GetComponent<VRCPhysBoneColliderBase>();
                return go.GetComponent<MisakaLocalPlayerObject>();
            }, maxPlayerCount);

            return colliders;
        }

        [CustomEditor(typeof(InteractiveDoorManager))]
        internal sealed class InteractiveDoorManagerEditor : Editor
        {
            public override void OnInspectorGUI()
            {
                var behaviour = (InteractiveDoorManager)target;

                base.OnInspectorGUI();

                if (GUILayout.Button("Setup"))
                {
                    behaviour.Setup();
                }
            }
        }

        internal sealed class InteractiveDoorBuildPipelineCallback : IVRCSDKBuildRequestedCallback
        {
            public int callbackOrder => 0;

            public bool OnBuildRequested(VRCSDKRequestedBuildType requestedBuildType)
            {
                Setup();
                return true;
            }

            private static void Setup()
            {
                var doorManager = FindObjectOfType<InteractiveDoorManager>();
                if (!doorManager)
                    return;

                doorManager.Setup();
            }
        }
#endif
    }
}