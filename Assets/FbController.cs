using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using System;


public class FbController : MonoBehaviour {

//	[Header("FIREBASE AUTHENTICATION")]
//    public Firebase_Auth FBA;

	// Use this for initialization
	void Awake () {
		if (!FB.IsInitialized) {
			FB.Init ();
		}
	}
	void Start(){
		if (FB.IsLoggedIn) {
			FB.Mobile.RefreshCurrentAccessToken(result => {				
				PlayerPrefs.SetString("FbUser", AccessToken.CurrentAccessToken.TokenString);
			});
		}
	}
	// Update is called once per frame
	void Update () {
		
	}
	public void OnFacebook(){
        //StartCoroutine (WaitSomeTimes ());
        Application.OpenURL("https://business.facebook.com/knightsdomain/");

	}
	IEnumerator WaitSomeTimes(){
		yield return new WaitForSeconds (0.5f);				
		if (FB.IsLoggedIn) {			
			PlayerPrefs.SetString("FbUser", AccessToken.CurrentAccessToken.TokenString);				
//			if(HomeUIControllerScript.instance.BackData == 8)
//				FBA.CreateFBUser(AccessToken.CurrentAccessToken.TokenString);		
			// FB.Mobile.AppInvite (new Uri ("https://fb.me/270771816772626"),
			// 	new Uri("http://knightsdomainapp.kingsolomonsgames.com/pimgo_distr.jpg"),
			// 	callback: this.HandleResult);
		} else {			
			FB.LogInWithReadPermissions (new List<string>(){"public_profile", "email", "user_friends"} , 
			callback: this.HandleResult);
		
		}
	}
	protected void HandleResult(IResult result)
	{			
		if (result != null && string.IsNullOrEmpty(result.Error) && !result.Cancelled)
		{			
			PlayerPrefs.SetString("FbUser", AccessToken.CurrentAccessToken.TokenString);				
		//	if(HomeUIControllerScript.instance.BackData == 8)
		//		FBA.CreateFBUser(AccessToken.CurrentAccessToken.TokenString);					
		}
	}
}
