using System;
using JetBrains.Annotations;
using MisakaLab.Common;
using UdonSharp;
using UnityEngine;
using VRC.SDK3.Data;
using VRC.SDKBase;

#if !COMPILER_UDONSHARP && UNITY_EDITOR
using UnityEditor;
#endif

// ReSharper disable ArrangeObjectCreationWhenTypeEvident
// ReSharper disable UseArrayEmptyMethod
namespace MisakaLab.ObjectPool
{
    [UdonBehaviourSyncMode(BehaviourSyncMode.None)]
    public sealed class MisakaLocalPlayerObjectPool : UdonSharpBehaviour
    {
        public MisakaLocalPlayerObject[] pool = new MisakaLocalPlayerObject[0];
        private readonly DataDictionary _playerObjectDictionary = new DataDictionary();

        private void Start()
        {
            foreach (var o in pool)
            {
                o.gameObject.SetActive(false);
            }

            var players = VRCPlayerApi.GetPlayers();
            foreach (var player in players)
            {
                TakeForPlayer(player);
            }
        }

        public override void OnPlayerJoined(VRCPlayerApi player)
        {
            TakeForPlayer(player);
        }

        public override void OnPlayerLeft(VRCPlayerApi player)
        {
            ReturnForPlayer(player);
        }

        private void TakeForPlayer(VRCPlayerApi player)
        {
            if (_playerObjectDictionary.ContainsKey(player.playerId))
                return;

            if (pool.Length == 0)
            {
                LogWarning(
                    $"Player {player.displayName} (ID: {player.playerId}) requested an object, but the pool is empty.");
                return;
            }

            var playerObject = pool[0];
            pool = pool.RemoveAt(0);
            _playerObjectDictionary[player.playerId] = playerObject;

            playerObject.AssignPlayer(player);
            playerObject.gameObject.SetActive(true);
        }

        private void ReturnForPlayer(VRCPlayerApi player)
        {
            if (!_playerObjectDictionary.TryGetValue(player.playerId, out var playerObject))
            {
                LogWarning(
                    $"Player {player.displayName} (ID: {player.playerId}) tried to return an object, but doesn't have one.");
                return;
            }

            var go = (MisakaLocalPlayerObject)playerObject.Reference;
            go.ReturnToPool();
            go.gameObject.SetActive(false);

            pool = pool.Add(go);
            _playerObjectDictionary.Remove(player.playerId);
        }

        [CanBeNull]
        public MisakaLocalPlayerObject _GetPlayerObject(int playerId)
        {
            if (!_playerObjectDictionary.TryGetValue(playerId, out var playerObject))
                return null;

            return (MisakaLocalPlayerObject)playerObject.Reference;
        }

        private void LogWarning(string msg)
        {
            Debug.LogWarning($"[{nameof(MisakaLocalPlayerObjectPool)}] [{gameObject.name}] {msg}");
        }

#if !COMPILER_UDONSHARP && UNITY_EDITOR
        public void SetupPool(
            Func<int, MisakaLocalPlayerObject> objectFactory,
            int count
        )
        {
            foreach (var oldObject in pool)
            {
                DestroyImmediate(oldObject.gameObject);
            }

            var newPoolArray = new MisakaLocalPlayerObject[count];
            for (var index = 0; index < count; index++)
            {
                var playerObject = objectFactory(index);
                newPoolArray[index] = playerObject;
            }

            pool = newPoolArray;
            EditorUtility.SetDirty(this);
        }
#endif
    }
}