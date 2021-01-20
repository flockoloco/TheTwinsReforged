using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegisterMenu : MonoBehaviour
{   
    public TMP_InputField pass1Input;
    public TMP_InputField pass2Input;
    public TMP_InputField nameInput;
    public GameObject popUpPrefab;
    public void Register()
    {
        if (pass1Input.text == pass2Input.text)
        {
            GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>().RegisterNewPlayer(nameInput.text,pass1Input.text);
            nameInput.text = "";
            pass1Input.text = "";
            pass2Input.text = "";
        }
        else if (pass1Input.text != pass2Input.text)
        {
            GameObject popUp = Instantiate(popUpPrefab, gameObject.transform);
            popUp.transform.position = new Vector3(popUp.transform.position.x, popUp.transform.position.y - 300, popUp.transform.position.z);
            popUp.GetComponent<DialogScript>().GiveText("Password inputs must match");
        }
    }
}
