using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Huan1 : MonoBehaviour
{
    public float rotationSpeed; // �̶�ת��
    public float fallSpeed;   //�̶��½��ٶ�

    void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        transform.position += new Vector3(0, -fallSpeed * Time.deltaTime, 0);
    }
}
