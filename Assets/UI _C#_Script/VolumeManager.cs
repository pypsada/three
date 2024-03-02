using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    /*此代码用于调节音量，分为bgm和音效两部分*/
    /*游戏内部也使用两个物体播放*/

    public int bgmValue;

    public GameObject bgm;
    public AudioSource bgmSource;
    public Slider bgmSlider;
    public GameObject effect;
    public AudioSource effectSource;
    public Slider effectSlider;

    private static float BgmVol = 0.5f;
    private static float EffectVol = 1f;

    private void Awake()
    {
        bgm = GameObject.Find("Audio Source");
        if (bgm != null)
            bgmSource = bgm.GetComponent<AudioSource>();
        effect = GameObject.Find("NewBroadcast");
        if (effect != null)
            effectSource = effect.GetComponent<AudioSource>();
    }
    void Start()
    {
        /*下方是bgm音量代码*/
        if (bgm != null)
        {
            bgmSlider.value = BgmVol;
            bgmSlider.onValueChanged.AddListener(UpdateVolume);
            bgmSource.volume = BgmVol;
        }
        /*下方是音效音量代码*/
        effectSlider.value = EffectVol;
        effectSlider.onValueChanged.AddListener(UpdateVolume2);
        effectSource.volume = EffectVol;
    }

    void UpdateVolume(float value)  //更新音量
    {
        if (bgm != null)
        {
            BgmVol = value;
            bgmSource.volume = BgmVol;
        }
    }
    void UpdateVolume2(float value)  //更新音量2
    {
        EffectVol = value;
        effectSource.volume = EffectVol;
    }
}
