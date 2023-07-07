using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] SO_EnemySettings settings;

    protected int currentHealth;
    protected float moveSpeed;

    [SerializeField] protected List<Transform> waypoints;
    protected Transform currentWaypoint;

    public List<Transform> Waypoints
    {
        set { waypoints = value; }
    }

    private void Awake()
    {
        currentHealth = settings.MaxHealth;
        moveSpeed = settings.MoveSpeed;
    }
}
