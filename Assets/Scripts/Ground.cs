using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] GameObject refObj;
    [SerializeField] GameObject prefabItem;
    [SerializeField] PlayerData playerData;
    [SerializeField] GameEvent UpdateCoins;
    public void SpawnItem(FarmItem fItem)
    {
        if (playerData.PayCoins(fItem.Prize))
        {
            UpdateCoins.Raise();
            GameObject tmpObj = Instantiate(prefabItem);
            tmpObj.transform.SetParent(refObj.transform);
            tmpObj.transform.localPosition = Vector3.zero;
            tmpObj.GetComponent<Item>().SetFarmItem(fItem);

        }
        
    }
}
