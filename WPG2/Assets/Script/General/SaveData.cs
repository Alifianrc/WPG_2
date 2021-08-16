using UnityEngine;

// Contain user data
// Several can be change
[System.Serializable]
public class SaveData
{
    // Bomb data
    private int MAXBOMB = 6;
    private int MINBOMB = 0;
    private int bombCount;

    // Timer data
    private float[] TIMERDATA = { 5, 15, 30, 45, 60, 100, 150, 240};
    private int timeOrder;

    // Level Level Lock Data
    private bool[] levelIsLocked = new bool[6];

    // High Score Level Data
    private int[] highScoreLevel = new int[6];

    // Defult Constructor
    public SaveData()
    {
        bombCount = 4;

        timeOrder = 2;

        for(int i = 0; i < levelIsLocked.Length; i++)
        {
            if (i == 0)
            {
                levelIsLocked[i] = false;
            }
            else
            {
                levelIsLocked[i] = true;
            }
        }

        for (int i = 0; i < highScoreLevel.Length; i++)
        {
            highScoreLevel[i] = 0;
        }
    }

    // Customize Constructor
    public SaveData(SaveData data)
    {
        bombCount = data.GetBombCount();

        timeOrder = data.GetTimeOrder();

        for (int i = 0; i < levelIsLocked.Length; i++)
        {
            levelIsLocked[i] = data.GetLevelIsLockedData(i);
        }

        for (int i = 0; i < highScoreLevel.Length; i++)
        {
            highScoreLevel[i] = data.GetHighScoreLevelData(i);
        }
    }

    // Get Set Bomb
    public int GetMaxBomb()
    {
        return MAXBOMB;
    }
    public int GetMinBomb()
    {
        return MINBOMB;
    }
    public void SetBombCount(int value)
    {
        bombCount = value;
    }
    public int GetBombCount()
    {
        return bombCount;
    }

    // Get Set Timer
    public float GetTimerData(int value)
    {
        return TIMERDATA[value];
    }
    public int GetTimerDataLength()
    {
        return TIMERDATA.Length;
    }
    public void SetTimeOrder(int value)
    {
        timeOrder = value;
    }
    public int GetTimeOrder()
    {
        return timeOrder;
    }

    // Get Set Level Level Lock Data
    public void SetLevelIsLockedData(int value, bool isLocked)
    {
        levelIsLocked[value] = isLocked;
    }
    public bool GetLevelIsLockedData(int value)
    {
        return levelIsLocked[value];
    }
    public int GetLevelIsLockedDataLength()
    {
        return levelIsLocked.Length;
    }

    // Get Set HighScore
    public void SetHighScoreLevelData(int value, int score)
    {
        highScoreLevel[value] = score;
    }
    public int GetHighScoreLevelData(int value)
    {
        return highScoreLevel[value];
    }
    
}
