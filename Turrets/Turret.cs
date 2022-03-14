using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] public float attackRange = 3f;

    // for storing enemies that entered range
    private List<Enemy> _enemies;

    // for checking if game has started
    private bool _gameStarted;

    // first target that entered the range
    public Enemy CurrentEnemyTarget { get; set; }

    // property for store turretupgrade class reference
    public TurretUpgrade TurretUpgrade { get; set; }

    public float _currentAttackRange;
    private void Start()
    {
        _gameStarted = true;
        _enemies = new List<Enemy>();
        TurretUpgrade = GetComponent<TurretUpgrade>();
    }
    private void Update()
    {
        GetCurrentEnemyTarget();
    }
    private void GetCurrentEnemyTarget()
    {
        if (_enemies.Count <= 0)
        {
            CurrentEnemyTarget = null;
            return;
        }

        CurrentEnemyTarget = _enemies[0];
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy newEnemy = other.GetComponent<Enemy>();
            _enemies.Add(newEnemy);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // getting enemy reference
            Enemy enemy = other.GetComponent<Enemy>();
            if (_enemies.Contains(enemy))
            {
                // removing enemy from list
                _enemies.Remove(enemy); 
            }
        }
    }
    private void OnDrawGizmos()
    {
        if (!_gameStarted)
        {
            GetComponent<CircleCollider2D>().radius = attackRange;
        }
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
