using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    // Reference
    public GameObject chesspiece;

    /*
    Matixer for at vide, hvor hver brik er placeret på boardet.
    Derudover er hver spillers brikker blevet separeret således at det det er nemmer at holde styr på.
    */
    private GameObject[,] positions = new GameObject[12, 8];
    private GameObject[] playerBlack = new GameObject[16];
    private GameObject[] playerWhite = new GameObject[16];
    private GameObject[] blockedField = new GameObject[4];

    // (virker ikke), men skulle bruges til at vide, hvem der måtte rykke.
    private string currentPlayer = "white";

    // Game ending
    private bool gameOver = false;

    // Unity kalder denne funktion når spillet starter, hvilket spawner alle brikkerne ind.
    public void Start()
    {
         playerWhite = new GameObject[] { Create("white_pawn", 0, 1), Create("white_pawn", 1, 1),
             Create("white_pawn", 2, 1), Create("white_guard", 3, 1), Create("white_pawn", 4, 1), Create("white_guard", 5, 1),
             Create("white_guard", 6, 1), Create("white_pawn", 7, 1), Create("white_guard", 8, 1), Create("white_pawn", 9, 1),
             Create("white_pawn", 10, 1), Create("white_pawn", 11, 1),

             Create("white_chicken", 0, 0), Create("white_rook", 1, 0), Create("white_chicken", 2, 0), Create("white_bishop", 3, 0),
             Create("white_knight", 4, 0), Create("white_queen", 5, 0), Create("white_king", 6, 0), Create("white_knight", 7, 0),
             Create("white_bishop", 8, 0), Create("white_chicken", 9, 0), Create("white_rook", 10, 0), Create("white_chicken", 11, 0),
         };

         playerBlack = new GameObject[] { Create("black_pawn", 0, 6), Create("black_pawn", 1, 6),
             Create("black_pawn", 2, 6), Create("black_guard", 3, 6), Create("black_pawn", 4, 6), Create("black_guard", 5, 6),
             Create("black_guard", 6, 6), Create("black_pawn", 7, 6), Create("black_guard", 8, 6), Create("black_pawn", 9, 6),
             Create("black_pawn", 10, 6), Create("black_pawn", 11, 6),

             Create("black_chicken", 0, 7), Create("black_rook", 1, 7), Create("black_chicken", 2, 7), Create("black_bishop", 3, 7), 
             Create("black_knight", 4, 7), Create("black_queen", 5, 7), Create("black_king", 6, 7), Create("black_knight", 7, 7), 
             Create("black_bishop", 8, 7), Create("black_chicken", 9, 7), Create("black_rook", 10, 7), Create("black_chicken", 11, 7),
         };
         blockedField = new GameObject[]
         {
             Create("blocked_field", 5, 4), Create("blocked_field", 6, 4),
             Create("blocked_field", 3, 3), Create("blocked_field", 3, 4),
             Create("blocked_field", 8, 3), Create("blocked_field", 8, 4),
             Create("blocked_field", 5, 3), Create("blocked_field", 6, 3)
         };


        /*
        Placerer alle brikkerne på pladen rent talmæssigt inde i dataet, hvorefter matrixen opdateres så de står efter,
        hvad der står i koden.
        */
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

    // Denne funktion bruges til at spawne hver skakbrik, og bruges rigtig mange gange ovenover, når spillet starter.
    public GameObject Create(string name, int x, int y)
    {
        GameObject obj = Instantiate(chesspiece, new Vector3(0, 0, -1), Quaternion.identity);
        Chessman cm = obj.GetComponent<Chessman>();
        cm.name = name;
        cm.SetXBoard(x);
        cm.SetYBoard(y);
        cm.Activate();
        return obj;
    }

    /*
    Denne funktion bruges til at placere brikkerne rigtigt på pladen i dataet, og derefter skulle den gerne skifte tur.   
    */
    public void SetPosition(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        //Overwrites either empty space or whatever was there
        positions[cm.GetXBoard(), cm.GetYBoard()] = obj;
        NextTurn();
    }

    // Sætter en valgt position til tom.
    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    // En funktion, der bruges til at finde ud af, om der står en brik på et specifikt. felt.
    public GameObject GetPosition(int x, int y)
    {
        return positions[x, y];
    }

    // Tjekker om en brik, eller "moveplate", er på boardet.
    public bool PositionOnBoard(int x, int y)
    {
        if (x < 0 || y < 0 || x >= positions.GetLength(0) || y >= positions.GetLength(1)) return false;
        return true;
    }

    // (virker ikke) finder ud af, hvilken spiller der må rykke lige nu.
    public string GetCurrentPlayer()
    {
        return currentPlayer;
    }

    // Spørger om spillet er slut.
    public bool IsGameOver()
    {
        return gameOver;
    }


    // (virker ikke) skifter tur.
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

    // Skulle spillet være færdigt, så genstarter det.
    public void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            SceneManager.LoadScene("Game");
        }
    }

    public void Winner(string playerWinner)
    {
        gameOver = true;
    }
}
