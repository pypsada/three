using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UsersChoice : MonoBehaviour
{
    /*���ڴ浵�м������Ŀ���*/

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
