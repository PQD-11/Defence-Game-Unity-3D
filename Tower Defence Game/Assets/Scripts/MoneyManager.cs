using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    [field: SerializeField] public int CurrentMoney { get; private set; }

    private void Start()
    {
        UIController.Instance.moneyText.text = CurrentMoney.ToString();
    }

    public void GiveMoney(int amount)
    {
        CurrentMoney += amount;
        UIController.Instance.moneyText.text = CurrentMoney.ToString();
    }

    public bool SpendMoney(int amount)
    {
        if (amount <= CurrentMoney)
        {
            CurrentMoney -= amount;
            UIController.Instance.moneyText.text = CurrentMoney.ToString();
            return true;
        }

        return false;
    }
}
