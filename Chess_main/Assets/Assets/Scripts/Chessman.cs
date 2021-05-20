using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    // References
    public GameObject controller;
    public GameObject movePlate;

    // Default position på boardet
    private int xBoard = -1;
    private int yBoard = -1;

    // Storing variables
    private string player;

    // Referencer til de forskellige sprites, der benyttes
    public Sprite black_queen, black_king, black_knight, black_pawn, black_bishop, black_rook, black_guard, black_chicken;
    public Sprite white_queen, white_king, white_knight, white_pawn, white_bishop, white_rook, white_guard, white_chicken;
    public Sprite blocked_field;


    /*
    Denne funktion aktiverer alle brikkerne således at vi kan placere dem på boardet. Den definerer også, hvilket
    hold de forskellige brikker er på.
    */
    public void Activate()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");
        SetCoords();

        switch (this.name)
        {
            // Black sprites
            case "black_queen": this.GetComponent<SpriteRenderer>().sprite = black_queen; player = "black"; break;
            case "black_king": this.GetComponent<SpriteRenderer>().sprite = black_king; player = "black"; break;
            case "black_knight": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = "black"; break;
            case "black_pawn": this.GetComponent<SpriteRenderer>().sprite = black_pawn; player = "black"; break;
            case "black_bishop": this.GetComponent<SpriteRenderer>().sprite = black_bishop; player = "black"; break;
            case "black_rook": this.GetComponent<SpriteRenderer>().sprite = black_rook; player = "black"; break;
            case "black_guard": this.GetComponent<SpriteRenderer>().sprite = black_guard; player = "black"; break;
            case "black_chicken": this.GetComponent<SpriteRenderer>().sprite = black_chicken; player = "black"; break;

            // White sprites
            case "white_queen": this.GetComponent<SpriteRenderer>().sprite = white_queen; player = "white"; break;
            case "white_king": this.GetComponent<SpriteRenderer>().sprite = white_king; player = "white"; break;
            case "white_knight": this.GetComponent<SpriteRenderer>().sprite = white_knight; player = "white"; break;
            case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = "white"; break;
            case "white_bishop": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = "white"; break;
            case "white_rook": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = "white"; break;
            case "white_guard": this.GetComponent<SpriteRenderer>().sprite = white_guard; player = "white"; break;
            case "white_chicken": this.GetComponent<SpriteRenderer>().sprite = white_chicken; player = "white"; break;

            // Blocked field sprites
            case "blocked_field": this.GetComponent<SpriteRenderer>().sprite = blocked_field; player = "blocked"; break;
        }
    }

    /*
    Denne funktion bruges når vi skal placere vores sprites på boardet. Da spritsne bliver placeret via matematik,
    og boardet ikke er helt præcist ift. decimaler, skal vi bruge denne funktion til matematikken bag positionerne.   
    */
    public void SetCoords()
    {
        float x = xBoard;
        float y = yBoard;
        
        x *= 1.105f;
        y *= 1.105f;

        x += -6.08f;
        y += -3.87f;

        this.transform.position = new Vector3(x, y, -1.0f);
    }

    /*
    De næste 4 funktioner hjælper egentlig bare til med placering af brikkerne og bliver brugt rundt omkring til fx.
    at finde ud af, hvilken position en brik har, samt at definere, hvor brikkerne er placeret.    
    */
    public int GetXBoard()
    {
        return xBoard;
    }
    public int GetYBoard()
    {
        return yBoard;
    }
    public void SetXBoard(int x)
    {
        xBoard = x;
    }
    public void SetYBoard(int y)
    {
        yBoard = y;
    }

    // Dette er funktionen, der køres når man trykker på pladen.
    private void OnMouseUp()
    {
        DestroyMovePlates();

        InitiateMovePlates();
    }

    // Denne funktion fjerner de "moveplates", der bliver lavet.
    public void DestroyMovePlates()
    {
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    /*
    Denne funktion laver de "moveplates", der laves når en brik bliver trykket på. Dette sker via en "switch-case", hvor
    alle objekterne (brikkerne) har et navn, og udfra det navn, køres der nogle yderligere funktioner.
    Alle de yderligere funktioner er baseret på matematik, der forklares ved de forskellige funktioner.
    */
    public void InitiateMovePlates()
    {
        switch (this.name)
        {
            case "black_queen":
            case "white_queen":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(1, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(1, -1);
                break;
            case "black_knight":
            case "white_knight":
                LMovePlate();
                break;
            case "black_bishop":
            case "white_bishop":
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, -1);
                LineMovePlate(-1, 1);
                break;
            case "black_king":
            case "white_king":
                SurroundMovePlate();
                break;
            case "black_rook":
            case "white_rook":
                LineMovePlate(1, 0);
                LineMovePlate(0, 1);
                LineMovePlate(-1, 0);
                LineMovePlate(0, -1);
                break;
            case "black_pawn":
                PawnMovePlate(xBoard, yBoard - 1);
                break;
            case "white_pawn":
                PawnMovePlate(xBoard, yBoard + 1);
                break;
            case "black_guard":
                GuardMovePlate("black");
                break;
            case "white_guard":
                GuardMovePlate("white");
                break;
            case "white_chicken":
                ChickenMovePlate("white");
                break;
            case "black_chicken":
                ChickenMovePlate("black");
                break;
        }
    }

    /*
    LineMovePlate er den funktion der bruges til vores rook og dronning. Måden virker på er, at når man kalder funktionen,
    definerer man en stigning den skal have hver brik. Kalder man LineMovePlate(1,0), vil den altså stige 1 gang på x-aksen
    og 0 gange på y-aksen indtil den rammer modstand. 
    Den tjekker altid i loopet om den rammer en fjende, hvor den er på pladen eller om den rammer et blocked field.
    Gør den dette, vil loopet stoppe.
    */
    public void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x,y) && sc.GetPosition(x,y) == null)
        {
            MovePlateSpawn(x, y);
            x += xIncrement;
            y += yIncrement;
        }

        if (sc.PositionOnBoard(x, y) && sc.GetPosition(x,y).GetComponent<Chessman>().player != player && sc.GetPosition(x, y).GetComponent<Chessman>().player != "blocked")
        {
            MovePlateAttackSpawn(x, y);
        }
    }

    /* 
    GuardMovePlate er vores egen funktion, der egentlig bare skal vide, hvilken farve brik, der spiller.
    Grunden til dette er, at de kun kan rykke sig på en bestemt måde, afhængig af farve. 
    Når farven er valgt, så kører den en anden funktion, der egentlig - kort sagt - kan lave en "moveplat" udfra,
    hvor man står henne (xBoard og yBoard).
    */
    public void GuardMovePlate(string playerColor)
    {
        if (playerColor == "white")
        {
            PointMovePlate(xBoard + 1, yBoard + 1);
            PointMovePlate(xBoard - 1, yBoard + 1);
            PointMovePlate(xBoard, yBoard - 1);
        }
        else
        {
            PointMovePlate(xBoard + 1, yBoard - 1);
            PointMovePlate(xBoard - 1, yBoard - 1);
            PointMovePlate(xBoard, yBoard + 1);
        }
    }

    /*
    ChickenMovePlate er igen vores egen funktion, der gør brug af PointMovePlate og LineMovePlate til at rykke.
    Den gør også brug af, hvilken farve spiller man har, så vi ved den rykker rigtigt.     
    */
    public void ChickenMovePlate(string playerColor)
    {
        if (playerColor == "white")
        {
            PointMovePlate(xBoard + 1, yBoard);
            PointMovePlate(xBoard - 1, yBoard);
            LineMovePlate(0, 1);
        }
        else
        {
            PointMovePlate(xBoard + 1, yBoard);
            PointMovePlate(xBoard - 1, yBoard);
            LineMovePlate(0, -1);
        }
    }

    /*
    LMovePlate er brugt til hesten (knight), der kan rykke på en helt besmemt måde som man også kan se her.
    Det er egentlig predefineret altid, hvor den kan rykke hen og gør brug af PointMovePlate.
    */
    public void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 2, yBoard + 1);

        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard - 2, yBoard - 1);
        PointMovePlate(xBoard - 2, yBoard + 1);
    }

    // Denne funktion bruges af kongen og er, ligesom hesten (knight), predefineret.
    public void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard - 1, yBoard);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }

    /*
    Denne funktion bruges rigtig mange steder, og er egentlig bare matematikken bag, hvor der skal rykkes hen.
    Den tager egentlig bare en position på boardet og tjekker, om der allerede står en spiller, der ikke er en selv,
    så tjekker den om det heller ikke er et "blocked field", og hvis det ikke er det, så spawner den en "AttackMovePlate",
    skulle de bare være et ledigt sted uden problemer, spawner den en normal "moveplate".
    */
    public void PointMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MovePlateSpawn(x, y);
            } 
            else if (cp.GetComponent<Chessman>().player != player && cp.GetComponent<Chessman>().player != "blocked")
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    /*
    Denne funktion bruges af alle bønderne (pawn) og er lidt speciel da den skal kunne rykke bestemt ift, hvor der er
    en fjende henne. Derfor står den for sig selv og er lidt mere avanceret end de andre.     
    */
    public void PawnMovePlate(int x, int y)
    {
        Game sc = controller.GetComponent<Game>();
        GameObject cp = sc.GetPosition(x, y);

        if (sc.PositionOnBoard(x, y))
        {
            if (sc.GetPosition(x, y) == null)
            {
                MovePlateSpawn(x, y);
            }

            if (sc.PositionOnBoard(x +1, y) 
                && sc.GetPosition(x+1,y) != null 
                && sc.GetPosition(x+1,y).GetComponent<Chessman>().player != player
                && cp.GetComponent<Chessman>().player != "blocked")
            {
                MovePlateAttackSpawn(x + 1, y);
            }

            if (sc.PositionOnBoard(x - 1, y)
                && sc.GetPosition(x + 1, y) != null
                && sc.GetPosition(x + 1, y).GetComponent<Chessman>().player != player
                && cp.GetComponent<Chessman>().player != "blocked")
            {
                MovePlateAttackSpawn(x - 1, y);
            }

        }
    }


    /*
    Denne funktion spawner "moveplatesne". Den bruger lidt matematik i starten for at være sikker på de er placeret rigtigt
    i felterne. Derefter placerer den moveplaten og gør det til et objekt.
    */
    public void MovePlateSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 1.105f;
        y *= 1.105f;

        x += -6.08f;
        y += -3.87f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }


    /*
    Denne funktion bruges når der skal lave en "attackmoveplate" og er det præcist samme som ovenover, vi ændrer
    bare farven på den moveplate, der spawner.
    */
    public void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        float x = matrixX;
        float y = matrixY;

        x *= 1.105f;
        y *= 1.105f;

        x += -6.08f;
        y += -3.85f;

        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}
