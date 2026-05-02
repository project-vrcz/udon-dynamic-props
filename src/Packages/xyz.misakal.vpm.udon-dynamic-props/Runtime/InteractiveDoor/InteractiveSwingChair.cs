using UdonDynamicProps.Runtime.ObjectPool;
using UdonSharp;
using VRC.SDKBase;

namespace UdonDynamicProps.Runtime.InteractiveDoor
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public sealed class InteractiveSwingChair : UdonSharpBehaviour
    {
        public MisakaLocalPlayerObjectPool playerObjectPool;

        public override void OnStationEntered(VRCPlayerApi player)
        {
            var playerObject = playerObjectPool._GetPlayerObject(player.playerId);
            if (!playerObject)
                return;

            playerObject.gameObject.SetActive(false);
        }

        public override void OnStationExited(VRCPlayerApi player)
        {
            var playerObject = playerObjectPool._GetPlayerObject(player.playerId);
            if (!playerObject)
                return;

            playerObject.gameObject.SetActive(true);
        }
    }
}