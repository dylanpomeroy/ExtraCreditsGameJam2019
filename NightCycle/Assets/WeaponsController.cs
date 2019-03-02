using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsController : MonoBehaviour
{
    public MoneyController MoneyController;
    public AmmoController AmmoController;

    public int PistolPurchasePrice;
    public int ShotgunPurchasePrice;
    public int MachineGunPurchasePrice;

    public int AmmoAmountPerPurchase;
    public int AmmoPurchasePrice;

    public bool PistolPurchased;
    public bool ShotgunPurchased;
    public bool MachinegunPurchased;

    public void PurchasePistol()
    {
        if (PistolPurchased || !MoneyController.CanAfford(PistolPurchasePrice))
        {
            return;
        }

        MoneyController.SubtractMoney(PistolPurchasePrice);
        PistolPurchased = true;
    }

    public void PurchaseShotgun()
    {
        if (ShotgunPurchased || !MoneyController.CanAfford(ShotgunPurchasePrice))
        {
            return;
        }

        MoneyController.SubtractMoney(ShotgunPurchasePrice);
        ShotgunPurchased = true;
    }

    public void PurchaseMachinegun()
    {
        if (MachinegunPurchased || !MoneyController.CanAfford(MachineGunPurchasePrice))
        {
            return;
        }

        MoneyController.SubtractMoney(MachineGunPurchasePrice);
        MachinegunPurchased = true;
    }

    public void PurchaseAmmo()
    {
        if (!MoneyController.CanAfford(AmmoPurchasePrice))
        {
            return;
        }

        MoneyController.SubtractMoney(AmmoPurchasePrice);
        AmmoController.AddAmmo(AmmoAmountPerPurchase);
    }
}
