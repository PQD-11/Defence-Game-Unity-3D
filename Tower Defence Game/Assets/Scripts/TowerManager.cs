using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;
    public Tower towerActive;
    [SerializeField] private Transform indicator;
    public bool isPlacing;
    public LayerMask layerMaskPlacement, layerMaskObstacle;
    public float topSafePercent = 10f;
    public Tower selectTower;
    private Tower placeTower;

    private void Awake()
    {
        Instance = this; //one scene
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {

                if (hit.collider.gameObject.GetComponent<Tower>() == null && selectTower != null)
                {
                    selectTower.rangeModel.SetActive(false);
                    UIController.Instance.TowerUpgradePanelOff();
                    selectTower = null;
                }
            }
        }


        if (isPlacing)
        {
            indicator.position = GetGridPosition();

            if (Input.mousePosition.y > Screen.height * (1f - topSafePercent / 100f))
            {
                indicator.gameObject.SetActive(false);
            }
            else if (Physics.OverlapSphereNonAlloc(indicator.position, 1f, new Collider[1], layerMaskObstacle) != 0)
            {
                indicator.gameObject.SetActive(false);
            }
            else
            {
                indicator.gameObject.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                {
                    if (MoneyManager.Instance.SpendMoney(towerActive.price))
                    {
                        isPlacing = false;

                        placeTower.enabled = true;
                        placeTower.GetComponent<Collider>().enabled = true;
                        placeTower.rangeModel.SetActive(false);

                        // Instantiate(towerActive, indicator.position, towerActive.transform.rotation);

                        // indicator.gameObject.SetActive(false);
                        indicator = null;

                        AudioManager.Instance.PlaySFX(8);
                    }
                }
                else if (Input.GetMouseButtonDown(1))
                {
                    isPlacing = false;
                    Destroy(placeTower.gameObject);
                    indicator = null;
                }
            }
        }
    }
    public void StartTowerPlacement(Tower towerToPlace)
    {
        towerActive = towerToPlace;

        if (MoneyManager.Instance.CurrentMoney < towerActive.price)
        {
            StartCoroutine(ActivateAndDeactivate());
            towerActive = null;
            return;
        }

        isPlacing = true;

        // indicator.gameObject.SetActive(true);
        // Destroy(indicator.gameObject);
        placeTower = Instantiate(towerActive);
        placeTower.enabled = false;
        placeTower.GetComponent<Collider>().enabled = false; // co the toi uu bang cach chi dung mot instantiate
        placeTower.rangeModel.SetActive(true);

        indicator = placeTower.transform;
        // placeTower.rangeModel.transform.localScale = new Vector3(placeTower.rangeDetect, 1f, placeTower.rangeDetect);
    }


    // Get position 
    private Vector3 GetGridPosition()
    {
        Vector3 location = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 200f, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f, layerMaskPlacement))
        {
            location = hit.point;
            location.y = 0;
        }

        return location;
    }

    IEnumerator ActivateAndDeactivate()
    {
        UIController.Instance.notEnoughMoneyWarning.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        UIController.Instance.notEnoughMoneyWarning.SetActive(false);
    }
}
