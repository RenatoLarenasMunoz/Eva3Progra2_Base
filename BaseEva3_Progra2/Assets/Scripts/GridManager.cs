using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridManager : MonoBehaviour
{
    //El arreglo requiere el mismo orden que el enum
    string[] mapa;
    public GameObject[] gridPiecesPrefabs;
    public Vector2Int gridSize;
    public Transform parent;

    public GameObject wallPref;
    public GameObject wallDestructiblePref;
    public GameObject spikePref;
    public GameObject winPref;
    public GameObject keyPref;

    public GridPiece[,] grid;

    public TextMeshProUGUI textoFin;
    public Image playerLife;
    public string winText;
    public bool gameFin = false;
    GridPiece_Win win;
    GridPiece_Key key;

    private void Awake()
    {
        CrearMapa();
        CreateGrid();
        key.win = win;
    }

    void CrearMapa()
    {
        mapa = new string[gridSize.x];


        mapa[9] = "XXXXXXXXXXXXX";
        mapa[8] = "XOOOOOOOOOOWX";
        mapa[7] = "XOOOOOYOOOYYX";
        mapa[6] = "XOOSSOYOSSOOX";
        mapa[5] = "XOOOYYOOOOOOX";
        mapa[4] = "XOSSSYOSYSOOX";
        mapa[3] = "XOOOOYOYKYOOX";
        mapa[2] = "XOOOOOOSYSOOX";
        mapa[1] = "XOOOOOOOOOOOX";
        mapa[0] = "XXXXXXXXXXXXX";
    }

    //Se encarga de crear la grilla
    public void CreateGrid()
    {
        //Inicializo la matriz (arreglo bidimensional) segun el tamaño de la grilla
        grid = new GridPiece[gridSize.x, gridSize.y];

        for (int x = 0; x < gridSize.x; x++)
        {
            for (int z = 0; z < gridSize.y; z++)
            {
                //Obtengo posicion en grilla
                Vector2Int gridPos = new Vector2Int(x, z);

                //Segun la posicion devuelvo un tipo de pieza
                GridPieceType gridPieceType = GetPieceType(gridPos);

                //Ocupo la posicion en grilla y el tipo de pieza para instancia la pieza
                GridPiece newPiece = CreatePiece(gridPieceType, gridPos);
                newPiece.pos = gridPos;
                //Guardo la pieza creada en la matriz
                grid[x, z] = newPiece;
            }
        }
    }

    //Se encarga de instanciar la pieza y setearla
    GridPiece CreatePiece(GridPieceType pieceType, Vector2Int gridPos)
    {
        GridPiece piece = null;
        Vector3 position = new Vector3(gridPos.x, -0.5f, gridPos.y);
        GameObject pref = gridPiecesPrefabs[(int)pieceType];

        GameObject pieceObj = Instantiate(pref, position, Quaternion.identity, parent);

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
                gridPiece_Win.grid = this;
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
        //GridPieceType gridPieceType = GridPieceType.Empty;
        if (pos.x == 0 || pos.x == gridSize.x - 1 || pos.y == 0 || pos.y == gridSize.y - 1)
        {
            return GridPieceType.Wall;
        }
        //else if (pos.x == 1 || pos.x == gridSize.x - 2 || pos.y == 1 || pos.y == gridSize.y - 2)
        //{
        //    gridPieceType = GridPieceType.DestructibleWall;
        //}
        //return gridPieceType;

        string linea = mapa[pos.y];
        char celda = linea[pos.x];

        switch (celda)
        {
            case 'O':
                return GridPieceType.Empty;
                break;
            case 'X':
                return GridPieceType.Wall;
                break;
            case 'Y':
                return GridPieceType.DestructibleWall;
                break;
            case 'W':
                return GridPieceType.Win;
                break;
            case 'K':
                return GridPieceType.Key;
                break;
            case 'S':
                return GridPieceType.Spike;
                break;
            default:
                return GridPieceType.Empty;
                break;
        }





    }

    public bool IsPieceWalkable(Vector2Int piecePos)
    {
        return grid[piecePos.x, piecePos.y].isWalkable;
    }

    public GridPiece GetGridPiece(Vector2Int piecePos)
    {
        return grid[piecePos.x, piecePos.y];
    }

    public bool IsPosOnArray(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < gridSize.x && pos.y >= 0 && pos.y < gridSize.y;
    }
}
