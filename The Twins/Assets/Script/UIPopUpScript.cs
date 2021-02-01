using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIPopUpScript : MonoBehaviour
{
    public GameObject Canvas1;
    public GameObject Canvas2;
    public GameObject Canvas0;

    private EventSystem system;

    public void Start()
    {
        system = EventSystem.current;
    }

    public void Update()
    {
        if (Canvas0.activeInHierarchy || Canvas1.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {   
                Selectable nextInput = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown(); //Find the selectable object below the selected one
                if (nextInput != null)
                {
                    system.SetSelectedGameObject(nextInput.gameObject, new BaseEventData(system));  // set the gameObject as selected and remove the previous one
                }
                Debug.Log("Navigation nao encontrado??AS<das");
            }
        }
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