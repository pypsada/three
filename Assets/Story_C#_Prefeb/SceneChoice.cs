using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChoice : MonoBehaviour
{
    /*ָ��������ת*/

    public string sceneChoice;

    public void SceneAlt()
    {
        if (!string.IsNullOrEmpty(sceneChoice))
        {
            SceneManager.LoadScene(sceneChoice);
        }
    }
}
