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
    public Transform firingPoint; // Miesto, kde sa strely vyp˙öùaj˙
    public float fireRate;
    private float timeToFire;
    public GameObject bulletPrefab;
    public GameObject ExtraLife;
    private int chanceToDrop;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate; // PlynulejöÌ pohyb
        timeToFire = fireRate;
    }

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        if (Vector2.Distance(target.position, transform.position) <= distanceToShoot)
        {
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        if (target == null) return;

        if (hasVision)
        {
            RotateToTarget(); // PÙvodnÈ ot·Ëanie nepriateæa

            float distance = Vector2.Distance(target.position, transform.position);

            if (distance >= distanceToStop)
            {
                MoveToTarget();
            }
            else
            {
                rb.velocity = Vector2.zero;
            }
        }
    }

    private void Shoot()
    {
        if (target == null) return;

        if (timeToFire <= 0f)
        {
            // VytvorÌme strelu v smere, ktor˝ je vûdy rovno, a upravÌme jej pozÌciu z firingPoint
            Vector2 shootDirection = transform.up; // Strela ide rovno od nepriateæa
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);

            // Resetujeme Ëas medzi v˝strelmi
            timeToFire = fireRate;
        }
        else
        {
            timeToFire -= Time.deltaTime;
        }
    }

    private void FindTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            target = player.transform;
        }
    }

    private void RotateToTarget()
    {
        if (target == null) return;

        // VypoËÌtame uhol k hr·Ëovi
        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f; // Uhol k hr·Ëovi
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));

        // Ot·Ëanie nepriateæa
        transform.localRotation = Quaternion.Slerp(transform.localRotation, q, rotateSpeed);
    }

    private void MoveToTarget()
    {
        if (target == null) return;

        Vector2 moveDirection = (target.position - (Vector3)rb.position).normalized;
        rb.velocity = moveDirection * speed;
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
            chanceToDrop = Random.Range(0, 10);
            if (chanceToDrop <= 2)
            {
                Instantiate(ExtraLife, transform.position, Quaternion.identity);
            }
        }
    }
}
