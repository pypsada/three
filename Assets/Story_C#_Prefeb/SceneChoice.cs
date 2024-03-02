using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChoice : MonoBehaviour
{
    public string sceneChoice;
    public float time = 2f;
    public GameObject effectPrefab;
    public GameObject canvas;
    public bool win;
    private GameObject pause;
    private void Start()
    {
        pause = GameObject.Find("PauseManager");
    }

    public void SceneAlt()
    {
        Destroy(pause);
        StartCoroutine(DelayedSceneLoad());
        CloneEffect();
    }

    public void Back()
    {
        Time.timeScale = 1;
        SoundsManager.PlayClick();
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator DelayedSceneLoad()
    {
        Time.timeScale = 1;
        SoundsManager.PlayClick();
        yield return new WaitForSeconds(time);
        if (!string.IsNullOrEmpty(sceneChoice))
        {
            if (sceneChoice == "Base" && SaveGameManager.SaveData.record >= 250)
            {
                sceneChoice = "BaseFinal";
            }
            SceneManager.LoadScene(sceneChoice);
        }
        else if (win)
        {
            SceneManager.LoadScene(BossFight.Scene);
        }
        else
        {
            SceneManager.LoadScene(BossFight.Scene2);
        }
    }

    void CloneEffect()
    {
        canvas = GameObject.Find("Canvas");
        if (effectPrefab != null)
        {
            Instantiate(effectPrefab, canvas.transform);
        }
    }
}
