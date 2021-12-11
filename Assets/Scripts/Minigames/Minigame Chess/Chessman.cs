using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    //References to objects in our Unity Scene
    public ChessGame controllerS;
    public GameObject movePlate;

    //Position for this Chesspiece on the Board
    //The correct position will be set later
    private int xBoard = -1;
    private int yBoard = -1;

    //Variable for keeping track of the player it belongs to "black" or "white"
    public bool player;
    public bool playerDead = false;

    //References to all the possible Sprites that this Chesspiece could be
    public Sprite black_knight;
    public Sprite white_bishop, white_rook, goal;

    private bool forward;


    public void Activate()
    {
		//Get the game controller
		controllerS = GameObject.FindGameObjectWithTag("GameController").GetComponent<ChessGame>();
        //Take the instantiated location and adjust transform
        SetCoords();

        //Choose correct sprite based on piece's name

        switch (this.name)
        {
            case "player": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = true; break;

            case "goal": this.GetComponent<SpriteRenderer>().sprite = goal; player = false; break;

            case "bishopLeft": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = false; break;
            case "bishopRight": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = false; break;

            case "rookVertical_BottomToTop": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = false; break;
            case "rookVertical_TopToBottom": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = false; break;
            case "rookHorizontal_LeftToRight": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = false; break;
            case "rookHorizotanl_RightToLeft": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = false; break;

        }
    }

	private void Update()
	{
		if (player && Input.GetMouseButtonDown(0))
		{
			////controllerS.minigameCam.ScreenToWorldPoint(
			//Vector3 firstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			//Vector3 viewportPos = Camera.main.WorldToViewportPoint(firstPos);

			//Vector3 finalPos = controllerS.minigameCam.ViewportToWorldPoint(viewportPos);
			//finalPos.z = 0;
			//RaycastHit2D hit = Physics2D.Raycast(finalPos, Vector2.zero, Mathf.Infinity, controllerS.layerMask);

			////Debug.Log("Final pos: " + finalPos);
			//if (hit.collider != null)
			//{
			//	//Debug.Log("Target Position: " + hit.collider.gameObject.transform.position);
			//	Debug.Log("Target GameObject: " + hit.collider.gameObject.name);
			//	OnClickOver();
			//}

			
		}
	}

	// Prepares SetCoords variables
	Vector2 InitSetCoords()
    {
        //Get the board value in order to convert to xy coords
        float x = xBoard;
        float y = yBoard;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        return new Vector2(x, y);
    }

    public void SetCoords()
    {
        Vector2 pos = InitSetCoords();

        this.transform.position = new Vector3(pos.x, pos.y, -1.0f);
    }

    public void SetRookCoordsCoroutine()
    {
        Vector2 pos = InitSetCoords();

        StartCoroutine(SmoothRookMovement(pos.x, pos.y));
    }

    public void SetBishopCoordsCoroutine()
    {
        Vector2 pos = InitSetCoords();

        StartCoroutine(SmoothBishopMovement(pos.x, pos.y));
    }

    IEnumerator SmoothRookMovement(float nextX, float nextY)
    {
        // Actual position
        float x = this.transform.position.x;
        float y = this.transform.position.y;

        // Distance between actual positions and next positions
        float distanceX = Mathf.Abs(x - nextX);
        float distanceY = Mathf.Abs(y - nextY);

        // Just a number
        int n = 4;

        float distance;
        if (distanceX > distanceY)
        {
            if (x > nextX) distance = -distanceX / n;
            else distance = distanceX / n;
            for (int i = 0; i <= n; i++)
            {
                yield return new WaitForSeconds(.05f);
                this.transform.position = new Vector3(x, y, -1.0f);
                x += distance;
            }
        }
        else
        {
            if (y > nextX) distance = -distanceY / n;
            else distance = distanceY / n;

            for (int i = 0; i <= n; i++)
            {
                yield return new WaitForSeconds(.05f);
                this.transform.position = new Vector3(x, y, -1.0f);

                y += distance;
            }
        }

        if (controllerS.CheckCollision(xBoard, yBoard))
        {
            Destroy(GameObject.Find("player"));
        }
    }

    IEnumerator SmoothBishopMovement(float nextX, float nextY)
    {
        // Actual position
        float x = this.transform.position.x;
        float y = this.transform.position.y;

        // Distance between actual positions and next positions
        float distanceX = Mathf.Abs(x - nextX);
        float distanceY = Mathf.Abs(y - nextY);

        // Just a number
        int n = 4;

        if (x > nextX) distanceX = -distanceX / n;
        else distanceX = distanceX / n;

        if (y > nextY) distanceY = -distanceY / n;
        else distanceY = distanceY / n;

        for (int i = 0; i <= n; i++)
        {
            yield return new WaitForSeconds(.05f);
            this.transform.position = new Vector3(x, y, -1.0f);
            x += distanceX;
            y += distanceY;
        }
        
        // Destroys the player once the piece gets to the same player's position
        if (controllerS.CheckCollision(xBoard, yBoard))
        {
            Destroy(GameObject.Find("player"));
        }
    }


    public bool GetPlayer()
    {
        return player;
    }

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

    public bool GetForward()
    {
        return forward;
    }

    public void SetForward(bool value)
    {
        forward = value;
    }

    public void OnClickOver()
    {
        if (!controllerS.IsGameOver() && player == true)
        {
            ChessGame sc = controllerS;
            for (int i = 0; i < sc.pieces.Length; i++)
            {
                if(sc.pieces[i] != null) InitiateMovePlates(sc.pieces[i]);
            }

            //Create player's MovePlates
            LMovePlate();
        }
    }

    public void DestroyMovePlates()
    {
        //Destroy old MovePlates
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]);
        }
    }

    void InitiateMovePlates(GameObject piece)
    {
        switch(piece.name)
        {
            case "bishopLeft":
                if (piece.GetComponent<Chessman>().forward)
                {
                    LineMovePlate(-1, 1, piece);
                }
                else
                {
                    LineMovePlate(1, -1, piece);

                }
                break;
            case "bishopRight":
                if (piece.GetComponent<Chessman>().forward)
                {
                    LineMovePlate(1, 1, piece);
                }
                else
                {
                    LineMovePlate(-1, -1, piece);
                }
                break;
            case "rookVertical_BottomToTop":
                if (piece.GetComponent<Chessman>().forward)
                {
                    LineMovePlate(0, 1, piece);
                }
                else
                {
                    LineMovePlate(0, -1, piece);
                }
                break;
            case "rookVertical_TopToBottom":
                if (piece.GetComponent<Chessman>().forward)
                {
                    LineMovePlate(0, -1, piece);
                }
                else
                {
                    LineMovePlate(0, 1, piece);
                }
                break;
            case "rookHorizontal_LeftToRight":
                if (piece.GetComponent<Chessman>().forward)
                {
                    LineMovePlate(1, 0, piece);
                }
                else
                {
                    LineMovePlate(-1, 0, piece);
                }
                break;
            case "rookHorizotanl_RightToLeft":
                if (piece.GetComponent<Chessman>().forward)
                {
                    LineMovePlate(-1, 0, piece);
                }
                else
                {
                    LineMovePlate(1, 0, piece);
                }
                break;
        }
    }

    void LineMovePlate(int xIncrement, int yIncrement, GameObject piece)
    {
        ChessGame sc = controllerS;

        int x = piece.GetComponent<Chessman>().xBoard + xIncrement;
        int y = piece.GetComponent<Chessman>().yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y) && (sc.GetPosition(x, y) == null || sc.GetPosition(x,y).GetComponent<Chessman>().player))
        {
            MovePlateSpawn(x, y, piece);
            x += xIncrement;
            y += yIncrement;
        }
    }

    void LMovePlate()
    {
        PointMovePlate(xBoard + 1, yBoard + 2);
        PointMovePlate(xBoard - 1, yBoard + 2);
        PointMovePlate(xBoard + 2, yBoard + 1);
        PointMovePlate(xBoard + 2, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard - 2);
        PointMovePlate(xBoard - 1, yBoard - 2);
        PointMovePlate(xBoard - 2, yBoard + 1);
        PointMovePlate(xBoard - 2, yBoard - 1);
    }

    void SurroundMovePlate()
    {
        PointMovePlate(xBoard, yBoard + 1);
        PointMovePlate(xBoard, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 0);
        PointMovePlate(xBoard - 1, yBoard - 1);
        PointMovePlate(xBoard - 1, yBoard + 1);
        PointMovePlate(xBoard + 1, yBoard + 0);
        PointMovePlate(xBoard + 1, yBoard - 1);
        PointMovePlate(xBoard + 1, yBoard + 1);
    }

    void PointMovePlate(int x, int y)
    {
        ChessGame sc = controllerS;
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MovePlateSpawn(x, y);
            }
            else
            {
                MovePlateAttackSpawn(x, y);
            }
        }
    }

    void MovePlateSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
		mp.layer = controllerS.gameObject.layer;
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }

    void MovePlateSpawn(int matrixX, int matrixY, GameObject piece)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
		mp.layer = controllerS.gameObject.layer;
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(piece);
        mpScript.SetCoords(matrixX, matrixY);
    }

    void MovePlateAttackSpawn(int matrixX, int matrixY)
    {
        //Get the board value in order to convert to xy coords
        float x = matrixX;
        float y = matrixY;

        //Adjust by variable offset
        x *= 0.66f;
        y *= 0.66f;

        //Add constants (pos 0,0)
        x += -2.3f;
        y += -2.3f;

        //Set actual unity values
        GameObject mp = Instantiate(movePlate, new Vector3(x, y, -3.0f), Quaternion.identity);
		mp.layer = controllerS.gameObject.layer;
        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}
