using UdonSharp;
using UnityEngine;
using VRC.Dynamics;

namespace UdonDynamicProps.Runtime.Components.Contact
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public sealed class PlayOnContact : UdonSharpBehaviour
    {
        public AudioSource audioSource;
        public AudioClip audioClip;

        public bool playOnEnter;
        public bool playOnExit;

        public override void OnContactEnter(ContactEnterInfo contactInfo)
        {
            if (!playOnEnter) return;
            PlayOneShot();
        }

        public override void OnContactExit(ContactExitInfo contactInfo)
        {
            if (!playOnExit) return;
            PlayOneShot();
        }

        private void PlayOneShot()
        {
            if (!audioSource)
            {
                Debug.LogWarning("Please assign AudioSource to PlayOnContactEnter component.", this);
                return;
            }

            if (!audioClip)
            {
                Debug.LogWarning("Please assign AudioClip to PlayOnContactEnter component.", this);
                return;
            }

            audioSource.PlayOneShot(audioClip);
        }
    }
}