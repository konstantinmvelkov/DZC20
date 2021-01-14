using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public Slider volumeSlider;

    // Start is called before the first frame update
    /*void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume", 1f);
        AudioListener.volume = PlayerPrefs.GetFloat("Volume", 1f);
        GameObject.Find("BackgroundMusic").GetComponent<AudioSource>().Play();
        gameObject.SetActive(false);
    }

    public void UpdateVolume(float newVolume)
    {
        PlayerPrefs.SetFloat("Volume", newVolume);
        AudioListener.volume = newVolume;
    }*/
}
