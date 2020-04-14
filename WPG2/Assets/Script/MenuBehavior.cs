using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuBehavior : MonoBehaviour
{
    public void triggerMenuBehavior(int i)
    {
        switch (i) {
            case 0:
                Application.Quit();
                break;
            case 1:
                SceneManager.LoadScene("Scene_1");
                Debug.Log("Move to Scene 1");
                break;
            case 2:
                SceneManager.LoadScene("Scene_2");
                Debug.Log("Move to Scene 2");
                break;
            case 3:
                SceneManager.LoadScene("Scene_3");
                Debug.Log("Move to Scene 3");
                break;
            case 4:
                SceneManager.LoadScene("Scene_4");
                Debug.Log("Move to Scene 4");
                break;
        }
    }
}
        

 

