using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i;

        //Right
        i = CurrentX;
        while (true)
        {
            i++;
            if (i >= 8)
                break;

            c = BoardManager.Instance.Chessmans[i, CurrentY];

            //If there is no piece
            if (c == null)
                r[i, CurrentY] = true;

            //If there is a piece
            else
            {
                //If the piece is on the opposit team
                if (c.isWhite != isWhite)
                    r[i, CurrentY] = true;

                break;
            }
        }

        //Left
        i = CurrentX;
        while (true)
        {
            i--;
            if (i < 0)
                break;

            c = BoardManager.Instance.Chessmans[i, CurrentY];

            //If there is no piece
            if (c == null)
                r[i, CurrentY] = true;

            //If there is a piece
            else
            {
                //If the piece is on the opposit team
                if (c.isWhite != isWhite)
                    r[i, CurrentY] = true;

                break;
            }
        }

        //Up
        i = CurrentY;
        while (true)
        {
            i++;
            if (i >= 8)
                break;

            c = BoardManager.Instance.Chessmans[CurrentX, i];

            //If there is no piece
            if (c == null)
                r[CurrentX, i] = true;

            //If there is a piece
            else
            {
                //If the piece is on the opposit team
                if (c.isWhite != isWhite)
                    r[CurrentX, i] = true;

                break;
            }
        }

        //Down
        i = CurrentY;
        while (true)
        {
            i--;
            if (i < 0)
                break;

            c = BoardManager.Instance.Chessmans[CurrentX, i];

            //If there is no piece
            if (c == null)
                r[CurrentX, i] = true;

            //If there is a piece
            else
            {
                //If the piece is on the opposit team
                if (c.isWhite != isWhite)
                    r[CurrentX, i] = true;

                break;
            }
        }

        return r;
    }
}
