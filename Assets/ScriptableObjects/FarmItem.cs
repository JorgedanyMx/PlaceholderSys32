using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FarmItem", menuName = "Farming/Item")]
public class FarmItem : ScriptableObject
{
    [SerializeField] public Sprite icon;
    [SerializeField] public string ItemName = "defaultItem";
    [SerializeField] private GameObject[] ModelsItem;
    [SerializeField] private float baseSpeed = 1.0f;
    public float TimeToTakeIt= 2.0f;
    [SerializeField] public int Prize = 5;

    public void ResetItem()
    {

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
}
