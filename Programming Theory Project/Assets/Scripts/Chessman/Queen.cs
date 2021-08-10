using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Chessman
{
    public override bool[,] PossibleMove()
    {
        bool[,] r = new bool[8, 8];

        Chessman c;
        int i, j;

        #region Rook Behaviour

        #region RightMove

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
        #endregion

        #region LeftMove 

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
        #endregion

        #region UpMove 

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

        #endregion

        #region DownMove 

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

        #endregion
        #endregion

        #region Bishop Behaviour
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
        #endregion

        return r;
    }
}
