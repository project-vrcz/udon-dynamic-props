using JetBrains.Annotations;
using UdonSharp;
using UnityEngine;
using VRC.SDKBase;

// ReSharper disable ArrangeObjectCreationWhenTypeEvident
namespace UdonDynamicProps.Runtime.Components
{
    public sealed class AdaptableStationHeight : UdonSharpBehaviour
    {
        public Transform stationEnterPosition;

        public HumanBodyBones referenceBone = HumanBodyBones.LeftUpperLeg;
        public VRCPlayerApi.TrackingDataType originTrackingDataType = VRCPlayerApi.TrackingDataType.Origin;

        public Vector3 additionalOffset;

        private Vector3 _initialLocalPosition;
        [CanBeNull] private VRCPlayerApi _playerInStation;

        private void Start()
        {
            _initialLocalPosition = stationEnterPosition.localPosition;
        }

        public override void OnStationEntered(VRCPlayerApi player)
        {
            _playerInStation = player;
            UpdateStationPosition();
        }

        public override void OnStationExited(VRCPlayerApi player)
        {
            _playerInStation = null;
            stationEnterPosition.localPosition = _initialLocalPosition;
        }

        public override void OnAvatarChanged(VRCPlayerApi player)
        {
            if (_playerInStation == null || player.playerId != _playerInStation.playerId)
                return;

            UpdateStationPosition();
        }

        public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float prevEyeHeightAsMeters)
        {
            if (_playerInStation == null || player.playerId != _playerInStation.playerId)
                return;

            UpdateStationPosition();
        }

        private void UpdateStationPosition()
        {
            if (_playerInStation == null || !Utilities.IsValid(_playerInStation))
                return;

            var bonePosition = _playerInStation.GetBonePosition(referenceBone);
            var originPosition = _playerInStation.GetTrackingData(originTrackingDataType).position;
            var offset = originPosition - bonePosition + additionalOffset;
            var scaledOffset = new Vector3(
                offset.x / stationEnterPosition.lossyScale.x,
                offset.y / stationEnterPosition.lossyScale.y,
                offset.z / stationEnterPosition.lossyScale.z
            );

            stationEnterPosition.transform.localPosition = _initialLocalPosition + scaledOffset;
        }
    }
}