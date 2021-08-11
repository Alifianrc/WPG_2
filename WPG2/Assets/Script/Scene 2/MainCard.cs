using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour
{
    // The card back
    [SerializeField] private GameObject CardBack;
    // Game manager
    [SerializeField] private GameManager manager;
    // This card id
    private int _id;
    private char wordId;

    private void Start()
    {
        manager = FindObjectOfType<GameManager>();
    }

    public void OnMouseDown()
    {
        // if card can revealed
        if (CardBack.activeSelf && manager.CanReveal && manager.Wait && !manager.MenuIsActive())
        {
            // SFX card flip
            FindObjectOfType<AudioManager>().Play("CardFlip");
            // Open card
            CardBack.SetActive(false);
            // Tell Game Manager
            manager.CardReveald(this);
        }
    }

    public int id
    {
        get { return _id; }
    }

    // Change card id and sprite
    public void changeSprite (int id, Sprite image)
    {
        _id = id;
        GetComponent < SpriteRenderer>().sprite = image;
    }
    // Close back card
    public void unreveal()
    {
        CardBack.SetActive(true);
    }
}
