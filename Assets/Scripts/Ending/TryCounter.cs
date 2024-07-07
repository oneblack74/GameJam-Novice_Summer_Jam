using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TryCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SaveData.Instance.Load();
        GetComponent<TextMeshProUGUI>().text = "You succeeded in : " + SaveData.Instance.data.deathCounter.ToString() + " attempts";
    }
}
