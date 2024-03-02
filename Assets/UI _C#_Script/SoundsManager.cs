using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    /*此代码用于存放各种音效*/

    public static AudioSource audioSrc;
    public static AudioClip pause;
    public static AudioClip win;
    public static AudioClip lose;
    public static AudioClip click;

    public static AudioClip SublimeWeakness;
    public static AudioClip YiSuoYanYuRenPingSheng;
    public static AudioClip YuePo;
    public static AudioClip Dragonflame;
    public static AudioClip CracksInvisible;
    public static AudioClip FreeLucky;

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        pause = Resources.Load<AudioClip>("pause");
        win = Resources.Load<AudioClip>("finalfanfare");
        lose = Resources.Load<AudioClip>("losemusic");
        click = Resources.Load<AudioClip>("click");

        SublimeWeakness = Resources.Load<AudioClip>("SublimeWeakness");
        YiSuoYanYuRenPingSheng = Resources.Load<AudioClip>("YiSuoYanYuRenPingSheng");
        YuePo = Resources.Load<AudioClip>("YuePo");
        Dragonflame = Resources.Load<AudioClip>("Dragonflame");
        CracksInvisible = Resources.Load<AudioClip>("CracksInvisible");
        FreeLucky = Resources.Load<AudioClip>("FreeLucky");
    }

    public static void PlayPauseClip()
    {
        audioSrc.PlayOneShot(pause);
    }
    public static void PlayWin()
    {
        audioSrc.PlayOneShot(win);
    }
    public static void PlayLose()
    {
        audioSrc.PlayOneShot(lose);
    }
    public static void PlayClick()
    {
        audioSrc.PlayOneShot(click);
    }

}
