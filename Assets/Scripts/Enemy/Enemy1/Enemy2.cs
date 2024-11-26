using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    private Rigidbody2D rb;
    public float rotateSpeed = 0.0025f;
    public bool hasVision = false;
    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;
    public Transform firingPoint;
    public float fireRate;
    private float timeToFire;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeToFire = fireRate;
    }

    private void Update()
    {
        if (!target)
        {
            FindTarget();
        }
        else
        {
            RotateToTarget();
        }

        //tu pokracuj
    }

    private void FixedUpdate()
    {
        if (hasVision == true)
        {
            if (Vector2.Distance(target.position, transform.position) >= distanceToShoot) 
            {
                rb.velocity = transform.up * speed;
            }
                
        }
    }

    private void FindTarget()
    {
        if (GameObject.FindGameObjectWithTag("Player"))
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    private void RotateToTarget()
    {
        if (hasVision == true)
        {
            Vector2 targetDirection = target.position - transform.position;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
            Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));
            transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(other.gameObject);
            target = null;
        }
        else if (other.gameObject.CompareTag("Bullet"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
