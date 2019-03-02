using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketMenu : MonoBehaviour
{
    public BulletInstantiator BulletInstantiator;

    void Update()
    {
        BulletInstantiator.DisableFiring = true;
    }
}
