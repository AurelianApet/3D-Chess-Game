using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System;
using ChessLibrary;
//using Firebase.Analytics;

public class HomeUIControllerScript : MonoBehaviour
{
    public GameObject Screenhome, Screenpracticewithai, Screenpracticeonline;
    public GameObject ScreenMenuList;
    public GameObject ScreenSetting;
    public GameObject ScreenMode;
    public GameObject[] ScreenModes;
    public GameObject [] menuScreens; // 0 = flag , 1 = about , 2 = tutorial , 3 = upgrade , 4 = settings
    public GameObject ModeSelectWnd;
    public GameObject ScreenSelectDevice, Room_Info_Pannel, Registration_Massage_Panel,
    ReplayInfoPanel;
    public GameObject MessageForm;
    public GameObject MessageText;
    public GameObject TitlePage;
    public GameObject ScreenSetTime;
    public bool CheckMaster;
    public GameObject PvP_Timer_Selection_Screen, Internet_Pannel, Back_But, Fb_button, QuitPanel, PurchasePopUP, LeaderBoardButton;
    public GameObject messagePopup;
    public GameObject PurchaseMenu, thankspanel, UpgradeBanner, LB_Loading, LeaderBoard_Pannel, Error_Massage_Panel2;

    public static HomeUIControllerScript instance;

    private int msgID = 0;

    public GameObject ScreenSignin;
    [Header("Massage Panels Error/Success")]
    public GameObject Error_Massage_Panel;
    public GameObject Success_Massage_Panel;

    [Header("SFX")]
    public AudioClip Button_SFX;
    public AudioClip Back_SFX, Upgrade_SFX, NewPlay_SFX, NewSettings_SFX, NewAbout_SFX, FaceBook_SFX;
    public AudioClip PvC_SFX, PvP_SFX, Easy_SFX, Normal_SFX, Hard_SFX, LocalPlayer_SFX, onlinePlayer_SFX, UpgradeRequire_SFX, Tutoril_SFX, SendChat_Sfx;
    public AudioClip KnightsTemplar_SFX, KnightsOfColumbus_SFX, KnightsRoundTable_SFX, Gallop_SFX, Cantor_SFX, Trot_SFX, CheerThen_SFX, Battle_Time_Sfx, Timer_On_Sfx, Timer_Off_Sfx;
    public AudioClip KnightsDomain_SFX;
    AudioSource aud_src;

    public Button Start_Btn, Chat_Btn, loginBtn;
    public Text Room_Timer_txt, Room_BattleTime_txt, Room_GameMode_txt;
    public GameObject ChatUI, ChatScript;
    public List<Button> AllButtons = new List<Button>();

    [Header("FIREBASE AUTHENTICATION")]
//    public Firebase_Auth FBA;

    //	[Header ("FIREBASE DATABASE")]
    //	public Firebase_Auth FBD_Scr;

    [Header("Curruncy Elements")]
    public GameObject Curruncy_UI;
    public GameObject Curruncy_UI_Error;
    public GameObject Curruncy_UI_Buy;
    public GameObject Curruncy_UI_Lobby_Massage;

    public int BackData = 0;
    bool isPaused;
    public GameObject purchaseUpgradeScreen;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        CheckSound();
    }

    void CheckSound()
    {
        if (PlayerPrefs.HasKey("Sound"))
        {
            if (PlayerPrefs.GetString("Sound") == "On")
            {
                Camera.main.GetComponent<AudioListener>().enabled = true;
            }
            else
            {
                Camera.main.GetComponent<AudioListener>().enabled = false;
            }
        }
        else
        {
            PlayerPrefs.SetString("Sound", "On");
            Camera.main.GetComponent<AudioListener>().enabled = true;
        }
        //		if (PlayerPrefs.GetString ("Sound") == "Off") {
        //			aud_src.mute = true;
        //
        //		} else {
        //			aud_src.mute = false;
        //
        //		}
    }


    void Start()
    {
        if (comunication.info.bannerAd != null)
            comunication.info.bannerAd.Show();
        //For testing all features of the game
        //comunication.info.Save_Purchses(4, 1);
        // comunication.info.Save_Purchses(2, 1);
        admobdemo.instance.ShowBanner();
        admobdemo.instance.LoadInterstitial();
        admobdemo.instance.ShowInterstitial();
        // ====== End Testing Features ========
        if (PlayerPrefs.GetInt("GameOpen") >= 5 && !comunication.info.Sku_1 && !comunication.info.Sku_5)
        {
            admobdemo.instance.LoadInterstitial();
            admobdemo.instance.ShowInterstitial();
        }
        if (!comunication.info.Sku_1)
            admobdemo.instance.ShowBanner();
        //		admobdemo.instance.LoadInterstitial ();
        //GameObject.Find ("OmmziTP").GetComponent<OmmziTPManager> ().ShowInterstitial ();
        aud_src = GetComponent<AudioSource>();

        comunication.info.Scene_Index = 1;

        // Temorary value
        //		PlayerPrefs.SetString("username","");
        //		PlayerPrefs.SetInt("IsFirst", 0);

        Chat_Btn.gameObject.SetActive(false);
        if (comunication.info.playAgain)
        {
            if (comunication.info.Gameplay_Type == 2)
            {
                comunication.info.PUN_IsOnline = true;
                Chat_Btn.gameObject.SetActive(true);
                Chat_Btn.transform.GetChild(0).gameObject.SetActive(true);
            }
            ReplayingGame();
        }
        else
        {
            
            clearScreen();
            TitlePage.SetActive(true);
            Curruncy_UI.SetActive(false);
            PlayerPrefs.SetInt("IsTurnTimeActivated", 0);
            
        }



        /*comunication.info.All_Formation_Purchased = true;
        comunication.info.Same_Device_Purchased = true;
        comunication.info.All_Purchased = true;
        comunication.info.All_K_Formation_Purchased = true;
        comunication.info.Same_Device_K_Formation_Purchased = true;
        comunication.info.Online_Purchased = true;*/
    }

    public void OnChatButton()
    {

        ChatUI.SetActive(true);
        //ChatScript.SetActive(true);

    }

    public void SendChat_Sound()
    {
        aud_src.clip = SendChat_Sfx;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
    }

    public void FaceBookSound()
    {
        aud_src.clip = FaceBook_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
    }

    public void ShowMessagePopup(string message)
    {
        messagePopup.SetActive(true);
        messagePopup.GetComponentInChildren<Text>().text = message;
    }

    void clearScreen()
    {
        Screenhome.SetActive(false);
        Screenpracticeonline.SetActive(false);
        Screenpracticewithai.SetActive(false);

        ScreenSelectDevice.SetActive(false);
        Room_Info_Pannel.SetActive(false);
        Start_Btn.gameObject.SetActive(false);

        ScreenSignin.SetActive(false);
        Error_Massage_Panel.SetActive(false);
        Success_Massage_Panel.SetActive(false);

        MessageForm.SetActive(false);
        ScreenMode.SetActive(false);
        ModeSelectWnd.SetActive(false);
        ScreenSetTime.SetActive(false);
        PvP_Timer_Selection_Screen.SetActive(false);
        Internet_Pannel.SetActive(false);

        PurchaseMenu.SetActive(false);

        Curruncy_UI_Error.SetActive(false);
        Curruncy_UI_Buy.SetActive(false);
        Curruncy_UI_Lobby_Massage.SetActive(false);
    }

    public void OnLeaderBoardBtn(bool show)
    {
        if (comunication.info.Sku_5)
        {

            //			if (comunication.info.Gameplay_Type == 2)// Online Mode
            //			{
            aud_src.clip = Button_SFX;
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();
            UpgradeBanner.SetActive(false);
            comunication.info.Calculate_Points();
            LB_Loading.SetActive(show);
            LeaderBoard_Pannel.SetActive(show);
//            Firebase_Database_KD.ins.RetrieveDataFromFireBase();
            // Refresh data
            // Firebase_Database_KD.ins.Check_Your_Pos();
            //Firebase_Database_KD.ins.ShowData();
            StartCoroutine(Load_LB(show));
        }
        else
        {
            aud_src.clip = UpgradeRequire_SFX;
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();
            ShowUpgradeBanner();
        }
    }

    public void ShowUpgradeBanner()
    {
        if ( !comunication.info.Sku_5)
        {
            UpgradeBanner.SetActive(true);
        }
    }

    IEnumerator Load_LB(bool LB_Show)
    {
        yield return new WaitForSeconds(5);
        UpgradeBanner.SetActive(false);
        LB_Loading.SetActive(false);
    }

    public void Activate_TurnTimer(bool yes)
    {
        if (yes)
        {
            aud_src.clip = Timer_On_Sfx;
        }
        else
        {
            aud_src.clip = Timer_Off_Sfx;
        }
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();

        StartCoroutine(TurnTimer_wait(yes));
    }

    IEnumerator TurnTimer_wait(bool b)
    {
        yield return new WaitForSeconds(aud_src.clip.length);        
        if (b)
        {
            aud_src.clip = Battle_Time_Sfx;
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();
            PlayerPrefs.SetInt("IsTurnTimeActivated", 1);// Timer ON
            changeScreen("screensettime");
            BackData = 7;
            Debug.Log("BackData");
     
        }
        else
        {
            PlayerPrefs.SetInt("IsTurnTimeActivated", 0);// Timer OFF

            LoadingScreenManager.LoadScene(2);               
            BackData = 8;

        }

        PlayerPrefs.SetInt("AILevel", -4);
        PvP_Timer_Selection_Screen.SetActive(false);
    }

    public void ActiveThanksPanel()
    {
        BackData = 2;
        Debug.Log("BackData");
        thankspanel.SetActive(true);
    }

    public void PlayButtonSound(AudioClip But_Clip)
    {
        aud_src.clip = But_Clip;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
    }

    public void OnUpgradeSound()
    {
        aud_src.clip = Upgrade_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Room_Info_Pannel.activeInHierarchy)
            {
                return;
            }
            if(messagePopup.activeInHierarchy){
                messagePopup.SetActive(false);
            }else if (BackData == 0)
            {
                QuitPanel.SetActive(true);
            }
            else
            {
                Back_Btn();
            }
        }
    }

    public void ActiveButtons()
    {
        for (int i = 0; i < AllButtons.Count; i++)
        {
            if (AllButtons[i] != null)
                AllButtons[i].interactable = true;
        }
    }

    public void TempMethod()
    {
        LeaderBoardButton.SetActive(false);
        PlayerPrefs.SetInt("IsTurnTimeActivated", 0);
        
        ScreenSelectDevice.SetActive(true);
        

        BackData = 4;
        Debug.Log("BackData");
        Start_Btn.gameObject.SetActive(false);
    }

    IEnumerator WaitForDeactivate()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Home");
    }

    public void Back_Btn()
    {
        Button_Sound();
        if (LeaderBoard_Pannel.activeInHierarchy)
        {
            LeaderBoard_Pannel.SetActive(false);
            return;
        }

        clearScreen();
        for (int i = 0; i < AllButtons.Count; i++)
        {
            if(AllButtons[i] != null)
                AllButtons[i].interactable = true;
        }
        switch (BackData)
        {
            case 1:
                TitlePage.SetActive(true);
                Back_But.SetActive(false);
                Fb_button.SetActive(false);
                LeaderBoardButton.SetActive(false);
                BackData = 0;
                
                break;

            case 2:
                Screenhome.SetActive(true);
                BackData = 1;
                break;

            case 3:
                if (comunication.info.PUN_GameplayMode > 2)
                {
                    
                    comunication.info.PUN_GameplayMode = 0;
                    ScreenMode.SetActive(true);
                }                   
                else
                {
                    Screenpracticewithai.SetActive(true);
                    BackData = 2;
                }
                break;

            case 4:
                Screenhome.SetActive(true);
                BackData = 1;
                Curruncy_UI.SetActive(false);
                comunication.info.PUN_IsOnline = false;
                comunication.info.Save();
                break;

            case 5:
                Screenhome.SetActive(true);
                ScreenMode.SetActive(false);
                BackData = 4;
                break;

            case 6:
                
                BackData = 5;
                ScreenMode.SetActive(true);
                PvP_Timer_Selection_Screen.SetActive(false);
                break;
            case 7:
                PvP_Timer_Selection_Screen.SetActive(true);
                ScreenSetTime.SetActive(false);
                BackData = 6;
                break;
            case 8:
                Screenhome.SetActive(true);
                ScreenSignin.SetActive(false);
                BackData = 4;
                break;

            case 9:
                ScreenSelectDevice.SetActive(false);
                StartCoroutine(waitConnect());
                BackData = 4;
                break;

            case 10:
                ScreenSignin.SetActive(true);
                ScreenSignin.transform.GetChild(2).gameObject.SetActive(true);
                ScreenSignin.transform.GetChild(1).gameObject.SetActive(false);
                ScreenMode.SetActive(false);
                BackData = 9;
                break;
            case 11:

                break;
            case 12:
                LeaderBoardButton.SetActive(false);
                PlayerPrefs.SetInt("IsTurnTimeActivated", 0);

                Screenhome.SetActive(true);                

                BackData = 4;
                Start_Btn.gameObject.SetActive(false);
                break;

            case 13:
                ScreenSignin.transform.GetChild(2).gameObject.SetActive(true);
                ScreenSignin.transform.GetChild(1).gameObject.SetActive(false);
                ScreenSignin.SetActive(true);

                BackData = 10;
                break;
            case 14: // about
                menuScreens[0].SetActive(true); // 0 = flag , 1 = about , 2 = tutorial , 3 = upgrade , 4 = settings
                menuScreens[1].SetActive(false); 
                BackData = 0;
                Debug.Log("BackData0 ----- 3");
                break;
            case 15: // setting
                menuScreens[0].SetActive(true); // 0 = flag , 1 = about , 2 = tutorial , 3 = upgrade , 4 = settings
                menuScreens[4].SetActive(false);
                BackData = 0;
                Debug.Log("BackData0 ----- 4");
                break;
            case 16: // tutorial
                menuScreens[0].SetActive(true); // 0 = flag , 1 = about , 2 = tutorial , 3 = upgrade , 4 = settings
                menuScreens[2].SetActive(false);
                BackData = 0;
                Debug.Log("BackData0 ----- 5");
                break;
            case 17: // upgrade
                menuScreens[0].SetActive(true); // 0 = flag , 1 = about , 2 = tutorial , 3 = upgrade , 4 = settings
                menuScreens[3].SetActive(false);
                BackData = 0;
                Debug.Log("BackData0 ----- 6");
                break;

        }
    }

    IEnumerator waitConnect()
    {
        comunication.info.Username = "";
        yield return new WaitForSeconds(3f);
        LeaderBoardButton.SetActive(false);
        //PUN_Scr.Connect();
        ScreenSignin.transform.GetChild(2).gameObject.SetActive(false);
        ScreenSignin.transform.GetChild(1).gameObject.SetActive(true);
        ScreenSelectDevice.SetActive(true);
        BackData = 4;
    }

    public void changeScreen(string _name)
    {
        clearScreen();

        switch (_name)
        {
            case "screensettime":
                ScreenSetTime.SetActive(true);
                break;

            case "homescreen":
                Screenhome.SetActive(true);
                break;

            case "playervsplayer":
                Screenhome.SetActive(true);
                break;

            case "practicewithai":
                Screenpracticewithai.SetActive(true);
                break;

            case "practiceonline":
                if (Board.MAX_COL == 8)
                {
                    Screenpracticeonline.SetActive(true);
                    break;
                }
                else
                {
                    ScreenMode.SetActive(true);
                }
                break;
            case "mode":
                if (Board.MAX_COL == 8)
                {
                    if (PlayerPrefs.GetInt("AILevel", -1) == -2)
                    {
                        Screenpracticeonline.SetActive(true);
                    }
                    else
                    {
                        Screenpracticewithai.SetActive(true);
                        onMode(0);
                    }
                }
                else
                {
                    ScreenMode.SetActive(true);
                }
                break;

            case "c_ui_error":
                //			Curruncy_UI_Error.SetActive (true);
                break;
            case "c_ui_buy":
                //			Curruncy_UI_Buy.SetActive (true);
                break;
            case "c_ui_lobby":
                //			Curruncy_UI_Lobby_Massage.SetActive (true);
                break;
        }
    }

    public void SetTurnTime(int num)
    {
        if (num == 1)
        {
            aud_src.clip = Gallop_SFX;
        }
        if (num == 3)
        {
            aud_src.clip = Cantor_SFX;
        }
        if (num == 5)
        {
            aud_src.clip = Trot_SFX;
        }
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();

        Room_BattleTime_txt.text = num + " min";

        PlayerPrefs.SetInt("TurnTime", num);
       
        StartCoroutine(SetTurnTime_wait());
    }

    IEnumerator SetTurnTime_wait()
    {
        yield return new WaitForSeconds(aud_src.clip.length);


        comunication.info.ActivePlayer = 2;
        LoadingScreenManager.LoadScene(2);
  
    }

    public void onOpenMenu()
    {
        ScreenMenuList.SetActive(!ScreenMenuList.activeSelf);
    }

    public void onPlayForRealCash()
    {

    }

    public void onPracticeWithOnline()
    {
        if ( comunication.info.Sku_5)
        {
            if (comunication.info.IsInternetAvailable())
            {
                Curruncy_UI.transform.GetChild(1).GetComponent<Text>().text = comunication.info.COINS.ToString();
                //Curruncy_UI.SetActive(true);
                aud_src.clip = onlinePlayer_SFX;
                if (PlayerPrefs.GetString("Sound") == "On")
                    aud_src.Play();
                comunication.info.Gameplay_Type = 2;
                StartCoroutine(OnlineLocal_wait(true));
            }
            else
            {
                Curruncy_UI.SetActive(false);
                Internet_Pannel.SetActive(true);                
            }
        }
        else
        {
            // Show the purchase screen menu
            PurchaseMenu.SetActive(true);
        }

    }

    public void onPlayOneDevice()
    {

        if (comunication.info.Sku_2 || comunication.info.Sku_5)
        {
            PurchaseMenu.SetActive(false);
            //aud_src.clip = LocalPlayer_SFX;
            aud_src.clip = PvP_SFX;
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();
            comunication.info.Gameplay_Type = 1;
            StartCoroutine(OnlineLocal_wait(false));
        }
        else
        {
            // Show the purchase screen menu
            PurchaseMenu.SetActive(true);
        }
    }

    IEnumerator OnlineLocal_wait(bool isOnline)
    {
        yield return new WaitForSeconds(aud_src.clip.length);

        ScreenSelectDevice.SetActive(false);

        if (isOnline)
        {
            if (comunication.info.COINS > 199)
            {
                //Curruncy_UI.SetActive(true);
                comunication.info.PUN_IsOnline = true;
                ScreenSignin.SetActive(true);

                // Check If its a new User
                if (!string.IsNullOrEmpty(PlayerPrefs.GetString("Email")) && !string.IsNullOrEmpty(PlayerPrefs.GetString("Pwd")))
                {
//                    FBA.Connect_pannel.SetActive(false);
                    ScreenSignin.transform.GetChild(4).gameObject.SetActive(true);
//                    FBA.ReSignIn();
                    BackData = 9;
                }
                else if (!string.IsNullOrEmpty(PlayerPrefs.GetString("FbUser")))
                {
//                    FBA.CreateFBUser(PlayerPrefs.GetString("FbUser"));
                    BackData = 9;
                }
                else
                {
                    if (string.IsNullOrEmpty(comunication.info.Username))
                    {                        
                        ScreenSignin.transform.GetChild(2).gameObject.SetActive(false);
                        ScreenSignin.transform.GetChild(1).gameObject.SetActive(true);
                        BackData = 8;
                    }
                    else
                    {                        
//                        FBA.Username_InputFeild.text = comunication.info.Username;
//                        FBA.Password_InputFeild.text = comunication.info.Password;
//                        FBA.Email_InputFeild.text = comunication.info.Email;
                        ScreenSignin.transform.GetChild(2).gameObject.SetActive(true);
                        ScreenSignin.transform.GetChild(1).gameObject.SetActive(false);
                        BackData = 9;
                    }
                }
            }
            else
            {
                changeScreen("c_ui_error");
            }
        }
        else
        {
            Curruncy_UI.SetActive(false);
            comunication.info.PUN_IsOnline = false;
            // aud_src.clip = Battle_Time_Sfx;
            // if (PlayerPrefs.GetString("Sound") == "On")
            //     aud_src.Play();
            ScreenMode.SetActive(true);
            //PvP_Timer_Selection_Screen.SetActive(true);
            BackData = 4;
        }
    }

    void FixedUpdate()
    {
        if (ScreenSignin.transform.GetChild(2).gameObject.activeInHierarchy)
        {
            ScreenSignin.transform.GetChild(4).gameObject.SetActive(false);
        }
    }

    public void Buy_Coin_Btn(bool show)
    {
        aud_src.clip = Button_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();

        if (show)
        {
            changeScreen("c_ui_buy");
        }
        else
        {
            changeScreen("playervsplayer");
        }
    }

    public void Buy_IAP_Btn(string amount)
    {
        switch (amount)
        {
            // back btn
            case "back":
                changeScreen("c_ui_error");
                break;

            case "200":

                break;

            case "500":

                break;

            case "1000":

                break;

            case "1600":

                break;
        }
    }

    // Call For Room Created Online
    public void temproom_created_callback()
    {
        // aud_src.clip = Battle_Time_Sfx;
        // if (PlayerPrefs.GetString("Sound") == "On")
        //     aud_src.Play();
        ScreenSignin.SetActive(false);
        ScreenMode.SetActive(true);
        //PvP_Timer_Selection_Screen.SetActive(true);
        BackData = 10;
        //PUN_Scr.Update_Room_Info ();
    }

    public void room_created_callback()
    {

    }

    public void Create_Join_Btn(bool IsMaster)
    {
        CheckMaster = IsMaster;
    }







    public void On_RoomInfoBack()
    {
        HomeUIControllerScript.instance.Off_RoomInfo();
    }

    public void Off_RoomInfo()
    {
        Room_Info_Pannel.transform.GetChild(7).gameObject.SetActive(false);
        Room_Info_Pannel.transform.GetChild(8).gameObject.SetActive(false);
        Room_Info_Pannel.SetActive(false);
    }

    public void PlayOnline_Back_Btn()
    {
        // Reset Values
        comunication.info.PUN_IsOnline = false;
        ScreenSignin.SetActive(true);



        ScreenSignin.SetActive(false);
        ScreenSelectDevice.SetActive(false);

        StartCoroutine(waitConnect());
    }

    public void onPracticeWithAI()
    {
        aud_src.clip = PvC_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();

        comunication.info.Gameplay_Type = 0;
        StartCoroutine(PlayerVerses_Wait(false));
    }

    public void onPlayerVSPlayer()
    {
        clearScreen();

        //for test
        if ( comunication.info.Sku_2 || comunication.info.Sku_5)
        {
            PurchaseMenu.SetActive(false);
            aud_src.clip = PvP_SFX;
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();
            StartCoroutine(PlayerVerses_Wait(true));
        }
        else
        {
            // Show the purchase screen menu
            PurchaseMenu.SetActive(true);
        }
    }

    IEnumerator PlayerVerses_Wait(bool isPvP)
    {
        yield return new WaitForSeconds(aud_src.clip.length);

        if (isPvP)
        {
            BackData = 4;
            changeScreen("playervsplayer");
        }
        else
        {
            BackData = 2;
            changeScreen("practicewithai");
        }
    }

    public void Purchase_Btn(int Purchase_Type)
    {
        Button_Sound();
        if (comunication.info.IsInternetAvailable() && !comunication.info.Sku_5)
        {
            switch (Purchase_Type)
            {
                case 0:                    
                    if(comunication.info.Sku_1)
                    {
                        ShowMessagePopup("Already Purchased.");
                        return;
                    }
                    admobdemo.instance.HideBanner();
                    aud_src.clip = Upgrade_SFX;
                    if (PlayerPrefs.GetString("Sound") == "On")
                        aud_src.Play();
                    FindObjectOfType<Purchaser>().BuyNonConsumable(Purchaser.skuThreeForCom);
                    break;
                case 1:                
                    if (comunication.info.Sku_2)
                    {
                        ShowMessagePopup("Already Purchased.");
                        return;
                    }    
                    aud_src.clip = Upgrade_SFX;
                    if (PlayerPrefs.GetString("Sound") == "On")
                        aud_src.Play();
                    if (!comunication.info.Sku_1)
                    {
                        ShowMessagePopup("Please Purchase normal formations packs first.");
                        return;
                    }
                    FindObjectOfType<Purchaser>().BuyNonConsumable(Purchaser.skuThreeForPlayer);
                    break;
                case 2:                    
                    if (comunication.info.Sku_3)
                    {
                        ShowMessagePopup("Already Purchased.");
                        return;
                    }
                    aud_src.clip = Upgrade_SFX;
                    if (PlayerPrefs.GetString("Sound") == "On")
                        aud_src.Play();
                    if (!comunication.info.Sku_1 || !comunication.info.Sku_2)
                    {
                        ShowMessagePopup("Please Purchase normal formations packs first.");
                        return;
                    }
                    FindObjectOfType<Purchaser>().BuyNonConsumable(Purchaser.skuCrossForCom);
                    break;
                case 3:                    
                    if (comunication.info.Sku_4)
                    {
                        ShowMessagePopup("Already Purchased.");
                        return;
                    }
                    aud_src.clip = Upgrade_SFX;
                    if (PlayerPrefs.GetString("Sound") == "On")
                        aud_src.Play();
                    if (!comunication.info.Sku_1 || !comunication.info.Sku_2|| !comunication.info.Sku_3 )
                    {
                        ShowMessagePopup("Please Purchase normal formations packs first.");
                        return;
                    }
                    FindObjectOfType<Purchaser>().BuyNonConsumable(Purchaser.skuCrossForPlayer);
                    break;
                case 4:
                    if (comunication.info.Sku_5)
                    {
                        ShowMessagePopup("Already Purchased.");
                        return;
                    }
                    aud_src.clip = Upgrade_SFX;
                    if (PlayerPrefs.GetString("Sound") == "On")
                        aud_src.Play();
                    

                    FindObjectOfType<Purchaser>().BuyNonConsumable(Purchaser.skuAllUnLock);
                    break;
                case 5:
                    aud_src.clip = Upgrade_SFX;
                    if (PlayerPrefs.GetString("Sound") == "On")
                        aud_src.Play();
                    FindObjectOfType<Purchaser>().RestorePurchases();
                    break;
                
            }
        }
        else
        {
            if(comunication.info.Sku_5){
                Purchase_Type = 10;
            }else
                
                Internet_Pannel.SetActive(true);
        }

        if (Purchase_Type == 10) // Back Btn
        {
            PurchaseMenu.SetActive(false);
            changeScreen("homescreen");
        }
    }

  

    private IEnumerator LoadUserInfo()
    {
        yield return new WaitForSeconds(0.1f);
        if (GameObject.Find("Username") != null)
        {
            string username = PlayerPrefs.GetString("username");
            string password = PlayerPrefs.GetString("password");
            GameObject.Find("Username").GetComponent<InputField>().text = username;
            GameObject.Find("Password").GetComponent<InputField>().text = password;
        }
    }

    public void onFriends()
    {

    }

    public void onProfile()
    {

    }

    public void onFacebook()
    {

    }

    public void onSetting()
    {
       
        ScreenSetting.SetActive(true);
    }

    public void onSettingClose()
    {
        ScreenSetting.SetActive(false);
    }

    public void onSigninClose()
    {
        ScreenSignin.SetActive(false);
    }

    public void OnRegistration()
    {

    }

    public void Login()
    {

    }

    public void ShowMsg(string msg, float showtime = 5.0f)
    {
        lock (MessageForm)
        {
            msgID++;
            MessageText.GetComponent<Text>().text = msg;
            MessageForm.SetActive(true);
            StartCoroutine(HideMsg(msgID, showtime));
        }
    }

    private IEnumerator HideMsg(int id, float showtime)
    {
        yield return new WaitForSeconds(showtime);
        lock (MessageForm)
        {
            if (msgID == id)
                MessageForm.SetActive(false);
        }
    }

    public void onAIGame(int level)
    {
        if (level == 0)
        {
            aud_src.clip = Easy_SFX;
        }

        if (level == 1)
        {
            aud_src.clip = Normal_SFX;
        }

        if (level == 2)
        {
            aud_src.clip = Hard_SFX;
        }
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
        comunication.info.PUN_IsOnline = false;
        comunication.info.Save();
        PlayerPrefs.SetInt("AILevel", level);
        changeScreen("mode");
        Debug.Log("BackData------------3");
        BackData = 3;
        
    }

    public void onBlitz(int type)
    {
        PlayerPrefs.SetInt("AILevel", -2);
        PlayerPrefs.SetInt("Type", type);
        changeScreen("mode");
    }

    public void onMode(int mode)
    {


        //TODO: Hashbyte admobdemo.instance.HideBanner();
        if (mode == 0)
        {
 
            if (comunication.info.Gameplay_Trail < 5 && comunication.info.Gameplay_Type == 0)
            {
                comunication.info.Gameplay_Trail = comunication.info.Gameplay_Trail + 1;
                aud_src.clip = KnightsTemplar_SFX;
                if (PlayerPrefs.GetString("Sound") == "On")
                    aud_src.Play();
                comunication.info.PUN_GameplayMode = mode;
                StartCoroutine(onMode_wait(mode));
            }
            else
            {
                if ((comunication.info.Sku_1 && comunication.info.Gameplay_Type == 0)
                    || (comunication.info.Sku_2 && comunication.info.Gameplay_Type == 1)
                    || comunication.info.Sku_5)
                {
                    aud_src.clip = KnightsTemplar_SFX;
                    if (PlayerPrefs.GetString("Sound") == "On")
                        aud_src.Play();
                    comunication.info.PUN_GameplayMode = mode;
                    StartCoroutine(onMode_wait(mode));
                }
                else
                {
                    // Show the purchase screen menu
                    PurchaseMenu.transform.GetChild(1).gameObject.SetActive(true);
                    PurchaseMenu.SetActive(true);
                    comunication.info.PUN_GameplayMode = 0;
                }
            }

        }

        else if (mode == 1)
        {
            if ((comunication.info.Sku_1 && comunication.info.Gameplay_Type == 0)
                || (comunication.info.Sku_2 && comunication.info.Gameplay_Type == 1)
                || comunication.info.Sku_5)
            {
                aud_src.clip = KnightsOfColumbus_SFX;
                if (PlayerPrefs.GetString("Sound") == "On")
                    aud_src.Play();
                //				PlayerPrefs.SetInt("Mode", mode);
                comunication.info.PUN_GameplayMode = mode;
                StartCoroutine(onMode_wait(mode));
            }
            else
            {
                // Show the purchase screen menu
                PurchaseMenu.transform.GetChild(1).gameObject.SetActive(false);
                PurchaseMenu.SetActive(true);
                //				PlayerPrefs.SetInt("Mode", 0);
                comunication.info.PUN_GameplayMode = 0;
            }
        }

        else if (mode == 2)
        {
            if ((comunication.info.Sku_1 && comunication.info.Gameplay_Type == 0)
                || (comunication.info.Sku_2 && comunication.info.Gameplay_Type == 1)
                || comunication.info.Sku_5)
            {
                aud_src.clip = KnightsRoundTable_SFX;
                if (PlayerPrefs.GetString("Sound") == "On")
                    aud_src.Play();
                //				PlayerPrefs.SetInt("Mode", mode);
                comunication.info.PUN_GameplayMode = mode;
                StartCoroutine(onMode_wait(mode));
            }
            else
            {
                // Show the purchase screen menu
                PurchaseMenu.transform.GetChild(1).gameObject.SetActive(false);
                PurchaseMenu.SetActive(true);
                //				PlayerPrefs.SetInt("Mode", 0);
                comunication.info.PUN_GameplayMode = 0;
            }
        }

        else
        {
            if ((comunication.info.Sku_3 && comunication.info.Gameplay_Type == 0)
                || (comunication.info.Sku_4 && comunication.info.Gameplay_Type == 1)
                || comunication.info.Sku_5)
            {
                ScreenMode.SetActive(false);
                ModeSelectWnd.SetActive(true);

                switch (mode)
                {
                    case 3:
                        ScreenModes[0].SetActive(true);
                        ScreenModes[2].SetActive(true);
                        ScreenModes[1].SetActive(false);

                        ScreenModes[0].GetComponent<RectTransform>().localPosition = new Vector3(0, 100, 0);
                        ScreenModes[2].GetComponent<RectTransform>().localPosition = new Vector3(0, -100, 0);
                        break;
                    case 4:
                        ScreenModes[0].SetActive(false);
                        ScreenModes[1].SetActive(true);
                        ScreenModes[2].SetActive(true);

                        ScreenModes[1].GetComponent<RectTransform>().localPosition = new Vector3(0, -100, 0);
                        ScreenModes[2].GetComponent<RectTransform>().localPosition = new Vector3(0, 100, 0);
                        break;
                    case 5:
                        ScreenModes[0].SetActive(true);
                        ScreenModes[1].SetActive(true);
                        ScreenModes[2].SetActive(false);

                        ScreenModes[0].GetComponent<RectTransform>().localPosition = new Vector3(0, -100, 0);
                        ScreenModes[1].GetComponent<RectTransform>().localPosition = new Vector3(0, 100, 0);
                        break;
                }

                aud_src.clip = KnightsDomain_SFX;

                if (PlayerPrefs.GetString("Sound") == "On")
                    aud_src.Play();

                comunication.info.PUN_GameplayMode = mode;
            }
            else
            {
                // Show the purchase screen menu
                PurchaseMenu.transform.GetChild(1).gameObject.SetActive(false);
                PurchaseMenu.SetActive(true);
                comunication.info.PUN_GameplayMode = 0;
            }
        }
        comunication.info.Save();
    }

    public void OnSelectMode(int formation_mode)
    {
        if (formation_mode == 0)
            aud_src.clip = KnightsTemplar_SFX;
        else if (formation_mode == 1)
            aud_src.clip = KnightsOfColumbus_SFX;
        else if (formation_mode == 2)
            aud_src.clip = KnightsRoundTable_SFX;

        comunication.info.PUN_GameFormationMode = formation_mode;

        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
        StartCoroutine(onMode_wait(comunication.info.PUN_GameplayMode));

    }

    public void OnMode_Purchase_Close_Btn()
    {
        aud_src.clip = KnightsTemplar_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
        StartCoroutine(onMode_wait(0));
    }

    IEnumerator onMode_wait(int Mode)
    {        
        yield return new WaitForSeconds(aud_src.clip.length);

        if (comunication.info.Gameplay_Type == 1)
        {
            
            BackData = 6;
            Back_But.SetActive(true);
            ScreenMode.SetActive(false);
            //added by me
            ModeSelectWnd.SetActive(false);
            PvP_Timer_Selection_Screen.SetActive(true);            
        }
        else
        {
            BackData = 6;
            comunication.info.ActivePlayer = 2;
            comunication.info.PUN_GameplayMode = Mode;
            LoadingScreenManager.LoadScene(2);
        }

    }


    public void onNextBtn()
    {
        aud_src.clip = NewPlay_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
        TitlePage.SetActive(false);
        changeScreen("homescreen");
        BackData = 1;
    }

    public void OnHome(){
        Debug.Log("BackData0 ----- 1");
        BackData = 0;
    }

    public void OnSetting()
    {
        BackData = 15;
        aud_src.clip = NewSettings_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
    }

    public void onTutorial(){
        BackData = 16;
    }

    public void onUpgradePressedOnMenu()
    {
        BackData = 17;
    }

    public void OnAbout()
    {
        BackData = 14;
        // admobdemo.instance.LoadInterstitial();
        admobdemo.instance.ShowInterstitial();
        aud_src.clip = NewAbout_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
    }

    public void ReplayingGame()
    {
        clearScreen();
        TitlePage.SetActive(false);
        Back_But.SetActive(true);
        if (comunication.info.Gameplay_Type == 0)
        {
            BackData = 3;
            changeScreen("practiceonline");            
        }
        else if (comunication.info.Gameplay_Type == 1)
        {
            ScreenMode.SetActive(true);
            BackData = 5;
        }
        else
        {
            ReplayInfoPanel.SetActive(true);
            Invoke("WaitingForOtherPlayer", 0);
            BackData = 11;            
        }
        
        Curruncy_UI.transform.GetChild(1).GetComponent<Text>().text = comunication.info.COINS.ToString();
    }

    void WaitingForOtherPlayer()
    {
        // comunication.info.playAgain = false;        
        ReplayInfoPanel.transform.GetChild(2).GetComponent<Text>().text =
        "Waiting for other player, use Chat to discuss formation";
    }

    public void Cancel_Btn()
    {
        aud_src.clip = Back_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
    }

    public void Quit_Btn()
    {
        aud_src.clip = CheerThen_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
        StartCoroutine(quit_wait());
    }

    IEnumerator quit_wait()
    {
        yield return new WaitForSeconds(aud_src.clip.length);
        Application.Quit();
    }

    void Button_Sound()
    {

        aud_src.clip = Button_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
        {
            aud_src.Play();
        }
    }


    public void CreateGame()
    {

        HomeUIControllerScript.instance.temproom_created_callback();
    }
}
