using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarChoice : MonoBehaviour
{
    /*ѡͷ���õ�*/

    public int value;
    public Dropdown dropdown;

    public void ChooseThis()
    {
        dropdown.value = value;
    }
}
