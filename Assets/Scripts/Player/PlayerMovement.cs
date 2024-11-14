using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    float speedx;
    float speedy;
    Rigidbody2D rb;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        speedx = Input.GetAxisRaw("Horizontal") * movementSpeed;
        speedy = Input.GetAxisRaw("Vertical") * movementSpeed;
    }
    private void FixedUpdate()
    {
        rb.velocity = new Vector2(speedx, speedy);
    }
}
