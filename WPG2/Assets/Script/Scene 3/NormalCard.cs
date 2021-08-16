using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCard : MonoBehaviour
{
    private GameManager2 manager;
    private bool selected;
    // Original position
    private Vector2 startPos;
    // This card Id (0 - 25)
    private int Id;
    // Locked or not (mean can't be moved)
    [SerializeField]private bool IsLocked;
    // If this card is for placing
    private bool canBePlaced;
    // Distance limit
    private float DistanceLimit = 0.7f;

    private void Start()
    {
        // Safe start pos
        startPos = new Vector2(this.transform.position.x, this.transform.position.y);
        // Set Game manager
        manager = FindObjectOfType<GameManager2>();
    }

    private void OnMouseDown()
    {
        if (IsLocked == false && canBePlaced == false)
        {
            // Tell manager that this is the object
            manager.SetSelectedObject(this.gameObject);
            selected = true;
        }
    }
    private void OnMouseUp()
    {
        if (selected == true && IsLocked == false && canBePlaced == false)
        {
            // Check if position is close to place holder
            CheckPosition();
            // Reset position
            ResetPosition();
            selected = false;
            // Tell manager to deselect
            manager.DeselectObject();
        }
    }

    private void CheckPosition()
    {
        for(int i = 0; i < manager.GetCardCloneLength(); i++)
        {
            GameObject temp = manager.GetCardClone(i);
            Vector2 pos = temp.transform.position;
            NormalCard normal = temp.GetComponent<NormalCard>();
            // Check if card is placable
            if (normal.CanBePlaced() == true)
            {
                // Calculate position
                if (Mathf.Abs(this.transform.position.x - pos.x) < DistanceLimit && Mathf.Abs(this.transform.position.y - pos.y) < DistanceLimit)
                {
                    // If close then check if card is correct
                    if (this.Id == normal.GetCardID())
                    {
                        // Snap to that clone position (except z pos)
                        Vector2 newPos = new Vector2(temp.transform.position.x, temp.transform.position.y);
                        this.transform.position = newPos;
                        // SFX card placed <- not working
                        FindObjectOfType<AudioManager>().Play("CardPlaced");
                        // Set can be placed to false (so can't be placed twice)
                        normal.SetCanBePlaced(false);
                        // Lock this card
                        LockCard();
                        // Tell manager
                        manager.CorrectCard();
                    }
                }
            }
        }
    }
    public void ResetPosition()
    {
        // Reset card position
        if (IsLocked == false && canBePlaced == false)
        {
            this.transform.position = startPos;
        }
    }

    // Get Set Function
    public void SetCardId(int id, Sprite mySprite)
    {
        Id = id;
        this.GetComponent<SpriteRenderer>().sprite = mySprite;
    }
    public int GetCardID()
    {
        return Id;
    }
    public void LockCard()
    {
        IsLocked = true;
    }
    public bool isLocked()
    {
        return IsLocked;
    }
    public void SetCanBePlaced(bool value)
    {
        canBePlaced = value;
    }
    public bool CanBePlaced()
    {
        return canBePlaced;
    }
}
