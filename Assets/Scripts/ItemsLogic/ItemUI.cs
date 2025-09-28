using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemUI : MonoBehaviour
{
    Image imageItem=null;
    [SerializeField] FarmItem myItem;
    [SerializeField] GameObject prizeUI;

    private void Awake()
    {
        imageItem = GetComponent<Image>();
        TMP_Text tmpText=GetComponentInChildren<TMP_Text>();
        if (imageItem.sprite != null)
        {
            if (myItem!=null)
            {
                imageItem.sprite = myItem.icon;
                tmpText.text = myItem.Prize.ToString();
                myItem.ResetItem();
            }
        }

    }
    public void SetImage()
    {
        imageItem.sprite = myItem.icon;
    }
    public void SetFarmItem(FarmItem fItem) 
    {
        //CleanTag();
        myItem = fItem;
        SetImage();
    }
    public FarmItem GetItem()
    {
        return myItem;
    }
    public void HidePrize()
    {
        prizeUI.SetActive(false);
    }
}
