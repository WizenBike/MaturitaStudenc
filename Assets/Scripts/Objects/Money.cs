using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    public int moneyValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MoneyCounter.instance.IncreaseMoney(moneyValue);
            Destroy(gameObject);
        }
    }
}
