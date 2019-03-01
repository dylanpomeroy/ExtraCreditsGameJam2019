using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoController : MonoBehaviour
{
    public int AmmoBalance { get; private set; }

    public void AddAmmo(int amount)
    {
        AmmoBalance += amount;
        this.UpdateAmmoBalanceText();
    }

    public void SutractAmmo(int amount)
    {
        this.AddAmmo(-amount);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            this.AddAmmo(25);
        }
    }

    private void UpdateAmmoBalanceText()
    {
        GetComponent<Text>().text = $"Ammo: x{AmmoBalance}";
    }
}
