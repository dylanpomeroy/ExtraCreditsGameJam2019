using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyController : MonoBehaviour
{
    public int MoneyBalance { get; private set; }

    public void AddMoney(int amount)
    {
        MoneyBalance += amount;
        this.UpdateMoneyBalanceText();
    }

    public void SubtractMoney(int amount)
    {
        MoneyBalance -= amount;
        this.UpdateMoneyBalanceText();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            this.AddMoney(100);
        }
    }

    private void UpdateMoneyBalanceText()
    {
        GetComponent<Text>().text = $"Money: ${MoneyBalance}";
    }
}
