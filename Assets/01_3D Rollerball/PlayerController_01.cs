using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController_01 : MonoBehaviour
{
    public float speed;

    public Action onCollectCoin;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.name);

        if (col.gameObject.GetComponent<CoinController_01>() != null)
        {
            GameObject.Destroy(col.gameObject);

            onCollectCoin();
        }
    }
}