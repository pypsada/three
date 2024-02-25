using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySth : MonoBehaviour
{
    public GameObject Sth;
    public void Start()
    {
        Destroy(Sth);
    }
}
