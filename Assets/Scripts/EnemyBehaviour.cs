using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBehaviour : MonoBehaviour
{
    public enum EnemyStates
    {
        Patrol = 0,
        Warning = 1,
        Attack = 2,
        GetBack = 3,
    }

    public NavMeshAgent agent;
    public List<Transform> wayPoints = new List<Transform>();

    protected EnemyStates currentState = EnemyStates.Patrol;
    protected int hp;
    protected float attackRange;
    protected float warningRange;
    protected Player playerTarget;
    protected Transform currentTarget;
    protected EnemyWeaponData baseWeapon;

    public bool IsAlive
    {
        get {return hp >= 0;}
    }

    public bool HasTarget
    {
        get {return currentTarget != null;}
    }

    public float DistanceFromTarget
    {
        get
        {
            if (currentTarget != null)
            {
                return Vector3.Distance(transform.position, currentTarget.position);
            } else {
                return 0;
            } 
        }
    }

    public float DistanceFromPlayer
    {
        get
        {
            if (playerTarget != null)
            {
                return Vector3.Distance(transform.position, playerTarget.gameObject.transform.position);
            } else {
                return 0;
            } 
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Display the Attack Range radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

        // Display the Warning Range radius when selected
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, warningRange);
    }

    private void Die()
    {
        Debug.Log("Enemy Die");
    }

    protected bool PlayerOnAttackRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, attackRange);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.CompareTag("Player"))
            {
                playerTarget = colliders[i].gameObject.GetComponent<Player>();
                return true;
            }
        }

        playerTarget = null;
        return false;
    }

    protected Vector3 SetRandomTarget()
    {
        currentTarget = wayPoints[Random.Range(0, wayPoints.Count)];
        return currentTarget.position;
    }

    protected void MoveToCurrentTarget()
    {
        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.position);
        }
    }

    protected void MoveToCurrentTarget(Vector3 _targetPosition)
    {
        agent.SetDestination(_targetPosition);
    }

    protected void AttackPlayer(EnemyWeaponData _weapon = null)
    {
        if (_weapon != null)
        {
            playerTarget.TakeDamage(_weapon.damage);
            Debug.Log("Enemy Attacking With " + _weapon.name);
        } else {
            playerTarget.TakeDamage(baseWeapon.damage);
            Debug.Log("Enemy Attacking With " + baseWeapon.name);
        }
    }

    public void InitEnemyProfile(EnemyProfile _profile)
    {
        hp = _profile.hp;
        agent.acceleration = _profile.speed;
        attackRange = _profile.attackRange;
        warningRange = _profile.warningRange;
        baseWeapon = _profile.basicWeapon;
    }

    public void TakeDamage(int _damage)
    {
        hp -= _damage;

        currentState = EnemyStates.GetBack;

        Debug.Log(name + " Get " + _damage + " Damage");

        if (hp <= 0)
        {
            Die();
        }
    }
}