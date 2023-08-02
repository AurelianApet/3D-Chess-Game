using UnityEngine;

using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;

using ChessLibrary;
using GoogleMobileAds.Api;

public class comunication : MonoBehaviour
{
    public static comunication info;

    public int Gameplay_Trail;
    public int ShowTut;

    [Header("In App Purchases")]
    public bool Sku_1;
    public bool Sku_2;
    public bool Sku_3;
    public bool Sku_4;
    public bool Sku_5;

    [Header("Knights Round Table")]

    public int Scene_Index;

    public bool PUN_IsOnline, Is_Master;

    public int PUN_IsTimerOn;
    public int PUN_BattleTime;

    public float Actual_Time;

    public string Player1_Name, Player2_Name;
    public int ActivePlayer;

    [Header("TYPE of Gameplay")]
    [Tooltip("0 > AI : 1 > START LOCAL : 2 > START ONLINE")]
    public int Gameplay_Type;

    [Header("MODE of Gameplay")]
    [Tooltip("( 0 > Knights Templar ) , ( 1 > Knights Of Columbus ) , ( 2 > Knights Round Table ) , '3,4,5' : Mergy mode")]
    public int PUN_GameplayMode;

    public bool Invert_flag = false;

    [Header("Formation Mode")]
    [Tooltip("( 0 > Knights Templar ) , ( 1 > Knights Of Columbus ) , ( 2 > Knights Round Table )")]
    public int PUN_GameFormationMode;

    [Header("Match Number out of total 6")]
    public int Match_No;

    [Header("Win Loase Strings")]

    [Header("Knights Templar")]
    public string KT_P1_Name;
    public string KT_P1_W_WL;
    public string KT_P1_B_WL;

    public string KT_P2_Name;
    public string KT_P2_W_WL;
    public string KT_P2_B_WL;

    [Header("Knights Of Columbus")]
    public string KC_P1_Name;
    public string KC_P1_W_WL;
    public string KC_P1_B_WL;

    public string KC_P2_Name;
    public string KC_P2_W_WL;
    public string KC_P2_B_WL;

    [Header("Knights Round Table")]
    public string KRT_P1_Name;
    public string KRT_P1_W_WL;
    public string KRT_P1_B_WL;

    public string KRT_P2_Name;
    public string KRT_P2_W_WL;
    public string KRT_P2_B_WL;

    /// <summary>
    /// Fire Base Data
    /// </summary>
    [Header("User Data")]
    public string UserID;
    public string Username, Temp_Username;
    public string Password;
    public string Email;
    public string Domain;
    public float COINS;

    [Header("Match Data")]
    public int KT_WIN;
    public int KC_WIN;
    public int KRT_WIN;

    public int KT_LOSE;
    public int KC_LOSE;
    public int KRT_LOSE;

    public int KT_DRAW;
    public int KC_DRAW;
    public int KRT_DRAW;

    [Header("Tournament Data")]
    public int Total_Wins;
    public int Total_Loses;
    public int Total_Draws;

    public int KT_Points, KC_Points, KRT_Points, User_Points;
    public string User_Degree;

    public int Tournament_ON_OFF; // 0 > OFF , 1 > ON

    public int Total_Matches;
    public float Ratings;
    public float percentage;

    // Local Data
    public int KT_W, KT_B;   // Knights Tampler White / Black >> 0 > Win, 1 > Lose 2 > Draw
    public int KC_W, KC_B;   // Knights of Columbus White / Black >> 0 > Win, 1 > Lose 2 > Draw
    public int KRT_W, KRT_B; // Knights of Round Table White / Black >> 0 > Win, 1 > Lose 2 > Draw
    public string tempUserID1;
    public string tempUserID2;
    public bool playAgain = false;
    public BannerView bannerAd;
	public InterstitialAd interstitial;
//    public Firebase.Auth.FirebaseUser user;
    public GameObject checkSquare;
    /// <summary>
    /// End
    /// </summary>

    void Awake()
    {        
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        //Check for Dublication
        if (info == null)
        {
            DontDestroyOnLoad(gameObject);
            info = this;
        }
        else
        {
            DestroyImmediate(gameObject);
            //			if (info != this)
            //			{
            //				Destroy (this.gameObject);
            //				return;
            //			}	
        }

        Load();
        Username = PlayerPrefs.GetString("Name");
        Password = PlayerPrefs.GetString("Pwd");
        Email = PlayerPrefs.GetString("Email");

    }

    public void Save_Purchses(int PurchaseType, int IsPurchsed)
    {
        switch (PurchaseType)
        {
            case 1:
                Sku_1 = (IsPurchsed == 1);
                break;
            case 2:
                Sku_2 = (IsPurchsed == 1);
                break;
            case 3:
                Sku_3 = (IsPurchsed == 1);
                break;
            case 4:
                Sku_4 = (IsPurchsed == 1);
                break;
            case 5:
                Sku_5 = (IsPurchsed == 1);
                break;

        }       
        Save();
    }

    // Check for Internet
    public bool IsInternetAvailable()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }

    public void Calculate_Points()
    {        
        //		Total_Wins = KT_WIN + KC_WIN + KRT_WIN;
        //		Total_Loses = KT_LOSE + KC_LOSE + KRT_LOSE;
        //		Total_Draws = KT_DRAW + KC_DRAW + KRT_DRAW;
        //		User_Points = ((Total_Wins * 3) + (Total_Loses * 0) + (Total_Draws * 1)) * 10f;

        KT_Points = ((KT_WIN * 3) + (KT_LOSE * 0) + (KT_DRAW * 1));
        KC_Points = ((KC_WIN * 3) + (KC_LOSE * 0) + (KC_DRAW * 1));
        KRT_Points = ((KRT_WIN * 3) + (KRT_LOSE * 0) + (KRT_DRAW * 1));

        //User_Points = KT_Points + KC_Points + KRT_Points;
    }

    public void Calculate_Degree()
    {        
        Total_Wins = KT_WIN + KC_WIN + KRT_WIN;
        Total_Loses = KT_LOSE + KC_LOSE + KRT_LOSE;
        Total_Draws = KT_DRAW + KC_DRAW + KRT_DRAW;

        Total_Matches = Total_Wins + Total_Loses + Total_Draws;
        Ratings = ((Total_Wins * 3) + (Total_Loses * 0) + (Total_Draws * 1)) * Total_Matches;
        if (Total_Matches != 0)
            percentage = (Total_Wins / Total_Matches) * 100;

        if (percentage > 0 && percentage < 60)
        {
            User_Degree = "First";
        }

        if (percentage > 61 && percentage < 90)
        {
            User_Degree = "Second";
        }

        if (percentage > 90)
        {
            User_Degree = "Third";
        }
    }

    public void Save()
    {

        //Debug.Log(Application.persistentDataPath + "/KD_Insight.bf");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/KD_Insight.bf");
        Game_Data data = new Game_Data();

        // Data Variables here
        data.SR_Gameplay_Trail = Gameplay_Trail;
        data.SR_ShowTut = ShowTut;

        data.Sku_1 = Sku_1;
        data.Sku_2 = Sku_2;
        data.Sku_3 = Sku_3;
        data.Sku_4 = Sku_4;
        data.Sku_5 = Sku_5;

        data.SR_UserID = UserID;
        data.SR_Username = Username;
        data.SR_Password = Password;
        data.SR_Email = Email;
        data.SR_COINS = COINS;

        bf.Serialize(file, data);
        file.Close();        
    }

    public void Load()
    {        
        if (File.Exists(Application.persistentDataPath + "/KD_Insight.bf"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/KD_Insight.bf", FileMode.Open);
            Game_Data data = (Game_Data)bf.Deserialize(file);
            file.Close();

            // Data Variables here
            Gameplay_Trail = data.SR_Gameplay_Trail;
            ShowTut = data.SR_ShowTut;

            Sku_1 = data.Sku_1;
            Sku_2 = data.Sku_2;
            Sku_3 = data.Sku_3;
            Sku_4 = data.Sku_4;
            Sku_5 = data.Sku_5;

            UserID = data.SR_UserID;
            Username = data.SR_Username;
            Password = data.SR_Password;
            Email = data.SR_Email;
            COINS = data.SR_COINS;
        }
        else
        {
            // if KD_Insight.bf is not available, give an exaption
            Debug.Log("Initial data Saving & Creating new file . . .");
            Save();
        }
    }
}
// Class GameData can save data to file located in data 
[Serializable]
public class Game_Data
{
    public int SR_Gameplay_Trail;
    public int SR_ShowTut;

    public bool Sku_1;
    public bool Sku_2;
    public bool Sku_3;
    public bool Sku_4;
    public bool Sku_5;


    public string SR_UserID;
    public string SR_Username;
    public string SR_Password;
    public string SR_Email;
    public float SR_COINS;
}
