using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    public GameObject controller;

    // Dette er den reference, der bruges til at vide, hvilken slags skakbrik, der trykkes p�.
    GameObject reference = null;

    // Disse variabler er positions-variabler.
    int matrixX;
    int matrixY;

    // er denne false, er det bare et normalt tr�k, er den true er det et "attack."
    public bool attack = false;

    // Dette er den funktion, der g�r, at n�r der skal spawnes en "attackmoveplate", s� skiftes farven p� "moveplaten" til r�d.

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

        // Skulle en skakbrik v�re blevet dr�bt, vil den her blive fjernet.
        if (attack)
        {
            GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);

            if (cp.name == "white_king") controller.GetComponent<Game>().Winner("black");
            if (cp.name == "black_king") controller.GetComponent<Game>().Winner("white");

            Destroy(cp);
        }

        // S�tte den brik, der lige er rykkets gamle position til at v�re tom.
        controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(),
            reference.GetComponent<Chessman>().GetYBoard());

        // Rykker den brik, der skal rykkes til en ny position rent talm�ssigt.
        reference.GetComponent<Chessman>().SetXBoard(matrixX);
        reference.GetComponent<Chessman>().SetYBoard(matrixY);
        reference.GetComponent<Chessman>().SetCoords();

        // Opdaterer hele pladen s�ledes den f�lger den matrix, der opdateres ovenover.
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
