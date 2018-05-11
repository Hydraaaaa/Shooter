using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class MainMenu : MonoBehaviour
{
    [SerializeField] InputField IPField;

    public void Host()
    {
        NetworkManager.singleton.StartHost();
    }

    public void Join()
    {
        NetworkManager.singleton.networkAddress = IPField.text;
        NetworkManager.singleton.StartClient();
    }

    public void Exit()
    {
        Application.Quit();
    }
}
