using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NetGame
{
    public class Player : MonoBehaviour
    {
        public PlayerTmpData tmpData = new();

        public NetButton netButton;
        //���ڶ����¼�
        public void AnimationFunc()
        {
            netButton.aniTrigger++;
        }
    }
}
