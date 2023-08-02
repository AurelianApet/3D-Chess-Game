using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ChessLibrary;

public class SquarScript : MonoBehaviour 
{
    public GameEngineScript GameEngine;
    private string m_DragSourceSquar;
    private string m_DragDestSquar;

    public void OnClick()
    {
        if(comunication.info.checkSquare && !comunication.info.checkSquare.name.Equals(name)){
            comunication.info.checkSquare = null;
        }
        bool moved = false;
        if (GameEngine.IsRunning && !GameEngine.ChessGame.ActivePlay.IsComputer())
        {
            if (!string.IsNullOrEmpty(GameEngine.LastSelectedSquar) && gameObject.name != GameEngine.LastSelectedSquar)
            {
                if (GameEngine.UserMove(GameEngine.LastSelectedSquar, gameObject.name, true))
                {
                    moved = true;
                    GameEngine.LastSelectedSquar = "";
                }
            }
        }

        if (gameObject.transform.childCount > 0 && !GameEngine.ChessGame.ActivePlay.IsComputer())
        {
            m_DragSourceSquar = m_DragDestSquar = gameObject.name;
            GameEngine.LastSelectedSquar = m_DragDestSquar;

			if (m_DragSourceSquar == m_DragDestSquar)
			{
				if (moved == false)
				{
					GameEngine.SelectedSquar = m_DragSourceSquar;

				} 
				else
				{
					GameEngine.SelectedSquar = "";
                   

          
				}
                GameEngine.RedrawBoard ();

			}
			else
			{
				GameEngine.UserMove (m_DragSourceSquar, m_DragDestSquar, true);
			}
        }
    }

    public void SetPosition (int pos)
    {
		const string cba = "IHGFEDCBA";
		gameObject.name = cba[pos % Board.MAX_COL] + ((pos / Board.MAX_COL) + 1).ToString();	

//		if (GameEngine.RotateMoveEnabled) //black	
//		{
//			const string abc = "ABCDEFGHI";
//			gameObject.name = abc[pos % Board.MAX_COL] + ((pos / Board.MAX_COL) + 1).ToString();	
//		}
//		else
//		{
//			const string cba = "IHGFEDCBA";
//			gameObject.name = cba[pos % Board.MAX_COL] + ((pos / Board.MAX_COL) + 1).ToString();	
//		}
    }
}
