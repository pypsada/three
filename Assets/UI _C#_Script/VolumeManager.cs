using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    /*�˴������ڵ�����������Ϊbgm����Ч������*/
    /*��Ϸ�ڲ�Ҳʹ���������岥��*/

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
        /*�·���bgm��������*/
        if (bgm != null)
        {
            bgmSlider.value = BgmVol;
            bgmSlider.onValueChanged.AddListener(UpdateVolume);
            bgmSource.volume = BgmVol;
        }
        /*�·�����Ч��������*/
        effectSlider.value = EffectVol;
        effectSlider.onValueChanged.AddListener(UpdateVolume2);
        effectSource.volume = EffectVol;
    }

    void UpdateVolume(float value)  //��������
    {
        if (bgm != null)
        {
            BgmVol = value;
            bgmSource.volume = BgmVol;
        }
    }
    void UpdateVolume2(float value)  //��������2
    {
        EffectVol = value;
        effectSource.volume = EffectVol;
    }
}
