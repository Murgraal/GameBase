using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Murgraal.Gamebase
{
    public static class GameSession
    {
        private const string RepoPath = "ScriptableObjects/MainRepo";
        
        private static Dictionary<int,CombatData> entityCombatDatas = new Dictionary<int, CombatData>();
        private static Dictionary<int,LocationData> entityLocationDatas = new Dictionary<int, LocationData>();

        private static GameRepository mainRepository;

        private static int _playerInstanceID;

        public class CoroutineRunner : MonoBehaviour {}
        public static CoroutineRunner Runner; 

        [RuntimeInitializeOnLoadMethod]
        public static void GameInit()
        {
            mainRepository = Resources.Load<GameRepository>(RepoPath);
            var go = GameObject.Instantiate(mainRepository.playerPrefab);
            go.transform.position = Vector3.zero;
            new GameObject().AddComponent<InputHandlerMono>();
            _playerInstanceID = go.GetInstanceID();
            CurrentGameState = GameState.GamePlay;
        }
    
        public static GameState CurrentGameState;

        public static bool ShouldUpdateEntities => CurrentGameState == GameState.GamePlay;

        public enum GameState
        {
            MainMenu,
            GamePlay,
            LevelLoaded,
            Interact,
        }

        private static void SpawnPlenty(int count, GameObject prefab)
        {
            for (int i = 0; i < count; i++)
            {
                var pos = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f));
                var go = GameObject.Instantiate(prefab);
                go.transform.position = pos;
            }
        }
    
        public static void AddEntityData(int instanceID,CombatData combatData, LocationData  locationData)
        {
            entityCombatDatas.Add(instanceID,combatData);
            entityLocationDatas.Add(instanceID,locationData);
        }

        public static void UpdateLocationData(int instanceID,LocationData locationData)
        {
            
            entityLocationDatas[instanceID] = locationData;
        }

        public static void UpdateCombatData(int instanceID, CombatData combatData)
        {
            entityCombatDatas[instanceID] = combatData;
        }

        public static CombatData PlayerCombatData => GetEntityCombatData(_playerInstanceID);
        public static LocationData PlayerLocationData => GetEntityLocationData(_playerInstanceID);
    
        public static CombatData GetEntityCombatData(int instanceID)
        {
            if (entityCombatDatas.ContainsKey(instanceID))
            {
                return entityCombatDatas[instanceID];
            }
            return new CombatData();
        }
    
        public static LocationData GetEntityLocationData(int instanceID)
        {
            if (entityLocationDatas.ContainsKey(instanceID))
            {
                var locationData = entityLocationDatas[instanceID];
                return locationData;
            }
            return new LocationData();

        }
    }
}