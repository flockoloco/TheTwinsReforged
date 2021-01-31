using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LogInMenu : MonoBehaviour
{
    public TMP_InputField inputPass;
    public TMP_InputField inputName;

    public void ClearInputs()
    {
        inputName.text = "";
        inputPass.text = "";
    }
    public void Login()
    {
        GameObject.FindWithTag("GameManager").GetComponent<GameManagerScript>().LoginPlayer(inputName.text, inputPass.text);
        inputName.text = "";
        inputPass.text = "";
    }
}
