using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject theEnemy;
    public int xPos;
    public int zPos;
    public float spawnInterval = 1.5f;
    private int enemyCount;

    void Start()
    {
        StartCoroutine(EnemyDrop());
    }
    IEnumerator EnemyDrop()
    {
        while (enemyCount < 50)
        {
            xPos = Random.Range(1, 12);
            zPos = Random.Range(1, 31);
            Instantiate(theEnemy, new Vector3(xPos, 1, zPos), Quaternion.identity);
            enemyCount++;

            yield return new WaitForSeconds(spawnInterval);
        }
        
    }
}
