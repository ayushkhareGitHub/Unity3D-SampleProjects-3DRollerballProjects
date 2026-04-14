using UnityEngine;
using System;

public class PlayerController_02 : MonoBehaviour
{
    public float speed = 10f;
    public bool canMove = false;

    public Action onCoinCollected;

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

        rb.velocity = new Vector3(
            movement.x * speed,
            rb.velocity.y,
            movement.z * speed
        );
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.GetComponent<CoinController_02>() != null)
        {
            Destroy(col.gameObject);
            onCoinCollected?.Invoke();
        }
    }
}