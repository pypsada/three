using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthNumber : MonoBehaviour
{
    public Player player;
    public AI AI;
    public Text textComponent; // 引用物体上的Text组件
    public bool ai;
    // Start is called before the first frame update
    void Start()
    {
        AI = FindObjectOfType<AI>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ai)
        {
            textComponent.text = AI.health.ToString();
        }    
        else
        {
            textComponent.text = player.health.ToString();
        }

    }
}
