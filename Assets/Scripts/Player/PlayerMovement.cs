using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    private float originalSpeed;
    private Vector2 movementInput;
    private Rigidbody2D rb;
    public int extralife;
    private bool hasSpeedBoost = false;
    private bool hasDoubleShot = false;
    private bool hasQuickerShots = false;
    private float originalFireRate;
    [SerializeField] private string mainMenu = "MainMenu";

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePosition;
    [Range(0.1f, 2f)]
    [SerializeField] private float fireRate = 0.5f;
    private float fireTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalSpeed = movementSpeed; 
        originalFireRate = fireRate; 
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
        if (hasDoubleShot)
        {
            Vector3 secondBulletPosition = firePosition.position + (firePosition.up * -0.5f);
            Instantiate(bulletPrefab, secondBulletPosition, firePosition.rotation);
        }
        GetComponent<AudioSource>().Play();
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (extralife == 0)
        {
            if (other.gameObject.CompareTag("EnemyBullet"))
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
                SceneManager.LoadScene(mainMenu);
            }
        }
        else
        {
            Destroy(other.gameObject);
            extralife = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Extralife"))
        {
            extralife = 1;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("SpeedBoost")) 
        {
            if (!hasSpeedBoost)
            {
                hasSpeedBoost = true;
                StartCoroutine(SpeedBoostCoroutine());
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("DoubleShot")) 
        {
            if (!hasDoubleShot)
            {
                hasDoubleShot = true;
                StartCoroutine(DoubleShotCoroutine());
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.CompareTag("QuickerShots")) 
        {
            if (!hasQuickerShots)
            {
                hasQuickerShots = true;
                StartCoroutine(QuickerShotsCoroutine());
                Destroy(other.gameObject);
            }
        }
    }

    private IEnumerator SpeedBoostCoroutine()
    {
        movementSpeed *= 2; 
        yield return new WaitForSeconds(10f);
        movementSpeed = originalSpeed; 
        hasSpeedBoost = false;
    }

    private IEnumerator DoubleShotCoroutine()
    {
        yield return new WaitForSeconds(10f); 
        hasDoubleShot = false;
    }

    private IEnumerator QuickerShotsCoroutine()
    {
        fireRate /= 2; 
        yield return new WaitForSeconds(10f); 
        fireRate = originalFireRate; 
        hasQuickerShots = false;
    }
}