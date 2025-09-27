using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Data")]

public class PlayerData : ScriptableObject
{
    [SerializeField] private int currentMoney=20;
    public int levels = 0;

    public int GetCoins()
    {
        return currentMoney;
    }
    public bool PayCoins(int prize)
    {
        if (currentMoney>=prize)
        {
            currentMoney -= prize;
            return true;
        }
        else
        {
            return false;
        }
    }
    public void EarnCoins(int newCoins)
    {
        currentMoney += newCoins;
    }
    public void Reset()
    {
        currentMoney = 20;
        levels = 0;
    }
}
