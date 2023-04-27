using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownManager : MonoBehaviour
{
    private void Start()
    {
        DialogTextManager.instance.SetScenarios(new string[] { "arrived at the town" });
    }
    public void OnToQuestButton()
    {
        SoundManager.instance.PlaySE(0);
    }
}
