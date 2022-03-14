using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public static Action<Enemy> OnEnemyKilled;
    public static Action<Enemy> OnEnemyHit;

    [SerializeField] private GameObject healthBarPrefab;
    [SerializeField] private Transform barPosition;
    [SerializeField] private float initialHealth = 100f;
    [SerializeField] private float maxHealth = 100f;

    public float CurrentHealth { get; set; }

    private Image _healthBar;
    private Enemy _enemy;

    // Start is called before the first frame update
    private void Start()
    {
        CreateHealthBar();
        CurrentHealth = initialHealth;
        _enemy = GetComponent<Enemy>();
    }
    private void Update()
    {
        // for testing DealDamage function
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    DealDamage(5f);
        //}
        // updating fill amount of healthbar
        _healthBar.fillAmount = Mathf.Lerp(_healthBar.fillAmount, 
            CurrentHealth / maxHealth, Time.deltaTime * 10);
    }
    private void CreateHealthBar()
    {
        GameObject newBar = Instantiate(healthBarPrefab, barPosition.position, Quaternion.identity);
        newBar.transform.SetParent(transform);
        EnemyHealthContainer container = newBar.GetComponent<EnemyHealthContainer>();
        _healthBar = container.FillAmountImage;
    }
    public void DealDamage(float damageReceived)
    {
        CurrentHealth -= damageReceived;
        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            Die();
        }
        else
        {
            OnEnemyHit?.Invoke(_enemy);
        }
    }
    public void ResetHealth()
    {
        CurrentHealth = initialHealth;
        _healthBar.fillAmount = 1f;
    }
    private void Die()
    {
        AchievementManager.Instance.AddProgress("Kill20", 1);
        AchievementManager.Instance.AddProgress("Kill50", 1);
        AchievementManager.Instance.AddProgress("Kill100", 1);
        OnEnemyKilled?.Invoke(_enemy);
    }
}
