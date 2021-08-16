using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpPanel : MonoBehaviour
{
    [SerializeField] private GameObject content;
    [SerializeField] private GameObject scrollbar;
    private float scrollPos = 0;
    private float[] pos;
    private float distance;
    private int currentPos;

    private void Start()
    {
        pos = new float[content.transform.childCount];
        distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distance * i;
        }
    }

   
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            scrollPos = scrollbar.GetComponent<Scrollbar>().value;
        }
        else
        {
            for(int i = 0; i < pos.Length; i++)
            {
                if(scrollPos < pos[i] + (distance / 2) && scrollPos > pos[i] - (distance / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.05f);
                    currentPos = i;
                }
            }
        }
    }

    public void NextSlide()
    {
        if(currentPos < pos.Length - 1)
        {
            currentPos++;
            scrollPos = pos[currentPos];
        }
    }
    public void PreviousSlide()
    {
        if (currentPos > 0)
        {
            currentPos--;
            scrollPos = pos[currentPos];
        }
    }
}
