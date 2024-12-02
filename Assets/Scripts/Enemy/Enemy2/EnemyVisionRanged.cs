using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisionRanged : MonoBehaviour
{
    public GameObject enemy2;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            enemy2.GetComponent<Enemy2>().hasVision = true;
        }
    }
}
