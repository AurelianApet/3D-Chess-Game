using UnityEngine;
using System.Collections;

public class GameInfo : MonoBehaviour {

	public static bool isLoading {
		get {
			if( PlayerPrefs.GetInt("loadingScreen") == 1 ) return true;
			return false;
		}
		set{
			if ( value ) PlayerPrefs.SetInt("loadingScreen", 1);
			else PlayerPrefs.SetInt("loadingScreen", 0);
		}
	}

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("AILevel", 0);
        isLoading = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
