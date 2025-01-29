using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExtralifeCounter : MonoBehaviour
{
    public TMP_Text extraLife;
    public PlayerMovement playermovement;
    void Start()
    {
        extraLife.text = null;
    }

    private void Update()
    {
        extraLife.text = playermovement.extralife.ToString();
    }
}
