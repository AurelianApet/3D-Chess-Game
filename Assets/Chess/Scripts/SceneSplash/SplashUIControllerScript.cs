using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SplashUIControllerScript : MonoBehaviour
{
    public GameObject ScreenLoading, Loading;
    public AudioClip KnightsDomain, Background;
    AudioSource aud_src;

    void Awake()
    {
//        if (PlayerPrefs.GetString("Sound") == "On" || !PlayerPrefs.HasKey("Sound"))
//        {
//            Camera.main.GetComponent<AudioListener>().enabled = true;
//        }
//        else
//        {
//            Camera.main.GetComponent<AudioListener>().enabled = false;
//        }
    }

    void Start()
    {
//		string f = "10,100";
//		f.Replace(",","");
//		print("f"+f);
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save ();        
        PlayerPrefs.DeleteKey("Email");
        PlayerPrefs.DeleteKey("Name");
        PlayerPrefs.DeleteKey("Pwd");
        PlayerPrefs.DeleteKey("UserID");

        aud_src = GetComponent<AudioSource>();
        aud_src.clip = KnightsDomain;
        aud_src.Play();

        ScreenLoading.SetActive(false);
        Loading.SetActive(true);
        PlayerPrefs.SetInt("GameOpen", PlayerPrefs.GetInt("GameOpen") + 1);
        StartCoroutine(Change_Loading());
    }

    IEnumerator Change_Loading()
    {
        yield return new WaitForSeconds(KnightsDomain.length);
        aud_src.Pause();
        aud_src.clip = Background;
        aud_src.Play();

        Loading.SetActive(false);
        ScreenLoading.SetActive(true);

        StartCoroutine(Change_Scene());
    }

    IEnumerator Change_Scene()
    {
        yield return new WaitForSeconds(Background.length);
        LoadingScreenManager.LoadScene(1);
    }

    public void skip()
    {
        LoadingScreenManager.LoadScene(1);
    }
}
