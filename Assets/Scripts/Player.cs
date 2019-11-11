using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp = 100;

    public bool IsAlive
    {
        get {return hp >= 0;}
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 4);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject.CompareTag("Enemy"))
                {
                    colliders[i].gameObject.GetComponent<EnemyBehaviour>().TakeDamage(10);
                }
            }
        }
    }

    public void TakeDamage(int _damage)
    {
        hp -= _damage;
        Debug.Log("Player loss " + _damage + " Damage");
    }
}