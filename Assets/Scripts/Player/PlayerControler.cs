using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PlayerControler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public GraphicRaycaster uiRaycaster;
    public Camera uiCamera;
    [SerializeField] GameObject prefabitemUI;
    [SerializeField] GameObject draggableItem=null;
    private void Awake()
    {
        uiRaycaster=GetComponent<GraphicRaycaster>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        // --- Raycast sobre la UI ---
        List<RaycastResult> results = new List<RaycastResult>();
        uiRaycaster.Raycast(eventData, results);
        foreach (var result in results)                                 //Busca objetos bajo el mouse
        {
            if (result.gameObject.CompareTag("ItemUI"))                 //LogicaRecogeLaSemilla
            {
                ItemUI itemResult = result.gameObject.GetComponent<ItemUI>();
                draggableItem = Instantiate(prefabitemUI, itemResult.transform.position, Quaternion.identity);
                draggableItem.transform.SetParent(transform);
                draggableItem.transform.localScale = Vector3.one * .5f;
                draggableItem.GetComponent<ItemUI>().SetFarmItem(itemResult.GetItem());
            }
        }
    }
    public void OnDrag(PointerEventData eventData)
    {

        if (draggableItem != null)
        {
            draggableItem.transform.position = eventData.position;
        }

    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggableItem != null)
        {
            // Raycast 3D
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    Debug.Log("Hit Ground 3D object: " + hit.collider.name);
                    hit.transform.gameObject.GetComponent<Ground>().SpawnItem(draggableItem.GetComponent<ItemUI>().GetItem());      //Manda la info del item a la tierra
                }
            }
            Destroy(draggableItem);
        }
    }
}
