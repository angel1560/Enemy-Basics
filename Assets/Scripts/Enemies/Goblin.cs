using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : EnemyBehaviour
{
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
               AttackPlayer();
               yield return new WaitForSeconds(3);
            }

            yield return new WaitForSeconds(1);
        }
    }
}