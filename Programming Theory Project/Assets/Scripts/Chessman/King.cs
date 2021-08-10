using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i, j;

        #region TopSide 

        i = CurrentX - 1;
        j = CurrentY + 1;
        //If we're not on the top of the board
        if (CurrentY != 7)
        {
            //Browse the 3 possible move 
            for (int k = 0; k < 3; k++)
            {
                //If we're not on the edges
                if (i >= 0 || i < 8)
                {
                    c = BoardManager.Instance.Chessmans[i, j];

                    //If there is no piece
                    if (c == null)
                        r[i, j] = true;

                    //If the piece is on the opposit team
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }
                i++;
            }
        }

        #endregion

        #region DownSide 

        i = CurrentX - 1;
        j = CurrentY - 1;
        //If we're not on the bottom of the board
        if (CurrentY != 0)
        {
            //Browse the 3 possible move 
            for (int k = 0; k < 3; k++)
            {
                //If we're not on the edges
                if (i >= 0 || i < 8)
                {
                    c = BoardManager.Instance.Chessmans[i, j];

                    //If there is no piece
                    if (c == null)
                        r[i, j] = true;

                    //If the piece is on the opposit team
                    else if (isWhite != c.isWhite)
                        r[i, j] = true;
                }
                i++;
            }
        }

        #endregion

        #region MiddleLeft 

        //If we're not on the left edge
        if (CurrentX != 0)
        {
            c = BoardManager.Instance.Chessmans[CurrentX - 1, CurrentY];

            //If there is no piece
            if (c == null)
                r[CurrentX - 1, CurrentY] = true;

            //If the piece is on the opposit team
            else if (isWhite != c.isWhite)
                r[CurrentX - 1, CurrentY] = true;
        }

        #endregion

        #region MiddleRight 

        //If we're not on the right edge
        if (CurrentX != 7)
        {
            c = BoardManager.Instance.Chessmans[CurrentX + 1, CurrentY];

            //If there is no piece
            if (c == null)
                r[CurrentX + 1, CurrentY] = true;

            //If the piece is on the opposit team
            else if (isWhite != c.isWhite)
                r[CurrentX + 1, CurrentY] = true;
        }

        #endregion

        return r;
    }
}
