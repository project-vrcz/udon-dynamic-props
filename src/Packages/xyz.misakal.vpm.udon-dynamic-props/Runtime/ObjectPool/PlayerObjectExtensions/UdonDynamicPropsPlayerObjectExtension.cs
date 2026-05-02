using UdonSharp;
using UnityEngine;

namespace UdonDynamicProps.Runtime.ObjectPool.PlayerObjectExtensions
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public sealed class UdonDynamicPropsPlayerObjectExtension : MisakaLocalPlayerObjectExtension
    {
        public GameObject physBoneCollidersRoot;

        public void SetPhysBoneCollidersActive(bool active)
        {
            if (!physBoneCollidersRoot) return;
            physBoneCollidersRoot.gameObject.SetActive(active);
        }
    }
}