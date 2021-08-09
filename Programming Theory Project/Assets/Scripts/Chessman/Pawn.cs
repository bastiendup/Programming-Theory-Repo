using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];
        Chessman c, c2;

        //White team move
        if (isWhite)
        {
            //Diagonal Left
            //If we're not on the left and top edge
            if (CurrentX != 0 && CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY + 1];

                //If there is a piece and the piece is not white
                if (c != null && !c.isWhite)
                {
                    r[CurrentX - 1, CurrentY + 1] = true;
                }
            }

            //Diagonal Right
            //If we're not on the right and top edge
            if (CurrentX != 7 && CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY + 1];

                //If there is a piece and the piece is black
                if (c != null && !c.isWhite)
                    r[CurrentX + 1, CurrentY + 1] = true;
            }

            //Middle
            //If we're not on top of the board
            if (CurrentY != 7)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];

                //If there is no piece
                if (c == null)
                    r[CurrentX, CurrentY + 1] = true;
            }

            //Middle on first move
            //If we're on first move
            if (CurrentY == 1)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 1];
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentY + 2];

                //If there is no piece in the next two spot
                if (c == null && c2 == null)
                    r[CurrentX, CurrentY + 2] = true;
            }


        }

        //Black team
        else
        {
            //Diagonal Left
            //If we're not on the left and bottom edge
            if (CurrentX != 0 && CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY - 1];

                //If there is a piece and the piece is not white
                if (c != null && c.isWhite)
                {
                    r[CurrentX - 1, CurrentY - 1] = true;
                }
            }

            //Diagonal Right
            //If we're not on the right and bottom edge
            if (CurrentX != 7 && CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY - 1];

                //If there is a piece and the piece is white
                if (c != null && c.isWhite)
                    r[CurrentX + 1, CurrentY - 1] = true;
            }

            //Middle
            //If we're not on the bottom of the board
            if (CurrentY != 0)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];

                //If there is no piece
                if (c == null)
                    r[CurrentX, CurrentY - 1] = true;
            }

            //Middle on first move
            //If we're on first move
            if (CurrentY == 6)
            {
                c = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 1];
                c2 = BoardManager.Instance.Chessmans[CurrentX, CurrentY - 2];

                //If there is no piece in the next two spot
                if (c == null && c2 == null)
                    r[CurrentX, CurrentY - 2] = true;
            }

        }
        return r;
    }
}
