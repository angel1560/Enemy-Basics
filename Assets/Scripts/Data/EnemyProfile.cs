using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Game Data/Enemy", order = 1)]
public class EnemyProfile : ScriptableObject
{
    public int hp = 25;
    public int speed = 4;
    public float attackRange = 3;
    public float warningRange = 7;
    public GameObject enemyPrefab;
}