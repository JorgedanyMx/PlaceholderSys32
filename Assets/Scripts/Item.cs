using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] FarmItem farmItem;
    [SerializeField] GameObject currentModel;
    // Start is called before the first frame update

    void Start()
    {
        farmItem.ResetItem();
        UpdateModel();
        StartCoroutine(CoUpdateLevel());
    }
    private void UpdateFarmItem()
    {
        if (currentModel != null)
        {
            farmItem.UpgradeItem();
            Destroy(currentModel);
            UpdateModel();
        }
    }
    private void UpdateModel()
    {
        currentModel = Instantiate(farmItem.GetModel());
        currentModel.transform.SetParent(transform);
        currentModel.transform.localPosition = Vector3.zero;
    }
    IEnumerator CoUpdateLevel()
    {
        // Esperar 1 segundo
        yield return new WaitForSeconds(farmItem.GetBaseSpeed());
        // Imprimir en consola
        UpdateFarmItem();
    }
}
