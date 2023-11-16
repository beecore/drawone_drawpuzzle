using UnityEngine;

public class SwitchSound : MonoBehaviour
{
    public GameObject Toggle_On_Sound;
    public GameObject Toggle_Off_Sound;
    public GameObject Toggle_On_Music;
    public GameObject Toggle_Off_Music;

    private void Start()
    {
        ChangeMusic(Music.instance.IsEnabled());
        ChangeSound(Sound.instance.IsEnabled());
    }

    private void ChangeMusic(bool isEnabled)
    {
        if (isEnabled)
        {
            Toggle_On_Music.SetActive(true);
            Toggle_Off_Music.SetActive(false);
        }
        else
        {
            Toggle_On_Music.SetActive(false);
            Toggle_Off_Music.SetActive(true);
        }
    }

    public void ToggleMusic()
    {
        bool isEnabled = !Music.instance.IsEnabled();
        Music.instance.SetEnabled(isEnabled, true);
        ChangeMusic(isEnabled);
    }

    public void ToggleSound()
    {
        bool isEnabled = !Sound.instance.IsEnabled();
        Sound.instance.SetEnabled(isEnabled);
        ChangeSound(isEnabled);
    }

    private void ChangeSound(bool isEnabled)
    {
        if (isEnabled)
        {
            Toggle_On_Sound.SetActive(true);
            Toggle_Off_Sound.SetActive(false);
        }
        else
        {
            Toggle_On_Sound.SetActive(false);
            Toggle_Off_Sound.SetActive(true);
        }
    }
}