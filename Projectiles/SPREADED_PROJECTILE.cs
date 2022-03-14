using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SPREADED_PROJECTILE : Projectile
{
    public Vector2 Direction { get; set; }
    protected override void Update()
    {
        MoveProjectile();
    }
    protected override void MoveProjectile()
    {
        Vector2 movement = Direction.normalized * moveSpeed * Time.deltaTime;
        transform.Translate(movement);
    }

    /// <summary>
    /// when enemy enters Collider range, Get Enemy Class components, compare if enemy current health
    /// more than 0, if enemyhit != null, invoke enemy damage method.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.EnemyHealth.CurrentHealth > 0f)
            {
                OnEnemyHit?.Invoke(enemy, damage);
                enemy.EnemyHealth.DealDamage(damage);
            }
            ObjectPooler.ReturnToPool(gameObject);
        }
    }
    private void OnEnable()
    {
        StartCoroutine(ObjectPooler.ReturnToPoolWithDelay(gameObject, 5f));
    }
}
