using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseAmmo : MonoBehaviour
{
    public MoneyController MoneyController;
    public WeaponsController WeaponsController;

    public void Pressed()
    {
        WeaponsController.PurchaseAmmo();
    }

    void Update()
    {
        if (MoneyController.CanAfford(WeaponsController.AmmoPurchasePrice))
        {
            GetComponent<Button>().interactable = true;
        }        
        else
        {
            GetComponent<Button>().interactable = false;
        }
    }
}
