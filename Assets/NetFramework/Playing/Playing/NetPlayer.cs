using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NetGame
{
    public class Player : MonoBehaviour
    {
        public PlayerTmpData tmpData = new();

        public NetButton netButton;
        //用在动画事件
        public void AnimationFunc()
        {
            netButton.aniTrigger++;
        }
    }
}
