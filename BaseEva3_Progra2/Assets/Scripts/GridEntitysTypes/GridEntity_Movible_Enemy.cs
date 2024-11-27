using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridEntity_Movible_Enemy : GridEntity_Movible
{
    public GridEntity_Movible_Player player;
    public Vector2Int startPos;

    public bool isStunned = false;


    public override void InteractWhitOtherEntity(GridEntity other)
    {

    }

    protected override void Awake2()
    {

    }

    protected override void Die()
    {

    }

    protected override void Update2()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        gridPos = startPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gridPos == gridPos)
        {
            player.currentLife = 0;
            gridManager.playerLife.fillAmount = player.currentLife / player.life;
        }

        if (player.isMoving && !isStunned || player.isShooting && !isStunned)
        {
            Vector2Int dir = Vector2Int.zero;

            if (player.nextPos.x > gridPos.x)
            {
                dir.x = 1;
            }
            if (player.nextPos.x < gridPos.x)
            {
                dir.x = -1;
            }
            if (player.nextPos.y > gridPos.y)
            {
                dir.y = 1;
            }
            if (player.nextPos.y < gridPos.y)
            {
                dir.y = -1;
            }

            if (dir.x != 0 && dir.y != 0) //movimiento diagonal
            {
                int ranXY = Random.Range(0, 2);
                if (ranXY == 0)
                {
                    dir.y = 0;
                }
                else
                {
                    dir.x = 0;
                }
            }

            Move(dir);
        }
        else
        {
            isStunned = false;
        }
    }
}
