using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    private Vector2 movementInput;
    private Rigidbody2D rb;
    private bool extralife;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePosition;
    [Range(0.1f, 2f)]
    [SerializeField] private float fireRate = 0.5f;
    private float fireTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movementInput = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) movementInput.y = 1;
        if (Input.GetKey(KeyCode.S)) movementInput.y = -1;
        if (Input.GetKey(KeyCode.A)) movementInput.x = -1;
        if (Input.GetKey(KeyCode.D)) movementInput.x = 1;

        if (Input.GetKey(KeyCode.UpArrow))
            RotateToFaceDirection(Vector2.up);
        else if (Input.GetKey(KeyCode.DownArrow))
            RotateToFaceDirection(Vector2.down);
        else if (Input.GetKey(KeyCode.LeftArrow))
            RotateToFaceDirection(Vector2.left);
        else if (Input.GetKey(KeyCode.RightArrow))
            RotateToFaceDirection(Vector2.right);

        if (Input.GetKey(KeyCode.Space) && fireTimer <= 0f)
        {
            Shoot();
            fireTimer = fireRate;
        }
        else
        {
            fireTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = movementInput.normalized * movementSpeed;
    }

    public void Shoot()
    {
        Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
    }

    private void RotateToFaceDirection(Vector2 direction)
    {
        float targetAngle = 0;

        if (direction == Vector2.up)
            targetAngle = 0;
        else if (direction == Vector2.down)
            targetAngle = 180;
        else if (direction == Vector2.left)
            targetAngle = 90;
        else if (direction == Vector2.right)
            targetAngle = -90;

        if (Mathf.Abs(transform.rotation.eulerAngles.z - targetAngle) > 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, targetAngle);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            if (extralife == true)
            {
                extralife = false;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (other.gameObject.CompareTag("Extralife"))
        {
            extralife = true; 
            Destroy(other.gameObject);
        }
    }
}
