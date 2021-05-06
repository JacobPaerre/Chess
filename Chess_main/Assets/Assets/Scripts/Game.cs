using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //Reference from Unity IDE
    public GameObject chesspiece;

    //Matrices needed, positions of each of the GameObjects
    //Also separate arrays for the players in order to easily keep track of them all
    //Keep in mind that the same objects are going to be in "positions" and "playerBlack"/"playerWhite"
    private GameObject[,] positions = new GameObject[12, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];
    private GameObject[] blockedField = new GameObject[4];

    //current turn
    private string currentPlayer = "white";

    //Game Ending
    private bool gameOver = false;

    //Unity calls this right when the game starts, there are a few built in functions
    //that Unity can call for you
    public void Start()
    {
         playerWhite = new GameObject[] { Create("white_rook", 2, 0), Create("white_knight", 3, 0),
             Create("white_bishop", 4, 0), Create("white_queen", 5, 0), Create("white_king", 6, 0),
             Create("white_bishop", 7, 0), Create("white_knight", 8, 0), Create("white_rook", 9, 0),
             Create("white_pawn", 2, 1), Create("white_pawn", 3, 1), Create("white_pawn", 4, 1),
             Create("white_pawn", 7, 1), Create("white_pawn", 8, 1), Create("white_pawn", 9, 1),
             Create("white_guard", 5, 1), Create("white_guard",6,1)
         };

         playerBlack = new GameObject[] { Create("black_rook", 2, 7), Create("black_knight",3,7),
             Create("black_bishop",4,7), Create("black_queen",5,7), Create("black_king",6,7),
             Create("black_bishop",7,7), Create("black_knight",8,7), Create("black_rook",9,7),
             Create("black_pawn", 2, 6), Create("black_pawn", 3, 6), Create("black_pawn", 4, 6),
             Create("black_pawn", 7, 6), Create("black_pawn", 8, 6), Create("black_pawn", 9, 6),
             Create("black_guard", 5,6), Create("black_guard",6,6)
         };
         blockedField = new GameObject[]
         {
             Create("blocked_field", 5, 4), Create("blocked_field", 6, 4),
             Create("blocked_field", 5, 3), Create("blocked_field", 6, 3)
         };



        //Set all piece positions on the positions board
        for (int i = 0; i < playerWhite.Length; i++)
        {
            SetPosition(playerBlack[i]);
            SetPosition(playerWhite[i]);
        }

        for (int i = 0; i < blockedField.Length; i++)
        {
            SetPosition(blockedField[i]);
        };
    }

    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>(); //We have access to the GameObject, we need the script
        cm.name = name; //This is a built in variable that Unity has, so we did not have to declare it before
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate(); //It has everything set up so it can now Activate()
        return obj;
    }

    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        //Overwrites either empty space or whatever was there
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
        NextTurn();
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }
    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    public bool IsGameOver()
    {
        return gameOver;
    }

    public void NextTurn()
    {
        if (currentPlayer == "white")
        {
            currentPlayer = "black";
        }
        else
        {
            currentPlayer = "white";
        }
    }

    public void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            //Using UnityEngine.SceneManagement is needed here
            SceneManager.LoadScene("Game"); //Restarts the game by loading the scene over again
        }
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;
    }
}
