using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShopManager : MonoBehaviour
{
    [SerializeField] private GameObject towerCardPrefab;
    [SerializeField] private Transform towerPanelContainer;

    [Header("Tower Settings")]
    [SerializeField] private TurretSettings[] towers;

    private Node _currentNodeSelected;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < towers.Length; i++)
        {
            CreateTowerCard(towers[i]);
        }
    }
    private void CreateTowerCard(TurretSettings towerSettings)
    {
        GameObject newInstance = Instantiate(towerCardPrefab, towerPanelContainer.position, Quaternion.identity);
        newInstance.transform.SetParent(towerPanelContainer);
        // to avoid errors resetting scale
        newInstance.transform.localScale = Vector3.one;
        // getting reference from towercard class
        TowerCard cardButton = newInstance.GetComponent<TowerCard>();
        cardButton.SetupTurretButton(towerSettings);
    }
    private void NodeSelected(Node nodeSelected)
    {
        _currentNodeSelected = nodeSelected;
    }
    // placing the prefab of the tower in the current node selected
    private void PlaceTower(TurretSettings towerLoaded)
    {
        if (_currentNodeSelected != null)
        {
            GameObject towerInstance = Instantiate(towerLoaded.TurretPrefab);
            towerInstance.transform.localPosition = _currentNodeSelected.transform.position;
            towerInstance.transform.parent = _currentNodeSelected.transform;
            // getting reference to Turret
            TurretUpgrade towerPlaced = towerInstance.GetComponent<TurretUpgrade>();
            _currentNodeSelected.SetTower(towerPlaced);
        }
    }
    private void TowerSold()
    {
        _currentNodeSelected = null;
    }
    // listening new event
    private void OnEnable()
    {
        Node.OnNodeSelected += NodeSelected;
        Node.OnTowerSold += TowerSold;
        TowerCard.OnPlaceTower += PlaceTower;
    }
    private void OnDisable()
    {
        Node.OnNodeSelected -= NodeSelected;
        TowerCard.OnPlaceTower -= PlaceTower;
    }
}
