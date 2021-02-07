using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GroupToggle : MonoBehaviour
{
    public Slider Music;
    public Slider sound;
    public ToggleGroup groupToggle;
    public TMP_Dropdown dropDown;
    public Button bigButton;
    public TextMeshProUGUI soundText;
    public TextMeshProUGUI audioText;
    public TextMeshProUGUI dificultyText;
    public TextMeshProUGUI fullScreenText;

    public void BigButton()
    {
        foreach (Toggle toggle in groupToggle.ActiveToggles())
        {
            dificultyText.text = ("Dificulty mode: " + toggle.name);
            Debug.Log("Dificulty mode: " + toggle.name); 
        }
        Debug.Log("Music volume: " + Mathf.RoundToInt(Music.value * 100) + "%"); 
        Debug.Log("Audio effects volume: "+ Mathf.RoundToInt(sound.value * 100) + "%");
        soundText.text = ("Music volume: " + Mathf.RoundToInt(Music.value * 100) + "%");
        audioText.text = ("Audio effects volume: " + Mathf.RoundToInt(sound.value * 100) + "%");

        Debug.Log(dropDown.value + "is the dropdown value");
        if (dropDown.value == 0)
        {
            fullScreenText.text = ("Screen mode: FullScreen");
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
        }
        else if (dropDown.value == 1)
        {
            fullScreenText.text = ("Screen mode: Windowed");
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
        gameObject.SetActive(false);
    }

}
