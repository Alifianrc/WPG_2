using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Card Positioning
    public const int gridX = 6;
    public const int gridY = 3;
    public const float offSetX = 1.5f;
    public const float offSetY = 2.2f;

    // Original card (prefab)
    [SerializeField] private MainCard originalCard;
    // Revealed card list
    private MainCard[] revealed;
    // Card sprite list
    [SerializeField] private Sprite[] word;
    // Start position
    [SerializeField] private Transform startCardPos;
    // Menu Controller
    [SerializeField] private MenuController menu;

    // Bomb count 
    private int BombCount = 4;

    // For game randomize
    private char[] alpha2;
    private char[] alpha3;
    public static char[] itemWord;
    private int itemRand;
    
    // Item kind selected
    public static int SelectedItem;
    /// <summary>
    /// 1. Animal
    /// 2. Clothes
    /// 3. Fruit
    /// 4. Furniture
    /// 5. KitchenSet
    /// 6. Stationary
    /// 7. Vegetable
    /// </summary>

    // Image
    [SerializeField] private ImageHolder imageHolder;
    [SerializeField] private GameObject image;

    // Game Data
    private GameData theData;

    // For check card
    private bool waitFunction = false;
    private int cardFound = 0;
    private int score = 0;
    private float cardFlipHoldTime = 0.7f;
    [SerializeField] private Text scoreLabel;
    private string ScoreText = "Letter Found : ";

    [SerializeField] private GameObject FinishPanel;

    // Start is called before the first frame update
    void Start()
    {
        FinishPanel.SetActive(false);
        // Load game data
        theData = new GameData();
        // Randomize card
        RandomizeCard();
        // Set UI
        scoreLabel.text = ScoreText + score + "/" + itemWord.Length;
        image.GetComponent<Image>().sprite = imageHolder.GetSprite(SelectedItem, itemRand);
        // Send image to next level
        GameManager2.questImage = imageHolder.GetSprite(SelectedItem, itemRand);
    }

    private void RandomizeCard()
    {
        // Get alphabet data
        alpha2 = new char[theData.GetAlpha().Length];
        for (int i = 0; i < alpha2.Length; i++)
        {
            alpha2[i] = theData.GetDetailAlpha(i);
        }

        // Take random item
        RandomSelectedItem();
        TakeRandomItem();

        revealed = new MainCard[itemWord.Length];

        // Make alpha3 lenght
        alpha3 = new char[itemWord.Length + alpha2.Length];
        // Delete some word in alpha2
        for (int i = 0; i < itemWord.Length; i++)
        {
            for (int j = 0; j < alpha2.Length; j++)
            {
                if (itemWord[i] == alpha2[j])
                {
                    alpha2[j] = '0'; // Change to 0 for null
                }
            }
        }
        // Shuffle alpha2
        for (int i = 0; i < alpha2.Length; i++)
        {
            char tempo = alpha2[i];
            int tempo2 = Random.Range(i, alpha2.Length);
            alpha2[i] = alpha2[tempo2];
            alpha2[tempo2] = tempo;
        }
        // Copy itemword into alpha3
        int alpha2TrueLenght = 0;
        for (int i = 0; i < itemWord.Length; i++)
        {
            alpha3[i] = itemWord[i];
            alpha2TrueLenght++;
        }
        // Copy already shuffle alpha2 to alpha3
        for (int i = 0; i < alpha2.Length; i++)
        {
            if (alpha2[i] != '0') // Not copy if null
            {
                alpha3[alpha2TrueLenght] = alpha2[i];
                alpha2TrueLenght++;
            }
        }
        // Make a bomb
        for (int i = itemWord.Length; i < itemWord.Length + BombCount; i++)
        {
            int rand = Random.Range(i, (gridX * gridY));
            for (int j = 0; j < itemWord.Length; j++)
            {
                if (itemWord[j] != alpha3[rand])
                {
                    alpha3[rand] = '0';
                }
            }
        }
        // Shuffle alpha3
        for (int i = 0; i < 24; i++)
        {
            char temp = alpha3[i];
            int temp2 = Random.Range(i, (gridX * gridY));
            alpha3[i] = alpha3[temp2];
            alpha3[temp2] = temp;
        }

        //print the card and give id
        Vector3 startPos = startCardPos.position;
        int alphaLoop = 0;
        for (int i = 0; i < gridY; i++)
        {
            for (int j = 0; j < gridX; j++)
            {
                MainCard card = Instantiate(originalCard) as MainCard;

                int index = j * gridX + i;
                int id = 0;// Temporary id
                int wordNumber = 0;// Serial number

                for (int k = 0; k < theData.GetAlpha().Length; k++)
                {
                    if (alpha3[alphaLoop] == theData.GetDetailAlpha(k))
                    {
                        wordNumber = k;
                        for (int l = 0; l < itemWord.Length; l++)
                        {
                            if (alpha3[alphaLoop] == itemWord[l])
                            {
                                id = 1; // id true answer is 1
                                break;
                            }
                        }
                        alphaLoop++;
                        break;
                        //Debug.Log("Inside Looping"); Debug.Log(wordNumber);
                    }
                    // if card is bomb
                    else if (alpha3[alphaLoop] == '0')
                    {
                        wordNumber = 26;
                        id = 2;
                        alphaLoop++;
                        break;
                    }
                }
                //Debug.Log(wordNumber);
                card.changeSprite(id, word[wordNumber]);
                float posX = (offSetX * j) + startPos.x;
                float posY = (offSetY * i) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }
    private void RandomSelectedItem()
    {
        // Random kind of item if not selected
        if(SelectedItem == 0 || SelectedItem == null)
        {
            SelectedItem = Random.Range(0, 7) + 1;
        }
    }
    private void TakeRandomItem()
    {
        switch (SelectedItem)
        {
            case 1:
                itemRand = Random.Range(0, theData.GetAnimalLength());
                itemWord = theData.GetAnimal(itemRand).ToCharArray();
                break;
            case 2:
                itemRand = Random.Range(0, theData.GetClothesLength());
                itemWord = theData.GetClothes(itemRand).ToCharArray();
                break;
            case 3:
                itemRand = Random.Range(0, theData.GetFruitLength());
                itemWord = theData.GetFruit(itemRand).ToCharArray();
                break;
            case 4:
                itemRand = Random.Range(0, theData.GetFurnitureLength());
                itemWord = theData.GetFurniture(itemRand).ToCharArray();
                break;
            case 5:
                itemRand = Random.Range(0, theData.GetKitchenSetLength());
                itemWord = theData.GetKitchenSet(itemRand).ToCharArray();
                break;
            case 6:
                itemRand = Random.Range(0, theData.GetStationaryLength());
                itemWord = theData.GetStationary(itemRand).ToCharArray();
                break;
            case 7:
                itemRand = Random.Range(0, theData.GetVegetableLength());
                itemWord = theData.GetVegetable(itemRand).ToCharArray();
                break;
            default:

                break;
        }
    }

    // Check card method
    public bool CanReveal
    {
        get { return revealed[itemWord.Length - 1] == null; }
    }
    public bool Wait
    {
        get { return waitFunction == false; }
    }
    public bool MenuIsActive()
    {
        return menu.GetMenuIsActive();
    }
    public void CardReveald(MainCard card)
    {
        revealed[cardFound] = card;
        cardFound++;
        StartCoroutine(chekMatch());
    }
    private IEnumerator chekMatch()
    {
        // If card is correct
        if (revealed[cardFound - 1].id == 1)
        {
            // Add score (card correct)
            score++;
            // Change UI
            scoreLabel.text = ScoreText + score + "/" + itemWord.Length;
        }
        // If card is bomb
        else if (revealed[cardFound - 1].id == 2)
        {
            // Reset score (card correct)
            score = 0;
            scoreLabel.text = ScoreText + score + "/" + itemWord.Length;
            // Set bool to wait, so other card can't be flipped
            waitFunction = true;
            // Wait for few second
            yield return new WaitForSeconds(cardFlipHoldTime);
            // Close all revealed card
            for (int i = 0; i < cardFound; i++)
            {
                revealed[i].unreveal();
                revealed[i] = null;
            }
            // Set bool wait to false
            waitFunction = false;
            // Reset cardfound
            cardFound = 0;
        }
        // If card is not correct
        else
        {
            // Set bool to wait, so other card can't be flipped
            waitFunction = true;
            // Wait for few second
            yield return new WaitForSeconds(cardFlipHoldTime);
            // Set bool wait to false
            waitFunction = false;
            // Close the card again
            revealed[cardFound - 1].unreveal();
            revealed[cardFound - 1] = null;
            // Decrement cardFounded
            if (cardFound > 0)
            {
                cardFound--;
            }
        }

        // If all corrct card revealed
        if(score == itemWord.Length)
        {
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
        FinishPanel.SetActive(true);
    }
}
