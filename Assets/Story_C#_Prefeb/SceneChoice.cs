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

    public void SceneAlt()
    {
        StartCoroutine(DelayedSceneLoad());
        CloneEffect();
    }

    public void Back()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    IEnumerator DelayedSceneLoad()
    {
        yield return new WaitForSeconds(time);
        if (!string.IsNullOrEmpty(sceneChoice))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(sceneChoice);
        }
        else if (win)
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(BossFight.Scene);
        }
        else
        {
            Time.timeScale = 1;
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
