using UnityEngine;

[CreateAssetMenu(fileName = "MainRepo", menuName = "Data/MainRepo")]
public class GameRepository : ScriptableObject
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;
}