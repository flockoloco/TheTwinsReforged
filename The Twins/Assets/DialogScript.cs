using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogScript : MonoBehaviour
{
    private float popupTimer = 2;
    private string getText;
    public TextMeshProUGUI popUpText;
    public void GiveText(string text)
    {
        getText = text;
    }
    void Start()
    {
        popUpText.text = getText;
    }
    void Update()
    {
        popupTimer -= Time.deltaTime;
        if (popupTimer < 0)
        {
            Destroy(gameObject);
        }
    }
}
