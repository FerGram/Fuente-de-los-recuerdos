using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    //Reference from Unity IDE
    public GameObject chesspiece;

    private GameObject[,] positions = new GameObject[8, 8];
    private GameObject[,] positionsAux = new GameObject[8, 8];

    // Cantidad de enemigos --> seguramente cambiara en cada nivel
    // O puede ser la cantidad de piezas en pantalla --> siendo pieces[0] la pieza del jugador ?¿
    private GameObject playerKnight;
    private GameObject[] pieces = new GameObject[2]; 

    //Game Ending
    private bool gameOver = false;
    private bool win = false;

    public void Start()
    {
        // Creamos las piezas en una posicion del tablero
        pieces = new GameObject[] { Create("white_rook", 0, 0),
            Create("white_bishop", 2, 0)};

        playerKnight = Create("black_knight", 1, 2);

        //Set all piece positions on the positions board
        for (int i = 0; i < pieces.Length; i++)
        {
            SetPosition(pieces[i]);
            SetPositionAux(pieces[i]);
        }
        SetPosition(playerKnight);
        SetPositionAux(playerKnight);
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
    }

    public void SetPositionAux(GameObject obj)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        //Overwrites either empty space or whatever was there
        positionsAux[cm.GetXBoard(), cm.GetYBoard()] = obj;
    }

    public void SetPositionEmpty(int x, int y)
    {
        positions[x, y] = null;
    }

    public void SetPositionEmptyAux(int x, int y)
    {
        positionsAux[x, y] = null;
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

    public bool IsGameOver()
    {
        return gameOver;
    }

    // Seguramente en vez de MoveEnemies es mejor tener una funcion de movimiento para cada tipo de pieza
    // Es decir, una funcion para mover la torre y otra funcion para mover el alfil
    public void MoveEnemies()
    {
        for (int i = 0; i < positions.GetLength(0); i++)
        {
            for (int j = 0; j < positions.GetLength(1); j++)
            {
                if (positions[i,j] != null)
                {
                    GameObject obj = positions[i, j];
                    Chessman cm = obj.GetComponent<Chessman>(); //We have access to the GameObject, we need the script

                    int boardX = cm.GetXBoard();
                    int boardY = cm.GetYBoard();

                    //Set the Chesspiece's original location to be empty

                    SetPositionEmpty(boardX, boardY);
                    SetPositionEmptyAux(boardX, boardY);

                    if (cm.name == "white_rook")
                    {
                        Debug.Log("Rook is about to move");
                        MoveRook(obj, true);
                    }
                    else if (cm.name == "white_bishop")
                    {
                        MoveBishop(obj, false);
                    }

                }
            }

        }

        PositionsAuxIntoPosition();
    }

    void PositionsAuxIntoPosition()
    {
        for (int i = 0; i < positions.GetLength(0); i++)
        {
            for (int j = 0; j < positions.GetLength(1); j++)
            {
                if (positions[i,j] != positionsAux[i,j])
                {
                    positions[i, j] = positionsAux[i, j];
                }
            }
        }
    }

    int DiagonalLimit(Chessman cm, bool left)
    {
        int x = cm.GetXBoard();
        int y = cm.GetYBoard();

        while(true)
        {
            if (x == 7 || y == 7) break;
            x += 1;
            y += 1;
        }


    }

    // PENSAR EN EL LIMITE DEL MOVIMIENTO DIAGONAL DEL ALFIL, tanto en la funcion como en el for de MoveBishop()

    void MoveBishop(GameObject obj, bool left)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        if (cm.GetForward() == true)
        {
            Debug.Log("Goes Forward");
            if (left)
            {
                Debug.Log("Goes to the Left 1");
                for (int i = cm.GetYBoard() + 1; i <= DiagonalLimit(cm, left); i++)
                {
                    cm.SetXBoard(cm.GetXBoard());
                    cm.SetYBoard(i);
                    cm.SetCoords();
                    if (CheckCollision(cm.GetXBoard(), i)) break;
                }
            }
            else
            {
                Debug.Log("Goes to the Right 1");
                for (int i = cm.GetXBoard() + 1; i <= DiagonalLimit(cm, left); i++)
                {
                    cm.SetXBoard(i);
                    cm.SetYBoard(cm.GetYBoard());
                    cm.SetCoords();
                    if (CheckCollision(i, cm.GetYBoard())) break;
                }
            }

            cm.SetForward(false);
        }
        else // !cm.GetForward()
        {
            Debug.Log("Goes Backward");
            if (left)
            {
                Debug.Log("Goes to the Left 2");
                for (int i = cm.GetYBoard() - 1; i >= DiagonalLimit(cm, left); i--)
                {
                    cm.SetXBoard(cm.GetXBoard());
                    cm.SetYBoard(i);
                    cm.SetCoords();
                    if (CheckCollision(cm.GetXBoard(), i)) break;
                }
            }
            else
            {
                Debug.Log("Goes to the Right 2");
                for (int i = cm.GetXBoard() - 1; i >= DiagonalLimit(cm, left); i--)
                {
                    cm.SetXBoard(i);
                    cm.SetYBoard(cm.GetYBoard());
                    cm.SetCoords();
                    if (CheckCollision(i, cm.GetYBoard())) break;
                }
            }

            cm.SetForward(true);
        }
        Debug.Log("Pasa por SetPositionAux(obj)");
        SetPositionAux(obj);
    }

    void MoveRook(GameObject obj, bool vertical)
    {
        Chessman cm = obj.GetComponent<Chessman>();

        if (cm.GetForward() == true)
        {
            Debug.Log("Goes Forward");
            if (vertical)
            {
                Debug.Log("Goes Vertical 1");
                for (int i = cm.GetYBoard() + 1; i <= 7; i++)
                {
                    cm.SetXBoard(cm.GetXBoard());
                    cm.SetYBoard(i);
                    cm.SetCoords();
                    if (CheckCollision(cm.GetXBoard(), i)) break;
                }  
            }
            else
            {
                for (int i = cm.GetXBoard() + 1; i <= 7; i++)
                {
                    cm.SetXBoard(i);
                    cm.SetYBoard(cm.GetYBoard());
                    cm.SetCoords();
                    if (CheckCollision(i, cm.GetYBoard())) break;
                }
            }

            cm.SetForward(false);
        }
        else // !cm.GetForward()
        {
            Debug.Log("Goes Backward");
            if (vertical)
            {
                Debug.Log("Goes Vertical 2");
                for (int i = cm.GetYBoard() - 1; i >= 0; i--)
                {
                    cm.SetXBoard(cm.GetXBoard());
                    cm.SetYBoard(i);
                    cm.SetCoords();
                    if (CheckCollision(cm.GetXBoard(), i)) break;
                }
            }
            else
            {
                for (int i = cm.GetXBoard() - 1; i >= 0; i--)
                {
                    cm.SetXBoard(i);
                    cm.SetYBoard(cm.GetYBoard());
                    cm.SetCoords();
                    if (CheckCollision(i, cm.GetYBoard())) break;
                }
            }

            cm.SetForward(true);
        }
        Debug.Log("Pasa por SetPositionAux(obj)");
        SetPositionAux(obj);
    }

    bool CheckCollision(int x, int y)
    {
        Chessman player = playerKnight.GetComponent<Chessman>();
        int playerX = player.GetXBoard();
        int playerY = player.GetYBoard();

        if (x == playerX && y == playerY )
        {
            Destroy(GameObject.Find("black_knight"));
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Update()
    {
        if (gameOver == true && Input.GetMouseButtonDown(0))
        {
            gameOver = false;

            if (win)
            {
                // next level
            }
            else
            {
                // repeat level
            }
        }
    }
    
    public void Winner(string text)
    {
        gameOver = true;

        //Using UnityEngine.UI is needed here
        GameObject.FindGameObjectWithTag("NextLevelTextChess").GetComponent<Text>().enabled = true;
        GameObject.FindGameObjectWithTag("NextLevelTextChess").GetComponent<Text>().text = "Click for the next level";
    }
}
