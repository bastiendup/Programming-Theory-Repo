using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardHighlights : MonoBehaviour
{
    public static BoardHighlights Instance { get; set; }

    public GameObject highlightPrefab;
    public Material highlightEmptySquare;
    public Material highlightEnemySquare;
    private List<GameObject> highlights;


    private void Start()
    {
        Instance = this;
        highlights = new List<GameObject>();
    }

    private GameObject GetHighlightObject()
    {
        //return the first go in the highlights list that match the condition
        GameObject go = highlights.Find(g => !g.activeSelf);

        if (go == null)
        {
            go = Instantiate(highlightPrefab);
            highlights.Add(go);
        }
        return go;
    }

    public void HighlightAllowedMoves(bool[,] moves)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (moves[i, j])
                {
                    GameObject go = GetHighlightObject();
                    go.SetActive(true);
                    go.transform.position = new Vector3(i + 0.5f, 0, j + 0.5f);

                    //Check if there is an enemy on the square, if so use the enemySquare material (red), else use the green one
                    Chessman c = BoardManager.Instance.Chessmans[i, j];

                    if (c != null && c.isWhite != BoardManager.Instance.isWhiteTurn)
                        go.GetComponent<MeshRenderer>().material = highlightEnemySquare;

                    else
                        go.GetComponent<MeshRenderer>().material = highlightEmptySquare;
                }
            }

        }
    }

    public void HideHighlights()
    {
        foreach (var go in highlights)
            go.SetActive(false);
    }

}
