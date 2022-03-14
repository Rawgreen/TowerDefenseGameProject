using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{

    private Animator _animator;
    private Enemy _enemy;
    private EnemyHealth _enemyHealth;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _enemy = GetComponent<Enemy>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }
    // plays hurt animation
    private void PlayHurtAnimation()
    {
        _animator.SetTrigger("Hurt");
    }
    // plays die animation
    private void PlayDieAnimation()
    {
        // triggering animations
        _animator.SetTrigger("Die");
    }
    // to know how many seconds of an animation
    private float GetCurrentAnimationLenght()
    {
        float animationLenght = _animator.GetCurrentAnimatorStateInfo(0).length;
        return animationLenght;
    }
    // playing hurt animation
    private IEnumerator PlayHurt()
    {
        _enemy.StopMovement();
        PlayHurtAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLenght() + 0.26f);
        _enemy.ResumeMovement();
    }
    // calling ReturnToPool and ResetHealth lines for playing die animation
    private IEnumerator PlayDead()
    {
        _enemy.DieMovement();
        PlayDieAnimation();
        yield return new WaitForSeconds(GetCurrentAnimationLenght() + 1.3f);
        //if enemy reused later, need to reset its movement
        _enemy.ResumeMovement();
        _enemyHealth.ResetHealth();
        ObjectPooler.ReturnToPool(_enemy.gameObject);
    }
    private void EnemyHit(Enemy enemy)
    {
        if (_enemy == enemy)
        {
            StartCoroutine(PlayHurt());
        }
    }
    private void EnemyDead(Enemy enemy)
    {
        if (_enemy == enemy)
        {
            StartCoroutine(PlayDead());
        }
    }
    private void OnEnable()
    {
        EnemyHealth.OnEnemyHit += EnemyHit;
        EnemyHealth.OnEnemyKilled += EnemyDead;
    }
    private void OnDisable()
    {
        EnemyHealth.OnEnemyHit -= EnemyHit;
        EnemyHealth.OnEnemyKilled -= EnemyDead;
    }
}
