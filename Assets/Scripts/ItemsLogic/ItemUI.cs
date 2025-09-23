using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    Image imageItem=null;
    [SerializeField] FarmItem myItem;
    private void Awake()
    {
        imageItem = GetComponent<Image>();
        if(imageItem.sprite != null)
        {
            if (myItem!=null)
            {
                imageItem.sprite = myItem.icon;

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

}
