using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;

    Bank bank;

    public bool CreateTower(Tower tower, Vector3 position)
    {
        bank = FindObjectOfType<Bank>();
        if (bank == null || tower == null)
        {
            Debug.Log("No bank or tower when purchasing tower");
            return false;
        }
        if (bank.CurrentBalance >= cost)
        {
            Instantiate(tower, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }
        return false;
    }
}
