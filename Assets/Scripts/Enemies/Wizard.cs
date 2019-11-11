using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : EnemyBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(UpdateState());
        StartCoroutine(MainBehaviour());
    }

    void OnPatrol()
    {
        if (DistanceFromTarget < 2 || !HasTarget)
        {
            SetRandomTarget();
        }

        if (HasTarget)
        {
            MoveToCurrentTarget();
        }
    }

    void OnWarning()
    {
        MoveToCurrentTarget(playerTarget.gameObject.transform.position);
    }

    void OnAttack()
    {
        playerTarget.TakeDamage(2);
    }

    void OnGetBack()
    {
        MoveToCurrentTarget(new Vector3(0,0,0));
    }

    IEnumerator UpdateState()
    {
        while (IsAlive)
        {
            if (currentState == EnemyStates.GetBack)
                yield return new WaitForSeconds(3);
            
            if (PlayerOnAttackRange() && playerTarget.IsAlive)
            {
                if (DistanceFromPlayer < attackRange / 2)
                {
                    currentState = EnemyStates.Attack; 
                } else {
                    currentState = EnemyStates.Warning; 
                }
                
            } else {
                currentState = EnemyStates.Patrol;
            }

            // Update Time...
            yield return new WaitForSeconds(1);
        }
    }

    IEnumerator MainBehaviour()
    {
        while (IsAlive)
        {
            switch (currentState)
            {
                case EnemyStates.Patrol:
                    OnPatrol(); 
                break;

                case EnemyStates.Warning:
                    OnWarning();
                break;

                case EnemyStates.Attack:
                    OnAttack();
                break;

                case EnemyStates.GetBack:
                    OnGetBack();
                break;
                
                default:
                    OnPatrol(); 
                break;
            }
            
            // Update Time
            yield return new WaitForSeconds(1);
        }
    }
}