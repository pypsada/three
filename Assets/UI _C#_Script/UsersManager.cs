using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UsersManager : MonoBehaviour
{
    /*��ͨҳ�����SaveGameManager����*/

    public Text usersName;
    public Image usersAvatar;

    void Start()
    {
        SaveData saveData = SaveGameManager.SaveData;
        if (saveData != null)
        {
            usersName.text = SaveGameManager.Nickname;
            Sprite avatarSprite = Resources.Load<Sprite>(saveData.avatarPath);
            usersAvatar.sprite = avatarSprite;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
