using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject bulletPrefab;
    private int chanceToDrop;
    public GameObject Coin;
    public GameObject Cash;
    public GameObject ExtraLife;
    public GameObject SpeedUp;
    public GameObject DoubleShot;
    public GameObject QuickerShots;
    [SerializeField] private string mainMenu = "MainMenu";

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
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
            RotateToTarget();

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
            Vector2 shootDirection = transform.up;
            Instantiate(bulletPrefab, firingPoint.position, firingPoint.rotation);

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

        Vector2 targetDirection = target.position - transform.position;
        float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f;
        Quaternion q = Quaternion.Euler(new Vector3(0, 0, angle));

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
            SceneManager.LoadScene(mainMenu);
        }
        else if (other.gameObject.CompareTag("Bullet") || other.gameObject.CompareTag("EnemyBullet"))
        {
            Destroy(other.gameObject);
            WaveManager.Instance.EnemyDefeated(gameObject);
            Destroy(gameObject);

            chanceToDrop = Random.Range(0, 21);
            if (chanceToDrop <= 10)
            {
                Instantiate(Coin, transform.position, Quaternion.identity);
            }
            else if (chanceToDrop == 12 || chanceToDrop == 11)
            {
                Instantiate(Cash, transform.position, Quaternion.identity);
            }
            else if (chanceToDrop == 14 || chanceToDrop == 15)
            {
                Instantiate(ExtraLife, transform.position, Quaternion.identity);
            }
            else if (chanceToDrop == 16 || chanceToDrop == 17)
            {
                Instantiate(SpeedUp, transform.position, Quaternion.identity);
            }
            else if (chanceToDrop == 18 || chanceToDrop == 19)
            {
                Instantiate(DoubleShot, transform.position, Quaternion.identity);
            }
            else if (chanceToDrop == 20 || chanceToDrop == 21)
            {
                Instantiate(QuickerShots, transform.position, Quaternion.identity);
            }
        }
    }
}
