using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingPanel : MonoBehaviour
{
    // UI
    [SerializeField] private Text bombText;
    [SerializeField] private Text timeText;

    // Just for Debugging
    [SerializeField] private Text screenSizeText;

    // Game data
    private SaveData theData;

    // Counting
    private int bomb;
    private float time;
    private int timeOrder;

    void Start()
    {
        // Load Game data
        theData = SaveGame.LoadData();
        // Set data
        bomb = theData.GetBombCount();
        timeOrder = theData.GetTimeOrder();
        time = theData.GetTimerData(timeOrder);
        // Set UI
        // Bomb
        bombText.text = bomb.ToString();
        // Time
        GameManager.ChangeTimeUI(time, timeText);

        // Debugging
        screenSizeText.text = "Screen Size : " + Camera.main.pixelWidth + "x" + Camera.main.pixelHeight;
    }

    public void BombIncrease()
    {
        // Check the value
        if(bomb != theData.GetMaxBomb())
        {
            // Increase Bomb
            bomb++;

            // Save it
            theData.SetBombCount(bomb);

            // Change UI
            bombText.text = bomb.ToString();
        }
    }
    public void BombDecrease()
    {
        // Check the value
        if (bomb != theData.GetMinBomb())
        {
            // Decrease Bomb
            bomb--;

            // Save it
            theData.SetBombCount(bomb);

            // Change UI
            bombText.text = bomb.ToString();
        }
    }

    public void TimeIncrease()
    {
        // Check value
        if(timeOrder != theData.GetTimerDataLength() - 1)
        {
            // Increase time
            timeOrder++;
            time = theData.GetTimerData(timeOrder);

            // Save it
            theData.SetTimeOrder(timeOrder);

            // Change UI
            GameManager.ChangeTimeUI(time, timeText);
        }
    }
    public void TimeDecrease()
    {
        // Check value
        if (timeOrder != 0)
        {
            // Increase time
            timeOrder--;
            time = theData.GetTimerData(timeOrder);

            // Save it
            theData.SetTimeOrder(timeOrder);

            // Change UI
            // Change UI
            GameManager.ChangeTimeUI(time, timeText);
        }
    }

    public SaveData GetSettingData()
    {
        return theData;
    }
}
