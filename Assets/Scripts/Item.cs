using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] FarmItem item;
    [SerializeField] GameObject currentModel;
    // Start is called before the first frame update

    void Start()
    {
        item.ResetItem();
        UpdateModel();
    }
    private void OnEnable()
    {
        if (currentModel != null)
        {
            item.UpgradeItem();
            Destroy(currentModel);
            UpdateModel();
        }

    }
    private void UpdateModel()
    {
        currentModel = Instantiate(item.GetModel());
        currentModel.transform.SetParent(transform);
        currentModel.transform.localPosition = Vector3.zero;
    }
}
