using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageController : MonoBehaviour
{
    public CoinInstantiator CoinInstantiator;
    public GameObject MarketMenu;

    private bool started;
    private bool completed;

    private void Update()
    {
        CheckCompletion();

        if (started && !completed)
            return;
        if (!completed)
        {
            LastStart();
            started = true;
        }
        else
        {
            MarketMenu.SetActive(true);
        }
    }

    private void LastStart()
    {
        for (var i = 0; i < 19; i++)
        {
            var randomVector = new Vector2(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            var coinPosition = (Vector2)transform.position + randomVector;

            CoinInstantiator.InstantiateCoin(coinPosition);
        }
    }

    private void CheckCompletion()
    {
        if (!started || completed)
            return;

        if (CoinInstantiator.ActuallyActiveCoinCount == 0)
        {
            completed = true;
        }
    }
}
