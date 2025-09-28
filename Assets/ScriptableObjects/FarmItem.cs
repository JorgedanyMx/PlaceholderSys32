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
    [SerializeField] private int currentCreepyCounter = 0;
    public float TimeToTakeIt= 2.0f;
    [SerializeField] public int Prize = 5;
    

    public void ResetItem()
    {
        currentCreepyCounter = 0;
    }
    public GameObject GetModel(int currentLevel)
    {
        GameObject outputModel = ModelsItem[currentLevel];
        return outputModel;
    }
    public float GetBaseSpeed()
    {
        return baseSpeed;
    }
    public int GetMaxLevel()
    {
        return ModelsItem.Length;   
    }
    public bool UpdateCounter()
    {
        if (creepyModel == null)
        {
            return false;
        }
        if (creepyCounter == 0)
        {
            return false;
        }
        currentCreepyCounter++;
        return currentCreepyCounter % creepyCounter == 0;
    }
}
