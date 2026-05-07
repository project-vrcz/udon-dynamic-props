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

        public HumanBodyBones referenceLeftUpperLegBone = HumanBodyBones.LeftUpperLeg;
        public HumanBodyBones referenceRightUpperLegBone = HumanBodyBones.RightUpperLeg;
        public VRCPlayerApi.TrackingDataType originTrackingDataType = VRCPlayerApi.TrackingDataType.Origin;

        public Vector3 additionalOffset;
        public float maxAutoAdjustAllowedEyeHeight = 1.4f;

        private Vector3 _initialLocalPosition;
        [CanBeNull] private VRCPlayerApi _playerInStation;

        private void Start()
        {
            _initialLocalPosition = stationEnterPosition.localPosition;
        }

        public override void OnStationEntered(VRCPlayerApi player)
        {
            _playerInStation = player;
            BeginUpdateStationPosition();
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

            BeginUpdateStationPosition();
        }

        public override void OnAvatarEyeHeightChanged(VRCPlayerApi player, float prevEyeHeightAsMeters)
        {
            if (_playerInStation == null || player.playerId != _playerInStation.playerId)
                return;

            BeginUpdateStationPosition();
        }

        private void BeginUpdateStationPosition()
        {
            if (_playerInStation == null || !Utilities.IsValid(_playerInStation))
                return;

            if (_playerInStation.GetAvatarEyeHeightAsMeters() > maxAutoAdjustAllowedEyeHeight)
                return;

            UpdateStationPositionFromEyeHeight();
            SendCustomEventDelayedSeconds(nameof(_UpdateStationPosition), 1.2f);
        }

        private void UpdateStationPositionFromEyeHeight()
        {
            if (_playerInStation == null || !Utilities.IsValid(_playerInStation))
                return;

            var eyeHeight = _playerInStation.GetAvatarEyeHeightAsMeters();
            var scaledOffset = InverseScale(
                new Vector3(0f, -(eyeHeight / 2), 0f) + additionalOffset,
                stationEnterPosition.lossyScale
            );

            stationEnterPosition.transform.localPosition = _initialLocalPosition + scaledOffset;
        }

        public void _UpdateStationPosition()
        {
            if (_playerInStation == null || !Utilities.IsValid(_playerInStation))
                return;

            var leftBonePosition = _playerInStation.GetBonePosition(referenceLeftUpperLegBone);
            var rightBonePosition = _playerInStation.GetBonePosition(referenceRightUpperLegBone);
            var bonePosition = ((leftBonePosition + rightBonePosition) / 2f);

            var originPosition = _playerInStation.GetTrackingData(originTrackingDataType).position;
            var boneOffset = originPosition - bonePosition;
            var offset = new Vector3(0, boneOffset.y, 0) + additionalOffset;
            var scaledOffset = InverseScale(offset, stationEnterPosition.lossyScale);

            stationEnterPosition.transform.localPosition = _initialLocalPosition + scaledOffset;
        }

        private static Vector3 InverseScale(Vector3 a, Vector3 scale)
        {
            return new Vector3(
                a.x / scale.x,
                a.y / scale.y,
                a.z / scale.z
            );
        }
    }
}