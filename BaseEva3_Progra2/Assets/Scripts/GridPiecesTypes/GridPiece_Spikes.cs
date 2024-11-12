using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece_Spikes : GridPiece
{
    public override void OnEntityEnter(GridEntity gridEntity)
    {
        base.OnEntityEnter(gridEntity);
        currentGridEntity.TakeDamage(2);
    }

    public override void OnEntityExit()
    {

    }

    public override void OnEntityStay()
    {

    }
}
