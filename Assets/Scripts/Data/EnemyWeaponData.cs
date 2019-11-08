using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Wepon Data", menuName = "Game Data/Enemy Weapon Data", order = 1)]
public class EnemyWeaponData : ScriptableObject
{
    public Sprite icon;
    public string description;
    public int damage = 1;
}