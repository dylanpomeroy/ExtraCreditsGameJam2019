﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public MoneyController MoneyController;

    public float speed;
    public bool DisableMovement;

    public int Health = 100;

    public void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health <= 0)
        {
            Debug.Log("You died.");
            Time.timeScale = 0;
        }
    }

    void Start()
    {
        if (speed == 0f) speed = 1;
    }

    void Update()
    {
        if (DisableMovement)
            return;

        var moveVector = Vector2.zero;
        if (Input.GetKey(KeyCode.W))
            moveVector += Vector2.up;
        if (Input.GetKey(KeyCode.S))
            moveVector += Vector2.down;
        if (Input.GetKey(KeyCode.A))
            moveVector += Vector2.left;
        if (Input.GetKey(KeyCode.D))
            moveVector += Vector2.right;

        moveVector = moveVector.normalized * speed * Time.deltaTime;

        GetComponent<CharacterController>().Move(moveVector);
    }

    private void LateUpdate()
    {
        objectIdsAlreadyHit.Clear();
    }

    private List<int> objectIdsAlreadyHit = new List<int>();
    public void HandleCollision(Collider2D collision)
    {
        Debug.Log($"Hit {collision.gameObject.GetInstanceID()}");

        if (objectIdsAlreadyHit.Contains(collision.gameObject.GetInstanceID()))
            return;

        objectIdsAlreadyHit.Add(collision.gameObject.GetInstanceID());

        if (collision.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Hit coin");
            MoneyController.AddMoney(collision.gameObject.GetComponent<CoinController>().Value);
            CoinInstantiator.DestroyCoin(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit enemy");
            GetComponent<CharacterController>().Move((transform.position - collision.transform.position).normalized * Time.deltaTime * 10);
            TakeDamage(10);
        }
    }
}
