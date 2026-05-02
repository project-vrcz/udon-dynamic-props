using UdonDynamicProps.Runtime.ObjectPool;
using UdonDynamicProps.Runtime.ObjectPool.PlayerObjectExtensions;
using UdonSharp;

namespace UdonDynamicProps.Runtime
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public sealed class UdonDynamicPropsManager : UdonSharpBehaviour
    {
        public MisakaLocalPlayerObjectPool playerObjectPool;

        public void SetPlayerPhysBoneColliderActive(int playerId, bool active)
        {
            var playerObject = playerObjectPool._GetPlayerObject(playerId);
            if (!playerObject) return;

            var propsExtension = playerObject.GetComponentInChildren<UdonDynamicPropsPlayerObjectExtension>();
            propsExtension.SetPhysBoneCollidersActive(active);
        }
    }
}