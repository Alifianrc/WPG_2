using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager2 : MonoBehaviour
{
    // Card selected by player
    private GameObject selectedObject;
    private NormalCard selectedCard;
    private bool selected;
    // Card prefab
    [SerializeField] private GameObject theCard;
    // Card List
    private GameObject[] cardList;
    // Card clone (for placing card)
    private GameObject[] cardClone;
    // Sprite list
    [SerializeField] private Sprite[] CardSprite;
    // For card position calculation
    private float PosResult;

    // Question
    private char[] wordList;
    public static Sprite questImage;
    [SerializeField] private GameObject gameImage;
    private int correctCount;

    // Timer
    private float countDownTime;
    [SerializeField] private Text timerText;

    // Menu
    [SerializeField] private MenuController menuController;

    // Game Over
    private bool GameIsOver;
    // Panel Game Over
    [SerializeField] private GameObject finishPanel;
    [SerializeField] private Text finishText;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject returnButton;

    // Game data
    private GameData wordData = new GameData();
    private SaveData saveData;

    // Score
    [HideInInspector] public static float finalScore;
    private int pickUpCardCount = 0;

    private void Start()
    {
        // Set UI
        finishPanel.SetActive(false);

        // Load Save Game Data
        saveData = SaveGame.LoadData();
        countDownTime = saveData.GetTimerData(saveData.GetTimeOrder());
        GameManager.ChangeTimeUI(countDownTime, timerText);

        // Change Image
        gameImage.GetComponent<Image>().sprite = questImage;

        // Take the question
        wordList = GameManager.itemWord;

        // Instantiate variable
        cardList = new GameObject[wordList.Length];
        cardClone = new GameObject[wordList.Length];

        // Calculate card position and spawn it
        InstantiateCards();

        // Start BGM
        FindObjectOfType<AudioManager>().Play("BGM");

        // Count the correct answer
        correctCount = 0;

        // Google Ads
        AdsManager.instance.RequestInterstitial();
        AdsManager.instance.DeleteBanner();
    }

    private void Update()
    {
        // If object is selected
        if(selected == true)
        {
            // Get mouse position
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Move object
            selectedObject.transform.position = new Vector2(pos.x , pos.y);
        }

        // Timer
        CoolDownTime();
    }

    private void CoolDownTime()
    {
        if (countDownTime >= 0 && GameIsOver == false)
        {
            // Calculate count down time
            countDownTime -= Time.deltaTime;
            // Set UI
            GameManager.ChangeTimeUI(countDownTime, timerText);
        }
        // If times up
        else if (countDownTime <= 0 && GameIsOver == false)
        {
            // Player lose
            GameOver(false);
        }
    }

    private void InstantiateCards()
    {
        // Take min and max coord
        Vector2 minPosCamera = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 maxPosCamera = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));
        // Calculate division x
        float divX;
        // Just for better x position
        if (wordList.Length <= 5)
        {
            divX = (maxPosCamera.x - minPosCamera.x) / (wordList.Length + 4);
            PosResult = minPosCamera.x + (divX * 2.5f);
        }
        else if (wordList.Length <= 9)
        {
            divX = (maxPosCamera.x - minPosCamera.x) / (wordList.Length + 2);
            PosResult = minPosCamera.x + (divX * 1.5f);
        }
        else
        {
            divX = (maxPosCamera.x - minPosCamera.x) / (wordList.Length);
            PosResult = minPosCamera.x + (divX * 0.5f);
        }
        // Calculate division y
        float divY = (maxPosCamera.y - minPosCamera.y) / 4;

        // Spawn card
        for(int i = 0; i < wordList.Length; i++)
        {
            // Instantiate
            cardList[i] = Instantiate(theCard, new Vector3(PosResult, minPosCamera.y + (divY * 3/2), 0), Quaternion.identity);
            PosResult += divX;
            NormalCard cardManager = cardList[i].GetComponent<NormalCard>();
            // Change sprite and id
            for(int k = 0; k < wordData.GetAlpha().Length; k++)
            {
                if (wordList[i] == wordData.GetDetailAlpha(k))
                {
                    cardManager.SetCardId(k, CardSprite[k]);
                }
            }
        }

        // Make a copy for place
        for (int i = 0; i < cardList.Length; i++)
        {
            // Instantiate
            cardClone[i] = Instantiate(cardList[i]);
            // Change position
            cardClone[i].transform.position = new Vector3(cardClone[i].transform.position.x, minPosCamera.y + (divY * 1/2), 1);
            // Change color to black
            cardClone[i].GetComponent<SpriteRenderer>().color = Color.black;
            // Lock movement
            cardClone[i].GetComponent<NormalCard>().LockCard();
            // Set to can be placed
            cardClone[i].GetComponent<NormalCard>().SetCanBePlaced(true);
            // Copy id manually
            cardClone[i].GetComponent<NormalCard>().SetCardId(cardList[i].GetComponent<NormalCard>().GetCardID(), cardList[i].GetComponent<SpriteRenderer>().sprite);
        }

        // Shuffle card position
        for(int i = 0; i< wordList.Length; i++)
        {
            int rand = Random.Range(0, wordList.Length);
            var temp = cardList[i].transform.position;
            cardList[i].transform.position = cardList[rand].transform.position;
            cardList[rand].transform.position = temp;
        }
    }
    public void SetSelectedObject(GameObject theObject)
    {
        if (selectedObject == null && theObject.GetComponent<NormalCard>().isLocked() == false)
        {
            // Set selected object
            selectedObject = theObject;
            selected = true;
            // Save object start position
            selectedCard = selectedObject.GetComponent<NormalCard>();
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Increase pick up count
            pickUpCardCount++;
        }
    }
    public void DeselectObject()
    {
        // Reset position
        selectedObject = null;
        selected = false;
    }

    public GameObject GetCardClone(int value)
    {
       return cardClone[value];
    }
    public int GetCardCloneLength()
    {
        return cardClone.Length;
    }

    public void CorrectCard()
    {
        correctCount++;

        // Check if all is correct
        if(correctCount == wordList.Length)
        {
            // Game is over
            GameOver(true);
        }
    }

    private void GameOver(bool isWin)
    {
        // Can't open menu window
        menuController.SetCanOpenMenu(false);
        // Set menu is active
        menuController.SetMenuIsActive(true);

        // Set Game over
        GameIsOver = true;

        // Stop BGM
        FindObjectOfType<AudioManager>().Stop("BGM");

        // If player win
        if (isWin == true)
        {
            // Calculate score
            CalculateScore();
            // Save Game
            SaveNewGame();
            // Load win panel
            StartCoroutine(WinLoad());
        }
        // If player lose
        else
        {
            // Load lose panel
            StartCoroutine(LoseLoad());

            // Hide score
            scoreText.text = "";
        }
    }
    private IEnumerator WinLoad()
    {
        // SFX win
        FindObjectOfType<AudioManager>().Play("Win");

        // Hold few second
        yield return new WaitForSeconds(1.5f);

        // Set UI
        finishPanel.SetActive(true);
        finishText.text = "YOU ARE GREAT !";
        restartButton.SetActive(false);
        returnButton.SetActive(true);

        // Ads
        int rand = Random.Range(0, 4); 
        if (rand == 0)
        {
            AdsManager.instance.ShowInterstitial();
        }
        else if(rand == 1)
        {
            AdsManager.instance.ShowRewarded();
        }
    }
    private IEnumerator LoseLoad()
    {
        // SFX win
        FindObjectOfType<AudioManager>().Play("Lose");

        // Hold few second
        yield return new WaitForSeconds(1.3f);

        // Set UI
        finishPanel.SetActive(true);
        finishText.text = "DON'T GIVE UP !";
        restartButton.SetActive(true);
        returnButton.SetActive(false);

        // Ads
        AdsManager.instance.ShowInterstitial();
    }

    private void CalculateScore()
    {
        float totalScore = 0;

        // Time
        totalScore += (saveData.GetTimerDataLength() - saveData.GetTimeOrder()) * 85;
        totalScore += countDownTime;

        // Pick up count
        if(GameManager.itemWord.Length * 2.5f > pickUpCardCount)
        {
            totalScore += (GameManager.itemWord.Length * 2.5f - pickUpCardCount) * 15;
        }

        finalScore += totalScore;

        // Set UI score
        scoreText.text = "Your Score : " + (int)finalScore;
    }

    private void SaveNewGame()
    {
        // Get Level
        int level = GameManager.SelectedItem - 1;
        // Set highest Score
        if (saveData.GetHighScoreLevelData(level) < finalScore)
        {
            saveData.SetHighScoreLevelData(level, (int)finalScore);
        }

        // Open new level
        if(level != saveData.GetLevelIsLockedDataLength() - 1)
        {
            saveData.SetLevelIsLockedData(level + 1, false);
        }

        // Save Game
        SaveGame.SaveProgress(saveData);
    }
}
