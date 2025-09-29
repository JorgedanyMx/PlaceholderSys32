using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player/Data")]

public class PlayerData : ScriptableObject
{
    [SerializeField] private int currentMoney=20;
    public GameEvent UpdateUI;
    public int levels = 0;
    public GameEvent[] creepyEvents;    //del 0 al 9 sera eventos repetidos, en adelante, seran eventos que sigan el lore del creepy
    public bool isFirstItem = true;
    public bool isFirstUnlock = true;
    public bool isCreepy = false;
    public int currentCreepyLevel = 1;    
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
            if (currentMoney < 5)
            {
                creepyEvents[0].Raise();//Si no tiene dinero, le da 5 pesos
                currentMoney = 5;
                UpdateUI.Raise();
            }
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
        levels = 4;
        isCreepy = false;
        isFirstUnlock = true;
        isFirstItem = true;
        currentCreepyLevel = 1;
    }
    public void ProgressHistory(int story)
    {
        levels++;
        creepyEvents[story].Raise();
    }
}
/*
 Event 0: Dame Dinero
Event 1: 

Event 5: Primer cultivo
Event 6: Primer Desbloqueo

 */
