using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int reward = 25;
    [SerializeField] int penalty = 25;

    Bank bank;

    void Start()
    {
        bank = FindObjectOfType<Bank>();
    }

    public void Kill()
    {
        bank?.Deposit(reward);
    }

    public void ReachedDestination()
    {
        bank?.Withdraw(penalty);
    }
}
