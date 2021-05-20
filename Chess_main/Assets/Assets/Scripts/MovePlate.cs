using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    // Dette er den reference, der bruges til at vide, hvilken slags skakbrik, der trykkes på.
    GameObject reference = null;

    // Disse variabler er positions-variabler.
    int matrixX;
    int matrixY;

    // er denne false, er det bare et normalt træk, er den true er det et "attack."
    public bool attack = false;

    // Dette er den funktion, der gør, at når der skal spawnes en "attackmoveplate", så skiftes farven på "moveplaten" til rød.

    public void Start()
    {
        if (attack)
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }

    public void OnMouseUp()
    {
        controller = GameObject.FindGameObjectWithTag("GameController");

        // Skulle en skakbrik være blevet dræbt, vil den her blive fjernet.
        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            if (cp.name == "white_king") controller.GetComponent<Game>().Winner("black");
            if (cp.name == "black_king") controller.GetComponent<Game>().Winner("white");

            Destroy(cp);
        }

        // Sætte den brik, der lige er rykkets gamle position til at være tom.
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(),
            reference.GetComponent<Chessman>().GetYBoard());

        // Rykker den brik, der skal rykkes til en ny position rent talmæssigt.
        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        // Opdaterer hele pladen således den følger den matrix, der opdateres ovenover.
        controller.GetComponent<Game>().SetPosition(reference);

        //Switch Current Player
        controller.GetComponent<Game>().NextTurn();

        // Fjerner de moveplates, der var spawnet.
        reference.GetComponent<Chessman>().DestroyMovePlates();
    }


    // Reference til, hvor en brik er placeret.
    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    // Reference
    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    // Mere reference til debugging.
    public GameObject GetReference()
    {
        return reference;
    }
}
