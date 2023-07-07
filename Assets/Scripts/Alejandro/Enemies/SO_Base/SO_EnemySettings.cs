using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New enemy settings", menuName = "Scriptable Objects/Enemy settings")]
public class SO_EnemySettings : ScriptableObject
{
    public int MaxHealth;
    public float MoveSpeed;
}