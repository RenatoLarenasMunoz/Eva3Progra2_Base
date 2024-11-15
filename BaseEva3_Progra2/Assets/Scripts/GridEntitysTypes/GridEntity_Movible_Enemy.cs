using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEntity_Movible_Enemy : GridEntity_Movible
{
    public GridEntity_Movible_Player player;
    public GridShooter gridShooter;
    public Vector2Int startPos;
    public Vector2Int playerOldPos;
    public Vector2Int playerPos;


    protected override void Awake2()
    {

    }

    private void Start()
    {
        SetEnemyPos(startPos);
    }

    public void SetEnemyPos(Vector2Int pos)
    {
        gridPos = pos;
        gridManager.GetGridPiece(pos).OnEntityEnter(this);
    }

    protected override void Update2()
    {
        playerPos = player.gridPos;

        if (playerPos != playerOldPos)
        {
            isMoving = true;
            playerOldPos = playerPos;

            Vector2Int dir = Vector2Int.zero;

            Move(dir);
        }
    }

    public override void InteractWhitOtherEntity(GridEntity other)
    {
        other.InteractWhitOtherEntity(this);
    }

    protected override void Die()
    {
        Destroy(this);
    }
}
