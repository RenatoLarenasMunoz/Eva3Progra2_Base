using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPiece_Spikes : GridPiece
{
    public float yOffSet;
    GameObject spikes;

    public void CreateSpikes(GameObject spikePref)
    {
        Vector3 pos = transform.position;
        pos += Vector3.up * yOffSet;
        spikes = Instantiate(spikePref, pos, Quaternion.identity, transform);
    }

    public override void OnEntityEnter(GridEntity gridEntity)
    {
        base.OnEntityEnter(gridEntity);
        currentGridEntity.TakeDamage(1);
    }

    public override void OnEntityExit()
    {

    }

    public override void OnEntityStay()
    {

    }
}
