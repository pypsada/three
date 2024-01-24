using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetMain : MonoBehaviour
{
    [Header("Remote IP Adress and Port")]
    public string ip;
    public int port;
    
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        NetManager.Update();
    }
}
