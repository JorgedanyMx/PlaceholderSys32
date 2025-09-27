using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] FarmItem farmItem;
    [SerializeField] GameObject currentModel;
    [SerializeField] private int currentLevel = 0;
    private int maxLevel=0;
    public bool isReady=false;
    [SerializeField] PlayerData playerData;
    [SerializeField] GameEvent UpdateCoins;

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
            Destroy(currentModel);
            UpgradeItem();
        }
    }
    private void UpdateModel()
    {
        InstantiateNewItem();
    }
    IEnumerator CoUpdateLevel() //Delay para actualizar el item
    {
        yield return new WaitForSeconds(farmItem.GetBaseSpeed());
        Debug.Log("Esta aumentandooo");
        UpdateFarmItem();
    }
    void UpgradeItem()                //desabilita el model previo y habilita el actual
    {
        if (currentLevel < maxLevel - 1)
        {
            currentLevel++;
            UpdateModel();
            StartCoroutine(CoUpdateLevel());
        }
        else
        {
            InstantiateNewItem();
            StopAllCoroutines();
            isReady = true;
            Destroy(gameObject,farmItem.TimeToTakeIt);//tiempo que tarda en desaparecer el item
        }
    }
    public void SetFarmItem(FarmItem fItem)
    {
        farmItem = fItem;
    }
    private void OnMouseDown()
    {
        if (isReady)
        {
            playerData.EarnCoins(farmItem.Prize*2);
            UpdateCoins.Raise();
            Destroy(gameObject);
        }
    }
    void InstantiateNewItem()
    {
        GameObject tmp=null;
        tmp = farmItem.GetModel(currentLevel);
        if (tmp != null)
        {
            currentModel = Instantiate(tmp);
            currentModel.transform.SetParent(transform);
            currentModel.transform.localPosition = Vector3.zero;
        }
        
    }
}
