using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        if (speed == 0f) speed = 1;
    }

    // Update is called once per frame
    void Update()
    {
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

        transform.position += new Vector3(moveVector.x, moveVector.y);
    }
}
