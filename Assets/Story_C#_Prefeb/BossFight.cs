using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public string playerCareer;
    public int playerLevel;
    public string AICareer;
    public int AILevel;
    public string goingScene;
    public static string Scene;
    public string backScene;
    public static string Scene2;
    public bool story = false;

    public void ControlFight()
    {
        if (story)
        {
            playerCareer = "Assassin";
            playerLevel = SaveGameManager.SaveData.level;
        }
        Whole.PlayerCareer = playerCareer;
        Whole.AICareer = AICareer;
        Whole.Characterlevel = playerLevel;
        Whole.AICharacterlevel = AILevel;
        Scene = goingScene;
        Scene2 = backScene;
    }
}
