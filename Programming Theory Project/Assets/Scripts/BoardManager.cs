using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardManager : MonoBehaviour
{
    public static BoardManager Instance { get; set; }
    private bool[,] allowedMoves { get; set; }

    public Chessman[,] Chessmans { get; set; }
    private Chessman selectedChessman;

    private const float TILE_SIZE = 1.0f;
    private const float TILE_OFFSET = 0.5f;

    private int selectionX = -1;
    private int selectionY = -1;

    public List<GameObject> chessmanPrefabs;
    private List<GameObject> activeChessman;

    public bool isWhiteTurn = true;

    public GameObject endGamePanel;
    public TextMeshProUGUI endGameText;


    private void Start()
    {
        Instance = this;
        SpawnAllChessman();
    }

    private void Update()
    {
        UpdateSelection();
        //DrawChessboard();

        //Left Click
        if (Input.GetMouseButtonDown(0))
        {
            //If we're on the board
            if (selectionX >= 0 && selectionY >= 0 && selectionX <= 8 && selectionY <= 8)
            {
                if (selectedChessman == null)
                {
                    //select the chessman
                    SelectChessman(selectionX, selectionY);
                }
                else
                {
                    //Move the chessman
                    MoveChessman(selectionX, selectionY);
                }
            }
        }
    }

    private void SelectChessman(int x, int y)
    {
        //Check if we are on a chessman
        if (Chessmans[x, y] == null)
            return;

        //Check if the chessman we are currently clicking on is our (if it's white turn the chessman need to be white)
        if (Chessmans[x, y].isWhite != isWhiteTurn)
            return;

        bool hasAtLeastOneMove = false;

        //Browse the board to check if there is at least one move possible for the piece
        allowedMoves = Chessmans[x, y].PossibleMove();
        for (int i = 0; i < 8; i++)
            for (int j = 0; j < 8; j++)
                if (allowedMoves[i, j])
                    hasAtLeastOneMove = true;

        //If there is no possible move, return (so the piece can't be selected)
        if (!hasAtLeastOneMove)
            return;

        selectedChessman = Chessmans[x, y];
        selectedChessman.GetComponent<Chessman>().highlight.SetActive(true);
        BoardHighlights.Instance.HighlightAllowedMoves(allowedMoves);

    }

    private void MoveChessman(int x, int y)
    {
        //If the chessman can move
        if (allowedMoves[x, y])
        {
            Chessman c = Chessmans[x, y];
            //If there is a piece and the piece is on the opposit team
            if (c != null && c.isWhite != isWhiteTurn)
            {
                //If it is the king
                if (c.GetType() == typeof(King))
                {
                    //End the game
                    EndGame();
                    return;
                }

                //Capture the piece
                activeChessman.Remove(c.gameObject);
                Destroy(c.gameObject);
            }


            #region Promotion 

            if (selectedChessman.GetType() == typeof(Pawn))
            {
                //If we're on the top of the board (= white team)
                if (y == 7)
                {
                    //Destroy the pawn
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);

                    //Instantiate the white queen
                    SpawnChessman(1, x, y);
                    selectedChessman = Chessmans[x, y];
                }

                //If we're on the bottom of the board (= black team)
                else if (y == 0)
                {
                    //Destroy the pawn
                    activeChessman.Remove(selectedChessman.gameObject);
                    Destroy(selectedChessman.gameObject);

                    //Instantiate the black queen
                    SpawnChessman(7, x, y);
                    selectedChessman = Chessmans[x, y];
                }
            }

            #endregion

            Chessmans[selectedChessman.CurrentX, selectedChessman.CurrentY] = null;
            selectedChessman.transform.position = GetTileCenter(x, y);
            selectedChessman.SetPosition(x, y);
            Chessmans[x, y] = selectedChessman;

            isWhiteTurn = !isWhiteTurn;
        }

        selectedChessman.GetComponent<Chessman>().highlight.SetActive(false);
        BoardHighlights.Instance.HideHighlights();

        //If we have selected a chessman and click elsewhere, unselect it
        selectedChessman = null;
    }

    private void UpdateSelection()
    {
        if (!Camera.main)
            return;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 25.0f, LayerMask.GetMask("ChessPlane")))
        {
            selectionX = (int)hit.point.x;
            selectionY = (int)hit.point.z;
        }
        else
        {
            selectionX = -1;
            selectionY = -1;
        }
    }

    private void SpawnAllChessman()
    {
        activeChessman = new List<GameObject>();
        Chessmans = new Chessman[8, 8];

        //Spawn the white team

        //King
        SpawnChessman(0, 4, 0);

        //Queen
        SpawnChessman(1, 3, 0);

        //Rooks
        SpawnChessman(2, 0, 0);
        SpawnChessman(2, 7, 0);

        //Bishop
        SpawnChessman(3, 2, 0);
        SpawnChessman(3, 5, 0);

        //Knight
        SpawnChessman(4, 1, 0);
        SpawnChessman(4, 6, 0);

        //Pawn
        for (int i = 0; i < 8; i++)
            SpawnChessman(5, i, 1);


        //Spawn the black team

        //King
        SpawnChessman(6, 4, 7);

        //Queen
        SpawnChessman(7, 3, 7);

        //Rooks
        SpawnChessman(8, 0, 7);
        SpawnChessman(8, 7, 7);

        //Bishop
        SpawnChessman(9, 2, 7);
        SpawnChessman(9, 5, 7);

        //Knight
        SpawnChessman(10, 1, 7);
        SpawnChessman(10, 6, 7);

        //Pawn
        for (int i = 0; i < 8; i++)
            SpawnChessman(11, i, 6);
    }

    private void SpawnChessman(int index, int x, int y)
    {
        GameObject go = Instantiate(chessmanPrefabs[index], GetTileCenter(x, y), Quaternion.identity);
        go.transform.SetParent(transform);
        Chessmans[x, y] = go.GetComponent<Chessman>();
        Chessmans[x, y].SetPosition(x, y);
        activeChessman.Add(go);
    }

    private Vector3 GetTileCenter(int x, int y)
    {
        Vector3 origin = Vector3.zero;
        origin.x += (TILE_SIZE * x) + TILE_OFFSET;
        origin.z += (TILE_SIZE * y) + TILE_OFFSET;
        return origin;
    }

    private void DrawChessboard() //Use to draw the board without gameobject
    {
        Vector3 widthLine = Vector3.right * 8;
        Vector3 heightLine = Vector3.forward * 8;

        //Draw the chessboard
        for (int i = 0; i <= 8; i++)
        {
            Vector3 start = Vector3.forward * i;
            Debug.DrawLine(start, start + widthLine);

            for (int j = 0; j <= 8; j++)
            {
                start = Vector3.right * j;
                Debug.DrawLine(start, start + heightLine);
            }
        }

        //Draw the selection
        if (selectionX >= 0 && selectionY >= 0)
        {
            Debug.DrawLine(
                Vector3.forward * selectionY + Vector3.right * selectionX,
                Vector3.forward * (selectionY + 1) + Vector3.right * (selectionX + 1)
                );

            Debug.DrawLine(
                Vector3.forward * (selectionY + 1) + Vector3.right * selectionX,
                Vector3.forward * selectionY + Vector3.right * (selectionX + 1)
            );
        }
    }

    private void EndGame()
    {
        endGamePanel.SetActive(true);
        if (isWhiteTurn)
            endGameText.text = $"White Team Wins !";

        else
            endGameText.text = $"Black Team Wins !";

    }

    public void RestartNewGame()
    {
        //Destroy every piece left
        foreach (var go in activeChessman)
            Destroy(go);

        //Start a new game
        isWhiteTurn = true;
        BoardHighlights.Instance.HideHighlights();
        SpawnAllChessman();

        //Disable end game panel
        endGamePanel.SetActive(false);
    }
}
