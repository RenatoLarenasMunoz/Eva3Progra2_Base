using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Entity
{
    public int turn, turnTarget;


    public void ChoseRandomMove()
    {
        int direction = Random.Range(0, 4);
        if (direction == 0) // derecha
        {
            bool inArray = gameManager.IsInsideArray(posX + 1, posY);
            if (inArray)
            {
                GridPieceType casillaCheck = gameManager.GetValueCell(posX + 1, posY);
                if (casillaCheck != GridPieceType.Wall)
                {
                    MoveToPos(posX + 1, posY);
                }
            }
        }
        else if (direction == 1) // izquierda
        {
            bool inArray = gameManager.IsInsideArray(posX - 1, posY);
            if (inArray)
            {
                GridPieceType casillaCheck = gameManager.GetValueCell(posX - 1, posY);
                if (casillaCheck != GridPieceType.Wall)
                {
                    MoveToPos(posX - 1, posY);
                }
            }
        }
        else if (direction == 2) // arriba
        {
            bool inArray = gameManager.IsInsideArray(posX, posY + 1);
            if (inArray)
            {
                GridPieceType casillaCheck = gameManager.GetValueCell(posX, posY + 1);
                if (casillaCheck != GridPieceType.Wall)
                {
                    MoveToPos(posX, posY + 1);
                }
            }
        }
        else if (direction == 3) // abajo
        {
            bool inArray = gameManager.IsInsideArray(posX, posY - 1);
            if (inArray)
            {
                GridPieceType casillaCheck = gameManager.GetValueCell(posX, posY - 1);
                if (casillaCheck != GridPieceType.Wall)
                {
                    MoveToPos(posX, posY - 1);
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        MoveToPos(Random.Range(0, gameManager.sizeX -1), Random.Range(0, gameManager.sizeY - 1));
    }

    // Update is called once per frame
    void Update()
    {
        //contador += Time.deltaTime;
    }
}
