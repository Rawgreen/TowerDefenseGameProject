using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : Singleton<UIManager>
{
    [Header("Panels")]
    [SerializeField] private GameObject towerShopPanel;
    [SerializeField] private GameObject nodeUIPanel;
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject towerShopCloseButtonPanel;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI upgradeText;
    [SerializeField] private TextMeshProUGUI sellText;
    [SerializeField] private TextMeshProUGUI turretLevelText;
    // for Life-Coin bar
    [SerializeField] private TextMeshProUGUI totalCoinsText;
    [SerializeField] private TextMeshProUGUI lifesText;
    [SerializeField] private TextMeshProUGUI currentWaveText;
    [SerializeField] private TextMeshProUGUI gameOverTotalCoinsText;

    

    private Node _currentNodeSelected;

    private void Update()
    {
        totalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
        lifesText.text = LevelManager.Instance.TotalLives.ToString();
        currentWaveText.text = $"Wave {LevelManager.Instance.CurrentWave}";
    }
    public void SlowTime()
    {
        Time.timeScale = Time.timeScale/2;
    }
    public void ResumeTime()
    {
        Time.timeScale = 1f;
    }
    public void FastTime()
    {
        Time.timeScale = Time.timeScale*2;
    }
    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
        gameOverTotalCoinsText.text = CurrencySystem.Instance.TotalCoins.ToString();
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void OpenAchievementPanel(bool status)
    {
        achievementPanel.SetActive(status);
    }
    // to close shop
    public void CloseTowerShopPanel()
    {
        towerShopPanel.SetActive(false);
        CloseTowerShopButton();
    }
    private void CloseTowerShopButton()
    {
        towerShopCloseButtonPanel.SetActive(false);
    }
    public void CloseNodeUIPanel()
    {
        nodeUIPanel.SetActive(false);
    }
    public void UpgradeTurret()
    {
        _currentNodeSelected.Tower.UpgradeTower();
        UpdateUpgradeText();
        UpdateTurretLevel();
        UpdateSellValue();
    }
    public void SellTurret()
    {
        _currentNodeSelected.SellTower();
        _currentNodeSelected = null;
        nodeUIPanel.SetActive(false);
    }
    private void ShowNodeUI()
    {
        towerShopPanel.SetActive(false);
        towerShopCloseButtonPanel.SetActive(false);
        nodeUIPanel.SetActive(true);
        UpdateUpgradeText();
        UpdateTurretLevel();
        UpdateSellValue();
    }
    private void UpdateUpgradeText()
    {
        upgradeText.text = _currentNodeSelected.Tower.UpgradeCost.ToString();
    }
    private void UpdateTurretLevel()
    {
        turretLevelText.text = $"Level {_currentNodeSelected.Tower.Level}";
    }
    private void UpdateSellValue()
    {
        int sellAmount = _currentNodeSelected.Tower.GetSellValue();
        sellText.text = sellAmount.ToString();
    }
    private void NodeSelected(Node nodeSelected)
    {
        _currentNodeSelected = nodeSelected;
        if (_currentNodeSelected.IsEmpty())
        {
            nodeUIPanel.SetActive(false);
            towerShopPanel.SetActive(true);
            towerShopCloseButtonPanel.SetActive(true);
        }
        else
        {
            ShowNodeUI();
        }
    }
    // listening new event
    private void OnEnable()
    {
        Node.OnNodeSelected += NodeSelected;
    }
    private void OnDisable()
    {
        Node.OnNodeSelected -= NodeSelected;
    }
}
