using UnityEngine;
using System.Diagnostics;

public class OpenWebpage : MonoBehaviour
{
    public string url;

    public void OpenURL()
    {
        Process.Start(url);
    }
}