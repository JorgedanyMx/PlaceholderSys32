using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Data")]

public class PlayerData : ScriptableObject
{
    private protected int currentMoney=20;

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
    public void Reset()
    {
        currentMoney = 20;
    }
}
