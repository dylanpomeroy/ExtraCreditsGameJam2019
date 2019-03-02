using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    public int Value;
    public MoneyController MoneyController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Player"))
            return;

        MoneyController.AddMoney(Value);
        CoinInstantiator.DestroyCoin(this.gameObject);
    }
}
