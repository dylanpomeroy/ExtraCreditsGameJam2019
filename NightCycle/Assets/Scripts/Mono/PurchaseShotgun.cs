using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseShotgun : MonoBehaviour
{
    public MoneyController MoneyController;
    public WeaponsShopController WeaponsController;

    public void Pressed()
    {
        WeaponsController.PurchaseShotgun();
    }

    void Update()
    {
        if (WeaponsController.ShotgunPurchased)
        {
            GetComponentInChildren<Text>().text = "Already\nPurchased";
            GetComponent<Button>().interactable = false;
            return;
        }

        if (MoneyController.CanAfford(WeaponsController.ShotgunPurchasePrice))
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
