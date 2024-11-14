using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece_Win : GridPiece
{
    public bool isOpen;
    public float yOffSet;
    GameObject winPiece;

    public void CreateWin(GameObject winPref)
    {
        Vector3 pos = transform.position;
        pos += Vector3.up * yOffSet;
        winPiece = Instantiate(winPref, pos, Quaternion.identity, transform);
    }

    public void CheckIfOpen()
    {
        if (isOpen)
        {
            Debug.Log("Victoria");
        }
        else
        {
            Debug.Log("No disponible");
        }
    }

    public override void OnEntityEnter(GridEntity gridEntity)
    {
        base.OnEntityEnter(gridEntity);
        CheckIfOpen();
    }

    public override void OnEntityExit()
    {

    }

    public override void OnEntityStay()
    {

    }
}
