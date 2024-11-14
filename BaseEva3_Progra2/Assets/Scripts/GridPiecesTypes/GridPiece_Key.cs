using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece_Key : GridPiece
{
    public bool isAvaliable;
    public float yOffSet;
    GameObject keyPiece;
    public GridPiece_Win win;


    public void CreateKey(GameObject winPref)
    {
        Vector3 pos = transform.position;
        pos += Vector3.up * yOffSet;
        keyPiece = Instantiate(winPref, pos, Quaternion.identity, transform);
    }

    public void PickKey()
    {
        if (isAvaliable)
        {
            isAvaliable = false;
            win.isOpen = true;
        }
    }

    public override void OnEntityEnter(GridEntity gridEntity)
    {
        base.OnEntityEnter(gridEntity);
        PickKey();
    }

    public override void OnEntityExit()
    {

    }

    public override void OnEntityStay()
    {

    }
}
