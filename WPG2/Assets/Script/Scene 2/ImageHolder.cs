using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageHolder : MonoBehaviour
{
    [SerializeField] private Sprite[] Clothes;
    [SerializeField] private Sprite[] Fruit;
    [SerializeField] private Sprite[] Furniture;
    [SerializeField] private Sprite[] KitchenSet;
    [SerializeField] private Sprite[] Stationary;
    [SerializeField] private Sprite[] Vegetable;

    public Sprite GetSprite(int kind, int value)
    {
        switch (kind)
        {
            case 1:
                return Clothes[value];
                break;
            case 2:
                return Fruit[value];
                break;
            case 3:
                return Furniture[value];
                break;
            case 4:
                return KitchenSet[value];
                break;
            case 5:
                return Stationary[value];
                break;
            case 6:
                return Vegetable[value];
                break;
            default:
                return Fruit[value];
        }
    }
}
