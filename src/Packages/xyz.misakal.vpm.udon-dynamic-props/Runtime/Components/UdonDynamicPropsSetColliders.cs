#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using VRC.SDK3.Dynamics.PhysBone.Components;
using VRC.SDKBase;

namespace UdonDynamicProps.Runtime.Components
{
    [RequireComponent(typeof(VRCPhysBone))]
    public class UdonDynamicPropsSetColliders : MonoBehaviour, IEditorOnly
    {
        internal void Setup(VRCPhysBoneCollider[] colliders)
        {
            var physBones = GetComponent<VRCPhysBone>();
            physBones.colliders.Clear();
            physBones.colliders.AddRange(colliders);

#if UNITY_EDITOR
            EditorUtility.SetDirty(physBones);
#endif
        }
    }
}