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

    public void SceneAlt()
    {
        StartCoroutine(DelayedSceneLoad());
        CloneEffect();
    }

    IEnumerator DelayedSceneLoad()
    {
        yield return new WaitForSeconds(time);
        if (!string.IsNullOrEmpty(sceneChoice))
        {
            SceneManager.LoadScene(sceneChoice);
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
