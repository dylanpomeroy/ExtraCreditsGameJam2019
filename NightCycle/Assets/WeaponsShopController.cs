using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static BulletInstantiator;

public class WeaponsShopController : MonoBehaviour
{
    public MoneyController MoneyController;
    public AmmoController AmmoController;
    public BulletInstantiator BulletInstantiator;

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
        BulletInstantiator.GunSelected = GunType.Pistol;
    }

    public void PurchaseShotgun()
    {
        if (ShotgunPurchased || !MoneyController.CanAfford(ShotgunPurchasePrice))
        {
            return;
        }

        MoneyController.SubtractMoney(ShotgunPurchasePrice);
        ShotgunPurchased = true;
        BulletInstantiator.GunSelected = GunType.Shotgun;
    }

    public void PurchaseMachinegun()
    {
        if (MachinegunPurchased || !MoneyController.CanAfford(MachineGunPurchasePrice))
        {
            return;
        }

        MoneyController.SubtractMoney(MachineGunPurchasePrice);
        MachinegunPurchased = true;
        BulletInstantiator.GunSelected = GunType.MachineGun;
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
