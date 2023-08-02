using UnityEngine;
using System.Collections;
using ChessLibrary;
using UnityEngine.UI;
using System.Threading;
using System;
//using Firebase.Analytics;

public class GameEngineScript : MonoBehaviour
{
    public GameObject Panel, PeerObj, YouObj;
    public GameObject PieceSet;
    public GameObject[] Pieces;

    public GameObject DiePieceSet;
    public GameObject GameLoading;
    public GameObject ScreenWin;
    public GameObject ScreenLost;
    public GameObject Screen_Draw;

    public GameObject TryAgain;
    public GameObject ScreenLock;
    public GameObject ScreenMoves;
    public GameObject ScreenTutorial;

    public Text Black_Move_History_Text, White_Move_History_Text;
    public bool IsBlackMove, RotateMoveEnabled;

    public GameObject MessageBox;
    public GameObject ResignDialog;
    public GameObject DrawDialog;
    public GameObject CloseButton;
    public GameObject UpgradeBanner;
    public GameObject Rotate_Massage_Pannel;
    public GameObject SelectColorDlg;
    public RectTransform Cross_top, Cross_bottom;
    public Sprite[] banners;
    public GameObject PeerAvata;
    public GameObject PeerName;
    public GameObject PeerTime;
    public GameObject PeerDieQueen;
    public GameObject PeerDieRook;
    public GameObject PeerDieBishop;
    public GameObject PeerDieKnight;
    public GameObject PeerDiePawn;

    public GameObject Hourglass;

    public GameObject YourAvata;
    public GameObject YourName;
    public GameObject YourTime;
    public GameObject YourDieQueen;
    public GameObject YourDieRook;
    public GameObject YourDieBishop;
    public GameObject YourDieKnight;
    public GameObject YourDiePawn;

    public GameObject RotateButton;
    public int Rotate_Joust_SFX;
    float Rotate_Joust_SFX_timer;

    public AudioClip[] AClips;

    int YourTimeValue = 0;
    int PeerTimeValue = 0;
    float ResetCountDown = 0;
    public Game ChessGame = null;
    public bool ShowMoveHelp = true;
    public string LastSelectedSquar;
    public string SelectedSquar;
    public bool BlackPlayerComputer = true;
    public bool WhitePlayerComputer = true;
    public string YourColor = "White";
    public GameLevel Difficulty = GameLevel.Beginner;
    private int count_Joust = 0;
    private float mTime;
    private bool mAutoTurn = false;
    public bool m_isPlayerFirst = true;
    private bool PlayHorseSound = true;
    public bool IsRunning = false;
    public bool IsWin = false;
    public bool IsDraw = false;
    public bool IsOnline = false;
    public bool IsOneDev = false;
    public bool Is_Computer_Playing = false;
    public bool IsStarted = false;
    public bool stoptimer = true;
    bool stoptimerlocal = false;
    public static GameEngineScript instance;

    public bool EndedGame;
    //{ get; private set; }
    public int TurnTime { get; set; }

    public int Mode { get; set; }

    public int Type { get; set; }

    public bool IsAutoMove { get; private set; }

    public bool Dontcall;
    [Header("SFX")]
    public AudioClip Button_SFX;
    public AudioClip PlayWhite_SFX, PlayBlack_SFX, Review_SFX, Move_SFX, Rules_SFX, Rotate_SFX, UpgradeRequire_SFX, Win_SFX, Lose_SFX, Check_SFX, Horse_SFX, KT_SFX, KRT_SFX, KOC_SFX;
    public AudioClip New_Joust, Checkmate_SFX, Stalemate_SFX, KnightHood_SFX, WhiteTurn_SFX, BlackTurn_SFX, EnPassant_SFX, YourMove_SFX, Joust_SFX, Back_SFX, WhiteToPlay_SFX;
    AudioSource aud_src;
    public AudioSource aud_src1;
    public AudioSource aud_src2;
    public Text Selection_txt, ActualTimeText;
    public float ActualTime, Peer_Time, Your_Time;
    public float Turn_Time, Timer_Anim_Flash_Time, Timer_Anim_SFX_Time;
    public bool is_sfx_on, is_sfx_on1, is_setside_sfx_on, is_joust_sfx_on;
    public bool IsTurnTimeActivated, Is_P1, Is_P1_Checkmated, Is_P2_Checkmated;

    [Header("Checkmate Effect")]
    public GameObject Lightning_Bolt_Effect;
    public GameObject Lightning_Bolt_Effect_Lost;
    public GameObject Lightning_Bolt_Effect_StaleMate;
    public GameObject StarBurst_Effect;
    public AudioClip Lightning_Bolt_SFX;

    public bool IsPause;

    public GameObject LeaderBoard_Pannel, LB_Loading;
    public Button LeaderBoard_Btn;
    bool IsLeaderBoardActivated;


    [Header("Tournament Match Information")]
    [Tooltip("Online_Match_Information Game panel here. . .")]
    public GameObject OMI_Pannel;

    [Tooltip("Online_Match_Information Start Game Button here. . .")]
    public Button OMI_Btn;
    public bool OMI_IsWhite;

    [Header("Knights Templar")]
    [Tooltip("Knight Templar Player 1 Name text feild here. . .")]
    public Text KT_P1_Name;
    [Tooltip("Knight Templar Player 1 White Win / Lose text feild here. . .")]
    public Text KT_P1_W_WL;
    [Tooltip("Knight Templar Player 1 Black Win / Lose text feild here. . .")]
    public Text KT_P1_B_WL;

    [Tooltip("Knight Templar Player 2 Name text feild here. . .")]
    public Text KT_P2_Name;
    [Tooltip("Knight Templar Player 2 White Win / Lose text feild here. . .")]
    public Text KT_P2_W_WL;
    [Tooltip("Knight Templar Player 2 Black Win / Lose text feild here. . .")]
    public Text KT_P2_B_WL;

    [Header("Knights Of Columbus")]
    [Tooltip("Knights of Columbus Player 1 Name text feild here. . .")]
    public Text KC_P1_Name;
    [Tooltip("Knights of Columbus Player 1 White Win / Lose text feild here. . .")]
    public Text KC_P1_W_WL;
    [Tooltip("Knights of Columbus Player 1 Black Win / Lose text feild here. . .")]
    public Text KC_P1_B_WL;

    [Tooltip("Knights of Columbus Player 2 Name text feild here. . .")]
    public Text KC_P2_Name;
    [Tooltip("Knights of Columbus Player 2 White Win / Lose text feild here. . .")]
    public Text KC_P2_W_WL;
    [Tooltip("Knights of Columbus Player 2 Black Win / Lose text feild here. . .")]
    public Text KC_P2_B_WL;

    [Header("Knights Round Table")]
    [Tooltip("Knights Round Table Player 1 Name text feild here. . .")]
    public Text KRT_P1_Name;
    [Tooltip("Knights Round Table Player 1 White Win / Lose text feild here. . .")]
    public Text KRT_P1_W_WL;
    [Tooltip("Knights Round Table Player 1 Black Win / Lose text feild here. . .")]
    public Text KRT_P1_B_WL;

    [Tooltip("Knights Round Table Player 2 Name text feild here. . .")]
    public Text KRT_P2_Name;
    [Tooltip("Knights Round Table Player 2 White Win / Lose text feild here. . .")]
    public Text KRT_P2_W_WL;
    [Tooltip("Knights Round Table Player 2 Black Win / Lose text feild here. . .")]
    public Text KRT_P2_B_WL;
    public GameObject confrmPanel;
    public bool playAgain = false;
    public Button rotate_button;

    private enum BannerType
    {
        BLACK,
        GREEN,
        PURPLE,
        RED
    }

    public GameEngineScript()
    {
    }

    void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance == null)
        {
            instance = this;
        }
        Dontcall = false;


    }

    void OnDestroy()
    {
        instance = null;
    }

    public void Start()
    {
        aud_src = GetComponent<AudioSource>();
        //TODO: Hashbyte admobdemo.instance.HideBanner();
        if (PlayerPrefs.GetInt("AILevel") == -4)
        {
            Selection_txt.text = "Player Vs Player";
        }
        else
        {
            Selection_txt.text = "Player Vs Computer";
        }

        comunication.info.Scene_Index = 2;

        //        Debug.Log("IsTurnTimeActivated : " + comunication.info.PUN_IsTimerOn);
        //        Debug.Log("BattleTime : " + comunication.info.PUN_BattleTime);
        //        Debug.Log("Mode : " + comunication.info.PUN_GameplayMode);

        IsPause = true;

        Hourglass.SetActive(false);
        EndedGame = false;
        stoptimer = true;
        Lightning_Effect(false);
        if (PlayerPrefs.GetInt("IsTurnTimeActivated") == 0)
        {
            Activate_Timer_UI(false);
        }
        else
        {
            Activate_Timer_UI(true);
        }

    }

    public void Update_Text()
    {
        KT_P1_Name.text = comunication.info.Player1_Name;
        KT_P2_Name.text = comunication.info.Player2_Name;
        KRT_P1_Name.text = comunication.info.Player1_Name;
        KRT_P2_Name.text = comunication.info.Player2_Name;
        KC_P1_Name.text = comunication.info.Player1_Name;
        KC_P2_Name.text = comunication.info.Player2_Name;

        KT_P1_W_WL.text = comunication.info.KT_P1_W_WL;
        KT_P1_B_WL.text = comunication.info.KT_P1_B_WL;

        KT_P2_W_WL.text = comunication.info.KT_P2_W_WL;
        KT_P2_B_WL.text = comunication.info.KT_P2_B_WL;

        KC_P1_W_WL.text = comunication.info.KC_P1_W_WL;
        KC_P1_B_WL.text = comunication.info.KC_P1_B_WL;

        KC_P2_W_WL.text = comunication.info.KC_P2_W_WL;
        KC_P2_B_WL.text = comunication.info.KC_P2_B_WL;

        KRT_P1_W_WL.text = comunication.info.KRT_P1_W_WL;
        KRT_P1_B_WL.text = comunication.info.KRT_P1_B_WL;

        KRT_P2_W_WL.text = comunication.info.KRT_P2_W_WL;
        KRT_P2_B_WL.text = comunication.info.KRT_P2_B_WL;

        check_OMI(comunication.info.PUN_IsOnline, comunication.info.Match_No);        
    }

    void check_OMI(bool isOnline, int Match_No)
    {
        OMI_Pannel.SetActive(isOnline);

        if (Match_No == 1) // Knights Templar with White Player 1 
        {
            OMI_IsWhite = true;
            comunication.info.PUN_GameplayMode = 0;

            KT_P1_Name.text = comunication.info.Player1_Name;
            KT_P2_Name.text = comunication.info.Player2_Name;
        }

        if (Match_No == 2) // Knights Templar with Black Player 1 
        {
            OMI_IsWhite = false;
            comunication.info.PUN_GameplayMode = 0;

            KT_P1_Name.text = comunication.info.Player1_Name;
            KT_P2_Name.text = comunication.info.Player2_Name;
        }

        if (Match_No == 3) // Knights of Columbus with White Player 1 
        {
            OMI_IsWhite = true;
            comunication.info.PUN_GameplayMode = 1;

            KRT_P1_Name.text = comunication.info.Player1_Name;
            KRT_P2_Name.text = comunication.info.Player2_Name;
        }

        if (Match_No == 4) // Knights of Columbus with Black Player 1 
        {
            OMI_IsWhite = false;
            comunication.info.PUN_GameplayMode = 1;

            KRT_P1_Name.text = comunication.info.Player1_Name;
            KRT_P2_Name.text = comunication.info.Player2_Name;

        }

        if (Match_No == 5) // Knights of Round Table with White Player 1 
        {
            OMI_IsWhite = true;
            comunication.info.PUN_GameplayMode = 2;

            KC_P1_Name.text = comunication.info.Player1_Name;
            KC_P2_Name.text = comunication.info.Player2_Name;
        }

        if (Match_No == 6) // Knights of Round Table with Black Player 1 
        {
            OMI_IsWhite = false;
            comunication.info.PUN_GameplayMode = 2;

            KC_P1_Name.text = comunication.info.Player1_Name;
            KC_P2_Name.text = comunication.info.Player2_Name;
        }

        OMI_Btn.gameObject.SetActive(comunication.info.Is_Master);
    }

    public void CheckColor()
    {

        if (!comunication.info.PUN_IsOnline)
        {
            SelectColorDlg.SetActive(true);
        }
    }

    public void Activate_Timer_UI(bool show)
    {
        PeerTime.SetActive(show);
        YourTime.SetActive(show);
        PeerTime.transform.parent.gameObject.SetActive(show);
        YourTime.transform.parent.gameObject.SetActive(show);
    }

    // Online match Information Start Button
    public void OMI_Start_Btn()
    {
        //		if (OMI_IsWhite)
        //		{
        //			OnWhite ();
        //		} else
        //		{
        //			// Hand over controlls to player 2 Lock the screen
        //			comunication.info.ActivePlayer = 2;
        //			OnWhite ();
        //		}
    }

    public void PlayWhiteSfx()
    {
        aud_src.clip = PlayWhite_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
    }

    public void PlayBlackSfx()
    {
        aud_src.clip = PlayBlack_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
    }
    private bool onPlayerColor = true; // true -- white , false --- black
    public void OnWhite()
    {

        if (!comunication.info.PUN_IsOnline)
            PlayWhiteSfx();

        IsBlackMove = true;
        RotateMoveEnabled = true;
        onPlayerColor = true;
        StartCoroutine(SelectSide_wait(true));
    }

    public void OnBlack()
    {
        //added in 8.2
        GetComponent<GameSceneManagerScript>().Check_for_board_sprite(comunication.info.PUN_GameplayMode, true);

        if ((comunication.info.Gameplay_Type == 1) && (Mode > 2))
        {
            aud_src.clip = WhiteToPlay_SFX;
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();
        }
        else
        {
            aud_src.clip = PlayBlack_SFX;
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();
        }
       

        /*if (!comunication.info.PUN_IsOnline)
        {
            PlayBlackSfx();
        }*/


        IsBlackMove = true;
        RotateMoveEnabled = false;
        onPlayerColor = false;
        StartCoroutine(SelectSide_wait(false));
    }

    IEnumerator SelectSide_wait(bool isWhite)
    {
        //        yield return new WaitForSeconds(aud_src.clip.length);                
        if (isWhite)
        {
            m_isPlayerFirst = true;
            BlackPlayerComputer = true;
            WhitePlayerComputer = false;

            //modified by me
            //Cross_bottom.GetComponent<Image>().sprite = this.banners[comunication.info.PUN_GameplayMode + 1];//Cross_top.GetComponent<Image>().sprite;
            if(comunication.info.PUN_GameplayMode < 3)
                Cross_bottom.GetComponent<Image>().sprite = this.banners[comunication.info.PUN_GameplayMode + 1];//Cross_top.GetComponent<Image>().sprite;
            else
                Cross_bottom.GetComponent<Image>().sprite = this.banners[comunication.info.PUN_GameFormationMode + 1];

            Cross_top.GetComponent<Image>().sprite = this.banners[(int)BannerType.BLACK];
            if (IsOneDev && !comunication.info.PUN_IsOnline)
            {
                GameObject Temp = YourDieKnight;
                Vector3 temPos = YourDieKnight.GetComponent<RectTransform>().anchoredPosition;
                string YourDieKnighttxt, YourDiePawntxt, PeerDieKnighttxt, PeerDiePawntxt;
                YourDieKnighttxt = YourDieKnight.GetComponent<Text>().text;
                YourDiePawntxt = YourDiePawn.GetComponent<Text>().text;
                PeerDieKnighttxt = PeerDieKnight.GetComponent<Text>().text;
                PeerDiePawntxt = PeerDiePawn.GetComponent<Text>().text;
                YourDieKnight.GetComponent<Text>().text = PeerDieKnighttxt;
                YourDiePawn.GetComponent<Text>().text = PeerDiePawntxt;
                PeerDieKnight.GetComponent<Text>().text = YourDieKnighttxt;
                PeerDiePawn.GetComponent<Text>().text = YourDiePawntxt;
                YourDieKnight = PeerDieKnight;
                PeerDieKnight = Temp;
                Temp = YourDiePawn;
                YourDiePawn = PeerDiePawn;
                PeerDiePawn = Temp;                
            }

        }
        else
        {
            if (IsOneDev == false && comunication.info.PUN_IsOnline == false)
            {                
                Hourglass.SetActive(true);
                GameObject Temp = PeerName;
                Vector3 temPos = PeerName.GetComponent<RectTransform>().anchoredPosition;

                PeerName = YourName;
                PeerName.GetComponent<RectTransform>().anchoredPosition = YourName.GetComponent<RectTransform>().anchoredPosition;

                YourName = Temp;
                YourName.GetComponent<RectTransform>().anchoredPosition = temPos;
                YourName.GetComponent<Text>().text = "Computer";

                Sprite Tex = Cross_bottom.GetComponent<Image>().sprite;

                if (comunication.info.PUN_GameplayMode < 3)
                {
                    Cross_bottom.GetComponent<Image>().sprite = this.banners[(int)BannerType.BLACK];//Cross_top.GetComponent<Image>().sprite;
                    Cross_top.GetComponent<Image>().sprite = this.banners[comunication.info.PUN_GameplayMode + 1];
                }
                else               //added by me
                {
                    Cross_bottom.GetComponent<Image>().sprite = this.banners[(int)BannerType.BLACK];//Cross_top.GetComponent<Image>().sprite;

                    switch(comunication.info.PUN_GameplayMode)
                    {
                        case 3:
                            if(comunication.info.PUN_GameFormationMode == 0)
                                Cross_top.GetComponent<Image>().sprite = this.banners[3];
                            else
                                Cross_top.GetComponent<Image>().sprite = this.banners[1];
                            break;
                        case 4:
                            if (comunication.info.PUN_GameFormationMode == 1)
                                Cross_top.GetComponent<Image>().sprite = this.banners[3];
                            else
                                Cross_top.GetComponent<Image>().sprite = this.banners[2];
                            break;
                        case 5:
                            if (comunication.info.PUN_GameFormationMode == 1)
                                Cross_top.GetComponent<Image>().sprite = this.banners[1];
                            else
                                Cross_top.GetComponent<Image>().sprite = this.banners[2];
                            break;
                    }
                    
                }
                //aud_src.clip = WhiteToPlay_SFX;
                //if (PlayerPrefs.GetString("Sound") == "On")
               //     aud_src.Play();
                yield return new WaitForSeconds(aud_src.clip.length);
            }


            m_isPlayerFirst = false;

            BlackPlayerComputer = false;
            WhitePlayerComputer = true;

        }

        // Correct Rotation
        if (RotateMoveEnabled) // On White 
        {
            Panel.GetComponent<PanelScript>().Rotate(0);
            Panel.transform.rotation = Quaternion.Euler(0, 0, 0);
            GetComponent<GameSceneManagerScript>().ChangeBoard_Rotation(0);
            Flash.ins.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            RotateMoveEnabled = false;

        }
        else // On Black
        {
            //if(IsOneDev && comunication.info.PUN_IsOnline)
            ChangeAvataData();
            //if()
            {
                GameObject Temp = YourDieKnight;
                YourDieKnight = PeerDieKnight;
                PeerDieKnight = Temp;
                Temp = YourDiePawn;
                YourDiePawn = PeerDiePawn;
                PeerDiePawn = Temp;
            }

            //added by me
            if (comunication.info.PUN_GameplayMode <= 2)
            {
                Panel.GetComponent<PanelScript>().Rotate(180);
                Panel.transform.rotation = Quaternion.Euler(0, 0, 180);
                GetComponent<GameSceneManagerScript>().ChangeBoard_Rotation(180);

                Flash.ins.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);

                RotateMoveEnabled = true;
            }
            
        }
        
        Gameplay_Type(comunication.info.Gameplay_Type);
        SelectColorDlg.SetActive(false);
    }

    void Gameplay_Type(int Type)
    {
        if (Type == 0)// Start Local with AI
        {
            StartLocal();
        }

        if (Type == 1)// Start One Device
        {
            StartOneDev();
        }

        if (Type == 2)//  Start Online
        {
            StartOnline();
        }

        if (comunication.info.ShowTut < 2 && comunication.info.PUN_IsOnline == false)
        {
            // ShowTutorial(true);
            comunication.info.ShowTut++;
            comunication.info.Save();
        }
        // sku_5 -- all purchase
        if (comunication.info.Sku_1 || comunication.info.Sku_5)
        {
            RotateButton.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            RotateButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }

        if (comunication.info.Sku_5)
        {
            IsLeaderBoardActivated = true;
            LeaderBoard_Btn.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {
            IsLeaderBoardActivated = false;
            LeaderBoard_Btn.GetComponent<Image>().color = new Color(1, 1, 1, 0.65f);
        }

        Rotate_Massage_Pannel.transform.GetChild(1).gameObject.SetActive(false);
        Rotate_Massage_Pannel.transform.GetChild(2).gameObject.SetActive(false);
        Rotate_Massage_Pannel.SetActive(false);
    }

    public void StartLocal()
    {
        Is_Computer_Playing = true;

        if (instance == null)
        {
            instance = this;
        }

        StartCoroutine(ShowLoading());

        if (IsOnline == false)
        {
            PlayerPrefs.SetString("Peer", "Computer");

            ShowScreenLock(false);

            if (m_isPlayerFirst)
            {
                StartGame("You");
            }
            else
            {
                StartGame("Computer");
            }
        }
    }

    public void StartOneDev()
    {

        if (instance == null)
        {
            instance = this;
        }

        //		Activate_Timer_UI (true);

        StartCoroutine(ShowLoading());

        if (IsOnline == false)
        {
            BlackPlayerComputer = false;
            WhitePlayerComputer = false;
            IsOneDev = true;
            is_sfx_on = true;
            is_sfx_on1 = true;
            is_setside_sfx_on = true;
            is_joust_sfx_on = true;

            PlayerPrefs.SetString("Peer", "You2");
            ShowScreenLock(false);
        }

        if (m_isPlayerFirst)
        {
            StartGame("You");
        }
        else
        {
            StartGame("You2");
        }
    }

    public void StartOnline()
    {
        if (instance == null)
        {
            instance = this;
        }
        IsOneDev = true;
        StartCoroutine(ShowLoading());
        BlackPlayerComputer = false;
        WhitePlayerComputer = false;
        is_sfx_on1 = true;
        is_sfx_on = true;
        is_setside_sfx_on = true;
        is_joust_sfx_on = true;

        // init Effects
        Flash.ins.init();

        PlayerPrefs.SetString("Peer", "You2");

        if (m_isPlayerFirst) // white First
        {
            StartGame("You");	// Player 1
        }
        else
        {
            ShowScreenLock(false);
            StartGame("You2"); // Player
        }

        // If selected player is First Player
        if (ChessGame.ActivePlay.Name == "You")
        {
            comunication.info.ActivePlayer = 1;
        }
        // If selected player is Second Player
        //else
        if (ChessGame.ActivePlay.Name == "You2")
        {
            comunication.info.ActivePlayer = 2;
        }

        //		IsOnline = true;
        //      PlayerPrefs.SetString("Peer", "Peer");
        //      TryAgain.SetActive(false);
        //        m_appwarp = gameObject.transform.Find("AppwarpManager").GetComponent<appwarp>();
        //		ShowScreenLock(false);
        //        appwarp.instance.Init(username, Type, Mode, TurnTime);
    }

    public void StartGame(string firstTurnUser)
    {
        Debug.Log("---Player : " + firstTurnUser + "---");
        Mode = comunication.info.PUN_GameplayMode;
        Init(firstTurnUser);
        StartCoroutine(NewGame(firstTurnUser));
    }

    public void ShowTutorial(bool isShowing)
    {
        IsPause = isShowing;

        //ScreenTutorial.SetActive(isShowing);

        if (isShowing)
        {
            TutorialScreen.ins.transform.GetChild(0).gameObject.SetActive(true);
            aud_src.clip = Rules_SFX;
        }
        else
        {
            aud_src.clip = Button_SFX;
        }
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
    }

    void AddPieceToPieceSet()
    {
        for (int i = 0; i < 24; i++)
        {
            //modified by me
            //GameObject _temp = Instantiate(Pieces[Mode]) as GameObject;
            GameObject _temp = Instantiate(Pieces[Mode%3]) as GameObject;
            _temp.transform.SetParent(PieceSet.transform);
        }

        GameSceneManagerScript.instance.SetCellSize();
        SetPiecesScale(GameSceneManagerScript.instance.GetCellSize());
    }

    public void SetPiecesScale(float _cellSize)
    {
        for (int i = 0; i < PieceSet.transform.childCount; i++)
        {
            RectTransform _rtTransform = PieceSet.transform.GetChild(i).gameObject.GetComponent<RectTransform>();
            //          float _rate = _cellSize / _rtTransform.rect.width;
            //			print ("Scaling Cell : " + _rate );

            _rtTransform.sizeDelta = new Vector2(_cellSize * 0.65f, _cellSize);
            //          _rtTransform.sizeDelta = new Vector2(_cellSize, _rtTransform.rect.height * _rate);

            //          PieceSet.transform.GetChild(i).transform.localScale = new Vector2(_val,_val);
        }
    }

    void Init(string firstTurnUser)
    {
        mAutoTurn = false;
        IsRunning = false;
        IsWin = false;
        IsDraw = false;

        Is_P1_Checkmated = false;
        Is_P2_Checkmated = false;


        ActualTime = 0;
        

        Rotate_Joust_SFX = 0;
        Rotate_Joust_SFX_timer = 0;

        if (PlayerPrefs.GetInt("IsTurnTimeActivated") == 0)
        {
            IsTurnTimeActivated = false;
        }
        else
        {
            IsTurnTimeActivated = true;
        }

        Turn_Time = 60 * PlayerPrefs.GetInt("TurnTime");

        Timer_Anim_Flash_Time = Turn_Time - 5;
        Timer_Anim_SFX_Time = Turn_Time - 10;

        clearScreen();

        string peer = PlayerPrefs.GetString("Peer");

        Lightning_Effect(false);

        IsPause = false;


        if (firstTurnUser == peer)
        {
            YourColor = "Black";
        }
        else
        {
            YourColor = "White";
        }

        if (IsOneDev)
        {
            if (m_isPlayerFirst)
            {
                YourColor = "White";
            }
            else
            {
                YourColor = "Black";
            }
        }
        if (ChessGame.ActivePlay.Name == "You")
        {
            PeerName.GetComponent<Text>().color = Color.white;
            YourName.GetComponent<Text>().color = Color.yellow;
            Is_P1 = true;
        }
        else
        {
            PeerName.GetComponent<Text>().color = Color.yellow;
            YourName.GetComponent<Text>().color = Color.white;
            Is_P1 = false;
        }

        Panel.GetComponent<PanelScript>().Init();


        Black_Move_History_Text.text = "";
        White_Move_History_Text.text = "";

        AddPieceToPieceSet();

        for (int i = 0; i < DiePieceSet.transform.childCount; i++)
        {
            Transform obj = DiePieceSet.transform.GetChild(i);
            obj.GetComponent<PieceScript>().aliveCheck = PieceScript.AliveCheck.Alive;
            obj.SetParent(PieceSet.transform);
            obj.GetComponent<RectTransform>().sizeDelta = new Vector2(44, 72);
        }

        for (int i = 0; i < (Board.MAX_COL * 4); i++)
        {
            GameObject obj = GameObject.Find("piece_" + i);
            if (obj)
            {
                obj.gameObject.GetComponent<PieceScript>().aliveCheck = PieceScript.AliveCheck.Alive;
                obj.transform.SetParent(PieceSet.transform);
            }
        }

        PeerName.GetComponent<Text>().text = peer;
        PeerTime.GetComponent<Text>().text = "00 : 00";
        PeerDieQueen.GetComponent<Text>().text = "0";
        PeerDieRook.GetComponent<Text>().text = "0";
        PeerDieBishop.GetComponent<Text>().text = "0";
        PeerDieKnight.GetComponent<Text>().text = "0";
        PeerDiePawn.GetComponent<Text>().text = "0";

        YourName.GetComponent<Text>().text = "You";
        YourTime.GetComponent<Text>().text = "00 : 00";
        YourDieQueen.GetComponent<Text>().text = "0";
        YourDieRook.GetComponent<Text>().text = "0";
        YourDieBishop.GetComponent<Text>().text = "0";
        YourDieKnight.GetComponent<Text>().text = "0";
        YourDiePawn.GetComponent<Text>().text = "0";

        if (comunication.info.Gameplay_Type == 1) // Starting Local Gameplay
        {
            YourName.GetComponent<Text>().text = "Player1";
            PeerName.GetComponent<Text>().text = "Player2";
        }

        if (comunication.info.Gameplay_Type == 2) // Starting Online Gamepay 
        {
            YourName.GetComponent<Text>().text = comunication.info.Player1_Name;
            PeerName.GetComponent<Text>().text = comunication.info.Player2_Name;
        }
        if ( comunication.info.Sku_5)
        {            
            IsLeaderBoardActivated = true;
            LeaderBoard_Btn.GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }
        else
        {            
            IsLeaderBoardActivated = false;
            LeaderBoard_Btn.GetComponent<Image>().color = new Color(1, 1, 1, 0.65f);
        }        
    }

    void clearScreen()
    {
        ScreenWin.SetActive(false);
        ScreenLost.SetActive(false);
        Screen_Draw.SetActive(false);

        ScreenLock.SetActive(false);
    }

    public void ShowMsg(string msg)
    {
        Debug.Log("Screen Locked -- 2");
        ScreenLock.transform.GetChild(0).GetComponent<Text>().text = msg;
        ScreenLock.SetActive(true);
    }

    private void ChangeAvataData()
    {
        GameObject Temp = YourDieKnight;
        Vector3 temPos = YourDieKnight.GetComponent<RectTransform>().anchoredPosition;
        string YourDieKnighttxt, YourDiePawntxt, PeerDieKnighttxt, PeerDiePawntxt;
        YourDieKnighttxt = YourDieKnight.GetComponent<Text>().text;
        YourDiePawntxt = YourDiePawn.GetComponent<Text>().text;
        PeerDieKnighttxt = PeerDieKnight.GetComponent<Text>().text;
        PeerDiePawntxt = PeerDiePawn.GetComponent<Text>().text;
        YourDieKnight.GetComponent<Text>().text = PeerDieKnighttxt;
        YourDiePawn.GetComponent<Text>().text = PeerDiePawntxt;
        PeerDieKnight.GetComponent<Text>().text = YourDieKnighttxt;
        PeerDiePawn.GetComponent<Text>().text = YourDiePawntxt;
        //        YourDieKnight = PeerDieKnight;
        //        PeerDieKnight = Temp;
        //        Temp = YourDiePawn;
        //        YourDiePawn = PeerDiePawn;
        //        PeerDiePawn = Temp;
        Temp = PeerName;
        temPos = PeerName.GetComponent<RectTransform>().anchoredPosition;
        if (IsOneDev)
        {

            //PeerName = YourName;
        }
        /* if (IsOneDev && !comunication.info.PUN_IsOnline)
        {
			
            PeerName.GetComponent<RectTransform>().anchoredPosition = YourName.GetComponent<RectTransform>().anchoredPosition;
            YourName = Temp;
            YourName.GetComponent<RectTransform>().anchoredPosition = temPos;
        }*/
        if (!IsOneDev && !comunication.info.PUN_IsOnline)
        {

            PeerName.GetComponent<RectTransform>().anchoredPosition = YourName.GetComponent<RectTransform>().anchoredPosition;
            YourName = Temp;
            YourName.GetComponent<RectTransform>().anchoredPosition = temPos;
        }

        Vector3 tempRect = Cross_bottom.position;
        Vector3 tempRect2 = Cross_top.position;

        if (IsOneDev && !comunication.info.PUN_IsOnline)
        {
            Sprite Tex = Cross_bottom.GetComponent<Image>().sprite;
            Cross_bottom.GetComponent<Image>().sprite = Cross_top.GetComponent<Image>().sprite;
            Cross_top.GetComponent<Image>().sprite = Tex;
            Color color = YourName.GetComponent<Text>().color;
            YourName.GetComponent<Text>().color = PeerName.GetComponent<Text>().color;
            PeerName.GetComponent<Text>().color = color;
        }

    }


    public void PUN_WIN_LOSE(bool isWin)
    {

        // clearScreen();

        Flash.ins.init();

        IsPause = true;
        if (isWin)
        {
            Debug.Log("----PUN_WIND_LOSE Win----");
            ChangeScreen("win");
            IsStarted = false;
            is_joust_sfx_on = false;
        }
        else
        {
            Debug.Log("----PUN_WIND_LOSE Lost----");
            ChangeScreen("lost");
            IsStarted = false;
            is_joust_sfx_on = false;
        }

        ScreenLock.SetActive(false);
    }

    public void ChangeScreen(string screen)
    {
        if (Dontcall)
            return;
        Dontcall = true;
        if (Screen_Draw.activeInHierarchy || ScreenLost.activeInHierarchy || ScreenWin.activeInHierarchy)
            return;
        clearScreen();
        Flash.ins.init();

        if (screen == "draw")
        {

          //  Screen_Draw.transform.GetChild(0).gameObject.SetActive(true);
           // Screen_Draw.transform.GetChild(1).gameObject.SetActive(false);
           // Screen_Draw.transform.GetChild(2).gameObject.SetActive(false);
            

            Screen_Draw.SetActive(true);

            YourTime.GetComponent<Text>().text = "Draw";
            PeerTime.GetComponent<Text>().text = "Draw";

            IsStarted = false;
        }

        if (screen == "win")
        {
            aud_src.clip = Win_SFX;
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();
            StartCoroutine(PlayPostSound(true));

           // ScreenWin.transform.GetChild(0).gameObject.SetActive(true); // Offline Pannel
           // ScreenWin.transform.GetChild(1).gameObject.SetActive(false); // Online Pannel
           // ScreenWin.transform.GetChild(2).gameObject.SetActive(false);
            
            if (!ScreenWin.activeInHierarchy)
                ScreenWin.SetActive(true);
            is_joust_sfx_on = false;
            YourTime.GetComponent<Text>().text = "Win";
            PeerTime.GetComponent<Text>().text = "Lose";


            IsStarted = false;
        }

        if (screen == "lost")
        {
            

            aud_src.clip = Lose_SFX;
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();
            StartCoroutine(PlayPostSound(false));

           // ScreenLost.transform.GetChild(0).gameObject.SetActive(true); // Offline Panel
           // ScreenLost.transform.GetChild(1).gameObject.SetActive(false);
           // ScreenLost.transform.GetChild(2).gameObject.SetActive(false);// Online Panel
            
            if (!ScreenLost.activeInHierarchy)
                ScreenLost.SetActive(true);

            YourTime.GetComponent<Text>().text = "Lose";
            PeerTime.GetComponent<Text>().text = "Win";

            is_joust_sfx_on = false;
            IsStarted = false;
        }
        IsPause = true;

        

    }

    IEnumerator PlayPostSound(bool win)
    {
        yield return new WaitForSeconds(aud_src.clip.length);
        if (comunication.info.PUN_GameplayMode == 0)
            aud_src.clip = KT_SFX;
        else if (comunication.info.PUN_GameplayMode == 1)
            aud_src.clip = KOC_SFX;
        else if (comunication.info.PUN_GameplayMode == 2)
            aud_src.clip = KRT_SFX;
        else 
        {
            if (comunication.info.PUN_GameFormationMode == 0)
            {
                aud_src.clip = KT_SFX;
                
                /*if (comunication.info.Invert_flag)
                {
                    if (comunication.info.PUN_GameplayMode == 3)
                        aud_src.clip = KT_SFX; // fix angela
                    else if (comunication.info.PUN_GameplayMode == 5)
                        aud_src.clip = KOC_SFX;
                }*/
                if(comunication.info.Gameplay_Type == 1)
                {
                    if(YourColor != WinPlayerColor)
                    {
                        if (comunication.info.PUN_GameplayMode == 3)
                            aud_src.clip = KRT_SFX;
                        else if (comunication.info.PUN_GameplayMode == 5)
                            aud_src.clip = KOC_SFX;
                    }
                }

            }
            else if (comunication.info.PUN_GameFormationMode == 1)
            {
                aud_src.clip = KOC_SFX;

                /* if (comunication.info.Invert_flag)
                 {
                     if (comunication.info.PUN_GameplayMode == 4)
                         aud_src.clip = KOC_SFX;
                     else if (comunication.info.PUN_GameplayMode == 5)
                         aud_src.clip = KT_SFX;
                 }*/
                if (comunication.info.Gameplay_Type == 1)
                {
                    if (YourColor != WinPlayerColor)
                    {
                        if (comunication.info.PUN_GameplayMode == 4)
                            aud_src.clip = KRT_SFX;
                        else if (comunication.info.PUN_GameplayMode == 5)
                            aud_src.clip = KT_SFX;
                    }
                }
            }
            else if (comunication.info.PUN_GameFormationMode == 2)
            {
                aud_src.clip = KRT_SFX;

                /*if (comunication.info.Invert_flag)
                {
                    if (comunication.info.PUN_GameplayMode == 3)
                        aud_src.clip = KT_SFX;
                    else if (comunication.info.PUN_GameplayMode == 4)
                        aud_src.clip = KOC_SFX;
                }*/

                if (comunication.info.Gameplay_Type == 1)
                {
                    if (YourColor != WinPlayerColor)
                    {
                        if (comunication.info.PUN_GameplayMode == 3)
                            aud_src.clip = KT_SFX;
                        else if (comunication.info.PUN_GameplayMode == 4)
                            aud_src.clip = KOC_SFX;
                    }
                }
            }
        }
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
    }
    // Wait for 5 Sce and get Ready for next Match
    IEnumerator online_win_lose_wait()
    {
        yield return new WaitForSeconds(1);

        // Update OMI and send RPC's
        comunication.info.Save();
        //pun_src.Update_OMI(comunication.info.Match_No);
    }

    IEnumerator ShowLoading()
    {
        if (IsOnline)
        {
            GameLoading.SetActive(true);
            CloseButton.SetActive(true);
        }
        while (IsRunning == false)
        {
            yield return new WaitForSeconds(0.1f);
        }
        if (IsOnline)
        {
            GameLoading.SetActive(false);
            CloseButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnResign(true);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (PlayerPrefs.GetInt("IsTurnTimeActivated") == 0)
            {
                ResetCountDown = 0;
            }
        }
        lock (ChessGame)
        {
            if (mAutoTurn && IsPause == false)
            {
                float delta = Time.unscaledTime - mTime;
                //				float delta = Time.deltaTime - mTime;
                if (delta >= 0.001f)
                {
                    mAutoTurn = false;
                    StartCoroutine(NextPlayerTurn());
                }
            }
        }

        if (IsOnline == false)
        {
            //GoogleMobileAdsDemoScript._instance.ShowInterstitial();
        }



        if (IsPause == false)
        {
            if (PlayHorseSound && !aud_src1.isPlaying)
                StartCoroutine(playHorseSound());
            if (Rotate_Joust_SFX == 0)
            {
                Rotate_Joust_SFX_timer += Time.deltaTime;
                if (Rotate_Joust_SFX_timer > 10 && IsDraw == false)
                {
                    StartCoroutine(PlayJoust());
                    Rotate_Joust_SFX = 1;
                }
            }
            else if (Rotate_Joust_SFX == 1)
            {
                Rotate_Joust_SFX_timer += Time.deltaTime;
                if (Rotate_Joust_SFX_timer > 20 && IsDraw == false)
                {
                    StartCoroutine(PlayJoust());

                    Rotate_Joust_SFX = 1;
                    Rotate_Joust_SFX_timer = 0;
                }
            }
        }        

    }

    IEnumerator playHorseSound()
    {
        PlayHorseSound = false;
        yield return new WaitForSeconds(30f);
        aud_src2.clip = Horse_SFX;

        if (IsPause == false)
        {
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src2.Play();
        }
        
        StartCoroutine(playHorseSound());
    }

    void StartTimer()
    {




    }

    IEnumerator PlayJoust()
    {
        yield return new WaitForSeconds(aud_src1.clip.length);
        if (!aud_src2.isPlaying)
        {
            if (count_Joust == 0)
            {
                count_Joust = 1;
                aud_src1.clip = Joust_SFX;
            }
            else
            {
                count_Joust = 0;
                aud_src1.clip = New_Joust;
            }
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src1.Play();
        }
    }

    IEnumerator Wait_()
    {
        yield return new WaitForSeconds(aud_src.clip.length);
        comunication.info.Player1_Name = "";
        comunication.info.Player2_Name = "";

        LoadingScreenManager.LoadScene(1);
    }

    void FixedUpdate()
    {
        if (IsPause == false)
        {
            // Actual Timer

            if (IsOneDev)
            {
                // ActualTime+=Time.deltaTime;
                ResetCountDown++;
                if ((int)ResetCountDown / 60 >= 900 && PlayerPrefs.GetInt("IsTurnTimeActivated") == 0)
                {
                    IsPause = true;
                    IsRunning = false;
                    aud_src.clip = GameSceneManagerScript.instance.Cheers_SFX;
                    if (PlayerPrefs.GetString("Sound") == "On")
                        aud_src.Play();

                    StartCoroutine(Wait_());
                }
            }
            else
            {
                ActualTime += Time.deltaTime;
                ActualTimeText.text = string.Format("{00:00} : {01:00}", (int)(ActualTime / 60), ActualTime % 60);
            }

            //}


            if (IsOneDev && IsStarted)
            {
                // Player 1 (You)
                if (Is_P1)
                {
                    if (IsTurnTimeActivated && stoptimer)
                    {
                        PeerTime.transform.parent.GetComponent<Animator>().SetBool("flash", false);
                        Your_Time += Time.deltaTime;
                        ActualTime += Time.deltaTime;
                        YourTime.GetComponent<Text>().text = string.Format("{00:00} : {01:00}", (int)(Your_Time / 60), Your_Time % 60);
                        ActualTimeText.text = string.Format("{00:00} : {01:00}", (int)(ActualTime / 60), ActualTime % 60);
                        PeerTime.GetComponent<Text>().text = "0 : 00";
                        Peer_Time = 0;
                        is_sfx_on1 = true;
                        if (Your_Time > 2)
                        {
                            if (is_setside_sfx_on)
                            {
                                if (IsBlackMove)
                                {
                                    if (aud_src.isPlaying)
                                    {
                                        StartCoroutine(turn_sfx_wait(true));
                                    }
                                    else
                                    {
                                        aud_src.clip = WhiteTurn_SFX;
                                        if (PlayerPrefs.GetString("Sound") == "On")
                                            aud_src.Play();
                                    }
                                }

                                if (IsBlackMove == false)
                                {
                                    if (aud_src.isPlaying)
                                    {
                                        StartCoroutine(turn_sfx_wait(false));
                                    }
                                    else
                                    {
                                        aud_src.clip = BlackTurn_SFX;
                                        if (PlayerPrefs.GetString("Sound") == "On")
                                            aud_src.Play();
                                    }
                                }

                                Rotate_Joust_SFX = 0;
                                Rotate_Joust_SFX_timer = 0;

                                is_setside_sfx_on = false;
                            }
                        }


                        if (Your_Time > 10 && Turn_Time != 60)
                        {
                            if (is_joust_sfx_on && IsPause == false && IsDraw == false)
                            {
                                StartCoroutine(PlayJoust());

                                is_joust_sfx_on = false;
                            }
                        }

                        if (Your_Time > Timer_Anim_SFX_Time)
                        {
                            if (is_sfx_on)
                            {
                                aud_src.clip = YourMove_SFX;
                                if (PlayerPrefs.GetString("Sound") == "On")
                                    aud_src.Play();
                                is_sfx_on = false;

                            }
                        }

                        if (Your_Time > Timer_Anim_Flash_Time)
                        {
                            YourTime.transform.parent.GetComponent<Animator>().SetBool("flash", true);
                        }
                        else
                        {
                            YourTime.transform.parent.GetComponent<Animator>().SetBool("flash", false);
                        }

                        if (Your_Time > Turn_Time)
                        {
                            YourTime.GetComponent<Text>().text = "0 : 00";

                            YourName.GetComponent<Text>().color = Color.white;
                            PeerName.GetComponent<Text>().color = Color.yellow;

                            YourTime.transform.parent.GetComponent<Animator>().SetBool("flash", false);

                            is_sfx_on = true;
                            is_joust_sfx_on = true;
                            is_setside_sfx_on = true;

                            // Check for gallop
                            if (Turn_Time == 60)
                            {
                                // loss 
                                IsDraw = false;
                               // if (!clickRotate)
                                    IsWin = false;
                               // else
                                //    IsWin = true;
                                IsRunning = false;

                                aud_src.clip = Lightning_Bolt_SFX;
                                if (PlayerPrefs.GetString("Sound") == "On")
                                    aud_src.Play();
                                Lightning_Lost_Effect(true);
                                StartCoroutine(Wait_After_TurnTime());
                            }

                            // Cantor
                            if (Turn_Time == 180)
                            {
                                IsDraw = false;
                                //if (!clickRotate)
                                    IsWin = false;
                                //else
                                //    IsWin = true;
                                IsRunning = false;

                                aud_src.clip = Lightning_Bolt_SFX;
                                if (PlayerPrefs.GetString("Sound") == "On")
                                    aud_src.Play();
                                Lightning_Lost_Effect(true);
                                StartCoroutine(Wait_After_TurnTime());
                            }

                            // Trot
                            if (Turn_Time == 300)
                            {
                                if (Is_P1)
                                {
                                    if (Is_P2_Checkmated)
                                    {
                                        ChessGame.NextPlayerTurn();
                                        RedrawBoard();
                                    }
                                    else
                                    {
                                        // If Player 1 is not encounter checkmate conditoin, LOSS the game
                                        IsDraw = false;
                                        //if (!clickRotate)
                                            IsWin = false;
                                       // else
                                        //   IsWin = true;
                                        IsRunning = false;
                                        aud_src.clip = Lightning_Bolt_SFX;
                                        if (PlayerPrefs.GetString("Sound") == "On")
                                            aud_src.Play();
                                        Lightning_Lost_Effect(true);
                                        StartCoroutine(Wait_After_TurnTime());
                                    }
                                }
                                else
                                {
                                    if (Is_P1_Checkmated)
                                    {
                                        ChessGame.NextPlayerTurn();
                                        RedrawBoard();
                                    }
                                    else
                                    {
                                        // If Player 2 is not encounter checkmate conditoin, WINS the game
                                        IsDraw = false;
                                       // if (!clickRotate)
                                       //     IsWin = true;
                                       // else
                                            IsWin = false;
                                        IsRunning = false;
                                        aud_src.clip = Lightning_Bolt_SFX;
                                        if (PlayerPrefs.GetString("Sound") == "On")
                                            aud_src.Play();
                                        Lightning_Lost_Effect(true);
                                        StartCoroutine(Wait_After_TurnTime());
                                    }
                                }
                            }

                            Your_Time = 0;
                        }
                    }
                }

                // Player 2 (You2)
                else
                {
                    if (IsTurnTimeActivated && stoptimer)
                    {
                        YourTime.transform.parent.GetComponent<Animator>().SetBool("flash", false);
                        Peer_Time += Time.deltaTime;
                        ActualTime += Time.deltaTime;
                        PeerTime.GetComponent<Text>().text = string.Format("{00:00} : {01:00}", (int)(Peer_Time / 60), Peer_Time % 60);
                        ActualTimeText.text = string.Format("{00:00} : {01:00}", (int)(ActualTime / 60), ActualTime % 60);
                        YourTime.GetComponent<Text>().text = "0 : 00";
                        Your_Time = 0;
                        is_sfx_on = true;

                        if (Peer_Time > 2)
                        {
                            if (is_setside_sfx_on)
                            {
                                if (IsBlackMove)
                                {
                                    if (aud_src.isPlaying)
                                    {
                                        StartCoroutine(turn_sfx_wait(true));
                                    }
                                    else
                                    {
                                        aud_src.clip = WhiteTurn_SFX;
                                        if (PlayerPrefs.GetString("Sound") == "On")
                                            aud_src.Play();
                                    }
                                }

                                if (IsBlackMove == false)
                                {
                                    if (aud_src.isPlaying)
                                    {
                                        StartCoroutine(turn_sfx_wait(false));
                                    }
                                    else
                                    {
                                        aud_src.clip = BlackTurn_SFX;
                                        if (PlayerPrefs.GetString("Sound") == "On")
                                            aud_src.Play();
                                    }
                                }

                                Rotate_Joust_SFX = 0;
                                Rotate_Joust_SFX_timer = 0;

                                is_setside_sfx_on = false;
                            }
                        }

                        if (Your_Time > 10 && Turn_Time != 60)
                        {
                            if (is_joust_sfx_on && IsPause == false && IsDraw == false)
                            {
                                StartCoroutine(PlayJoust());
                                is_joust_sfx_on = false;
                            }
                        }

                        if (Peer_Time > Timer_Anim_SFX_Time)
                        {
                            //if (/*is_sfx_on &&*/ !comunication.info.PUN_IsOnline)
                            if (is_sfx_on1 && IsOneDev && !comunication.info.PUN_IsOnline)
                            {
                                aud_src.clip = YourMove_SFX;
                                if (PlayerPrefs.GetString("Sound") == "On")
                                    aud_src.Play();
                                is_sfx_on1 = false;
                            }
                        }

                        if (Peer_Time > Timer_Anim_Flash_Time)
                        {
                            PeerTime.transform.parent.GetComponent<Animator>().SetBool("flash", true);
                        }
                        else
                        {
                            PeerTime.transform.parent.GetComponent<Animator>().SetBool("flash", false);
                        }

                        if (Peer_Time > Turn_Time)
                        {
                            PeerTime.GetComponent<Text>().text = "0 : 00";

                            PeerName.GetComponent<Text>().color = Color.white;
                            YourName.GetComponent<Text>().color = Color.yellow;

                            PeerTime.transform.parent.GetComponent<Animator>().SetBool("flash", false);

                            is_sfx_on1 = true;
                            is_joust_sfx_on = true;
                            is_setside_sfx_on = true;

                            // Check for gallop
                            if (Turn_Time == 60)
                            {
                                IsDraw = false;
                             //   if (!clickRotate)
                              //      IsWin = true;
                             //   else
                                    IsWin = false;
                                IsRunning = false;

                                aud_src.clip = Lightning_Bolt_SFX;
                                if (PlayerPrefs.GetString("Sound") == "On")
                                    aud_src.Play();
                                Lightning_Lost_Effect(true);
                                StartCoroutine(Wait_After_TurnTime());
                            }

                            // Check for Cantor
                            if (Turn_Time == 180)
                            {
                                IsDraw = false;
                             //   if (!clickRotate)
                              //      IsWin = true;
                              //  else
                                    IsWin = false;
                                IsRunning = false;

                                aud_src.clip = Lightning_Bolt_SFX;
                                if (PlayerPrefs.GetString("Sound") == "On")
                                    aud_src.Play();
                                Lightning_Lost_Effect(true);
                                StartCoroutine(Wait_After_TurnTime());
                            }

                            // Check for Trot
                            if (Turn_Time == 300)
                            {
                                print("checking Trot");

                                if (Is_P1)
                                {
                                    print("Trot Player 1");

                                    if (Is_P2_Checkmated)
                                    {
                                        ChessGame.NextPlayerTurn();
                                        RedrawBoard();
                                    }
                                    else
                                    {
                                        IsDraw = false;
                                   //     if (!clickRotate)
                                            IsWin = false;
                                   //     else
                                    //        IsWin = true;
                                        IsRunning = false;
                                        aud_src.clip = Lightning_Bolt_SFX;
                                        if (PlayerPrefs.GetString("Sound") == "On")
                                            aud_src.Play();
                                        Lightning_Lost_Effect(true);
                                        StartCoroutine(Wait_After_TurnTime());
                                    }
                                }
                                else
                                {
                                    print("Trot Player 2");

                                    if (Is_P1_Checkmated)
                                    {
                                        ChessGame.NextPlayerTurn();
                                        RedrawBoard();
                                    }
                                    else
                                    {
                                        IsDraw = false;
                                   //     if (!clickRotate)
                                   //         IsWin = true;
                                   //     else
                                            IsWin = false;
                                        IsRunning = false;

                                        aud_src.clip = Lightning_Bolt_SFX;
                                        if (PlayerPrefs.GetString("Sound") == "On")
                                            aud_src.Play();
                                        Lightning_Lost_Effect(true);
                                        StartCoroutine(Wait_After_TurnTime());
                                    }
                                }
                            }

                            Peer_Time = 0;
                        }
                    }
                }
            }
        }
    }

    IEnumerator Wait_After_TurnTime()
    {
        stoptimer = false;
        rotate_button.interactable = false;
        yield return new WaitForSeconds(aud_src.clip.length);

        Lightning_Effect(false);

//        FirebaseAnalytics.LogEvent("Timer expires     " + Turn_Time);
        if (comunication.info.IsInternetAvailable())
            StartCoroutine(EndGameWhenCheckMate());
        else
        {
            GameEngineScript.instance.PUN_WIN_LOSE(false);
            //comunication.info.PUN_IsOnline = false;
            comunication.info.Is_Master = false;


        }
    }

    IEnumerator turn_sfx_wait(bool IsWhite)
    {
        yield return new WaitForSeconds(aud_src.clip.length);
        if (IsWhite)
        {
            aud_src.clip = WhiteTurn_SFX;
        }
        else
        {
            aud_src.clip = BlackTurn_SFX;
        }

        if (!aud_src.isPlaying)
        {
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();
        }
    }

    public void SetTurn(string user, string data)
    {
        Debug.Log("----ActivePlayerName : " + ChessGame.ActivePlay.Name);
        

        if (ChessGame.ActivePlay.Name == "You")
        {
            PeerName.GetComponent<Text>().color = Color.white;
            YourName.GetComponent<Text>().color = Color.yellow;
            Is_P1 = true;
        }
        else
        {
            PeerName.GetComponent<Text>().color = Color.yellow;
            YourName.GetComponent<Text>().color = Color.white;
            Is_P1 = false;
        }

        if (ChessGame.ActivePlay.Name != user)
        {
            if(!(Mode <= 2 && YourColor == "Black" && comunication.info.Gameplay_Type == 1))
                ChessGame.NextPlayerTurn();
        }

        if (ChessGame.ActivePlay.Name != "You" && ChessGame.ActivePlay.Name != "Computer")
        {
            //			ShowScreenLock (true);
        }
        else
        {
            //			ShowScreenLock (false);
        }
    }

    public void TimeTick(int time)
    {
        bool bThinking = false;
        if (time < 0)
        {
            if (time < -2 && IsAutoMove == false && ChessGame.ActivePlay.Name == "You")//&& appwarp.instance.Type == 0) // blitz
            {
                ShowScreenLock(true);
                //                SendMove("", "", time);
                //IsAutoMove = true;
                //StartCoroutine(AutoMove());
            }
            if (time < -2)//&& appwarp.instance.Type != 0) // rapid
            {
                //                EndRapidGame(time);
                return;
            }
            time = 0;
        }
        else
        {
            IsAutoMove = false;
        }

        if (time == 0)
        {
            ShowScreenLock(true);
            //bThinking = true;
        }

        if (ChessGame.ActivePlay.Name == "You")
        {
            YourTimeValue = time;
            YourTime.GetComponent<Text>().text = bThinking ? "Thinking" : string.Format("{0:00}:{1:00}", (time / 60), (time % 60));
            if (Type == 0)
            {
                PeerTime.GetComponent<Text>().text = "Waiting";
            }
        }
        else
        {
            PeerTimeValue = time;
            PeerTime.GetComponent<Text>().text = bThinking ? "Thinking" : string.Format("{0:00}:{1:00}", (time / 60), (time % 60));
            if (Type == 0)
            {
                YourTime.GetComponent<Text>().text = "Waiting";
            }
        }
    }

    public void HideMsg()
    {
        ScreenLock.SetActive(false);
    }

    public void ShowScreenLock(bool bShow)
    {
        //        if (bShow)
        //            ScreenLock.transform.GetChild(0).GetComponent<Text>().text = "";
        Debug.Log("Lock Screen");
        ScreenLock.SetActive(bShow);
    }

    public void ChangeColorBoard() // change colors
    {
        Debug.Log("----zzzzzzzzzzzzzzzzzzzzzzz-------");
        for (int i = 0; i < (Board.MAX_COL * 4); i++)
        {
            GameObject obj = GameObject.Find("piece_" + i);
            if (obj && obj.GetComponent<PieceScript>().aliveCheck == PieceScript.AliveCheck.Alive)
            {
                Piece piece = ChessGame.Board[obj.transform.parent.gameObject.name].piece;
                obj.gameObject.GetComponent<PieceScript>().InvertPiece();
                obj.gameObject.GetComponent<PieceScript>().SetImage(piece, piece.Side.isWhite());
                //Debug.Log(piece.Side.isWhite() +"2");
                //obj.gameObject.GetComponent<PieceScript>().SetImage(piece, piece.Side.isWhite());


                //obj.gameObject.GetComponent<PieceScript>().aliveCheck = PieceScript.AliveCheck.Alive;
                //obj.transform.SetParent(PieceSet.transform);
            }
        }
        // RedrawBoard();
    }

    public void RotateBoard()
    {
        // Check is rotate button is Activated 
        if (comunication.info.Sku_2 || comunication.info.Sku_5)
        {
            UpgradeBanner.SetActive(false);
            if (comunication.info.Gameplay_Type == 1)
            {
                // Activating Joust SFX for 10sec and 20sec sequence
                if (Rotate_Joust_SFX != 0)
                {
                    Rotate_Joust_SFX = 0;
                }
                if (!comunication.info.PUN_IsOnline)
                    aud_src.clip = Rotate_SFX;
                if (PlayerPrefs.GetString("Sound") == "On")
                    aud_src.Play();

                StartCoroutine(Rotate_Wait(1));
            }
            else
            {
                aud_src.clip = Button_SFX;
                if (PlayerPrefs.GetString("Sound") == "On")
                    aud_src.Play();

                Rotate_Massage_Pannel.transform.GetChild(1).gameObject.SetActive(true);
                Rotate_Massage_Pannel.transform.GetChild(2).gameObject.SetActive(false);
                Rotate_Massage_Pannel.SetActive(true);
            }
        }
        else
        {
            aud_src.clip = UpgradeRequire_SFX;
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();
            Rotate_Massage_Pannel.transform.GetChild(1).gameObject.SetActive(true);
            Rotate_Massage_Pannel.transform.GetChild(2).gameObject.SetActive(false);
            Rotate_Massage_Pannel.SetActive(true);
            //UpgradeBanner.SetActive(true);
            StartCoroutine(Rotate_Wait(0));
        }
    }

    private bool clickRotate = false;
    IEnumerator Rotate_Wait(int IsRotate)
    {
        IsPause = true;

        if (aud_src != null && aud_src.clip != null)
            yield return new WaitForSeconds(aud_src.clip.length);
        else
            yield return 0;

        if (IsRotate == 1)
        {
            if (RotateMoveEnabled)
            {
                Panel.GetComponent<PanelScript>().Rotate(0);
                Panel.transform.rotation = Quaternion.Euler(0, 0, 0);
                GetComponent<GameSceneManagerScript>().ChangeBoard_Rotation(0);
                Flash.ins.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                RotateMoveEnabled = false;

            }
            else
            {

                Panel.GetComponent<PanelScript>().Rotate(180);
                Panel.transform.rotation = Quaternion.Euler(0, 0, 180);
                GetComponent<GameSceneManagerScript>().ChangeBoard_Rotation(180);
                // Flash.ins.gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);

                RotateMoveEnabled = true;


            }
            ChangeAvataData();
            if (IsOneDev && !comunication.info.PUN_IsOnline)
            {

                if (YourColor == "White")
                {
                    YourColor = "Black";
                }
                else
                {
                    YourColor = "White";
                }                
                float tempTime = Your_Time;
                Your_Time = Peer_Time;
                Peer_Time = tempTime;
                //Your_Time = 0;
                //Peer_Time = 0;
                Is_P1 = !Is_P1;
                clickRotate = !clickRotate;
                string temp = ChessGame.WhitePlayer.Name;
                ChessGame.WhitePlayer.Name = ChessGame.BlackPlayer.Name;
                ChessGame.BlackPlayer.Name = temp;
                if (Flash.ins.isFlashOn)
                {
                    Flash.ins.isComputer = !Flash.ins.isComputer;
                    Flash.ins.FlashOff();
                    Flash.ins.FlashOn();
                }
                /*Vector3 peer_time_temp_pos = PeerTime.transform.parent.GetComponent<RectTransform>().position; 
                PeerTime.transform.parent.GetComponent<RectTransform>().position = YourTime.transform.parent.GetComponent<RectTransform>().position;
			
                YourTime.transform.parent.GetComponent<RectTransform>().position = peer_time_temp_pos;*/

            }

            // Flash.ins.SwapSides();

            //			GetComponent<GameSceneManagerScript>().ChangeBoardImgRotated();
            //			for ( int i = Panel.transform.childCount - 1; i >0; i --)
            //			{
            //				GameObject _child = Panel.transform.GetChild(Panel.transform.childCount-1).gameObject as GameObject;
            //				_child.transform.SetSiblingIndex((Panel.transform.childCount - 1) - i);
            //			}

            //			Panel.GetComponent<PanelScript> ().Init ();
        }

        if (IsRotate == 0)
        {
            // ShowUpgradeBanner();
        }

        IsPause = false;

    }

    public void Show_Rotate_Panel()
    {

    }

    public void Moves_WIN_LOSE(bool bShow)
    {
        IsPause = true;
        IsRunning = false;
        RectTransform rec = ScreenMoves.GetComponent<RectTransform>();


        //        rec.SetSiblingIndex(26);





        ScreenMoves.SetActive(bShow);


        if (bShow)
        {
            aud_src.clip = Review_SFX;
        }
        else
        {
            aud_src.clip = Button_SFX;
        }
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
    }

    public void ShowScreenMoves(bool bShow)
    {

        RectTransform rec = ScreenMoves.GetComponent<RectTransform>();

        //		if (IsRunning)
        //		{
        //			rec.SetSiblingIndex (22);
        //		} else
        //		{
        //        rec.SetSiblingIndex(26);
        //		}
        if (IsRunning)
            IsPause = bShow;

        if (IsOnline == true)
        {
            ScreenMoves.SetActive(bShow);
        }
        else
        {
            if (IsMyTurn() || IsRunning == false)
            {
                ScreenMoves.SetActive(bShow);
            }
        }

        if (bShow)
        {
            aud_src.clip = Move_SFX;

            //stoptimerlocal = true;
        }
        else
        {
            aud_src.clip = Button_SFX;
        }
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();

    }

    public void CloseUpgradeBanner()
    {
        UpgradeBanner.SetActive(false);
    }

    public void OnUpgradeBanner()
    {
        // Call In app purchases here
        PlayerPrefs.SetInt("IsRotateUpgrated", 1);
        CloseUpgradeBanner();
    }

    public void ShowUpgradeBanner()
    {
        if (!comunication.info.Sku_5)
        {
            UpgradeBanner.SetActive(true);
        }
    }

    // LeaderBoard Button
    public void OnLeaderBoardBtn(bool show)
    {
        if (comunication.info.Sku_5)
        {
            UpgradeBanner.SetActive(false);
            if (comunication.info.Gameplay_Type == 2)// Online Mode
            {
                aud_src.clip = Button_SFX;
                if (PlayerPrefs.GetString("Sound") == "On")
                    aud_src.Play();
                UpgradeBanner.SetActive(false);
                if (IsLeaderBoardActivated)
                {
                    UpgradeBanner.SetActive(false);
                    comunication.info.Calculate_Points();
                    comunication.info.Calculate_Degree();

                    LB_Loading.SetActive(show);
                    LeaderBoard_Pannel.SetActive(show);
//                    Firebase_Database_KD.ins.RetrieveDataFromFireBase();
                    // Refresh data
                    // Firebase_Database_KD.ins.Check_Your_Pos();
                    // Firebase_Database_KD.ins.ShowData();

                    StartCoroutine(Load_LB(show));
                }
            }
            else
            {
                Rotate_Massage_Pannel.transform.GetChild(1).gameObject.SetActive(false);
                Rotate_Massage_Pannel.transform.GetChild(2).gameObject.SetActive(true);
                Rotate_Massage_Pannel.SetActive(true);
            }
        }
        else
        {
            aud_src.clip = UpgradeRequire_SFX;
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();
            Rotate_Massage_Pannel.transform.GetChild(1).gameObject.SetActive(false);
            Rotate_Massage_Pannel.transform.GetChild(2).gameObject.SetActive(true);
            Rotate_Massage_Pannel.SetActive(true);
            //ShowUpgradeBanner();
        }
    }

    public void onLeaderboardClick(){
        Rotate_Massage_Pannel.transform.GetChild(1).gameObject.SetActive(false);
        Rotate_Massage_Pannel.transform.GetChild(2).gameObject.SetActive(true);
        Rotate_Massage_Pannel.SetActive(true);
    }

    IEnumerator Load_LB(bool LB_Show)
    {
        yield return new WaitForSeconds(5);
        UpgradeBanner.SetActive(false);
        LB_Loading.SetActive(false);
    }

    public void OnDraw(bool bShow)
    {
        if (IsOnline == true)
        {
            lock (ChessGame)
            {
                //                if (appwarp.instance.IsMyTurn())
                //                {
                //                    appwarp.instance.Draw();
                //                    GameSceneManagerScript.instance.Draw();
                //                }
            }
        }
        else
        {
            if (IsMyTurn())
            {
                DrawDialog.SetActive(bShow);
            }
        }
    }

    public void OnDrawDialog_OK()
    {
        DrawDialog.SetActive(false);
        GameSceneManagerScript.instance.Draw();
    }

    public void OnResign(bool bShow)
    {
        IsPause = bShow;

        if ((bShow == true && IsRunning == false) || EndedGame == true)
        {
            GameSceneManagerScript.instance.Resign();
        }
        else
        {
            //MessageBox.SetActive(bShow);
            ResignDialog.SetActive(bShow);
        }
    }

    public void GetMoveHistory()
    {
        //        appwarp.instance.GetMoveHistory();
    }

    public void OnMessageOK()
    {
        if (IsOnline == true)
        {
            lock (ChessGame)
            {
                GameSceneManagerScript.instance.Resign();
            }
        }
        else
        {
            if (IsMyTurn())
            {
                GameSceneManagerScript.instance.Resign();
            }
        }
    }

    public void DisableResignButtons(Button cancel)
    {
        cancel.interactable = false;
    }

    IEnumerator NewGame(string firstTurnUser)
    {
        Mode = comunication.info.PUN_GameplayMode;
        
        for (int i = 0; i < PieceSet.transform.childCount; i++)
        {
            PieceSet.transform.GetChild(i).name = "piece_" + i;
        }
        if (ChessGame != null)
            ChessGame = null;

        ChessGame = new Game();
            
        // IsStarted = true;
        mTime = Time.unscaledTime;

        yield return new WaitForSeconds(0.1f);

        //ChessGame.ComputerThinking += ChessGame_ComputerThinking;
        ChessGame.Reset(Mode, comunication.info.PUN_GameFormationMode);
        yield return new WaitForSeconds(0.1f);

        string peer = PlayerPrefs.GetString("Peer");

        //modified by me
        if (Mode <= 2)
        {
            if (YourColor == "White")
            {
                ChessGame.WhitePlayer.Name = "You";
                ChessGame.BlackPlayer.Name = peer;

                comunication.info.Invert_flag = false;
            }
            else
            {
                ChessGame.WhitePlayer.Name = peer;
                ChessGame.BlackPlayer.Name = "You";

                comunication.info.Invert_flag = true;
            }
        }
        else
        {
            //8.2
            comunication.info.Invert_flag = false;
            if (YourColor == "White")
            {
                ChessGame.WhitePlayer.Name = "You";
                ChessGame.BlackPlayer.Name = peer;
            }
            else
            {

                if (comunication.info.Gameplay_Type < 1)
                {
                    ChessGame.WhitePlayer.Name = "You";
                    ChessGame.BlackPlayer.Name = peer;
                                        
                }
                else
                {
                    ChessGame.WhitePlayer.Name = peer;
                    ChessGame.BlackPlayer.Name = "You";
                }

            }
        }


        if (IsOnline == false)
        {
            Debug.Log("-----OFF Line----");
            //added by me
            if (Mode < 3)
            {
                if (WhitePlayerComputer)
                {
                    ChessGame.WhitePlayer.PlayerType = Player.Type.Computer;
                }
                else
                {
                    ChessGame.WhitePlayer.PlayerType = Player.Type.Human;
                }

                if (BlackPlayerComputer)
                {
                    ChessGame.BlackPlayer.PlayerType = Player.Type.Computer;
                }
                else
                {
                    ChessGame.BlackPlayer.PlayerType = Player.Type.Human;
                }
            }
            else
            {
                if (WhitePlayerComputer || BlackPlayerComputer)
                {
                    //if (YourColor == "White")
                    //{
                        ChessGame.WhitePlayer.PlayerType = Player.Type.Human;
                        ChessGame.BlackPlayer.PlayerType = Player.Type.Computer;
                    //}
                    //else
                   /* {
                        ChessGame.WhitePlayer.PlayerType = Player.Type.Computer;
                        ChessGame.BlackPlayer.PlayerType = Player.Type.Human;
                    }*/
                }
                else
                {
                    ChessGame.WhitePlayer.PlayerType = Player.Type.Human;
                    ChessGame.BlackPlayer.PlayerType = Player.Type.Human;
                }

               /* if (WhitePlayerComputer)
                {
                    ChessGame.WhitePlayer.PlayerType = Player.Type.Computer;
                }
                else
                {
                    ChessGame.WhitePlayer.PlayerType = Player.Type.Human;
                }

                if (BlackPlayerComputer)
                {
                    ChessGame.BlackPlayer.PlayerType = Player.Type.Computer;
                }
                else
                {
                    ChessGame.BlackPlayer.PlayerType = Player.Type.Human;
                }*/
            }
        }
        else
        {
            Difficulty = GameLevel.Beginner;
            ChessGame.WhitePlayer.PlayerType = Player.Type.Human;
            ChessGame.BlackPlayer.PlayerType = Player.Type.Human;
        }
        yield return new WaitForSeconds(0.1f);

        if (Difficulty == GameLevel.Beginner)
        {
            ChessGame.WhitePlayer.TotalThinkTime = 3;
            ChessGame.BlackPlayer.TotalThinkTime = 3;
        }
        if (Difficulty == GameLevel.Intermediate)
        {
            ChessGame.WhitePlayer.TotalThinkTime = 5;
            ChessGame.BlackPlayer.TotalThinkTime = 5;
        }
        if (Difficulty == GameLevel.Master)
        {
            ChessGame.WhitePlayer.TotalThinkTime = 10;
            ChessGame.BlackPlayer.TotalThinkTime = 10;
        }
        yield return new WaitForSeconds(0.1f);

        int p = 0;
        for (int row = 1; row <= Board.MAX_ROW; row++)
        {
            for (int col = 1; col <= Board.MAX_COL; col++)
            {
                Cell cell = ChessGame.Board[row, col];
                if (cell.IsEmpty() == false)
                {
                    PieceScript ps = PieceSet.transform.GetChild(p++).GetComponent<PieceScript>();
                    string cn = cell.ToString();
                    int n = int.Parse("" + cn[1]);
                    ps.SetPosition(cn);

                    if (Mode > 2)
                    {
                        if (YourColor == "White")
                        {
                            ps.SetImage(cell.piece, n > Board.MAX_ROW / 2);
                        }
                        else
                        {
                            ps.InvertPiece();
                            ps.SetImage(cell.piece, n > Board.MAX_ROW / 2);
                        }
                    }
                    else
                        ps.SetImage(cell.piece, n > Board.MAX_ROW / 2);
                    //					print ("Position call");
                }
            }
        }
        yield return new WaitForSeconds(0.1f);
        
        RedrawBoard();
        yield return new WaitForSeconds(0.1f);
        
        if (peer != firstTurnUser)
            firstTurnUser = "You";     
        
        if (m_isPlayerFirst || IsOneDev)
        {
            //SetTurn(firstTurnUser, null);
            SetTurn("You", null);
        }
        else
        {
            SetTurn("Computer", null);
        }


        mTime = Time.unscaledTime;

        mAutoTurn = true;
        IsRunning = true;
        IsStarted = true;

        NextPlayerTurn();
    }

    IEnumerator NextPlayerTurn()
    {
        if (stoptimerlocal)
            yield return new WaitForSeconds(aud_src.clip.length);
        if (ChessGame.ActivePlay.IsComputer())
        {
            Move nextMove = null;
            nextMove = ChessGame.ActivePlay.GetBestMove(Difficulty);
            if (nextMove != null)
            {
                UserMove(nextMove.StartCell.ToString(), nextMove.EndCell.ToString());
            }            
            Hourglass.SetActive(false);
            //			Flash.ins.Custom_Update(false); // FlashOff
        }
        if (IsOneDev)
        {
            if (PlayerPrefs.GetString("Peer") == "You2")
            {
                //				IsBackActivated = true;
            }
        }


        stoptimerlocal = false;
        yield return null;
    }

    IEnumerator AutoMove()
    {
        Move nextMove = null;
        if (IsAutoMove)
        {
            if (ChessGame.ActivePlay.Name == "You")
            {// && appwarp.instance.listen.State == AppWarpStatus.MyTurn)
                nextMove = ChessGame.ActivePlay.GetBestMove(GameLevel.Beginner);

                if (nextMove != null)
                {
                    SendMove(nextMove.StartCell.ToString(), nextMove.EndCell.ToString(), YourTimeValue);
                    //                  UserMove(nextMove.StartCell.ToString(), nextMove.EndCell.ToString(), true);
                }
            }
        }
        yield return null;
    }

    private bool IsMyTurn()
    {
        return !ChessGame.ActivePlay.IsComputer();
    }

    GameObject FindPieceByPosition(string pos)
    {
        if (pos == null)
            return null;

        for (int i = 0; i < (Board.MAX_COL * 4); i++)
        {
            GameObject obj = GameObject.Find("piece_" + i);
            if (obj != null)
            {
                if (obj.GetComponent<PieceScript>().aliveCheck != PieceScript.AliveCheck.Die && obj.GetComponent<PieceScript>().GetPosition() == pos)
                {
                    return obj.gameObject;
                }
            }
        }
        return null;
    }

    void MoveToNewPosition(GameObject obj, Piece piece)
    {
        //		print ("Moving : "+ obj.name + " Piece : "+ piece.ToString());
        string pos = piece.m_prevpos;
        //Vector3 vtr = GameObject.Find(pos).transform.position;// + new Vector3(0, 20f, 0);
        //obj.transform.position = vtr;
        obj.transform.SetParent(GameObject.Find(pos).transform);
        obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);

        obj.GetComponent<PieceScript>().SetImage(piece, piece.Side.isWhite());
        obj.GetComponent<PieceScript>().SetPosition(pos);
    }

    public void RedrawBoard()
    {
        Color _colorWhite = Color.green;//Color.cyan;
        //_colorYan.a = 0;
        Debug.Log("---Max_Col : " + Board.MAX_COL);
        for (int i = 0; i < (Board.MAX_COL * 4); i++)
        {
            GameObject obj = GameObject.Find("piece_" + i);
            if (obj != null && obj.GetComponent<PieceScript>().aliveCheck != PieceScript.AliveCheck.Die)
            {
                obj.GetComponent<PieceScript>().aliveCheck = PieceScript.AliveCheck.AliveCheck;
            }
        }
        for (int i = 0; i < Panel.transform.childCount; i++)
        {
            GameObject squar = Panel.transform.GetChild(i).gameObject;

            //ChessGame.Board[squar.name];
            Piece piece = ChessGame.Board[squar.name].piece;
            if (piece != null && piece.Type != Piece.PieceType.Empty && string.IsNullOrEmpty(piece.m_prevpos))
                piece.m_prevpos = piece.m_prevpos;

            if (piece != null && piece.Type != Piece.PieceType.Empty)
            {
                //                if (piece.m_prevpos != squar.name)
                //                {
                GameObject real = FindPieceByPosition(piece.m_prevpos);
                if (real != null)
                {
                    real.GetComponent<PieceScript>().aliveCheck = PieceScript.AliveCheck.Alive;
                    piece.m_prevpos = squar.name;

                    MoveToNewPosition(real, piece);
                }
                //                }
            }
            if (squar.name == SelectedSquar && ShowMoveHelp == true)
            {
                squar.GetComponent<Image>().color = _colorWhite;// Color.cyan;
                squar.transform.GetChild(0).gameObject.GetComponent<PieceScript>().SetSelectedImage(piece, piece.Side.isWhite());
            }
            else
            {
                if (Board.MAX_COL != 8 && (squar.name == "D1" || squar.name == "E1" || squar.name == "F1"
                    || squar.name == "D2" || squar.name == "E2" || squar.name == "F2"
                    || squar.name == ("D" + Board.MAX_ROW) || squar.name == ("E" + Board.MAX_ROW) || squar.name == ("F" + Board.MAX_ROW)
                    || squar.name == ("D" + (Board.MAX_ROW - 1)) || squar.name == ("E" + (Board.MAX_ROW - 1)) || squar.name == ("F" + (Board.MAX_ROW - 1))))
                {
                    Color _temp = Color.yellow;
                    _temp.a = 0;
                    squar.GetComponent<Image>().color = _temp;
                }
                else
                {
                    Color _temp = Color.white;
                    _temp.a = 0;
                    squar.GetComponent<Image>().color = _temp;
                }
            }
        }
        for (int i = 0; i < (Board.MAX_COL * 4); i++)
        {
            GameObject obj = GameObject.Find("piece_" + i);
            if (obj != null && obj.GetComponent<PieceScript>().aliveCheck == PieceScript.AliveCheck.AliveCheck)
            {
                obj.GetComponent<PieceScript>().aliveCheck = PieceScript.AliveCheck.Die;
                obj.transform.SetParent(DiePieceSet.transform);
                //obj.GetComponent<Image>().preserveAspect = true;
                //obj.gameObject.SetActive(false);
            }
        }

        if (SelectedSquar != "" && SelectedSquar != "" && ShowMoveHelp == true)
        {
            Cell basecell = ChessGame.Board[SelectedSquar];
            Piece piece = basecell.piece;
            if (piece != null && !piece.IsEmpty() && piece.Side.type == ChessGame.GameTurn)
            {
                ArrayList moves = ChessGame.GetLegalMoves(basecell);
                foreach (Cell cell in moves)
                {
                    GameObject squar = GameObject.Find(cell.ToString()).gameObject;

                    squar.GetComponent<Image>().color = _colorWhite;// Color.cyan;
                }
            }
        }
        SelectedSquar = "";
        Move move = ChessGame.GetLastMove();
        if (move != null)
        {
            GameObject squar = GameObject.Find(move.EndCell.ToString()).gameObject;
            if (squar.GetComponent<Image>().color == _colorWhite)//Color.cyan)
            {
                //				Color _temp = Color.blue;
                //				_temp.a = 0;
                //				squar.GetComponent<Image> ().color = _temp;	
            }
            else
            {
                //                if (!ChessGame.ActivePlay.IsComputer())
                //                {
                Color _temp = Color.red;
                //_temp.a = 0;
                squar.GetComponent<Image>().color = _temp;
                //                }
            }
        }

    }

    public Piece GetPromoPiece(Side PlayerSide)
    {
        /*SelectPiece SelectPieceDlg = new SelectPiece();

        // Initialize the images to show on the form
        SelectPieceDlg.Piece1.Image = ChessImages.GetImageForPiece(new Piece(Piece.PieceType.Queen, PlayerSide));
        SelectPieceDlg.Piece2.Image = ChessImages.GetImageForPiece(new Piece(Piece.PieceType.Knight, PlayerSide));
        SelectPieceDlg.Piece3.Image = ChessImages.GetImageForPiece(new Piece(Piece.PieceType.Rook, PlayerSide));
        SelectPieceDlg.Piece4.Image = ChessImages.GetImageForPiece(new Piece(Piece.PieceType.Bishop, PlayerSide));

        SelectPieceDlg.ShowDialog(this.ParentForm); // Show the promo select dialog*/

        int nChoise = UnityEngine.Random.Range(1, 1000) % 4;
        nChoise += 1;
        // Now return back corresponding piece 
        switch (nChoise)
        {
            case 1:
                return new Piece(Piece.PieceType.Queen, PlayerSide);
            case 2:
                return new Piece(Piece.PieceType.Knight, PlayerSide);
            case 3:
                return new Piece(Piece.PieceType.Rook, PlayerSide);
            case 4:
                return new Piece(Piece.PieceType.Bishop, PlayerSide);
        }
        return null;
    }

    private void SendMove(string source, string dest, int time)
    {

    }

    public void UndoMove()
    {

    }

    IEnumerator MarkAsCheck(GameObject squar)
    {
        yield return new WaitForSeconds(1f);
        squar.GetComponent<SquarScript>().OnClick();
    }

    private string WinPlayerColor = "";
    public bool UserMove(string source, string dest, bool userAction = false)
    {

        bool success = true;
        int MoveResult = ChessGame.DoMove(source, dest);

        switch (MoveResult)
        {

            case 0: // move was successfull;

                //          check if the last move was promo move
                Move move = ChessGame.GetLastMove();    // get the last move 	

                // check for the check mate situation
                if (ChessGame.IsCheckMate(ChessGame.GameTurn))
                {
                    is_joust_sfx_on = false;

                    IsDraw = false;
                    if (YourColor == "White")
                    {
                        if (ChessGame.WhitePlayer.Name == ChessGame.GetPlayerBySide(ChessGame.GameTurn).Name)
                        { // when white lost the game
                            IsWin = false;
                            WinPlayerColor = "Black";
                        }
                        else
                        {
                            IsWin = true; // when you win
                            WinPlayerColor = "White";
                        }
                    }

                    if (YourColor == "Black")
                    {
                        if (ChessGame.BlackPlayer.Name == ChessGame.GetPlayerBySide(ChessGame.GameTurn).Name)
                        { // when black lost the game                            
                            IsWin = true;
                            WinPlayerColor = "Black";

                        }
                        else
                        {
                            WinPlayerColor = "White";
                            IsWin = false; // when you win	                            
                        }
                    }

                    IsPause = true;

                    // Activate Flashing effect here 
                    if (Is_Computer_Playing)
                    {
                        if (ChessGame.ActivePlay.IsComputer())
                        {
                            Flash.ins.isComputer = true;
                        }
                        else
                        {
                            Flash.ins.isComputer = false;
                        }
                        Flash.ins.Custom_Update(true); // FlashOn
                    }
                    else
                    {
                        if (Is_P1)
                        {
                            Flash.ins.isComputer = true;
                        }
                        else
                        {
                            Flash.ins.isComputer = false;
                        }
                        Flash.ins.FlashOn();
                    }

                    StartCoroutine(wait_for_Check_SFX(move));
                }
                else if (ChessGame.IsUnderCheck())
                {
                    // auto select the king ( both side ) which is in check 
                    for (int i = 0; i < (Board.MAX_COL * 4); i++)
                    {
                        GameObject obj = GameObject.Find("piece_" + i);
                        if (obj != null && obj.GetComponent<PieceScript>().aliveCheck == PieceScript.AliveCheck.Alive)
                        {
                            GameObject squar = obj.transform.parent.gameObject;
                            Piece piece = ChessGame.Board[squar.name].piece;

                            if (piece.IsKing() && move.Piece.Side.type != piece.Side.type)
                            {
                                if (!ChessGame.ActivePlay.IsComputer())
                                {
                                    comunication.info.checkSquare = squar;
                                    StartCoroutine(MarkAsCheck(squar));
                                }
                            }
                        }
                    }

                    // Activate Flashing effect here 
                    if (Is_Computer_Playing)
                    {
                        if (ChessGame.ActivePlay.IsComputer())
                        {
                            Flash.ins.isComputer = true;
                        }
                        else
                        {
                            Flash.ins.isComputer = false;
                        }
                        Flash.ins.Custom_Update(true); // FlashOn
                    }
                    else
                    {
                        if (Is_P1)
                        {
                            Flash.ins.isComputer = true;
                        }
                        else
                        {
                            Flash.ins.isComputer = false;
                        }
                        //                        if (comunication.info.PUN_IsOnline && !comunication.info.Is_Master)
                        //                        {
                        //                            Flash.ins.gameObject.transform.rotation = Quaternion.Euler(0, 0, 180);
                        //                        }
                        Flash.ins.FlashOn();
                    }

                    // Check if th epiece is taken or not 

                    stoptimerlocal = true;

                    StartCoroutine(waitforCheck());
                    StartCoroutine(wait_for_clang(move, true));
                }
                else
                {
                    Flash.ins.FlashOff();
                    Flash.ins.Custom_Update(false);

                    if (move.Type == Move.MoveType.EnPassant)
                    {
                        if (PlayerPrefs.GetString("Sound") == "On")
                            aud_src.PlayOneShot(EnPassant_SFX, 1.5f);
                    }

                    if (move.Type == Move.MoveType.NormalMove || move.Type == Move.MoveType.TowerMove)
                    {
                        if (PlayerPrefs.GetString("Sound") == "On")
                            aud_src1.PlayOneShot(AClips[4], 0.5f); // Move SFX
                    }
                    else
                    {
                        if (move.IsPromoMove())
                        {
                            if (PlayerPrefs.GetString("Sound") == "On")
                                aud_src1.PlayOneShot(AClips[4], 0.5f);
                            stoptimerlocal = true;
                            StartCoroutine(playKnightHoodSFX());

                        }
                        else if (move.Piece.IsKnight() || move.Piece.IsKing() || move.Piece.IsPawn())
                        {
                            if (PlayerPrefs.GetString("Sound") == "On")
                                aud_src1.PlayOneShot(AClips[2], 0.5f); // Sward Clang sfx
                            if (move.IsPromoMove())
                            {
                                //aud_src1.PlayOneShot (AClips [4], 0.5f);
                                stoptimerlocal = true;
                                StartCoroutine(playKnightHoodSFX());

                            }
                        }
                    }

                }
                // Add to the capture list
                if (move.Type == Move.MoveType.EnPassant)
                {
                    bool isYour = true;
                    if (YourColor == "White")
                    {
                        isYour = move.EnPassantPiece.Side.type == Side.SideType.Black;
                    }
                    else
                    {
                        isYour = move.EnPassantPiece.Side.type == Side.SideType.White;
                    }

                    if (isYour)
                    {
                        if (move.EnPassantPiece.IsPawn())
                            YourDiePawn.GetComponent<Text>().text = (int.Parse(YourDiePawn.GetComponent<Text>().text) + 1).ToString();
                    }
                    else
                    {
                        if (move.EnPassantPiece.IsPawn())
                            PeerDiePawn.GetComponent<Text>().text = (int.Parse(PeerDiePawn.GetComponent<Text>().text) + 1).ToString();
                    }



                    //ParentForm.ChessCaptureBar.Add(ChessImages.GetImageForPiece(move.CapturedPiece));
                }


                // Add to the capture list
                if (move.IsCaptureMove())
                {
                    bool isYour = true;
                    if (YourColor == "White")
                    {
                        isYour = move.CapturedPiece.Side.type == Side.SideType.Black;
                    }
                    else
                    {
                        isYour = move.CapturedPiece.Side.type == Side.SideType.White;
                    }

                    if (isYour)
                    {
                        if (move.CapturedPiece.IsQueen())
                            YourDieQueen.GetComponent<Text>().text = (int.Parse(YourDieQueen.GetComponent<Text>().text) + 1).ToString();
                        else if (move.CapturedPiece.IsRook())
                            YourDieRook.GetComponent<Text>().text = (int.Parse(YourDieRook.GetComponent<Text>().text) + 1).ToString();
                        else if (move.CapturedPiece.IsBishop())
                            YourDieBishop.GetComponent<Text>().text = (int.Parse(YourDieBishop.GetComponent<Text>().text) + 1).ToString();
                        else if (move.CapturedPiece.IsKnight())
                            YourDieKnight.GetComponent<Text>().text = (int.Parse(YourDieKnight.GetComponent<Text>().text) + 1).ToString();
                        else if (move.CapturedPiece.IsPawn())
                            YourDiePawn.GetComponent<Text>().text = (int.Parse(YourDiePawn.GetComponent<Text>().text) + 1).ToString();
                    }
                    else
                    {
                        if (move.CapturedPiece.IsQueen())
                            PeerDieQueen.GetComponent<Text>().text = (int.Parse(PeerDieQueen.GetComponent<Text>().text) + 1).ToString();
                        else if (move.CapturedPiece.IsRook())
                            PeerDieRook.GetComponent<Text>().text = (int.Parse(PeerDieRook.GetComponent<Text>().text) + 1).ToString();
                        else if (move.CapturedPiece.IsBishop())
                            PeerDieBishop.GetComponent<Text>().text = (int.Parse(PeerDieBishop.GetComponent<Text>().text) + 1).ToString();
                        else if (move.CapturedPiece.IsKnight())
                            PeerDieKnight.GetComponent<Text>().text = (int.Parse(PeerDieKnight.GetComponent<Text>().text) + 1).ToString();
                        else if (move.CapturedPiece.IsPawn())
                            PeerDiePawn.GetComponent<Text>().text = (int.Parse(PeerDiePawn.GetComponent<Text>().text) + 1).ToString();
                    }



                    //ParentForm.ChessCaptureBar.Add(ChessImages.GetImageForPiece(move.CapturedPiece));
                }

                // check for the statemate situation
                if (ChessGame.IsStaleMate(ChessGame.GameTurn) || ChessGame.CheckForDrawCon1() || ChessGame.CheckForThreeFoldRepition())
                {
                    IsDraw = true;
                    IsWin = false;
                    IsRunning = false;
                    is_joust_sfx_on = false;

                    IsPause = true;

                    rotate_button.interactable = false;
                    Lightning_Stalemate_Effect(true);

                    // Activate Flashing effect here 
                    if (Is_Computer_Playing)
                    {
                        if (ChessGame.ActivePlay.IsComputer())
                        {
                            Flash.ins.isComputer = true;
                        }
                        else
                        {
                            Flash.ins.isComputer = false;
                        }
                        Flash.ins.Custom_Update(true); // FlashOn
                    }
                    else
                    {
                        if (Is_P1)
                        {
                            Flash.ins.isComputer = true;
                        }
                        else
                        {
                            Flash.ins.isComputer = false;
                        }
                        Flash.ins.FlashOn();
                    }

                    StartCoroutine(wait_for_Stalemate_SFX());
                }

                // If last move was a pawn promotion move, get the new selected piece from user
                if (move.IsPromoMove() && !ChessGame.ActivePlay.IsComputer())
                    //;// ChessGame.SetPromoPiece(GetPromoPiece(move.EndCell.piece.Side));    // Set the promo piece as selected by user

                    // If last move was a pawn promtion move play sound
                    if (move.IsPromoMove())
                    {
                        stoptimerlocal = true;
                        StartCoroutine(playKnightHoodSFX());                        
                        if (move.Type == Move.MoveType.NormalMove || move.Type == Move.MoveType.TowerMove)
                        {
                            if (PlayerPrefs.GetString("Sound") == "On")
                                aud_src1.PlayOneShot(AClips[4], 0.5f); // Move SFX
                        }
                        else
                        {
                            if (move.Piece.IsKnight() || move.Piece.IsKing() || move.Piece.IsPawn())
                            {
                                if (PlayerPrefs.GetString("Sound") == "On")
                                    aud_src1.PlayOneShot(AClips[2], 0.5f); // Sward Clang sfx
                            }
                        }

                    }
                LogUserMove(move.ToString());   // Log the user action

                lock (ChessGame)
                {
                    if (IsRunning == false)
                    {
                        StartCoroutine(EndGame());
                    }
                    else
                    {
                        if (ChessGame.ActivePlay.Name == "You")
                        {
                            PeerName.GetComponent<Text>().color = Color.white;
                            YourName.GetComponent<Text>().color = Color.yellow;
                            Is_P1 = !Is_P1;
                            if (Is_Computer_Playing)
                            {
                                print("Activating Hour Glass");
                                Hourglass.SetActive(true);
                            }
                        }
                        else
                        {
                            if (Is_Computer_Playing)
                            {
                                print("Activating Hour Glass");
                                Hourglass.SetActive(true);
                            }
                            PeerName.GetComponent<Text>().color = Color.yellow;
                            YourName.GetComponent<Text>().color = Color.white;
                            Is_P1 = !Is_P1;
                        }

                        if (IsPause == false)
                        {
                            mTime = Time.unscaledTime;
                        }

                        mAutoTurn = true;

                    }

                }
                break;
            default:
                success = false;
                break;
        }
        //StartCoroutine (TempWait());
        //		if (!comunication.info.PUN_IsOnline) 
        RedrawBoard();  // Refresh the board

        return success;
    }

    IEnumerator playKnightHoodSFX()
    {
        Lightning_Lost_Effect(true);
        yield return new WaitForSeconds(2f);
        aud_src.clip = KnightHood_SFX;
        yield return new WaitForSeconds(1f);
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.PlayOneShot(aud_src.clip, 1f);

    }

    IEnumerator waitforCheck()
    {
        yield return new WaitForSeconds(aud_src.clip.length);
        aud_src.clip = Check_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
        //RedrawBoard ();	
        stoptimerlocal = true;
    }

    IEnumerator TempWait()
    {
        yield return new WaitForSecondsRealtime(aud_src.clip.length);
        RedrawBoard();
    }

    IEnumerator wait_for_clang(Move move, bool isCheck)
    {
        yield return new WaitForSeconds(aud_src.clip.length);

        if (move.Type == Move.MoveType.EnPassant)
        {
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.PlayOneShot(EnPassant_SFX, 1.5f);
        }

        if (move.Type == Move.MoveType.NormalMove || move.Type == Move.MoveType.TowerMove)
        {
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src1.PlayOneShot(AClips[4], 0.5f); // Move SFX
        }
        else
        {
            if (move.Piece.IsKnight() || move.Piece.IsKing() || move.Piece.IsPawn())
            {
                if (PlayerPrefs.GetString("Sound") == "On")
                    aud_src1.PlayOneShot(AClips[2], 0.5f); // Sward Clang sfx
            }
        }

        yield return new WaitForSeconds(aud_src.clip.length);
        if (isCheck)
        {
            // Player is under check
            //			aud_src.clip = Check_SFX;
            //			aud_src.Play ();
        }
        else
        {
            // Player Checkmate
            //			aud_src.clip = Checkmate_SFX;
            //			aud_src.Play ();
        }
    }

    IEnumerator wait_for_Check_SFX(Move move)
    {
        print("waiting after lightning");
        stoptimer = false;
        YourTime.transform.parent.GetComponent<Animator>().SetBool("flash", false);
        PeerTime.transform.parent.GetComponent<Animator>().SetBool("flash", false);
        rotate_button.interactable = false;
        StartCoroutine(wait_for_clang(move, false));
        yield return new WaitForSeconds(1f);
        Lightning_Effect(true);
        yield return new WaitForSeconds(4f);
        Lightning_Effect(false);
        aud_src.clip = Checkmate_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();


        yield return new WaitForSeconds(aud_src.clip.length);
        //		if(comunication.info.PUN_IsOnline)
        //			pun_src.Disconnect ();

        //added by me 8.2
        if (comunication.info.Gameplay_Type == 1)
        { 
            IsWin = true;
        }
        else if (comunication.info.Gameplay_Type == 0)
        {
            if(comunication.info.Invert_flag)
                IsWin = !IsWin;
        }


        StartCoroutine(EndGameWhenCheckMate());
    }

    IEnumerator wait_for_Stalemate_SFX()
    {
        print("Stalemate & waiting");
        yield return new WaitForSeconds(aud_src.clip.length);
        Lightning_Effect(false);

        aud_src.clip = Stalemate_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
        yield return new WaitForSeconds(aud_src.clip.length);

        StartCoroutine(EndGame(true));
    }

    // Call from PUN Rpc
    public void Update_NPT_Call_PUN()
    {
        // then Update Player's Turn
        ChessGame.NextPlayerTurn();

        // Then DrawNew Updated Board
        RedrawBoard();
    }

    private void LogUserMove(string movestring)
    {
        if (IsBlackMove == false)
        {
            Black_Move_History_Text.text += movestring + "," + System.Environment.NewLine;
            IsBlackMove = true;
        }
        else
        {
            White_Move_History_Text.text += movestring + "," + System.Environment.NewLine;
            IsBlackMove = false;
        }
    }

    void UnDoMovehistory()
    {
        // Undo Black String 
        string Black_Str = Black_Move_History_Text.text;
        char Black_delimiter = ',';
        string[] Black_substrings = Black_Str.Split(Black_delimiter);

        Black_Move_History_Text.text = "";

        for (int i = 0; i < Black_substrings.Length - 2; i++)
        {
            Black_Move_History_Text.text += Black_substrings[i] + ",";
        }

        if (Black_Move_History_Text.text != "")
        {
            Black_Move_History_Text.text += " ";
        }

        // Undo White String 
        string White_Str = White_Move_History_Text.text;
        char White_delimiter = ',';
        string[] White_substrings = White_Str.Split(White_delimiter);

        White_Move_History_Text.text = "";

        for (int i = 0; i < White_substrings.Length - 2; i++)
        {
            White_Move_History_Text.text += White_substrings[i] + ",";
        }

        if (White_Move_History_Text.text != "")
        {
            White_Move_History_Text.text += " ";
        }
    }

    public void EndRapidGame(int time)
    {
        lock (ChessGame)
        {
            if (IsRunning == true)
            {
                if (ChessGame.ActivePlay.Name == "You")
                {
                    IsDraw = false;
                    if (time > 0)
                    {
                        IsWin = true;
                    }
                    else
                    {
                        IsWin = false;
                    }
                }
                else
                {
                    IsDraw = false;
                    IsWin = true;
                }

                IsRunning = false;

                StartCoroutine(EndGame());
            }
        }
    }

    // temp
    public void CheckGameEnd(int i)
    {
        switch (i)
        {
            case 0: // draw
                IsDraw = true;
                IsWin = false;
                break;

            case 1: // win 
                IsDraw = false;
                IsWin = true;
                break;

            case 2: // lose
                IsDraw = false;
                IsWin = false;
                break;
        }

        IsRunning = false;

        StartCoroutine(EndGame());
    }
    IEnumerator EndGameWhenCheckMate()
    {
        if (EndedGame == false)
        {
            EndedGame = true;

            yield return new WaitForSeconds(0);

            if (IsDraw)
            {
                ChangeScreen("draw");
            }
            else
            {
                if (IsWin)
                {

                    Debug.Log("----EndGameWhenCheckMate Wind");
                    ChangeScreen("win");

                }
                else // On Losing
                {
                    Debug.Log("----EndGameWhenCheckMate Lost");
                    StartCoroutine("LostThread");
                    

                }
            }

        }
    }
    IEnumerator LostThread()
    {
        Lightning_Lost_Effect(true);
        yield return new WaitForSeconds(2.0f);
        ChangeScreen("lost");
    }
    IEnumerator EndGame(bool isCheckmate = false)
    {
        if (EndedGame == false)
        {
            EndedGame = true;

            
            yield return new WaitForSeconds(0);


            if (IsDraw)
            {

                ChangeScreen("draw");
            }
            else
            {
                if (IsWin)
                {
                    Debug.Log("----EndGame Win");
                    ChangeScreen("win");

                }
                else // On Losing
                {

                        Debug.Log("----EndGame Lost");
                    StartCoroutine("LostThread");

                }
            }

        }
    }

    void Lightning_Effect(bool IsShow)
    {
        if (IsShow)
        {
            aud_src.clip = Lightning_Bolt_SFX;
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();
            

            StartCoroutine(light_wait());
        }
        else
        {
            Lightning_Bolt_Effect.SetActive(false);
        }
    }
    void Lightning_Lost_Effect(bool IsShow)
    {
        if (IsShow)
        {
            aud_src.clip = Lightning_Bolt_SFX;
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();            

            StartCoroutine(light_wait_lost());
        }
        else
        {
            Lightning_Bolt_Effect_Lost.SetActive(false);
        }
    }
    void Lightning_Stalemate_Effect(bool IsShow)
    {
        if (IsShow)
        {
            aud_src.clip = Lightning_Bolt_SFX;
            if (PlayerPrefs.GetString("Sound") == "On")
                aud_src.Play();

            Lightning_Bolt_Effect_StaleMate.SetActive(true);

            StartCoroutine(light_wait());
        }
        else
        {
            Lightning_Bolt_Effect_StaleMate.SetActive(false);
        }
    }
    IEnumerator light_wait()
    {
        yield return new WaitForSeconds(2.0f);
        aud_src.clip = Lightning_Bolt_SFX;
        if (PlayerPrefs.GetString("Sound") == "On")
            aud_src.Play();
        Lightning_Bolt_Effect.SetActive(true);
        yield return new WaitForSeconds(2);
        Lightning_Effect(false);
    }

    IEnumerator light_wait_lost()
    {
        yield return new WaitForSeconds(0.5f);
        Lightning_Bolt_Effect_Lost.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Lightning_Bolt_Effect_Lost.SetActive(false);
    }

    IEnumerator light_wait_statemate()
    {
        yield return new WaitForSeconds(2);
        Lightning_Bolt_Effect_StaleMate.SetActive(false);
    }

    public void AskForPlayAgain()
    {
        confrmPanel.SetActive(true);
    }

    public void ClosePlayAgainPop()
    {
        confrmPanel.SetActive(false);
        playAgain = true;

    }

    public void ShowDenayPlay()
    {
        ShowMsg("Opponent does not want to play");
        StartCoroutine(Hide_Msg());
    }

    IEnumerator Hide_Msg()
    {
        yield return new WaitForSeconds(3);
        HideMsg();
    }

}
