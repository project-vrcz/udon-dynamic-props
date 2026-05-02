using UdonSharp;
using VRC.SDKBase;

namespace UdonDynamicProps.Runtime.Components.Toggle
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public sealed class UdonDynamicPropsToggleCollidersStationBinding : UdonSharpBehaviour
    {
        public UdonDynamicPropsManager udonDynamicPropsManager;

        public override void OnStationEntered(VRCPlayerApi player)
        {
            udonDynamicPropsManager.SetPlayerPhysBoneColliderActive(player.playerId, false);
        }

        public override void OnStationExited(VRCPlayerApi player)
        {
            udonDynamicPropsManager.SetPlayerPhysBoneColliderActive(player.playerId, true);
        }
    }
}