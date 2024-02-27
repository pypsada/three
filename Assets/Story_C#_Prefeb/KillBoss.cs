using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBoss : MonoBehaviour
{
    public GameObject player;
    private Player get;

    private void Start()
    {
        get = player.GetComponent<Player>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.K) && Input.GetKey(KeyCode.L))
        {
            get.AI.health = 0;
            get.Sum();
        }
        if (Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.M))
        {
            get.health = 0;
            get.Sum();
        }
    }
}
