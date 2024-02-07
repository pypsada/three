using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public PlayerGame2 PlayerGame2;
    // Start is called before the first frame update
    void Start()
    {
        PlayerGame2= FindObjectOfType<PlayerGame2>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HealthJiLu()
    {
        transform.GetChild(PlayerGame2.health-1).gameObject.SetActive(false);
    }
}
