using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    // スタートボタンが押されたら
    public void OnToTownButton()
    {
        SoundManager.instance.PlaySE(0);
    }
    
}
