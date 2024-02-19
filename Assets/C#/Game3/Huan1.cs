using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huan1 : MonoBehaviour
{
    public float rotationSpeed; // 固定转速
    public float fallSpeed;   //固定下降速度

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0);
    }
}
