using System;
using UnityEngine;
using VRC.SDK3.Dynamics.PhysBone.Components;
using VRC.SDKBase;

namespace UdonDynamicProps.Runtime
{
    public class UdonDynamicPropsSetup : MonoBehaviour, IEditorOnly
    {
        public VRCPhysBoneCollider[] additionalPhysBoneColliders = Array.Empty<VRCPhysBoneCollider>();
        public int maxPlayerCount = 80;

        [SerializeField] internal GameObject playerObjectTemplate;
    }
}