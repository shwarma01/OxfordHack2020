using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    private GameObject enemyPrefab;
    // [SerializeField] private GameObject[] spawnPoints;
    // private GridGraph gridGraph = AstarPath.active.data.gridGraph;
    [SerializeField] private float timeBetweenSpawns = 0.5f;
    // [SerializeField] private float spawnRandomFactor = 0.3f;
    [SerializeField] private int numberOfEnemies = 5;
    // Does nothing right now
    [SerializeField] private float moveSpeed = 2f;

    public GameObject GetEnemyPrefab() {
        enemyPrefab = enemyPrefabs[Random.Range(0, 2)];
        return enemyPrefab; 
    }

    public Vector3 FindWalkablePointInArea()
    {
       return new Vector3(1f, 1f, 1f);
    }

    void Start()
    {

    }

    public Vector3 GetSpawnPoint()
    {   
        var gridGraph = AstarPath.active.data.gridGraph;
        var walkable = false;
        GridNode node = gridGraph.nodes[0];
        while (!walkable)
        {
            node = gridGraph.nodes[Random.Range(0, gridGraph.nodes.Length)];
            walkable = node.Walkable;
        }
        return (Vector3)node.position;
    }

    public float GetTimeBetweenSpawns() { return timeBetweenSpawns; }

    // public float GetSpawnRandomFactor() { return spawnRandomFactor; }

    public int GetNumberOfEnemies() { return numberOfEnemies; }

    public float GetMoveSpeed() { return moveSpeed; }

}
