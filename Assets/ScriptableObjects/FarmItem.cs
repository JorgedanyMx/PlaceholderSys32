using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FarmItem", menuName = "Farming/Item")]
public class FarmItem : ScriptableObject
{
    [SerializeField] public Sprite icon;
    [SerializeField] public string ItemName = "defaultItem";
    [SerializeField] private GameObject[] ModelsItem;
    [SerializeField] public GameObject rottenItem;
    public GameObject creepyModel;
    [SerializeField] private float baseSpeed = 1.0f;
    [SerializeField] int creepyCounter=3;
    private int currentCreepyCounter = 0;
    public float TimeToTakeIt= 2.0f;
    [SerializeField] public int Prize = 5;
    

    public void ResetItem()
    {
        currentCreepyCounter = 0;
    }
    public GameObject GetModel(int currentLevel)
    {
        currentCreepyCounter++; 
        if ((currentCreepyCounter % creepyCounter) == 0)
        {
            return creepyModel;
        }
        else
        {
            GameObject outputModel = ModelsItem[currentLevel];
            return outputModel;
        }

    }
    public float GetBaseSpeed()
    {
        return baseSpeed;
    }
    public int GetMaxLevel()
    {
        return ModelsItem.Length;   
    }
}
