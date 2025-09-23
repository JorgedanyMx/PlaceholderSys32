using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] FarmItem farmItem;
    [SerializeField] GameObject currentModel;
    [SerializeField] private int currentLevel = 0;
    private int maxLevel=0;

    void Start()
    {
        farmItem.ResetItem();
        maxLevel = farmItem.GetMaxLevel();
        UpdateModel();
        StartCoroutine(CoUpdateLevel());
    }
    private void UpdateFarmItem()
    {
        if (currentModel != null)
        {
            UpgradeItem();
            Destroy(currentModel);
            UpdateModel();
            StartCoroutine(CoUpdateLevel());
        }
    }
    private void UpdateModel()
    {
        currentModel = Instantiate(farmItem.GetModel(currentLevel));
        currentModel.transform.SetParent(transform);
        currentModel.transform.localPosition = Vector3.zero;
    }
    IEnumerator CoUpdateLevel() //Delay para actualizar el item
    {
        yield return new WaitForSeconds(farmItem.GetBaseSpeed());
        UpdateFarmItem();
    }
    void UpgradeItem()                //desabilita el model previo y habilita el actual
    {
        if (currentLevel < maxLevel - 1)
        {
            currentLevel++;
            Debug.Log("ActualLeveld" + currentLevel);
        }
        else
        {
            Destroy(gameObject,farmItem.TimeToTakeIt);
        }
    }
    public void SetFarmItem(FarmItem fItem)
    {
        farmItem = fItem;
    }
}
