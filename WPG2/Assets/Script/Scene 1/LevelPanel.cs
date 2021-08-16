using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPanel : MonoBehaviour
{
    [SerializeField] private GameObject[] levelLockUI;

    void Start()
    {
        // Load data
        SaveData theData = SaveGame.LoadData();

        // Unlock level based on data
        for(int i = 0; i < levelLockUI.Length; i++)
        {
            levelLockUI[i].SetActive(theData.GetLevelIsLockedData(i));
        }
    }
}
