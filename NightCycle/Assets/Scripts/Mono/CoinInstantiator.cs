using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinInstantiator : MonoBehaviour
{
    public MoneyController MoneyController;
    public GameObject CoinPrefab;
    public Transform CoinParent;
    public int CoinPoolSize;
    public PlayerController PlayerController;
    public EnemyInstantiator EnemyInstantiator;

    public static Queue<GameObject> CoinPool;
    public static Queue<GameObject> ActiveCoins;
    public static int ActuallyActiveCoinCount
    {
        get
        {
            return actuallyActiveCoinCount;
        }
        set
        {
            // Debug.Log($"{nameof(ActuallyActiveCoinCount)} being set to: {value}");
            actuallyActiveCoinCount = value;
        }
    }

    private static int actuallyActiveCoinCount;

    public static void DestroyCoin(GameObject coin)
    {
        coin.SetActive(false);
        CoinPool.Enqueue(coin);

        ActuallyActiveCoinCount--;
    }

    public void InstantiateCoin(Vector2 position, bool moveTowardsPlayerWhenStageEnds)
    {
        GameObject newCoin;
        if (CoinPool.Count == 0)
        {
            newCoin = ActiveCoins.Dequeue();

            // inactive coints are ones we put back in the coin pool
            // these are leftover entries we should ignore
            while (!newCoin.activeSelf)
                newCoin = ActiveCoins.Dequeue();
        }
        else
        {
            newCoin = CoinPool.Dequeue();
            ActuallyActiveCoinCount++;
        }

        newCoin.SetActive(true);
        newCoin.transform.position = position;
        newCoin.transform.position = new Vector3(newCoin.transform.position.x, newCoin.transform.position.y, 7);

        var coinScript = newCoin.GetComponent<CoinController>();
        coinScript.PlayerController = PlayerController;
        coinScript.EnemyInstantiator = EnemyInstantiator;
        coinScript.MoveToPlayerWhenStageEnds = moveTowardsPlayerWhenStageEnds;

        ActiveCoins.Enqueue(newCoin);
    }

    public void Start()
    {
        CoinPool = new Queue<GameObject>();
        ActiveCoins = new Queue<GameObject>();

        for (var i = 0; i < CoinPoolSize; i++)
        {
            var newCoin = Instantiate(CoinPrefab, CoinParent);
            newCoin.SetActive(false);
            CoinPool.Enqueue(newCoin);
        }
    }
}
