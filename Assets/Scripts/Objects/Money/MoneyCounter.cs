using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyCounter : MonoBehaviour
{
    public static MoneyCounter instance;
    public TMP_Text moneyText;
    public int currentMoney = 0;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        moneyText.text = "Money: " + currentMoney.ToString();
    }

    public void IncreaseMoney(int v)
    {
        currentMoney += v;
        moneyText.text = "Money: " + currentMoney.ToString(); 
    }

    void Update()
    {
        
    }
}
