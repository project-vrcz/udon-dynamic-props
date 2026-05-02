using JetBrains.Annotations;
using UdonDynamicProps.Runtime.ObjectPool.PlayerObjectExtensions;
using UdonSharp;
using VRC.SDKBase;

// ReSharper disable UseArrayEmptyMethod
namespace UdonDynamicProps.Runtime.ObjectPool
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public sealed class MisakaLocalPlayerObject : UdonSharpBehaviour
    {
        [PublicAPI] [CanBeNull] public VRCPlayerApi AssignedPlayer { get; private set; }

        [PublicAPI] public MisakaLocalPlayerObjectExtension[] extensions = new MisakaLocalPlayerObjectExtension[0];

        [PublicAPI]
        public void AssignPlayer(VRCPlayerApi player)
        {
            AssignedPlayer = player;
            SendEventToListeners(nameof(MisakaLocalPlayerObjectExtension._OnPlayerAssigned));
        }

        [PublicAPI]
        public void ReturnToPool()
        {
            SendEventToListeners(nameof(MisakaLocalPlayerObjectExtension._OnPlayerReturning));
            AssignedPlayer = null;
        }

        private void SendEventToListeners(string eventName)
        {
            foreach (var listener in extensions)
            {
                listener.SendCustomEvent(eventName);
            }
        }
    }
}