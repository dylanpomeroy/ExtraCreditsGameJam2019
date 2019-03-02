using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseMachinegun : MonoBehaviour
{
    public MoneyController MoneyController;
    public WeaponsController WeaponsController;

    public void Pressed()
    {
        WeaponsController.PurchaseMachinegun();
    }

    void Update()
    {
        if (WeaponsController.MachinegunPurchased)
        {
            GetComponentInChildren<Text>().text = "Already\nPurchased";
            GetComponent<Button>().interactable = false;
            return;
        }

        if (MoneyController.CanAfford(WeaponsController.MachineGunPurchasePrice))
        {
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
