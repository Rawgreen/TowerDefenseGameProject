using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : MonoBehaviour
{
    [SerializeField] protected Transform projectileSpawnPosition;
    [SerializeField] protected float delatBtwAttacks = 0.2f;

    protected float _nextAttackTime;
    protected ObjectPooler _pooler;
    protected Turret _turret;
    private Projectile _currentProjectileLoaded;

    private void Start()
    {
        // getting that reference
        _turret = GetComponent<Turret>();
        _pooler = GetComponent<ObjectPooler>();
        // need to call LoadProjectile() method, because we need to load projectile a single projectile to begin playing
        LoadProjectile();
    }

    protected virtual void Update()
    {
        // if turret is empty, load projectile
        if (IsTurretEmpty())
        {
            LoadProjectile();
        }
        if (Time.time > _nextAttackTime)
        {
            if (_turret.CurrentEnemyTarget != null &&
            _currentProjectileLoaded != null &&
            _turret.CurrentEnemyTarget.EnemyHealth.CurrentHealth > 0f)
            {
                _currentProjectileLoaded.transform.parent = null;
                _currentProjectileLoaded.SetEnemy(_turret.CurrentEnemyTarget);
            }
            _nextAttackTime = Time.time + delatBtwAttacks;
        } 
    }
    protected virtual void LoadProjectile()
    {
        // assuming that there is a pool full of projectiles
        GameObject newInstance = _pooler.GetInstanceFromPool();
        newInstance.transform.localPosition = projectileSpawnPosition.position;
        // defining parent of the projectile
        newInstance.transform.SetParent(projectileSpawnPosition);
        // getting reference of the current projectile that is loaded
        _currentProjectileLoaded = newInstance.GetComponent<Projectile>();
        // defining owner of the current projectile 
        _currentProjectileLoaded.TurretOwner = this;
        // when loading a new projectile, reset it
        _currentProjectileLoaded.ResetProjectile();
        newInstance.SetActive(true);
    }
    // after resetting projectile, checking turret emptyness
    private bool IsTurretEmpty()
    {
        return _currentProjectileLoaded == null;
    }
    // resetting _currentProjectileLoaded
    public void ResetTurretProjectile()
    {
        _currentProjectileLoaded = null;
    }
}
