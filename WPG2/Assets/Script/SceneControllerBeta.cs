using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControllerBeta : MonoBehaviour
{
    //this function control scene 3
    public const int gridX = 8;
    public const int gridY = 3;
    public const float offSetX = 1.5f;
    public const float offSetY = 2.2f;

    [SerializeField] private MainCard originalCard;
    private MainCard[] revealed;
    [SerializeField] private Sprite[] word;
   

    string[] fruit = new string[] { "APPLE", "LEMON", "GRAPE", "MANGO", "BANANA" };
    public char[] alpha = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
    char[] alpha2;
    char[] alpha3;
    private char[] itemWord;
    int itemRand;

    private void Start()
    {
        //copy alpha to alpha2
        alpha2 = new char[alpha.Length];
        for (int i = 0; i < alpha.Length; i++)
        {
            alpha2[i] = alpha[i];
        }
        //----------------------------------------------------------------------
        //take some item
        itemRand = Random.Range(0, fruit.Length);
        itemWord = fruit[itemRand].ToCharArray();
        Debug.Log(fruit[itemRand]);
        revealed = new MainCard[itemWord.Length];//menyimpan card revealed
        //make a globar variabel itemWord
        GlobalVars.itemWordSelected = new char[itemWord.Length];//not use
        for(int i = 0; i < itemWord.Length; i++)
        {
            GlobalVars.itemWordSelected[i] = itemWord[i];
        }
        //---------------------------------------------------------------------
        //delete some word in alpha2 and make alpha3lenght
        alpha3 = new char[itemWord.Length + alpha2.Length];
        for (int i = 0; i < itemWord.Length; i++)
        {
            for (int j = 0; j < alpha2.Length; j++)
            {
                if (itemWord[i] == alpha2[j])
                {
                    alpha2[j] = '0';//giganti 0 biar gampang di if
                }
            }
              
        }
        //--------------------------------------------------
        //first shuffle alpha2
        for (int i = 0; i < alpha2.Length; i++)
        {
            char tempo = alpha2[i];
            int tempo2 = Random.Range(i, alpha2.Length);
            alpha2[i] = alpha2[tempo2];
            alpha2[tempo2] = tempo;
        }
        //--------------------------------------------------
        //copy itemword into alpha3
        int alpha2TrueLenght = 0;//jumlah huruf yang sudah dimasukkan ke alpha3
        for (int i = 0; i < itemWord.Length; i++)
        {
            alpha3[i] = itemWord[i];//melanjutkan memasukkan dari array sebelumnya
            alpha2TrueLenght++;
        }
        //copy already shuffle alpha2 to alpha3
        for (int i = 0; i < alpha2.Length; i++)
        {
            if (alpha2[i] != '0')//biar tidak ada array yang bolong di depan
            {
                alpha3[alpha2TrueLenght] = alpha2[i];
                alpha2TrueLenght++;
            }
        }
        //-------------------------------------------------------------------------
        //make a bomb
        for (int i = itemWord.Length; i < itemWord.Length + 4; i++)//jumlah bomb ada 4 bisa disesuaikan kembali
        {
            int rand = Random.Range(i, 24);
            for (int j = 0; j < itemWord.Length; j++)
            {
                if (itemWord[j] != alpha3[rand])
                {
                    alpha3[rand] = '0';
                }
            }
        }
        //---------------------------------------------------------------------------
        //shuffle alpha3
        for (int i = 0; i < 24; i++)
        {
            char temp = alpha3[i];
            int temp2 = Random.Range(i, 24);
            alpha3[i] = alpha3[temp2];
            alpha3[temp2] = temp;
        }
        //-------------------------------------------------------------------------
        //print the card and give id
        Vector3 startPos = originalCard.transform.position;
        int alphaLoop = 0; //variabel ini menunjukkan urutan array, bisa dihapus salah satu
        for (int i = 0; i < gridY; i++)
        {
            for (int j = 0; j < gridX; j++)
            {
                MainCard card;
                if (i==0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                }

                int index = j * gridX + i;
                int id = 0;//nomor id sementara
                int wordNumber = 0;//nomor urutan gambar
                
                for (int k = 0; k < alpha.Length; k++)
                {
                    if (alpha3[alphaLoop] == alpha[k])
                    {
                        wordNumber = k;
                        for (int l = 0; l<itemWord.Length; l++)
                        {
                            if (alpha3[alphaLoop] == itemWord[l])
                            {
                                id = 1;//id jawaban diberi nilai 1
                                break;
                            }
                        }
                        alphaLoop++;
                        break;
                        //Debug.Log("Inside Looping"); Debug.Log(wordNumber);
                    }
                    else if(alpha3[alphaLoop] == '0')
                    {
                        wordNumber = 26;
                        id = 2;
                        alphaLoop++;
                        break;
                    }
                }
                
                //Debug.Log("Outside Looping"); Debug.Log(wordNumber);
                card.changeSprite(id, word[wordNumber]);
                float posX = (offSetX * j) + startPos.x;
                float posY = (offSetY * i) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
                
            }
        }
    }

    //---------------------------------------------------------------------------------
    //chek card
    //private MainCard[] revealed = new MainCard[5];
    bool waitFunction = false;
    int test = 0;
    int cardFound = 0;
    private int score = 0;
    [SerializeField] private TextMesh scoreLabel;
    
    public bool canReveal
    {
        get { return revealed[itemWord.Length-1] == null; }
    }

    public bool wait
    {
        get { return waitFunction == false; }
    }

    public void cardReveald(MainCard card)
    {
        revealed[cardFound] = card;
        cardFound++;
        StartCoroutine(chekMatch());
    }

    private IEnumerator chekMatch()
    {
        if (revealed[cardFound - 1].id == 1)
        {
            score++;
            scoreLabel.text = "Score : " + score;
        }
        else if(revealed[cardFound - 1].id == 2)
        {
            scoreLabel.text = "Score : 0";
            score = 0;
            waitFunction = true;
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < cardFound; i++)
            {
                revealed[i].unreveal();
                revealed[i] = null;
            }
            waitFunction = false;
            cardFound = 0;
        }
        else
        {
            waitFunction = true;
            yield return new WaitForSeconds(0.5f);
            waitFunction = false;
            revealed[cardFound - 1].unreveal();
            revealed[cardFound - 1] = null;
            if (cardFound > 0)
            {
                cardFound--;
            }
        }
    }
}
