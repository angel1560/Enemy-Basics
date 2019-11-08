using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemySpawn : MonoBehaviour
{
    public int enemiesCountToSpawn = 7;
    public float spawnRoutineDelay = 1.5f;
    public EnemyProfile enemyToSpawn;
    public List<Transform> wayPoints = new List<Transform>();
    
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < enemiesCountToSpawn; i++)
        {
            GameObject enemyObject = Instantiate(enemyToSpawn.enemyPrefab, transform.position, Quaternion.identity);
            EnemyBehaviour enemyBehaviour = enemyObject.GetComponent<EnemyBehaviour>();
            
            enemyBehaviour.wayPoints = wayPoints; 
            enemyBehaviour.InitEnemyProfile(enemyToSpawn);

            yield return new WaitForSeconds(spawnRoutineDelay);
        }
    }
}