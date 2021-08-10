using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i, j;


        #region TopLeft 

        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            //Going left
            i--;
            //Going up
            j++;
            //If we're outside of the board
            if (i < 0 || j >= 8)
                break;

            c = BoardManager.Instance.Chessmans[i, j];

            //If there is no piece
            if (c == null)
                r[i, j] = true;

            //If there is a piece 
            else
            {
                //If the piece is on the opposit team
                if (isWhite != c.isWhite)
                    r[i, j] = true;

                break;
            }
        }

        #endregion

        #region TopRight 

        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            //Going right
            i++;
            //Going up
            j++;
            //If we're outside of the board
            if (i >= 8 || j >= 8)
                break;

            c = BoardManager.Instance.Chessmans[i, j];

            //If there is no piece
            if (c == null)
                r[i, j] = true;

            //If there is a piece 
            else
            {
                //If the piece is on the opposit team
                if (isWhite != c.isWhite)
                    r[i, j] = true;

                break;
            }
        }

        #endregion

        #region DownLeft 

        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            //Going left
            i--;
            //Going down
            j--;
            //If we're outside of the board
            if (i < 0 || j < 0)
                break;

            c = BoardManager.Instance.Chessmans[i, j];

            //If there is no piece
            if (c == null)
                r[i, j] = true;

            //If there is a piece 
            else
            {
                //If the piece is on the opposit team
                if (isWhite != c.isWhite)
                    r[i, j] = true;

                break;
            }
        }

        #endregion

        #region DownRight 

        i = CurrentX;
        j = CurrentY;
        while (true)
        {
            //Going right
            i++;
            //Going down
            j--;
            //If we're outside of the board
            if (i >= 8 || j < 0)
                break;

            c = BoardManager.Instance.Chessmans[i, j];

            //If there is no piece
            if (c == null)
                r[i, j] = true;

            //If there is a piece 
            else
            {
                //If the piece is on the opposit team
                if (isWhite != c.isWhite)
                    r[i, j] = true;

                break;
            }

        }

        #endregion

        return r;
    }
}
