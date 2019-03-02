using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketDoneButton : MonoBehaviour
{
    public GameObject MarketMenu;
    public BulletInstantiator BulletInstantiator;
    public PlayerController PlayerController;

    public void Pressed()
    {
        MarketMenu.SetActive(false);
        BulletInstantiator.DisableFiring = false;
        PlayerController.DisableMovement = false;
    }
}
