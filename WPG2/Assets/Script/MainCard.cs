using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour
{
    [SerializeField] private GameObject CardBack;
    [SerializeField] private SceneControllerBeta controller;
    private int _id;
    private char wordId;

    public void OnMouseDown()
    {
        if (CardBack.activeSelf && controller.canReveal && controller.wait)
        {
            CardBack.SetActive(false);
            controller.cardReveald(this);
        }
    }

    public int id
    {
        get { return _id; }
    }

    public void changeSprite (int id, Sprite image)
    {
        _id = id;
        GetComponent < SpriteRenderer>().sprite = image;
    }

    public void unreveal()
    {
        CardBack.SetActive(true);
    }
}
