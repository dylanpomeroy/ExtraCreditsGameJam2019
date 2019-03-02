﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed;
    public int health;

    public CoinInstantiator CoinInstantiator; 
    public GameObject Player;

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, Time.deltaTime * Speed);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            HandleBulletCollision(other);
        }
    }

    private void HandleBulletCollision(Collider2D bulletCollider)
    {
        transform.position += (transform.position - Player.transform.position).normalized * 10 * Time.deltaTime;

        BulletInstantiator.DestroyBullet(bulletCollider.gameObject);

        health--;
        if (health <= 0)
            HandleDeath();
    }

    private void HandleDeath()
    {
        CoinInstantiator.InstantiateCoin(transform.position);

        EnemyInstantiator.DestroyEnemy(this.gameObject);
    }
}
