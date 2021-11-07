using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chessman : MonoBehaviour
{
    //References to objects in our Unity Scene
    public GameObject controller;
    public GameObject movePlate;

    //Position for this Chesspiece on the Board
    //The correct position will be set later
    private int xBoard = -1;
    private int yBoard = -1;

    //Variable for keeping track of the player it belongs to "black" or "white"
    private bool player;
    public bool playerDead = false;

    //References to all the possible Sprites that this Chesspiece could be
    public Sprite black_knight;
    public Sprite white_bishop, white_rook, white_pawn;

    private bool forward;


    public void Activate()
    {
        //Get the game controller
        controller = GameObject.FindGameObjectWithTag("GameController");

        //Take the instantiated location and adjust transform
        SetCoords();

        //Choose correct sprite based on piece's name

        switch (this.name)
        {
            case "player": this.GetComponent<SpriteRenderer>().sprite = black_knight; player = true; break;

            case "goal": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = false; break;

            case "bishopLeft_Top": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = false; break;
            case "bishopLeft_Bottom": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = false; break;
            case "bishopRight_Top": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = false; break;
            case "bishopRight_Bottom": this.GetComponent<SpriteRenderer>().sprite = white_bishop; player = false; break;

            case "rookVertical_BottomToTop": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = false; break;
            case "rookVertical_TopToBottom": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = false; break;
            case "rookHorizontal_LeftToRight": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = false; break;
            case "rookHorizotanl_RightToLeft": this.GetComponent<SpriteRenderer>().sprite = white_rook; player = false; break;

            //case "white_pawn": this.GetComponent<SpriteRenderer>().sprite = white_pawn; player = false; break;
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

        if (controller.GetComponent<Game>().CheckCollision(xBoard, yBoard))
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
        if (controller.GetComponent<Game>().CheckCollision(xBoard, yBoard))
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

    private void OnMouseUp() // MODIFICAR PARA MOSTRAR LAS MOVEPLATES DE TODAS LAS PIEZAS
    {
        if (!controller.GetComponent<Game>().IsGameOver() && player == true)
        {
            //Create player's MovePlates
            LMovePlate();

            Game sc = controller.GetComponent<Game>();
            for (int i = 0; i < sc.pieces.Length; i++)
            {
                InitiateMovePlates(sc.pieces[i]);
            }
        }
    }

    public void DestroyMovePlates()
    {
        //Destroy old MovePlates
        GameObject[] movePlates = GameObject.FindGameObjectsWithTag("MovePlate");
        for (int i = 0; i < movePlates.Length; i++)
        {
            Destroy(movePlates[i]); //Be careful with this function "Destroy" it is asynchronous
        }
    }

    // Poner cosas omg omg omg omg jaja
    void InitiateMovePlates(GameObject piece)
    {
        bool isForward = GetComponent<Chessman>().GetForward();
        string name = GetComponent<Chessman>().name;
        switch (name)
        {
            case "bishopLeft_Top":
                if (isForward)
                {

                }
                else
                {

                }
                break;
            case "bishopLeft_Bottom":
                if (isForward)
                {

                }
                else
                {

                }
                break;
            case "bishopRight_Top":
                if (isForward)
                {

                }
                else
                {

                }
                break;
            case "bishopRight_Bottom":
                if (isForward)
                {

                }
                else
                {

                }
                break;
                /*
                LineMovePlate(1, 1);
                LineMovePlate(1, -1);
                LineMovePlate(-1, 1);
                LineMovePlate(-1, -1);
                */
            case "rookVertical_BottomToTop":
                if (isForward)
                {

                }
                else
                {

                }
                break;
            case "rookVertical_TopToBottom":
                if (isForward)
                {

                }
                else
                {

                }
                LineMovePlate(0, 1);
                break;
                LineMovePlate(0, -1);
            case "rookHorizontal_LeftToRight":
                if (isForward)
                {

                }
                else
                {

                }
                LineMovePlate(0, 1);
                break;
                LineMovePlate(1, 0);
            case "rookHorizotanl_RightToLeft":
                if (isForward)
                {

                }
                else
                {

                }
                LineMovePlate(0, 1);
                break;
                LineMovePlate(-1, 0);
                break;
        }
    }

    void LineMovePlate(int xIncrement, int yIncrement)
    {
        Game sc = controller.GetComponent<Game>();

        int x = xBoard + xIncrement;
        int y = yBoard + yIncrement;

        while (sc.PositionOnBoard(x, y) && sc.GetPosition(x, y) == null)
        {
            MovePlateSpawn(x, y);
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

    // Goal may be a King or maybe not
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
        Game sc = controller.GetComponent<Game>();
        if (sc.PositionOnBoard(x, y))
        {
            GameObject cp = sc.GetPosition(x, y);

            if (cp == null)
            {
                MovePlateSpawn(x, y);
            }
            else if (cp.GetComponent<Chessman>().player != player)
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

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.SetReference(gameObject);
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

        MovePlate mpScript = mp.GetComponent<MovePlate>();
        mpScript.attack = true;
        mpScript.SetReference(gameObject);
        mpScript.SetCoords(matrixX, matrixY);
    }
}
