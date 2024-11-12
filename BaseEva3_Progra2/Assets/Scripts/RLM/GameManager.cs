using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GridPieceType[,] gridTypes;
    public int sizeX, sizeY;
    public GameObject wallPrefab;


    void FillCellTypes()
    {
        for (int fila = 0; fila < gridTypes.GetLength(0); fila++)
        {
            for (int columna = 0; columna < gridTypes.GetLength(1); columna++)
            {
                gridTypes[fila, columna] = (GridPieceType)Random.Range(0, 4);
                if (gridTypes[fila, columna] == GridPieceType.Wall)
                {
                    GameObject newWall = Instantiate(wallPrefab);
                    newWall.transform.position = new Vector3(fila, columna, 0);
                }
            }
        }
    }

    public GridPieceType GetValueCell(int posX, int posY)
    {
        return gridTypes[posX, posY];
    }

    public bool IsInsideArray(int posX, int posY)
    {
        if (posX > sizeX - 1)
        {
            return false;
        }
        else if (posY > sizeY - 1)
        {
            return false;
        }
        else if (posX < 0)
        {
            return false;
        }
        else if (posY < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gridTypes = new GridPieceType[sizeX, sizeY];
        FillCellTypes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
