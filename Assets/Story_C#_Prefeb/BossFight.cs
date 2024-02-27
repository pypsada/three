using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour
{
    public string playerCareer;
    public int playrLevel;
    public string AICareer;
    public int AILevel;

    public void ControlFight()
    {
        Whole.PlayerCareer = playerCareer;
        Whole.AICareer = AICareer;
        Whole.Characterlevel = playrLevel;
        Whole.AICharacterlevel = AILevel;
    }
}
