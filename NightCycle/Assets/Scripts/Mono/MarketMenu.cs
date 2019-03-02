using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketMenu : MonoBehaviour
{
    public BulletInstantiator BulletInstantiator;
    public PlayerController PlayerController;

    void Update()
    {
        BulletInstantiator.DisableFiring = true;
        PlayerController.DisableMovement = true;
    }
}
