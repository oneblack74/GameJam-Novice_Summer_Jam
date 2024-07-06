using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private float timeRemaining = 12f;

    void Update()
    {
        if (timeRemaining <= 0f)
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "00,00";
        }
        else if (timeRemaining < 10f)
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "0" + String.Format("{0:0.00}", timeRemaining);
        }
        else
        {
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = String.Format("{0:0.00}", timeRemaining);
        }
    }

    void FixedUpdate()
    {
        timeRemaining -= Time.deltaTime;
    }
}
