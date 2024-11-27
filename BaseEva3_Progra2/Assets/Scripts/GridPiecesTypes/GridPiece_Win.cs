using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridPiece_Win : GridPiece
{
    public GridManager grid;
    public bool isOpen;
    public float yOffSet;
    public GameObject sparkles;

    public void CreateWin(GameObject winPref)
    {
        Vector3 pos = transform.position;
        pos += Vector3.up * yOffSet;
    }

    public void CheckIfOpen()
    {
        if (isOpen)
        {
            Debug.Log("Victoria");
            grid.gameFin = true;
            grid.winText = "VICTORIA";
            grid.textoFin.color = Color.yellow;
            grid.textoFin.text = grid.winText;
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
