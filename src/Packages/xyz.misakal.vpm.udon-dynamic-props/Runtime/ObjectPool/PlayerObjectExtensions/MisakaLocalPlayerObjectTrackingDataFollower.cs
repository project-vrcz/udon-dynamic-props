using UdonSharp;
using VRC.SDKBase;

namespace MisakaLab.ObjectPool.PlayerObjectExtensions
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public sealed class MisakaLocalPlayerObjectTrackingDataFollower : MisakaLocalPlayerObjectExtension
    {
        private void Update()
        {
            if (Player == null)
                return;

            var origin = Player.GetTrackingData(VRCPlayerApi.TrackingDataType.Origin);
            transform.SetPositionAndRotation(origin.position, origin.rotation);
        }
    }
}