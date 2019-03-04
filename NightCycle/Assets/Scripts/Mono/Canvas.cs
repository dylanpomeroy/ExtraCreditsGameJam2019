using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    public GameObject MarketMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //MarketMenu.SetActive(true);
        }
    }
}
