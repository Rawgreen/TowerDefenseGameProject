using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadProjectile : TurretProjectile
{
    [SerializeField] private bool WannaSpread;
    [SerializeField] private float spreadRange;

    protected override void Update()
    {
        if (Time.time > _nextAttackTime)
        {
            if (_turret.CurrentEnemyTarget != null)
            {
                Vector3 dirToTarget = _turret.CurrentEnemyTarget.transform.position - transform.position;
                FireProjectile(dirToTarget);
            }

            _nextAttackTime = Time.time + delatBtwAttacks;
        }
    }
    protected override void LoadProjectile() { }
    private void FireProjectile(Vector3 direction)
    {
        GameObject instance = _pooler.GetInstanceFromPool();
        instance.transform.position = projectileSpawnPosition.position;
        SPREADED_PROJECTILE projectile = instance.GetComponent<SPREADED_PROJECTILE>();
        projectile.Direction = direction;
        if (WannaSpread)
        {
            float randomSpread = Random.Range(-spreadRange, spreadRange);
            Vector3 spread = new Vector3(0f, 0f, randomSpread);
            Quaternion spreadValue = Quaternion.Euler(spread);
            Vector2 newDirection = spreadValue * direction;
            projectile.Direction = newDirection;
        }
        instance.SetActive(true);
    }
}
