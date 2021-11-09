using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    //Some functions will need reference to the controller
    public GameObject controller;

    //The Chesspiece that was tapped to create this MovePlate
    GameObject reference = null;

    //Location on the board
    int matrixX;
    int matrixY;

    //false: movement, true: attacking
    public bool attack = false;

    // CAMBIARLO, no funciona ya que la referencia siempre sera el jugador (el caballo)
    public void Start()
    {
        Chessman piece = reference.GetComponent<Chessman>();

        if (attack)
        {
            //Set to red
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else if (!piece.GetPlayer())
        {
            //Set to gray
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }

    }

    public void OnMouseUp()
    {
        if (reference.GetComponent<Chessman>().GetPlayer())
        {
            controller = GameObject.FindGameObjectWithTag("GameController");

            //Destroy the victim Chesspiece
            if (attack)
            {
                GameObject cp = controller.GetComponent<Game>().GetPosition(matrixX, matrixY);
                Destroy(cp);
            }

            //Set the Chesspiece's original location to be empty
            controller.GetComponent<Game>().SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(),
                reference.GetComponent<Chessman>().GetYBoard());

            //Move reference chess piece to this position
            reference.GetComponent<Chessman>().SetXBoard(matrixX);
            reference.GetComponent<Chessman>().SetYBoard(matrixY);
            reference.GetComponent<Chessman>().SetCoords();

            //Update the matrix
            controller.GetComponent<Game>().SetPosition(reference);

            //Destroy the move plates including self
            reference.GetComponent<Chessman>().DestroyMovePlates();

            if (reference.GetComponent<Chessman>().GetXBoard() == controller.GetComponent<Game>().GoalPosX &&
                reference.GetComponent<Chessman>().GetYBoard() == controller.GetComponent<Game>().GoalPosY)
            {
                controller.GetComponent<Game>().win = true;
            }

            //Move all the other pieces
            controller.GetComponent<Game>().MoveEnemies();
        }

    }

    public void SetCoords(int x, int y)
    {
        matrixX = x;
        matrixY = y;
    }

    public void SetReference(GameObject obj)
    {
        reference = obj;
    }

    public GameObject GetReference()
    {
        return reference;
    }
}
