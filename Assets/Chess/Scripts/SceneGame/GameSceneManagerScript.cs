using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using System.Collections;
using ChessLibrary;

public class GameSceneManagerScript : MonoBehaviour
{
    public GameObject CanvasObj, Board_BG;
    public GameObject Merge_Board_Parent;
    RectTransform Board_BG_Rect;

    public GameObject Panel, Internet_Pannel, PurchasePopUP, ChatUI, ChatScript, chatBtn, chatImg, thankspanel;

    [Header("KD Board Sprite")]
    public Sprite Board_Black_Gold;
    public Sprite Board_Black_Green, Board_Black_Purple, Board_White_Gold, Board_White_Green, Board_White_Purple, Temp_Board;

    [Header("KD New Board")]
    public Sprite KT_piece_1;
    public Sprite KT_piece_2, KRT_piece_1, KRT_piece_2, KC_piece_1, KC_piece_2;

    [Header("Newboard Special Area")]
    public Sprite KT_special1;
    public Sprite KT_special2, KRT_special1, KRT_special2, KC_special1, KC_special2;
    public Image Top_Special_Area, Bottom_Special_Area;

    [Header("Effect Area")]
    public RectTransform Top_Effect1;
    public RectTransform Top_Effect2, Top_Effect3, Bottom_Effect1, Bottom_Effect2, Bottom_Effect3;

    public static GameSceneManagerScript instance;

    int ScreenWidth;
    int ScreenHeight;

    public AudioClip PlayAgain_SFX, Draw_SFX, Resign_SFX, Home_Sfx, Cheers_SFX, Facebook_Sfx, Confermed_SFX, Cancel_SFX, Button_SFX, Back_SFX,
        Upgrade_SFX, SendChat_Sfx, NewSettings_SFX, NewAbout_SFX, Tutorial_Sfx;
    AudioSource aud_src;

    void Awake()
    {
        CheckSound();
    }

    void OnDestroy()
    {
        instance = null;
    }

    void CheckSound()
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

    void Start()
    {
        instance = this;

        if (comunication.info.PUN_GameplayMode == 0)
        {
            Board_BG.GetComponent<Image>().sprite = Board_White_Gold;

        }
        // KD Knights of Columbs > Purple
        if (comunication.info.PUN_GameplayMode == 1)
        {
            Board_BG.GetComponent<Image>().sprite = Board_White_Purple;
        }
        // KD Knights of Round table > Green
        if (comunication.info.PUN_GameplayMode == 2)
        {
            Board_BG.GetComponent<Image>().sprite = Board_White_Green;
        }
        //added by me
        //KT vs KRT
        if (comunication.info.PUN_GameplayMode == 3)
        {
            //Board_BG.GetComponent<Image>().sprite = NewBoard1_1;
            CreateMergeBoard(comunication.info.PUN_GameplayMode, comunication.info.PUN_GameFormationMode, false);
        }
        //KRT vs KC
        if (comunication.info.PUN_GameplayMode == 4)
        {
            //Board_BG.GetComponent<Image>().sprite = NewBoard2_1;
            CreateMergeBoard(comunication.info.PUN_GameplayMode, comunication.info.PUN_GameFormationMode, false);
        }
        //KC vs KT
        if (comunication.info.PUN_GameplayMode == 5)
        {
            //Board_BG.GetComponent<Image>().sprite = NewBoard3_1;
            CreateMergeBoard(comunication.info.PUN_GameplayMode, comunication.info.PUN_GameFormationMode, false);
        }
        PlayAgain();
        
			
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;

        aud_src = GetComponent<AudioSource>();
        Board_BG_Rect = Board_BG.GetComponent<RectTransform>();


        chatBtn.SetActive(false);
        chatImg.SetActive(false);

    }

    public void CreateMergeBoard(int gameModeID, int formationID, bool isBlack)
    {
        Board_BG.GetComponent<Image>().sprite = Temp_Board;
        Debug.Log("----Game Mode : " + gameModeID + " , formation Mode : " + formationID + "----");
        //Merge_Board_Parent
        Sprite sprite1_1=null, sprite1_2=null;
        Sprite sprite2_1=null, sprite2_2=null;
        switch (gameModeID)
        {
            case 3:
                if (formationID == 0)
                {
                    if (isBlack == false)
                    {
                        sprite1_1 = KT_piece_1;
                        sprite1_2 = KT_piece_2;
                        sprite2_1 = KRT_piece_1;
                        sprite2_2 = KRT_piece_2;
                        Top_Special_Area.sprite = KRT_special2;
                        Bottom_Special_Area.sprite = KT_special1;
                    }
                    else
                    {
                        sprite1_1 = KT_piece_2;
                        sprite1_2 = KT_piece_1;
                        sprite2_1 = KRT_piece_2;
                        sprite2_2 = KRT_piece_1;
                        Top_Special_Area.sprite = KRT_special1;
                        Bottom_Special_Area.sprite = KT_special2;
                    }

                    
                }
                else if(formationID == 2)
                {
                    if (isBlack == false)
                    {
                        sprite2_1 = KT_piece_1;
                        sprite2_2 = KT_piece_2;
                        sprite1_1 = KRT_piece_1;
                        sprite1_2 = KRT_piece_2;

                        Top_Special_Area.sprite = KT_special2;
                        Bottom_Special_Area.sprite = KRT_special1;
                    }
                    else
                    {
                        sprite2_1 = KT_piece_2;
                        sprite2_2 = KT_piece_1;
                        sprite1_1 = KRT_piece_2;
                        sprite1_2 = KRT_piece_1;

                        Top_Special_Area.sprite = KT_special1;
                        Bottom_Special_Area.sprite = KRT_special2;
                    }
                }
                break;
            case 4:
                if (formationID == 1)
                {
                    if (isBlack == false)
                    {
                        sprite2_1 = KRT_piece_1;
                        sprite2_2 = KRT_piece_2;
                        sprite1_1 = KC_piece_1;
                        sprite1_2 = KC_piece_2;
                        Top_Special_Area.sprite = KRT_special2;
                        Bottom_Special_Area.sprite = KC_special1;
                    }else
                    {
                        sprite2_1 = KRT_piece_2;
                        sprite2_2 = KRT_piece_1;
                        sprite1_1 = KC_piece_2;
                        sprite1_2 = KC_piece_1;
                        Top_Special_Area.sprite = KRT_special1;
                        Bottom_Special_Area.sprite = KC_special2;
                    }
                }
                else if (formationID == 2)
                {
                    if (isBlack == false)
                    {
                        sprite1_1 = KRT_piece_1;
                        sprite1_2 = KRT_piece_2;
                        sprite2_1 = KC_piece_1;
                        sprite2_2 = KC_piece_2;

                        Top_Special_Area.sprite = KC_special2;
                        Bottom_Special_Area.sprite = KRT_special1;
                    }
                    else
                    {
                        sprite1_1 = KRT_piece_2;
                        sprite1_2 = KRT_piece_1;
                        sprite2_1 = KC_piece_2;
                        sprite2_2 = KC_piece_1;

                        Top_Special_Area.sprite = KC_special1;
                        Bottom_Special_Area.sprite = KRT_special2;
                    }
                }
                break;
            case 5:
                if (formationID == 0)
                {
                    if (isBlack == false)
                    {
                        sprite2_1 = KC_piece_1;
                        sprite2_2 = KC_piece_2;
                        sprite1_1 = KT_piece_1;
                        sprite1_2 = KT_piece_2;

                        Top_Special_Area.sprite = KC_special2;
                        Bottom_Special_Area.sprite = KT_special1;
                    }
                    else
                    {
                        sprite2_1 = KC_piece_2;
                        sprite2_2 = KC_piece_1;
                        sprite1_1 = KT_piece_2;
                        sprite1_2 = KT_piece_1;

                        Top_Special_Area.sprite = KC_special1;
                        Bottom_Special_Area.sprite = KT_special2;
                    }
                }
                else if (formationID == 1)
                {
                    if (isBlack == false)
                    {
                        sprite1_1 = KC_piece_1;
                        sprite1_2 = KC_piece_2;
                        sprite2_1 = KT_piece_1;
                        sprite2_2 = KT_piece_2;

                        Top_Special_Area.sprite = KT_special2;
                        Bottom_Special_Area.sprite = KC_special1;
                    }else
                    {
                        sprite1_1 = KC_piece_2;
                        sprite1_2 = KC_piece_1;
                        sprite2_1 = KT_piece_2;
                        sprite2_2 = KT_piece_1;

                        Top_Special_Area.sprite = KT_special1;
                        Bottom_Special_Area.sprite = KC_special2;
                    }
                }
                break;
        }

        if (isBlack == false)
        {
            Top_Special_Area.gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0);
            Bottom_Special_Area.gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 0);

            /*Top_Effect1.localPosition = new Vector3(0, 412,0);
            Top_Effect2.localPosition = new Vector3(-118, 295, 0);
            Top_Effect3.localPosition = new Vector3(116, 295, 0);*/
            Top_Effect1.localPosition = new Vector3(-118, 412, 0);
            Top_Effect2.localPosition = new Vector3(116, 412, 0);
            Top_Effect3.localPosition = new Vector3(0, 295, 0);

            Bottom_Effect1.localPosition = new Vector3(0, -410, 0);
            Bottom_Effect2.localPosition = new Vector3(-118, -293, 0);
            Bottom_Effect3.localPosition = new Vector3(116, -293, 0);

            
        }
        else
        {
            Top_Special_Area.gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 180);
            Bottom_Special_Area.gameObject.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 180);

            /*Top_Effect1.localPosition = new Vector3(-118, 412, 0);
            Top_Effect2.localPosition = new Vector3(116, 412, 0);
            Top_Effect3.localPosition = new Vector3(0, 295, 0);*/
            Top_Effect1.localPosition = new Vector3(0, 412, 0);
            Top_Effect2.localPosition = new Vector3(-118, 295, 0);
            Top_Effect3.localPosition = new Vector3(116, 295, 0);
            /*Bottom_Effect1.localPosition = new Vector3(0, -410, 0);
            Bottom_Effect2.localPosition = new Vector3(-118, -293, 0);
            Bottom_Effect3.localPosition = new Vector3(116, -293, 0);*/

            Bottom_Effect1.localPosition = new Vector3(-118, -410, 0);
            Bottom_Effect2.localPosition = new Vector3(116, -410, 0);
            Bottom_Effect3.localPosition = new Vector3(0, -293, 0);
        }

        int child_id = 0;

        foreach (Image tile in Merge_Board_Parent.GetComponentsInChildren<Image>())
        {
            if (child_id < 36)
            {
                if(child_id % 2 == 0)
                    tile.sprite = sprite1_1;
                else
                    tile.sprite = sprite1_2;
            }
            else
            {
                if (child_id % 2 == 0)
                    tile.sprite = sprite2_1;
                else
                    tile.sprite = sprite2_2;
            }
            child_id++;

            if (child_id == 72)
                break;
        }

        Merge_Board_Parent.SetActive(true);
    }

    IEnumerator SelectSide()
    {
        yield return new WaitForSeconds(3f);
        PlayAgain();
    }

    public void ActiveThanksPanel()
    {
        thankspanel.SetActive(true);
    }

    

    public void Purchase_Btn(int Purchase_Type)
    {
        Button_Sound();

        if (comunication.info.IsInternetAvailable())
        {

            if (Purchase_Type == 0) // Purchasing lisence for All Three Formation @ 0.99 $
            {                
                //comunication.info.Save_Purchses(1,1);
//                admobdemo.instance.HideBanner();
                aud_src.clip = Upgrade_SFX;
                if (PlayerPrefs.GetString("Sound") == "On")
                    aud_src.Play();
              //TODO: Hashbyte   SmartIAP.Instance().Purchase("com.kd.allformations");
            }

            if (Purchase_Type == 1) // Purchasing lisence for Player Vs Player & One device @ 2.99 $
            {
                aud_src.clip = Upgrade_SFX;
                if (PlayerPrefs.GetString("Sound") == "On")
                    aud_src.Play();
                if (!comunication.info.Sku_1 && !comunication.info.Sku_5)
                {
                    PurchasePopUP.SetActive(true);
                    return;
                }
                //comunication.info.Save_Purchses (2, 1);
              //TODO: Hashbyte   SmartIAP.Instance().Purchase("com.kd.pvponedev");
            }

            if (Purchase_Type == 2) // Restoring previous purchases
            {
                aud_src.clip = Back_SFX;
                if (PlayerPrefs.GetString("Sound") == "On")
                    aud_src.Play();
            //TODO: Hashbyte     SmartIAP.Instance().RestoreCompletedTransactions();
            }
        }
        else
        {
            Internet_Pannel.SetActive(true);	
        }


    }

    void Button_Sound()
    {
        aud_src.clip = Button_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
    }

    public void OnSetting()
    {
        aud_src.clip = NewSettings_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
    }

    public void OnTutorial()
    {
        aud_src.clip = Tutorial_Sfx;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
    }

    public void OnAbout()
    {
        aud_src.clip = NewAbout_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
    }

    public void SendChat_Sound()
    {
        aud_src.clip = SendChat_Sfx;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
    }

    public void SetCellSize()
    {
        if (ScreenWidth != Screen.width || ScreenHeight != Screen.height)
        {
            ScreenWidth = Screen.width;
            ScreenHeight = Screen.height;
           
            Rect rect = Board_BG_Rect.rect;
            float n = (rect.width) / Board.MAX_COL;
            if (n > ((rect.height) / Board.MAX_ROW))
            {
                n = (rect.height) / Board.MAX_ROW;
            }
            GridLayoutGroup grid = Panel.GetComponent<GridLayoutGroup>();
            grid.cellSize = new Vector2(n, n);
        }
    }

    public float GetCellSize()
    {
        return Panel.GetComponent<GridLayoutGroup>().cellSize.x;
    }

    public void Quit_Btn()
    {
        aud_src.clip = Cheers_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
        StartCoroutine(Quit_Wait());
    }

    IEnumerator Quit_Wait()
    {
        yield return new WaitForSeconds(aud_src.clip.length);
        Application.Quit();
    }

    public void Facebook_Btn()
    {
        aud_src.clip = Facebook_Sfx;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
        StartCoroutine(fb_wait());
    }

    IEnumerator fb_wait()
    {
        yield return new WaitForSeconds(aud_src.clip.length);
        GameEngineScript.instance.UpgradeBanner.SetActive(true);

    }

    public void Draw()
    {
        aud_src.clip = Draw_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
        StartCoroutine(Wait_(1));
    }

    public void Resign_Btn_SFX(int i)
    {
        if (i == 1)
        {
            aud_src.clip = Resign_SFX;
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();
        }
        if (i == 2)
        {
            aud_src.clip = Confermed_SFX;
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();
            //GameEngineScript.instance.pun_src.Disconnect();
        }
        if (i == 3)
        {
            aud_src.clip = Cancel_SFX;
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();
        }
    }

    public void Resign()
    {
        StartCoroutine(Wait_(2));
    }

    public void OnGameClose()
    {
        aud_src.clip = Home_Sfx;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
        //HomeUIControllerScript.instance.PUN_Scr.Disconnect ();
        StartCoroutine(Wait_(3));
    }

    IEnumerator Wait_(int i)
    {
        comunication.info.playAgain = false;
        if (aud_src.clip != null)
            yield return new WaitForSeconds(aud_src.clip.length);
        else
            yield return new WaitForSeconds(1);
		
        if (i == 1) // Draw
        {
            LoadingScreenManager.LoadScene(2);
        }
        if (i == 2) // Resign
        {
			
            comunication.info.Player1_Name = "";
            comunication.info.Player2_Name = "";

            LoadingScreenManager.LoadScene(1);
        }
        if (i == 3) // GameClose
        {
            comunication.info.Player1_Name = "";
            comunication.info.Player2_Name = "";

            LoadingScreenManager.LoadScene(1);
        }
    }

    public void OnHomePage()
    {
        OnGameClose();
    }


    public void OnTryAgain(Button tryAgain)
    {
        //Admob Call
        if (PlayerPrefs.GetInt("GameOpen") >= 5 && !comunication.info.Sku_2 && !comunication.info.Sku_5)
        {
            admobdemo.instance.LoadInterstitial();
            admobdemo.instance.ShowInterstitial();
        }
		
//		admobdemo.instance.LoadInterstitial ();
        aud_src.clip = PlayAgain_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
        GameEngineScript.instance.stoptimer = true;

        Flash.ins.init();        

        StartCoroutine(TryAgain_wait());
       
    }



    public void StartPlayAgain()
    {
        comunication.info.PUN_IsOnline = true;

        comunication.info.Is_Master = false;
     
        comunication.info.ActivePlayer = 1;
        comunication.info.playAgain = true;
        LoadingScreenManager.LoadScene(1);
        //  PlayAgain();
//        aud_src.clip = PlayAgain_SFX;
//        if (PlayerPrefs.GetString("Sound") == "On")
//            aud_src.Play();
//        GameEngineScript.instance.stoptimer = true;
//
//
//        Flash.ins.init();
//        StartCoroutine(TryAgain_wait());

    }

    IEnumerator TryAgain_wait()
    {
        yield return new WaitForSeconds(aud_src.clip.length);


        comunication.info.Is_Master = false;
      

        comunication.info.playAgain = true;
        LoadingScreenManager.LoadScene(1); 
        
    }

    public void PlayAgain()
    {
        GameEngineScript gm = GetComponent<GameEngineScript>();
        gm.IsPause = true;
        gm.ScreenWin.SetActive(false);
        gm.ScreenLost.SetActive(false);
	
//        gm.Activate_Timer_UI(false);

        if (gm != null)
        {
            int type = PlayerPrefs.GetInt("Type");
            int mode = comunication.info.PUN_GameplayMode;
            int level = PlayerPrefs.GetInt("AILevel", 0);
            PlayerPrefs.SetInt("BoardRotation", mode);

            gm.IsOnline = false;
            gm.IsOneDev = false;
            gm.Type = type;
            gm.Mode = mode;
            if (level == -1)
            {
                level = 0;
            }

            if (level == -2)
            {
                gm.IsOnline = true;
                return;
            }
            else if (level == -4)
            {
                gm.IsOneDev = true;
                gm.IsTurnTimeActivated = true;
                gm.Activate_Timer_UI(true);
            }
            else if (level == 0)
            {
                gm.Difficulty = GameLevel.Beginner;
            }
            else if (level == 1)
            {
                gm.Difficulty = GameLevel.Intermediate;
            }
            else
            {
                if (level == 2)
                {
                    gm.Difficulty = GameLevel.Master;
                }
            }

            gm.CheckColor();
            Check_for_board_sprite(mode, false);
        }
    }

    public void ChangeBoard_Rotation(float Rotation)
    {	
        Board_BG.transform.rotation = Quaternion.Euler(0, 0, Rotation);

        // KD Knights Tampler > Gold
        if (comunication.info.PUN_GameplayMode == 0)
        {
            Board_BG.GetComponent<Image>().sprite = Board_White_Gold;

        }
        // KD Knights of Columbs > Purple
        if (comunication.info.PUN_GameplayMode == 1)
        {
            Board_BG.GetComponent<Image>().sprite = Board_White_Purple;
        }
        // KD Knights of Round table > Green
        if (comunication.info.PUN_GameplayMode == 2)
        {
            Board_BG.GetComponent<Image>().sprite = Board_White_Green;
        }

        if(comunication.info.PUN_GameplayMode > 2)
        {
            Merge_Board_Parent.transform.rotation = Quaternion.Euler(0, 0, Rotation);
        }
    }

    public void ChangeBoardImgRotated()
    {
        int mode = PlayerPrefs.GetInt("BoardRotation");
        int index = (mode + 3) % 6;
        PlayerPrefs.SetInt("BoardRotation", index);
        Check_for_board_sprite(index, false);
    }

    public void Check_for_board_sprite(int Board_Rotation_Mode , bool isBlack)
    {
        // KD Knights Tampler > Gold
        if (Board_Rotation_Mode == 0)
        {
            Board_BG.GetComponent<Image>().sprite = Board_White_Gold;
        }

        //removed by me
        /*if (Board_Rotation_Mode == 3)
        {
            Board_BG.GetComponent<Image>().sprite = Board_Black_Gold;
        }*/

        // KD Knights of Columbs > Purple
        if (Board_Rotation_Mode == 1)
        {
            Board_BG.GetComponent<Image>().sprite = Board_White_Purple;
        }

        //removed by me
        /*
        if (Board_Rotation_Mode == 4)
        {
            Board_BG.GetComponent<Image>().sprite = Board_Black_Purple;
        }*/

        // KD Knights of Round table > Green
        if (Board_Rotation_Mode == 2)
        {
            Board_BG.GetComponent<Image>().sprite = Board_White_Green;
        }

        //removed by me
        /*
        if (Board_Rotation_Mode == 5)
        {
            Board_BG.GetComponent<Image>().sprite = Board_Black_Green;
        }*/


        //added by me
        //KT vs KRT
        if (comunication.info.PUN_GameplayMode == 3)
        {
            //Board_BG.GetComponent<Image>().sprite = NewBoard1_1;
            CreateMergeBoard(comunication.info.PUN_GameplayMode, comunication.info.PUN_GameFormationMode, isBlack);
        }
        //KRT vs KC
        if (comunication.info.PUN_GameplayMode == 4)
        {
            //Board_BG.GetComponent<Image>().sprite = NewBoard2_1;
            CreateMergeBoard(comunication.info.PUN_GameplayMode, comunication.info.PUN_GameFormationMode, isBlack);
        }
        //KC vs KT
        if (comunication.info.PUN_GameplayMode == 5)
        {
            //Board_BG.GetComponent<Image>().sprite = NewBoard3_1;
            CreateMergeBoard(comunication.info.PUN_GameplayMode, comunication.info.PUN_GameFormationMode, isBlack);
        }
    }
}
