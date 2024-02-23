using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    /*�˴������ڴ�Ÿ�����Ч*/

    public static AudioSource audioSrc;
    public static AudioClip pause;

    public static AudioClip SublimeWeakness;
    public static AudioClip YiSuoYanYuRenPingSheng; 

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        pause = Resources.Load<AudioClip>("pause");

        SublimeWeakness = Resources.Load<AudioClip>("SublimeWeakness");
        YiSuoYanYuRenPingSheng = Resources.Load<AudioClip>("YiSuoYanYuRenPingSheng");
    }

    public static void PlayPauseClip()
    {
        audioSrc.PlayOneShot(pause);
    }
}
