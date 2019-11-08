using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elf : EnemyBehaviour
{
    public EnemyWeaponData magoWeapon;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MainBehaviour());
    }

    IEnumerator MainBehaviour()
    {
        while (IsAlive)
        {
            if (DistanceFromTarget <= 2)
            {
                SetRandomTarget();
            } else {
                MoveToCurrentTarget();
            }

            if (PlayerOnAttackRange())
            {
               AttackPlayer(magoWeapon);
               yield return new WaitForSeconds(3);
            }

            yield return new WaitForSeconds(1);
        }
    }
}