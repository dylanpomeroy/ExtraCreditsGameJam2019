using ExtensionMethods;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyInstantiator : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Transform EnemyParent;
    public GameObject Player;
    public CoinInstantiator CoinInstantiator;

    public GameObject InstantiationPointsParent;
    private List<Transform> InstantiationPoints;

    public int EnemyPoolSize;

    private static Queue<GameObject> EnemyPool;
    private static Queue<GameObject> ActiveEnemies;
    public static int ActuallyActiveEnemyCount;

    public static void DestroyEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        EnemyPool.Enqueue(enemy);

        ActuallyActiveEnemyCount--;
    }

    void Start()
    {
        InstantiationPoints = InstantiationPointsParent.GetComponentsInChildren<Transform>().ToList();

        EnemyPool = new Queue<GameObject>();
        ActiveEnemies = new Queue<GameObject>();

        for (var i = 0; i < EnemyPoolSize; i++)
        {
            var newEnemy = Instantiate(EnemyPrefab, EnemyParent);

            var enemyScript = newEnemy.GetComponent<EnemyController>();
            enemyScript.Player = Player;
            enemyScript.CoinInstantiator = CoinInstantiator;
            
            newEnemy.SetActive(false);
            EnemyPool.Enqueue(newEnemy);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SpawnEnemies(50);
        }   
    }

    public void SpawnEnemies(int count)
    {
        for (var i = 0; i < count; i++)
        {
            GameObject newEnemy;
            if (EnemyPool.Count == 0)
            {
                newEnemy = ActiveEnemies.Dequeue();
                
                // inactive enemies are ones we put back in the enemy pool
                // these are leftover entries we should ignore
                while (!newEnemy.activeSelf)
                    newEnemy = ActiveEnemies.Dequeue();
            }
            else
            {
                newEnemy = EnemyPool.Dequeue();
                ActuallyActiveEnemyCount++;
            }

            newEnemy.SetActive(true);
            newEnemy.transform.position = InstantiationPoints.GetRandom().position;
            newEnemy.transform.position = (Vector2)newEnemy.transform.position + new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));

            newEnemy.transform.position = new Vector3(newEnemy.transform.position.x, newEnemy.transform.position.y, 0);

            ActiveEnemies.Enqueue(newEnemy);
        }
    }
}
