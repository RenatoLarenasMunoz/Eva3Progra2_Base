using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Entity
{
    [Header("Variables Player")]

    public EnemyController enemy;


    private void Start()
    {
        //MoveToPos(5, 5);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // revisar si hay casilla disponible
            bool inArray = gameManager.IsInsideArray(posX + 1, posY);
            if (inArray)
            {
                GridPieceType casillaCheck = gameManager.GetValueCell(posX + 1, posY);
                if (casillaCheck != GridPieceType.Wall)
                {
                    MoveToPos(posX + 1, posY);
                    enemy.ChoseRandomMove();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            bool inArray = gameManager.IsInsideArray(posX - 1, posY);
            if (inArray)
            {
                GridPieceType casillaCheck = gameManager.GetValueCell(posX - 1, posY);
                if (casillaCheck != GridPieceType.Wall)
                {
                    MoveToPos(posX - 1, posY);
                    enemy.ChoseRandomMove();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            bool inArray = gameManager.IsInsideArray(posX, posY + 1);
            if (inArray)
            {
                GridPieceType casillaCheck = gameManager.GetValueCell(posX, posY + 1);
                if (casillaCheck != GridPieceType.Wall)
                {
                    MoveToPos(posX, posY + 1);
                    enemy.ChoseRandomMove();
                }
            }
            
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            bool inArray = gameManager.IsInsideArray(posX, posY - 1);
            if (inArray)
            {
                GridPieceType casillaCheck = gameManager.GetValueCell(posX, posY - 1);
                if (casillaCheck != GridPieceType.Wall)
                {
                    MoveToPos(posX, posY - 1);
                    enemy.ChoseRandomMove();
                }
            }
        }
    }
}
