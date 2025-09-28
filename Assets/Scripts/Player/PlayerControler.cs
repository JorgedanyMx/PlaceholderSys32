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
    [SerializeField] PlayerData playerData;
    [SerializeField] FarmItem[] farmItems;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        uiRaycaster=GetComponent<GraphicRaycaster>();
        playerData.Reset();
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
                draggableItem.GetComponent<ItemUI>().HidePrize();
            }
        }
    }
    public void OnDrag(PointerEventData eventData)              //arrastra la imagen a la tierra
    {

        if (draggableItem != null)
        {
            draggableItem.transform.position = eventData.position;
        }

    }
    public void OnEndDrag(PointerEventData eventData)           //Manda un Raycast hacia la tierra
    {
        if (draggableItem != null)
        {
            // Raycast 3D
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.CompareTag("Ground"))
                {
                    hit.transform.gameObject.GetComponent<Ground>().SpawnItem(draggableItem.GetComponent<ItemUI>().GetItem());      //Manda la info del item a la tierra
                }
            }
            Destroy(draggableItem);
        }
    }
    void playSFX(AudioClip clip)
    {
        float pitchoffset = Random.Range(0f, .4f);
        audioSource.pitch = .8f + pitchoffset;
        audioSource.PlayOneShot(clip);
    }
}
