using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NameInput : MonoBehaviour
{
    private InputField inputField;

    private void Start()
    {
        inputField = GetComponent<InputField>();
    }

    public void OnNameEntered()
    {
        PlayerPrefs.SetString("PlayerName", inputField.text);
    }
}
