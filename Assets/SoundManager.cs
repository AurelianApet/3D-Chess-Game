using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public GameObject SoundOn, SoundOff;
    // Use this for initialization
    void Awake()
    {
        CheckSound();
    }

    void CheckSound()
    {
		if (PlayerPrefs.HasKey ("Sound")) {
			if (PlayerPrefs.GetString ("Sound") == "On") {
				Camera.main.GetComponent<AudioListener> ().enabled = true;
			} else {
				Camera.main.GetComponent<AudioListener> ().enabled = false;
			}
		} else {
			PlayerPrefs.SetString ("Sound", "On");
			Camera.main.GetComponent<AudioListener> ().enabled = true;
		}
    }

    void Start()
    {
        if (!PlayerPrefs.HasKey("Sound"))
        {
            PlayerPrefs.SetString("Sound", "On");
            SoundOn.SetActive(true);
        }
        if (PlayerPrefs.GetString("Sound") == "Off")
        {
            SoundOff.SetActive(true);
            SoundOn.SetActive(false);
        }
        else
        {
            SoundOn.SetActive(true);
            SoundOff.SetActive(false);
        }
        CheckSound();
    }
	
    // Update is called once per frame
    void Update()
    {
		
    }

    public void On_SoundOn()
    {
        PlayerPrefs.SetString("Sound", "Off");
        CheckSound();

    }

    public void On_SoundOff()
    {
        PlayerPrefs.SetString("Sound", "On");
        CheckSound();
    }
}
