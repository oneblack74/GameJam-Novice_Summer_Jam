using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TryCounter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "Réuissit en : " + SaveData.Instance.data.deathCounter.ToString() + " essais";
    }
}
