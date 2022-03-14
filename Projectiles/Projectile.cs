using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public static Action<Enemy, float> OnEnemyHit;

    [SerializeField] protected float moveSpeed = 7f;
    [SerializeField] protected float damage = 10f;

    // this var is useful to check if the projectile iss close enough
    [SerializeField] protected float minDistanceToDealDamage = 0.1f;

    public TurretProjectile TurretOwner { get; set; }

    protected Enemy _enemyTarget;

    // virtual keyword allows to override 
    protected virtual void Update()
    {
        // if there is an enemy, move projectile
        if (_enemyTarget != null)
        {
            MoveProjectile();
            RotateProjectile();
        }
    }
    protected virtual void MoveProjectile()
    {
        transform.position = Vector2.MoveTowards(transform.position, _enemyTarget.transform.position, moveSpeed * Time.deltaTime);
        // getting length of the vector to see how much projectile close to target
        float distanceToTarget = (_enemyTarget.transform.position - transform.position).magnitude;
        // means close enough to deal damage
        if (distanceToTarget < minDistanceToDealDamage)
        {
            OnEnemyHit?.Invoke(_enemyTarget, damage);
            _enemyTarget.EnemyHealth.DealDamage(damage);
            TurretOwner.ResetTurretProjectile();
            ObjectPooler.ReturnToPool(gameObject);
        }
    }
    private void RotateProjectile()
    {
        Vector3 enemyPos = _enemyTarget.transform.position - transform.position;
        float angle = Vector3.SignedAngle(transform.up, enemyPos, transform.forward);
        transform.Rotate(0f, 0f, angle);
    }
    // turret going to have turret projectile class that is going to handle using projectiles from a pooler
    public void SetEnemy(Enemy enemy)
    {
        _enemyTarget = enemy;
    }
    // everytime reusing a projectile, reset enemy target and the rotation of projectile
    public void ResetProjectile()
    {
        _enemyTarget = null;
        transform.localRotation = Quaternion.identity;
    }
}
