using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarChoice : MonoBehaviour
{
    /*选头像用的*/

    public int value;
    public Dropdown dropdown;

    public void ChooseThis()
    {
        dropdown.value = value;
    }
}
