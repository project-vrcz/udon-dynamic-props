using JetBrains.Annotations;
using UdonSharp;
using VRC.SDKBase;

namespace MisakaLab.ObjectPool.PlayerObjectExtensions
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public abstract class MisakaLocalPlayerObjectExtension : UdonSharpBehaviour
    {
        public MisakaLocalPlayerObject localPlayerObject;
        protected VRCPlayerApi Player => localPlayerObject.AssignedPlayer;

        [PublicAPI]
        public virtual void _OnPlayerAssigned()
        {
        }

        [PublicAPI]
        public virtual void _OnPlayerReturning()
        {
        }
    }
}