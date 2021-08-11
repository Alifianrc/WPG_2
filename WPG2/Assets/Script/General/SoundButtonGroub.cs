using UnityEngine;

public class SoundButtonGroub : MonoBehaviour
{
    public void TurnOffButton()
    {
        FindObjectOfType<AudioManager>().TurnOffSound();
    }

    public void TurnOnButton()
    {
        FindObjectOfType<AudioManager>().TurnOnSound();
    }
}
