using UnityEngine;
using UnityEngine.UI;

public class PanelController : MonoBehaviour
{
    // Method for replace background based on screen size

    [SerializeField] private GameObject[] panelList;

    [SerializeField] private Sprite smallBackground;
    [SerializeField] private Sprite bigBackground;

    [SerializeField] private Sprite smallBackgroundBlur;
    [SerializeField] private Sprite bigBackgroundBlur;

    private void Start()
    {
        if(Camera.main.pixelWidth > 1920)
        {
            foreach(GameObject obj in panelList)
            {
                if(obj.name == "Loading Panel")
                {
                    obj.GetComponent<Image>().sprite = bigBackgroundBlur;
                }
                else
                {
                    obj.GetComponent<Image>().sprite = bigBackground;
                }
            }
        }
        else
        {
            foreach (GameObject obj in panelList)
            {
                if (obj.name == "Loading Panel")
                {
                    obj.GetComponent<Image>().sprite = smallBackgroundBlur;
                }
                else
                {
                    obj.GetComponent<Image>().sprite = smallBackground;
                }
            }
        }
    }
}
