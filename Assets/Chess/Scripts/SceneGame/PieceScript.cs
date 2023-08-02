using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using ChessLibrary;

public class PieceScript : MonoBehaviour
{
    public string Position;

    public enum AliveCheck
    {
        Alive,
        Die,
        AliveCheck
    }

    public AliveCheck aliveCheck;
    public Sprite[] images;
    public Sprite[] selectedImages;

    bool IsInverted = false;

    void Start () 
	{
        aliveCheck = AliveCheck.Alive;
	}
	
	void Update () 
	{
	
	}

    public void SetImage (Piece piece, bool isWhite)
    {
        if (IsInverted) isWhite = !isWhite;
        int img = isWhite ? 0 : 6;
        if (piece.IsKnight())
            img += 5;
        else if (piece.IsKing())
            img += 4;
        else if (piece.IsQueen())
            img += 3;
        else if (piece.IsBishop())
            img += 2;
        else if (piece.IsRook())
            img += 1;
        else if (piece.IsPawn())
            img += 0;
        if (gameObject.GetComponent<Image>().sprite != images[img])
            gameObject.GetComponent<Image>().sprite = images[img];
    }

    public void SetSelectedImage ( Piece piece, bool isWhite)
    {
        if (IsInverted) isWhite = !isWhite;
        int img = isWhite ? 0 : 6;
        if (piece.IsKnight())
            img += 5;
        else if (piece.IsKing())
            img += 4;
        else if (piece.IsQueen())
            img += 3;
        else if (piece.IsBishop())
            img += 2;
        else if (piece.IsRook())
            img += 1;
        else if (piece.IsPawn())
            img += 0;
        if (gameObject.GetComponent<Image>().sprite != selectedImages[img])
            gameObject.GetComponent<Image>().sprite = selectedImages[img];
    }

    public void SetPosition (string pos)
    {
        Position = pos;
    }

    public string GetPosition()
    {
        return Position;
    }

    public void InvertPiece()
    {
        IsInverted = !IsInverted;
    }
}
