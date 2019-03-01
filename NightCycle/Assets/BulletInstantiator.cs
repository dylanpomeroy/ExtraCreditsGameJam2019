using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInstantiator : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform BulletParent;
    public Transform InstantiationPoint;

    public enum GunType
    {
        Pistol,
        Shotgun,
        MachineGun
    }

    private GunType gunSelected;
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

        for (var i = 0; i < BulletPoolSize; i++)
        {
            var newBullet = Instantiate(BulletPrefab, BulletParent);
            newBullet.SetActive(false);
            BulletPool.Enqueue(newBullet);
        }
    }

    private void Update()
    {

        timeUntilCanFire[GunType.Pistol] -= Time.deltaTime;
        timeUntilCanFire[GunType.Shotgun] -= Time.deltaTime;
        timeUntilCanFire[GunType.MachineGun] -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            gunSelected = GunType.Pistol;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            gunSelected = GunType.Shotgun;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            gunSelected = GunType.MachineGun;
        }

        if (Input.GetMouseButton(0))
        {
            TryShoot(gunSelected);
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
    }

    private void ShootShotgun()
    {
        for (var i = 0; i < 5; i++)
        {
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
            newBullet.transform.Rotate(new Vector3(0, 0, Random.Range(-10f, 10f)));
            newBullet.transform.position = new Vector3(newBullet.transform.position.x, newBullet.transform.position.y, 0);

            ActiveBullets.Enqueue(newBullet);
        }
    }

    private void ShootMachineGun()
    {
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
    }
}
