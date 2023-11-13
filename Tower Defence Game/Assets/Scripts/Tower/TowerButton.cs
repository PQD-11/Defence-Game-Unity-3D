using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour
{
    [SerializeField] private Tower towerToPlace;

    public void SelectTower()
    {
        TowerManager.Instance.StartTowerPlacement(towerToPlace);
    }
}
