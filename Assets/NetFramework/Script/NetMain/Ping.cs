using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ping : MonoBehaviour
{
    public Text ping;
    // Start is called before the first frame update
    void Start()
    {
        ping.text = "ping -1";
        return;
    }

    // Update is called once per frame
    void Update()
    {
        ping.text = "ping " + NetManager.ping.ToString();
    }
}
