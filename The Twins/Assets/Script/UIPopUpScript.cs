using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopUpScript : MonoBehaviour
{
    public GameObject Canvas1;
    public GameObject Canvas2;
    public GameObject Canvas0;
    public void Start()
    {
    }
    public void CanvasSwitcher(int CanvasNumber) //0 MainMenu 1 Login 2 Register for the main menu, 
    {
        if (CanvasNumber == 0) 
        {
            Canvas1.SetActive(false);
            Canvas2.SetActive(false);
            Canvas0.SetActive(true);
        }
        else if (CanvasNumber == 1)
        {
            Canvas2.SetActive(false);
            Canvas1.SetActive(true);
            Canvas0.SetActive(false);
        }
        else if (CanvasNumber == 2) 
        {
            Canvas2.SetActive(true);
            Canvas1.SetActive(false);
            Canvas0.SetActive(false);
        }
    }
}
