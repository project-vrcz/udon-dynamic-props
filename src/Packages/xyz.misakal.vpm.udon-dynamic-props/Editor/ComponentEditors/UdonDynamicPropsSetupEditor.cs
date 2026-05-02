using UdonDynamicProps.Runtime;
using UnityEditor;
using UnityEngine;

namespace UdonDynamicProps.Editor.ComponentEditors
{
    [CustomEditor(typeof(UdonDynamicPropsSetup))]
    public sealed class UdonDynamicPropsSetupEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var behaviour = (UdonDynamicPropsSetup)target;

            base.OnInspectorGUI();

            if (GUILayout.Button("Setup"))
            {
                SetupUdonDynamicProps.Setup(behaviour);
            }
        }
    }
}