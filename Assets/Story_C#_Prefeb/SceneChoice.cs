using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChoice : MonoBehaviour
{
    /*指定场景跳转*/

    public string sceneChoice;

    public void SceneAlt()
    {
        if (!string.IsNullOrEmpty(sceneChoice))
        {
            SceneManager.LoadScene(sceneChoice);
        }
    }
}
