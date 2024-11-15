using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridEntity : MonoBehaviour
{
    public EntityType entityType;
    public Vector2Int gridPos;
    public bool isTrigger;
    public float life;
    public float currentLife;

    private void Awake()
    {
        currentLife = life;
        //life = currentLife;
        Awake2();
    }

    protected abstract void Awake2();

    public virtual void TakeDamage(float dmg)
    {
        currentLife -= dmg;
        if (currentLife <= 0)
        {
            Die();
        }
    }

    protected abstract void Die();

    public abstract void InteractWhitOtherEntity(GridEntity other);
}
