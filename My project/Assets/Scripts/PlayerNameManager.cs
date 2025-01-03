using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameManager : MonoBehaviour
{
    [SerializeField] TMP_InputField usernameInput;

    public void SelectName()
    {
        if (usernameInput.text.Length > 3)
        {
            PhotonNetwork.NickName = usernameInput.text;

            MainMenuManager.Instance.OpenMenu("title");
        }
    }
}
