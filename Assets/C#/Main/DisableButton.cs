using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableButton: MonoBehaviour
{
    public string DisableCareer;
    void Start()
    {
        if (Whole.PlayerCareer == DisableCareer)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Whole.PlayerCareer==DisableCareer)
        {
            gameObject.SetActive(false);
        }
    }
}
