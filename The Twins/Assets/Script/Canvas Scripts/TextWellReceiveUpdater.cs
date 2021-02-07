using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextWellReceiveUpdater : MonoBehaviour
{
    public TextMeshProUGUI oresText;
    public TextMeshProUGUI barsText;

    public void UpdateText(int ores,int bars)
    {
        oresText.text = ("Ores: " + ores);
        barsText.text = ("Bars: " + bars);
    }
}
