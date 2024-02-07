using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health1 : MonoBehaviour
{
    public PlayerGame1 PlayerGame1;
    // Start is called before the first frame update
    void Start()
    {
        PlayerGame1 = FindObjectOfType<PlayerGame1>();
    }

    // Update is called once per fram
    void Update()
    {
        
    }
    public void HealthJiLu()
    {
        transform.GetChild(PlayerGame1.health-1).gameObject.SetActive(false);
    }
}
