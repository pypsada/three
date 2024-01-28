using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetMain : MonoBehaviour
{
    [Header("Remote IP Adress and Port")]
    public string ip;
    public int port;

    [HideInInspector]
    public static Queue<Action> actions = new();
    private readonly int ACTIONS_MAX_FIRE_EVENT = 30;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        NetManager.Update();
        
        for(int i=0;i<ACTIONS_MAX_FIRE_EVENT;i++)
        {
            if (actions.Count > 0)
            {
                actions.Dequeue().Invoke();
            }
            else
            {
                break;
            }
        }
    }
}
