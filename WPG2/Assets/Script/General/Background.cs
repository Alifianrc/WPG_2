using UnityEngine;

public class Background : MonoBehaviour
{
    // Method for replace adn resize background based on screen size

    [SerializeField] private Sprite smallBackground;
    [SerializeField] private Sprite bigBackground;

    void Start()
    {
        if (Camera.main.pixelWidth > 1920)
        {
            this.GetComponent<SpriteRenderer>().sprite = bigBackground;

            this.GetComponent<Transform>().localScale = new Vector3(0.926f, 0.926f, 0.926f);
            this.transform.position = new Vector3(0, 0, 10);
        }
        else
        {
            this.GetComponent<SpriteRenderer>().sprite = smallBackground;

            this.GetComponent<Transform>().localScale = new Vector3(0.926f, 0.926f, 0.926f);
            this.transform.position = new Vector3(0, 0, 10);
        }
    }
}
