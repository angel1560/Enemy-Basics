using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 100;

    public void TakeDamage(int _damage)
    {
        hp -= _damage;
        Debug.Log("Player loss " + _damage + " Damage");
    }
}