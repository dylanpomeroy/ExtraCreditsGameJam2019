using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketDoneButton : MonoBehaviour
{
    public GameObject MarketMenu;
    public BulletInstantiator BulletInstantiator;

    public void Pressed()
    {
        MarketMenu.SetActive(false);
        BulletInstantiator.DisableFiring = false;
    }
}
