using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FarmItem", menuName = "Farming/Item")]
public class FarmItem : ScriptableObject
{
    [SerializeField]private Texture2D icon;
    [SerializeField] public string ItemName="defaultItem";
    [SerializeField] private GameObject[] ModelsItem;
    [SerializeField] private float baseSpeed=1.0f;
    [SerializeField] private int currentLevel = 0;
    private int MaxLevel = 0;
    public void UpgradeItem()                //desabilita el model previo y habilita el actual
    {
        if (currentLevel < MaxLevel-1)
        {
            currentLevel++;
            Debug.Log("ActualLeveld"+currentLevel);
        }
    }
    public void ResetItem()
    {
        currentLevel = 0;
        MaxLevel = ModelsItem.Length;
    }
    public GameObject GetModel()
    {
        GameObject outputModel = null;
        outputModel = ModelsItem[currentLevel];
        return outputModel;

    }
}
