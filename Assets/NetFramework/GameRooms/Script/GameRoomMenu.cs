using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomMenu : MonoBehaviour
{
    [Header("界面")]
    public GameObject createMenu;
    public GameObject findRoom;
    public GameObject match;

    [HideInInspector]
    public bool matching = false;
    [HideInInspector]
    public bool finding = false;

    // Start is called before the first frame update
    void Start()
    {
        return;
    }

    // Update is called once per frame
    void Update()
    {
        return;
    }

    public void OnClickCreateMenu()
    {
        if (createMenu.activeSelf == true)
        {
            return;
        }
        if (matching)
        {
            Debug.Log("正在匹配，请先取消");
            return;
        }
        if (finding)
        {
            Debug.Log("正在寻找房间，请先取消");
            return;
        }

        createMenu.SetActive(true);
        findRoom.SetActive(false);
        match.SetActive(false);
    }
    
    public void OnClickFindRooms()
    {
        if (findRoom.activeSelf == true)
        {
            return;
        }
        if (matching)
        {
            Debug.Log("正在匹配，请先取消");
            return;
        }
        if (finding)
        {
            Debug.Log("正在寻找房间，请先取消");
            return;
        }

        createMenu.SetActive(false);
        findRoom.SetActive(true);
        match.SetActive(false);
    }
    
    public void OnClickMatch()
    {
        if (match.activeSelf == true)
        {
            return;
        }
        if (matching)
        {
            Debug.Log("正在匹配，请先取消");
            return;
        }
        if (finding)
        {
            Debug.Log("正在寻找房间，请先取消");
            return;
        }

        createMenu.SetActive(false);
        findRoom.SetActive(false);
        match.SetActive(true);
    }
}
