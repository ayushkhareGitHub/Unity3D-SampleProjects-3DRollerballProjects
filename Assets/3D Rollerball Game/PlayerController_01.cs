using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController_01 : MonoBehaviour
{
    public float speed = 10f;

    public bool canMove = false;

    public Action onCollectCoin;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!canMove)
        {
            rb.velocity = Vector3.zero;
            return;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Better controlled movement than endless AddForce
        rb.velocity = new Vector3(
            movement.x * speed,
            rb.velocity.y,
            movement.z * speed
        );
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.name);

        if (col.gameObject.GetComponent<CoinController_01>() != null)
        {
            Destroy(col.gameObject);

            onCollectCoin?.Invoke();
        }
    }
}