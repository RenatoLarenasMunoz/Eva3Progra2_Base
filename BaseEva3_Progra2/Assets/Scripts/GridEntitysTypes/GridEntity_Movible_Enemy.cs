using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEntity_Movible_Enemy : GridEntity_Movible
{
    public GridEntity_Movible_Player player;
    public GridShooter gridShooter;
    public EnemyType enemyType;
    public float damage;
    public bool isStun;
    public Vector2Int startPos;
    public Vector2Int playerOldPos;


    protected override void Awake2()
    {

    }

    private void Start()
    {
        SetEnemyPos(startPos);
        damage = player.life;
    }

    public void SetEnemyPos(Vector2Int pos)
    {
        gridPos = pos;
        gridManager.GetGridPiece(pos).OnEntityEnter(this);
    }

    protected override void Update2()
    {
        /*switch (enemyType)
        {
            case EnemyType.Basic:
                break;
            case EnemyType.Tank:
                break;
        }*/

        if (player.gridPos != playerOldPos)
        {
            Vector2Int dir = player.gridPos - gridPos;
            if (dir.x > 0)
            {
                dir.x = 1;
            }
            else if (dir.x < 0)
            {
                dir.x = -1;
            }
            else if (dir.y > 0)
            {
                dir.y = 1;
            }
            else if (dir.y < 0)
            {
                dir.y = -1;
            }

            if (!isStun)
            {
                isMoving = true;
                Move(dir);
            }
            else
            {
                isStun = false;
            }

            playerOldPos = player.gridPos;
        }

        if (gridPos == player.gridPos)
        {
            player.TakeDamage(damage);
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
