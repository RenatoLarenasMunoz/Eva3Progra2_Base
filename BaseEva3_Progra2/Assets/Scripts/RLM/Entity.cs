using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Variables Entity")]

    public GameManager gameManager;

    public int posX;
    public int posY;


    public void MoveToPos(int newPosX, int newPosY)
    {
        posX = newPosX;
        posY = newPosY;
        transform.position = new Vector3(posX, posY, 0);
    }
}
