using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy1 : MonoBehaviour
{
    public Transform target;
    public float speed = 3f;
    private Rigidbody2D rb;
    public float rotateSpeed = 300f;
    public bool hasVision = false;
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
        FindTarget();
    }

    private void FixedUpdate()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        if (hasVision)
        {
            RotateToTarget();
            MoveToTarget();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void FindTarget()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player)
        {
            target = player.transform;
        }
    }

    private void RotateToTarget()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        float step = rotateSpeed * Time.fixedDeltaTime;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
    }

    private void MoveToTarget()
    {
        Vector2 moveDirection = (target.position - transform.position).normalized;
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
