using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gogogo : MonoBehaviour
{
    private int record;
    // Start is called before the first frame update
    void Start()
    {
        record = SaveGameManager.SaveData.record;
    }
    public void Go()
    {
        if (record < 70)
        {
            SaveGameManager.SaveData.record = 60;
            SceneManager.LoadScene("Act6");
        }
        else if (record >= 2)
        {
            SaveGameManager.SaveData.record = 60;
            SceneManager.LoadScene("Base");
        }
    }
}
