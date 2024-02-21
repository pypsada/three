using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class To : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToGame1()
    {
        SceneManager.LoadScene("Game1");
    }

    public void ToGame2()
    {
        SceneManager.LoadScene("Game2");
    }

    public void ToGame3()
    {
        SceneManager.LoadScene("Game3");
    }
}
