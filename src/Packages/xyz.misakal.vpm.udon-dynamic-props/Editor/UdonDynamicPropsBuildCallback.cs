using VRC.SDKBase.Editor.BuildPipeline;

namespace UdonDynamicProps.Editor
{
    internal sealed class UdonDynamicPropsBuildCallback : IVRCSDKBuildRequestedCallback
    {
        public int callbackOrder => 0;

        public bool OnBuildRequested(VRCSDKRequestedBuildType requestedBuildType)
        {
            Setup();
            return true;
        }

        private static void Setup()
        {
            SetupUdonDynamicProps.Setup();
        }
    }
}