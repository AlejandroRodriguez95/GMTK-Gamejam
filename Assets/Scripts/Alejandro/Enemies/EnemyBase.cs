using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] SO_EnemySettings settings;

    [SerializeField]
    protected float currentHealth;
    protected float moveSpeed;

    [SerializeField] protected List<Transform> internalWaypoints;

    [SerializeField]
    protected Transform currentWaypoint;

    protected bool alreadyLoadedSecondSegment;
    protected bool alreadyLoadedThirdSegment;
    protected bool alreadyLoadedLastSegment;
    protected bool side; // false = left, true = right

    [SerializeField] protected SpriteRenderer gameOverImage;

    public SpriteRenderer GameOverImage
    {
        set { gameOverImage = value; }
    }

    protected Transform armTransform;

    public Transform ArmTransform
    {
        get { return armTransform; }
        set { armTransform = value; }
    }

    public bool Side
    {
        get { return side; }
        set { side = value; }
    }

    public List<Transform> InternalWaypoints
    {
        set { internalWaypoints = value; }
    }

    public float MoveSpeed
    {
        set { moveSpeed = value; }
    }

    private void Awake()
    {
        currentHealth = settings.MaxHealth;
        moveSpeed = settings.MoveSpeed;
    }
}
