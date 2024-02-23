using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsersChoice : MonoBehaviour
{
    /*用于存档中几个面板的开关*/

    public GameObject LoadPannel;

    public void OpenPannel()
    {
        LoadPannel.SetActive(true);
    }
    public void ClosePannel()
    {
        LoadPannel.SetActive(false);
    }
}
