using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlate : MonoBehaviour
{
    //Some functions will need reference to the controller
    public ChessGame controllerS;

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
		controllerS = GameObject.FindGameObjectWithTag("GameController").GetComponent<ChessGame>();
		if (attack)
        {
            //Set to red
            gameObject.transform.localScale *= 2;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
        else if (!piece.GetPlayer())
        {
            //Change Color
            gameObject.transform.localScale *= 2;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.8332132f, 0.0f, 0.8396226f, 1.0f);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

	private void Update()
	{
		
	}

	public void OnClickOver()
    {
        if (reference.GetComponent<Chessman>().GetPlayer())
        {
            controllerS.canSpawnMovePlates = true;
            //Destroy the victim Chesspiece
            if (attack)
            {
                GameObject cp = controllerS.GetPosition(matrixX, matrixY);
                Destroy(cp);
            }

            //Set the Chesspiece's original location to be empty
            controllerS.SetPositionEmpty(reference.GetComponent<Chessman>().GetXBoard(),
                reference.GetComponent<Chessman>().GetYBoard());

            //Move reference chess piece to this position
            reference.GetComponent<Chessman>().SetXBoard(matrixX);
            reference.GetComponent<Chessman>().SetYBoard(matrixY);
            reference.GetComponent<Chessman>().SetCoords();

            //Update the matrix
            controllerS.SetPosition(reference);

            //Destroy the move plates including self
            reference.GetComponent<Chessman>().DestroyMovePlates();

            if (reference.GetComponent<Chessman>().GetXBoard() == controllerS.GoalPosX &&
                reference.GetComponent<Chessman>().GetYBoard() == controllerS.GoalPosY)
            {
                controllerS.win = true;
            }

            //Move all the other pieces
            controllerS.MoveEnemies();
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
