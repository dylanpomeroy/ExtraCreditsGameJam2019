using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchasePistol : MonoBehaviour
{
    public MoneyController MoneyController;
    public WeaponsController WeaponsController;

    public void Pressed()
    {
        WeaponsController.PurchasePistol();
    }

    void Update()
    {
        if (WeaponsController.PistolPurchased)
        {
            GetComponentInChildren<Text>().text = "Already\nPurchased";
            GetComponent<Button>().interactable = false;
            return;
        }

        if (MoneyController.CanAfford(WeaponsController.PistolPurchasePrice))
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
