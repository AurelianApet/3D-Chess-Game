using UnityEngine;
using UnityEngine.UI;

using System;
using System.Threading;
using System.Collections;

public class Flash : MonoBehaviour
{
    public static Flash ins;

    public Color KT_Color, KC_Color, KR_Color, Final_Color;

    public bool isComputer;

    Animator anim;
    public Image computer_color, player_color;

    public float Multi_Float;
	public bool isFlashOn = false;
    void Awake()
    {
        ins = this;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
        computer_color = transform.GetChild(0).GetComponent<Image>();
        player_color = transform.GetChild(1).GetComponent<Image>();
        init();
    }

    public void init()
    {
        Color topColor = new Color();

        /*if(comunication.info.PUN_GameplayMode == 3)
        {
            if (comunication.info.PUN_GameFormationMode == 0)
            {
                topColor = KR_Color;
                Final_Color = KT_Color;
            }
            else
            {
                topColor = KT_Color;
                Final_Color = KR_Color;
            }
        }
        else if (comunication.info.PUN_GameplayMode == 4)
        {
            if (comunication.info.PUN_GameFormationMode == 1)
            {
                topColor = KR_Color;
                Final_Color = KC_Color;
            }
            else
            {
                topColor = KC_Color;
                Final_Color = KR_Color;
            }
        }
        else if (comunication.info.PUN_GameplayMode == 5)
        {
            if (comunication.info.PUN_GameFormationMode == 0)
            {
                topColor = KC_Color;
                Final_Color = KT_Color;
            }
            else
            {
                topColor = KT_Color;
                Final_Color = KC_Color;
            }
        }*/
        if (comunication.info.PUN_GameplayMode > 2)
        {
            topColor = KT_Color;
            Final_Color = KT_Color;
        }

        if (comunication.info.PUN_GameplayMode == 0) // KT
        {
            Final_Color = KT_Color;
            topColor = KT_Color;
        }
        if (comunication.info.PUN_GameplayMode == 1) // KC
        {
            Final_Color = KC_Color;
            topColor = KT_Color;
        }
        if (comunication.info.PUN_GameplayMode == 2) // KR
        {
            Final_Color = KR_Color;
            topColor = KT_Color;
        }

        
        computer_color.color = topColor;
        player_color.color = Final_Color;

        computer_color.canvasRenderer.SetAlpha(0.75f);
        player_color.canvasRenderer.SetAlpha(0.75f);

        FlashOff();
        Custom_Update(false);
    }

    public void SwapSides()
    {
        /* Image temp = computer_color;
        computer_color = player_color;
        player_color = temp;*/
    }

    public void FlashOn()
    {
		isFlashOn = true;
        if (GameEngineScript.instance.Is_Computer_Playing == false)
        {            
            if (isComputer)
            {
                anim.SetBool("ply", false);
                anim.SetBool("com", true);

                if (comunication.info.PUN_GameplayMode == 0) // KT
                {
                    float r = KT_Color.r;
                    float g = KT_Color.g;
                    float b = KT_Color.b;

                    computer_color.color = new Color(r, g, b, 0.5f);

                }

                if (comunication.info.PUN_GameplayMode == 1) // KC
                {
                    float r = KC_Color.r;
                    float g = KC_Color.g;
                    float b = KC_Color.b;

                    computer_color.color = new Color(r, g, b, 0.5f);
                }

                if (comunication.info.PUN_GameplayMode == 2) // KRT
                {
                    float r = KR_Color.r;
                    float g = KR_Color.g;
                    float b = KR_Color.b;

                    computer_color.color = new Color(r, g, b, 0.5f);
                }

            }
            else
            {
                anim.SetBool("ply", true);
                anim.SetBool("com", false);

                if (comunication.info.PUN_GameplayMode == 0) // KT
                {
                    float r = KT_Color.r;
                    float g = KT_Color.g;
                    float b = KT_Color.b;

                    player_color.color = new Color(r, g, b, 0.5f);

                }

                if (comunication.info.PUN_GameplayMode == 1) // KC
                {
                    float r = KC_Color.r;
                    float g = KC_Color.g;
                    float b = KC_Color.b;

                    player_color.color = new Color(r, g, b, 0.5f);
                }

                if (comunication.info.PUN_GameplayMode == 2) // KRT
                {
                    float r = KR_Color.r;
                    float g = KR_Color.g;
                    float b = KR_Color.b;

                    player_color.color = new Color(r, g, b, 0.5f);
                }
            }
        }
    }

    public void FlashOff()
    {
		isFlashOn = false;
        if (GameEngineScript.instance.Is_Computer_Playing == false)
        {            

            anim.SetBool("com", false);
            anim.SetBool("ply", false);
            if (comunication.info.PUN_GameplayMode == 0) // KT
            {
                Final_Color = KT_Color;
            }
            if (comunication.info.PUN_GameplayMode == 1) // KC
            {
                Final_Color = KC_Color;
            }
            if (comunication.info.PUN_GameplayMode == 2) // KR
            {
                Final_Color = KR_Color;
            }
            computer_color.color = Final_Color;
            player_color.color = Final_Color;

            computer_color.canvasRenderer.SetAlpha(0.75f);
            player_color.canvasRenderer.SetAlpha(0.75f);
        }
    }

    public void Custom_Update(bool start)
    {
		isFlashOn = start;
        if (start)
        {
            if (isComputer)
            {
                anim.SetBool("ply", false);
                anim.SetBool("com", true);                

                computer_color.color = Final_Color;
                computer_color.canvasRenderer.SetAlpha(1.0f);

            }
            else
            {
                anim.SetBool("com", false);
                anim.SetBool("ply", true);                

                player_color.color = Final_Color;
                player_color.canvasRenderer.SetAlpha(1.0f);
            }
        }
        else
        {
            Color topColor = new Color();

            /*if (comunication.info.PUN_GameplayMode == 3)
            {
                if (comunication.info.PUN_GameFormationMode == 0)
                {
                    topColor = KR_Color;
                    Final_Color = KT_Color;
                }
                else
                {
                    topColor = KT_Color;
                    Final_Color = KR_Color;
                }
            }
            else if (comunication.info.PUN_GameplayMode == 4)
            {
                if (comunication.info.PUN_GameFormationMode == 1)
                {
                    topColor = KR_Color;
                    Final_Color = KC_Color;
                }
                else
                {
                    topColor = KC_Color;
                    Final_Color = KR_Color;
                }
            }
            else if (comunication.info.PUN_GameplayMode == 5)
            {
                if (comunication.info.PUN_GameFormationMode == 0)
                {
                    topColor = KC_Color;
                    Final_Color = KT_Color;
                }
                else
                {
                    topColor = KT_Color;
                    Final_Color = KC_Color;
                }
            }*/
            if (comunication.info.PUN_GameplayMode > 2)
            {
                topColor = KT_Color;
                Final_Color = KT_Color;
            }

            if (comunication.info.PUN_GameplayMode == 0) // KT
            {
                Final_Color = KT_Color;
                topColor = KT_Color;
            }
            if (comunication.info.PUN_GameplayMode == 1) // KC
            {
                Final_Color = KC_Color;
                topColor = KC_Color;
            }
            if (comunication.info.PUN_GameplayMode == 2) // KR
            {
                Final_Color = KR_Color;
                topColor = KR_Color;
            }
            computer_color.color = topColor;
            player_color.color = Final_Color;

            computer_color.canvasRenderer.SetAlpha(0.75f);
            player_color.canvasRenderer.SetAlpha(0.75f);

            anim.SetBool("ply", false);
            anim.SetBool("com", false);
        }
    }
}
