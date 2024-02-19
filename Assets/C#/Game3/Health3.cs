using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health3 : MonoBehaviour
{
    public PlayerGame3 PlayerGame3;
    // Start is called before the first frame update
    void Start()
    {
        PlayerGame3 = FindObjectOfType<PlayerGame3>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HealthJiLu()
    {
        transform.GetChild(PlayerGame3.health - 1).gameObject.SetActive(false);
    }
}
