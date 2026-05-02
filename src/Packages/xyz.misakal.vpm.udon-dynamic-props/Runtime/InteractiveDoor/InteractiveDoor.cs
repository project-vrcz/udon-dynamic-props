using UnityEditor;
using UnityEngine;
using VRC.Dynamics;
using VRC.SDK3.Dynamics.PhysBone.Components;
using VRC.SDKBase;

namespace UdonDynamicProps.Runtime.InteractiveDoor
{
    [RequireComponent(typeof(VRCPhysBone))]
    public class InteractiveDoor : MonoBehaviour, IEditorOnly
    {
        internal void Setup(VRCPhysBoneColliderBase[] colliders)
        {
            var physBones = GetComponent<VRCPhysBone>();
            physBones.colliders.Clear();
            physBones.colliders.AddRange(colliders);

            EditorUtility.SetDirty(physBones);
        }
    }
}