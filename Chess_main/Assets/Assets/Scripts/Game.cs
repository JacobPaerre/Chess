using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

    public GameObject chesspiece;

    // Positions
    private GameObject[,] positions = new GameObject[12, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];
    private GameObject[] blockedFields = new GameObject[4]; // Amount of blocked fields.

    private string currentPlayer = "white";

    private bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerWhite = new GameObject[] // range er -2 til 9 på x og 0 til 7 på y :)
        {
            Create("white_rook", 0, 0), Create("white_knight", 1, 0), Create("white_bishop", 2, 0), Create("white_queen", 3, 0), Create("white_king", 4, 0),
            Create("white_bishop", 5, 0), Create("white_knight", 6, 0), Create("white_rook", 7, 0), Create("white_pawn", -2, 1),
            Create("white_pawn", -1, 1), Create("white_pawn", 0, 1), Create("white_pawn", 1, 1),
            Create("white_pawn", 1, 1), Create("white_pawn", 2, 1), Create("white_pawn", 3, 1), Create("white_pawn", 4, 1), Create("white_pawn", 5, 1),
            Create("white_pawn", 6, 1), Create("white_pawn", 7, 1), Create("white_pawn", 8, 1), Create("white_pawn", 9, 1),
        };
        playerBlack = new GameObject[]
                {
            Create("black_rook", 0, 7), Create("black_knight", 1, 7), Create("black_bishop", 2, 7), Create("black_queen", 3, 7), Create("black_king", 4, 7),
            Create("black_bishop", 5, 7), Create("black_knight", 6, 7), Create("black_rook", 7, 7), Create("black_pawn", 0, 6), Create("black_pawn", 1, 6),
            Create("black_pawn", 1, 6), Create("black_pawn", 2, 6), Create("black_pawn", 3, 6), Create("black_pawn", 4, 6), Create("black_pawn", 5, 6),
            Create("black_pawn", 6, 6), Create("black_pawn", 7, 6)
                };

        blockedFields = new GameObject[]
        {
            Create("blocked_field", 4, 4), Create("blocked_field", 4, 3), Create("blocked_field", 3, 4), Create("blocked_field", 3, 3),
        };
        // Set piece positions
        for (int i = 0; i < playerBlack.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }
        for (int i = 0; i < blockedFields.Length; i++)
        {
            SetPosition(blockedFields[i]);
        }
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0,0,-1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

}
