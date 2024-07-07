using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private GameObject endScreen = null;
    [SerializeField] private GameObject creditRoll = null;

    void Start()
    {
        StartCoroutine(DisapearEndScreen());
    }

    private IEnumerator DisapearEndScreen()
    {
        yield return new WaitForSeconds(4);
        endScreen.SetActive(false);
        StartCoroutine(Scroll());
    }

    private IEnumerator Scroll()
    {
        for (int i = -1500; i < 2220; i++)
        {
            creditRoll.transform.localPosition = new Vector3(0, i, 0);
            yield return new WaitForSeconds(0.01f);
        }
        endScreen.SetActive(true);
    }
}
