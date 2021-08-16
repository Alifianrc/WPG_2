using UnityEngine;
using UnityEngine.UI;

public class HighScorePanel : MonoBehaviour
{
    [SerializeField] private Text[] scoreText;
    private SaveData theData;
    
    void Start()
    {
        theData = SaveGame.LoadData();

        for(int i = 0; i< scoreText.Length; i++)
        {
            scoreText[i].text = theData.GetHighScoreLevelData(i).ToString();
        }
    }
}
