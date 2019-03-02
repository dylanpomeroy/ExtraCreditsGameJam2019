using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInstantiator : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform BulletParent;
    public Transform InstantiationPoint;
    public AmmoController AmmoController;
    public WeaponsShopController WeaponsController;

    public bool DisableFiring;

    public enum GunType
    {
        Pistol,
        Shotgun,
        MachineGun
    }

    public GunType GunSelected;
    private Dictionary<GunType, float> secondsAllowedBetweenBullets = new Dictionary<GunType, float>
    {
        { GunType.Pistol, 0.3f },
        { GunType.Shotgun, 0.7f },
        { GunType.MachineGun, 0.1f },
    };

    private Dictionary<GunType, float> timeUntilCanFire = new Dictionary<GunType, float>
    {
        { GunType.Pistol, 0.0f },
        { GunType.Shotgun, 0.0f },
        { GunType.MachineGun, 0.0f },
    };

    public int BulletPoolSize;

    private static Queue<GameObject> BulletPool;
    private static Queue<GameObject> ActiveBullets;

    public static void DestroyBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        BulletPool.Enqueue(bullet);
    }

    private void Start()
    {
        BulletPool = new Queue<GameObject>();
        ActiveBullets = new Queue<GameObject>();
        DisableFiring = false;

        for (var i = 0; i < BulletPoolSize; i++)
        {
            var newBullet = Instantiate(BulletPrefab, BulletParent);
            newBullet.SetActive(false);
            BulletPool.Enqueue(newBullet);
        }
    }

    private void Update()
    {
        if (DisableFiring)
            return;

        timeUntilCanFire[GunType.Pistol] -= Time.deltaTime;
        timeUntilCanFire[GunType.Shotgun] -= Time.deltaTime;
        timeUntilCanFire[GunType.MachineGun] -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Alpha1) && WeaponsController.PistolPurchased)
        {
            GunSelected = GunType.Pistol;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && WeaponsController.ShotgunPurchased)
        {
            GunSelected = GunType.Shotgun;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && WeaponsController.MachinegunPurchased)
        {
            GunSelected = GunType.MachineGun;
        }

        if (Input.GetMouseButton(0))
        {
            TryShoot(GunSelected);
        }
    }

    public void TryShoot(GunType gun)
    {
        if (timeUntilCanFire[gun] > 0f)
        {
            return;
        }

        switch (gun)
        {
            case GunType.Pistol:
                ShootPistol();
                break;
            case GunType.Shotgun:
                ShootShotgun();
                break;
            case GunType.MachineGun:
                ShootMachineGun();
                break;
        }

        timeUntilCanFire[gun] = secondsAllowedBetweenBullets[gun];
    }

    private void ShootPistol()
    {
        if (AmmoController.AmmoBalance < 1)
        {
            return;
        }

        GameObject newBullet;
        if (BulletPool.Count == 0)
        {
            newBullet = ActiveBullets.Dequeue();
        }
        else
        {
            newBullet = BulletPool.Dequeue();
        }

        newBullet.SetActive(true);
        newBullet.transform.position = InstantiationPoint.position;
        newBullet.transform.rotation = InstantiationPoint.rotation;
        newBullet.transform.position = new Vector3(newBullet.transform.position.x, newBullet.transform.position.y, 0);

        ActiveBullets.Enqueue(newBullet);
        AmmoController.SutractAmmo(1);
    }

    private void ShootShotgun()
    {
        var bulletsInShot = 5;

        if (AmmoController.AmmoBalance < bulletsInShot)
        {
            return;
        }

        for (var i = 0; i < bulletsInShot; i++)
        {
            GameObject newBullet;
            if (BulletPool.Count == 0)
            {
                newBullet = ActiveBullets.Dequeue();

                // inactive bullets are ones we put back in the bullet pool
                // these are leftover entries we should ignore
                while (!newBullet.activeSelf)
                    newBullet = ActiveBullets.Dequeue();
            }
            else
            {
                newBullet = BulletPool.Dequeue();
            }

            newBullet.SetActive(true);
            newBullet.transform.position = InstantiationPoint.position;
            newBullet.transform.rotation = InstantiationPoint.rotation;
            newBullet.transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(-10f, 10f)));
            newBullet.transform.position = new Vector3(newBullet.transform.position.x, newBullet.transform.position.y, 0);

            ActiveBullets.Enqueue(newBullet);
        }

        AmmoController.SutractAmmo(bulletsInShot);
    }

    private void ShootMachineGun()
    {
        if (AmmoController.AmmoBalance < 1)
        {
            return;
        }

        GameObject newBullet;
        if (BulletPool.Count == 0)
        {
            newBullet = ActiveBullets.Dequeue();
        }
        else
        {
            newBullet = BulletPool.Dequeue();
        }

        newBullet.SetActive(true);
        newBullet.transform.position = InstantiationPoint.position;
        newBullet.transform.rotation = InstantiationPoint.rotation;
        newBullet.transform.Rotate(new Vector3(0, 0, UnityEngine.Random.Range(-5f, 5f)));
        newBullet.transform.position = new Vector3(newBullet.transform.position.x, newBullet.transform.position.y, 0);

        ActiveBullets.Enqueue(newBullet);
        AmmoController.SutractAmmo(1);
    }
}
