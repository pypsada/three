using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinClip : MonoBehaviour
{
    public bool win;
    void OnEnable()
    {
        if (win)
            SoundsManager.PlayWin();
        else
            SoundsManager.PlayLose();
    }
}
