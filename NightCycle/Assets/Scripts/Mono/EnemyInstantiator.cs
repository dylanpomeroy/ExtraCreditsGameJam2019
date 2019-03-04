using ExtensionMethods;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EnemyController;

public class EnemyInstantiator : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Transform EnemyParent;
    public PlayerController PlayerController;
    public CoinInstantiator CoinInstantiator;
    public AudioSource SoundPlayer;

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
            enemyScript.PlayerController = PlayerController;
            enemyScript.CoinInstantiator = CoinInstantiator;

            newEnemy.SetActive(false);
            EnemyPool.Enqueue(newEnemy);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //SpawnEnemies(50);
        }   
    }

    public void SpawnEnemies(int count, Vector2? overrideInstantiationPoints = null)
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

            if (overrideInstantiationPoints == null)
            {
                newEnemy.transform.position = InstantiationPoints.GetRandom().position;
            }
            else
            {
                var cornerPoints = new List<Vector2>
                {
                    overrideInstantiationPoints.Value + new Vector2(-1, -1),
                    overrideInstantiationPoints.Value + new Vector2(-1, 1),
                    overrideInstantiationPoints.Value + new Vector2(1, -1),
                    overrideInstantiationPoints.Value + new Vector2(1, 1),
                };

                newEnemy.transform.position = cornerPoints.GetRandom();
            }

            newEnemy.transform.position = (Vector2)newEnemy.transform.position + new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f));
            newEnemy.transform.position = new Vector3(newEnemy.transform.position.x, newEnemy.transform.position.y, 7);

            var enemyScript = newEnemy.GetComponent<EnemyController>();
            enemyScript.Health = 2;
            enemyScript.typeOfMovement = (MovementType)Random.Range(0, 2);
            enemyScript.SoundPlayer = SoundPlayer;
            enemyScript.PlayerController = PlayerController;

            var maxEnemyType = count / 10 - 1;
            var enemyType = Random.Range(0, maxEnemyType);

            if (enemyType == 0) // normal
            {
                newEnemy.transform.localScale = new Vector2(4, 4);
                enemyScript.Speed = 1;
                enemyScript.Health = 2;
            }
            if (enemyType == 1) // strong
            {
                newEnemy.transform.localScale = new Vector2(5, 4);
                enemyScript.Speed = 1;
                enemyScript.Health = 5;
            }
            else if (enemyType == 2) // fast
            {
                newEnemy.transform.localScale = new Vector2(3, 3);
                enemyScript.Speed = 2;
                enemyScript.Health = 2;
            }

            ActiveEnemies.Enqueue(newEnemy);
        }
    }

    public void SpawnBoss()
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

        newEnemy.transform.position = new Vector3(newEnemy.transform.position.x, newEnemy.transform.position.y, 7);

        var enemyScript = newEnemy.GetComponent<EnemyController>();
        enemyScript.Health = 2;
        enemyScript.typeOfMovement = (MovementType)Random.Range(0, 2);
        enemyScript.SoundPlayer = SoundPlayer;
        enemyScript.EnemyInstantiator = this;
        enemyScript.PlayerController = PlayerController;
        enemyScript.isBoss = true;

        newEnemy.transform.localScale = new Vector2(20, 20);
        enemyScript.Health = 100;
        enemyScript.Speed = 0.5f;
        
        ActiveEnemies.Enqueue(newEnemy);
    }
}
