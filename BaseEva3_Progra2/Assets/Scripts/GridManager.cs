using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    //El arreglo requiere el mismo orden que el enum
    public GameObject[] gridPiecesPrefabs;
    public Vector2Int gridSize;
    public Transform parent;

    public GameObject wallPref;
    public GameObject wallDestructiblePref;
    public GameObject spikePref;
    public GameObject winPref;
    public GameObject keyPref;

    public GridPiece[,] grid;

    GridPiece_Win win;
    GridPiece_Key key;

    private void Awake()
    {
        CreateGrid();
    }

    //Se encarga de crear la grilla
    public void CreateGrid()
    {
        //Inicializo la matriz (arreglo bidimensional) segun el tama�o de la grilla
        grid = new GridPiece[gridSize.x, gridSize.y];


        for (int x = 0; x < gridSize.x; x++)
        {
            for(int z = 0; z < gridSize.y; z++)
            {
                //Obtengo posicion en grilla
                Vector2Int gridPos = new Vector2Int(x, z);
                
                //Segun la posicion devuelvo un tipo de pieza
                GridPieceType gridPieceType = GetPieceType(gridPos);
                
                //Ocupo la posicion en grilla y el tipo de pieza para instancia la pieza
                GridPiece newPiece = CreatePiece(gridPieceType, gridPos);
                newPiece.pos = gridPos;
                //Guardo la pieza creada en la matriz
                grid[x,z] = newPiece;
            }
        }

        key.win = win;
    }

    //Se encarga de instanciar la pieza y setearla
    GridPiece CreatePiece(GridPieceType pieceType, Vector2Int gridPos)
    {
        GridPiece piece = null;
        Vector3 position = new Vector3(gridPos.x,-0.5f, gridPos.y);
        GameObject pref = gridPiecesPrefabs[(int)pieceType];

        GameObject pieceObj = Instantiate(pref, position, Quaternion.identity,parent);
        
        switch (pieceType)
        {
            case GridPieceType.Empty:
                GridPiece_Empty gridPiece_Empty = pieceObj.GetComponent<GridPiece_Empty>();
                gridPiece_Empty.isEmpty = true;
                gridPiece_Empty.isWalkable = true;
                piece = gridPiece_Empty;
                break;
            case GridPieceType.Wall:
                GridPiece_Wall gridPiece_Wall = pieceObj.GetComponent<GridPiece_Wall>();
                gridPiece_Wall.isWalkable = false;
                gridPiece_Wall.isEmpty = false;
                gridPiece_Wall.isDestructible = false;
                gridPiece_Wall.CreateWall(wallPref);
                piece = gridPiece_Wall;
                break;
            case GridPieceType.DestructibleWall:
                GridPiece_Wall gridPiece_WallDestructible = pieceObj.GetComponent<GridPiece_Wall>();
                gridPiece_WallDestructible.isWalkable = false;
                gridPiece_WallDestructible.isEmpty = false;
                gridPiece_WallDestructible.isDestructible = true;
                gridPiece_WallDestructible.CreateWall(wallDestructiblePref);
                piece = gridPiece_WallDestructible;
                break;
            case GridPieceType.Spike:
                GridPiece_Spikes gridPiece_Spikes = pieceObj.GetComponent<GridPiece_Spikes>();
                gridPiece_Spikes.isWalkable = true;
                gridPiece_Spikes.isEmpty = true;
                gridPiece_Spikes.CreateSpikes(spikePref);
                piece = gridPiece_Spikes;
                break;
            case GridPieceType.Win:
                GridPiece_Win gridPiece_Win = pieceObj.GetComponent<GridPiece_Win>();
                gridPiece_Win.isWalkable = true;
                gridPiece_Win.isEmpty = true;
                gridPiece_Win.isOpen = false;
                gridPiece_Win.CreateWin(winPref);
                piece = gridPiece_Win;
                win = gridPiece_Win;
                break;
            case GridPieceType.Key:
                GridPiece_Key gridPiece_Key = pieceObj.GetComponent<GridPiece_Key>();
                gridPiece_Key.isWalkable = true;
                gridPiece_Key.isEmpty = true;
                gridPiece_Key.isAvaliable = true;
                gridPiece_Key.CreateKey(keyPref);
                piece = gridPiece_Key;
                key = gridPiece_Key;
                break;
        }
        
        return piece;   
    }

    //Se encarga de elegir el tipo de pieza segun la posicion
    GridPieceType GetPieceType(Vector2Int pos)
    {
        GridPieceType gridPieceType = GridPieceType.Empty;
        if(pos.x == 0 || pos.x == gridSize.x-1 || pos.y == 0 || pos.y == gridSize.y-1)
        {
            gridPieceType = GridPieceType.Wall;
        }
        else if (pos.x == 1 || pos.x == gridSize.x - 2 || pos.y == 1 || pos.y == gridSize.y - 2)
        {
            gridPieceType = GridPieceType.DestructibleWall;
        }
        else if(pos.x == 3 && pos.y == 3)
        {
            gridPieceType = GridPieceType.Spike;
        }
        else if (pos.x == 7 && pos.y == 7)
        {
            gridPieceType = GridPieceType.Win;
        }
        else if (pos.x == 2 && pos.y == 5)
        {
            gridPieceType = GridPieceType.Key;
        }
        return gridPieceType;
    }

    public bool IsPieceWalkable(Vector2Int piecePos)
    {
        return grid[piecePos.x, piecePos.y].isWalkable;
    }

    public GridPiece GetGridPiece(Vector2Int piecePos)
    {
        return grid[piecePos.x,piecePos.y];
    }

    public bool IsPosOnArray(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < gridSize.x && pos.y >= 0 && pos.y < gridSize.y;
    }
}
