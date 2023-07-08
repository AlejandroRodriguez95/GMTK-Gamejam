using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] SO_EnemySettings settings;

    protected int currentHealth;
    protected float moveSpeed;

    [SerializeField] protected List<Transform> internalWaypoints;
    protected Transform currentWaypoint;

    protected bool alreadyLoadedSecondSegment;
    protected bool side; // false = left, true = right

    public bool Side
    {
        get { return side; }
        set { side = value; }
    }

    public List<Transform> InternalWaypoints
    {
        set { internalWaypoints = value; }
    }

    private void Awake()
    {
        currentHealth = settings.MaxHealth;
        moveSpeed = settings.MoveSpeed;
    }
}
