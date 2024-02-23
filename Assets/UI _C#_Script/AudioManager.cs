using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioSource bgmSrc;
    public int NowBgm = 0;
    private GameObject manager;

    private void Awake()
    {
        if (bgmSrc == null)
        {
            bgmSrc = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        manager = GameObject.Find("VolumeManager");
        int i = manager.GetComponent<VolumeManager>().bgmValue;
        if (i != NowBgm)
        {
            NowBgm = i;
            switch (i)
            {
                case 0:
                    bgmSrc.clip = SoundsManager.SublimeWeakness;
                    bgmSrc.Play();
                    break;
                case 1:
                    bgmSrc.clip = SoundsManager.YiSuoYanYuRenPingSheng;
                    bgmSrc.Play();
                    break;
            }
        }

    }
}
