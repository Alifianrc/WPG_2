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

    // Question
    private char[] wordList;
    public static Sprite questImage;
    [SerializeField] private GameObject gameImage;
    private int correctCount;

    // For calculation
    private float PosResult;

    // Game data
    private GameData theData;

    // Ui
    [SerializeField] private GameObject finishPanel;

    private void Start()
    {
        // Set UI
        finishPanel.SetActive(false);

        // Load Game Data
        theData = new GameData();

        // Change Image
        gameImage.GetComponent<Image>().sprite = questImage;

        // Take the question
        wordList = GameManager.itemWord;

        // Instantiate variable
        cardList = new GameObject[wordList.Length];
        cardClone = new GameObject[wordList.Length];

        // Calculate card position and spawn it
        InstantiateCards();

        // Count the correct answer
        correctCount = 0;
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
    }

    private void InstantiateCards()
    {
        // Take min and max coord
        Vector2 minPosCamera = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
        Vector2 maxPosCamera = Camera.main.ScreenToWorldPoint(new Vector2(Camera.main.pixelWidth, Camera.main.pixelHeight));
        // Calculate division x
        float divX;
        // Just for better position
        if (wordList.Length <= 3)
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
        float divY = (maxPosCamera.y - minPosCamera.y) / 4;

        // Spawn card
        for(int i = 0; i < wordList.Length; i++)
        {
            // Instantiate
            cardList[i] = Instantiate(theCard, new Vector3(PosResult, minPosCamera.y + (divY * 3/2), 0), Quaternion.identity);
            PosResult += divX;
            NormalCard cardManager = cardList[i].GetComponent<NormalCard>();
            // Change sprite and id
            for(int k = 0; k < theData.GetAlpha().Length; k++)
            {
                if (wordList[i] == theData.GetDetailAlpha(k))
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
            selectedObject = theObject;
            selected = true;
            // Save object start position
            selectedCard = selectedObject.GetComponent<NormalCard>();
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
            GameOver();
        }
    }

    private void GameOver()
    {
        // Add save game or some ui here

        // SFX Finish
        FindObjectOfType<AudioManager>().Play("Finish");

        // Open finish panel after few second
        StartCoroutine(HoldLoad());
    }
    private IEnumerator HoldLoad()
    {
        yield return new WaitForSeconds(1.5f);
        finishPanel.SetActive(true);
    }
}
