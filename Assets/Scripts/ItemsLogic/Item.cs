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
    [SerializeField] AudioSource audioItem;
    [SerializeField] AudioClip[] audioclips;
    bool isCreepy = false;
    [Header("CreepyEvent")]
    [SerializeField] GameEvent CreepyEvent;
    void Start()
    {
        audioItem = GetComponent<AudioSource>();
        maxLevel = farmItem.GetMaxLevel();
        UpdateModel();
        StartCoroutine(CoUpdateLevel());
        playSFX(audioclips[0]);

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
        UpdateFarmItem();
    }
    void UpgradeItem()                //desabilita el model previo y habilita el actual
    {
        playSFX(audioclips[1]);
        if (currentLevel < maxLevel - 1)
        {
            currentLevel++;
            UpdateModel();
            StartCoroutine(CoUpdateLevel());
        }
        else
        {
            isReady = true;
            isCreepy = farmItem.UpdateCounter();
            InstantiateNewItem();
            StopAllCoroutines();
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
            if (isReady & isCreepy)
            {
                tmp = farmItem.creepyModel;
                CreepyEvent.Raise();
                Debug.Log("farm es: " + isReady + " y creepy: " + isCreepy);
            }
            currentModel = Instantiate(tmp);
            currentModel.transform.SetParent(transform);
            currentModel.transform.localPosition = Vector3.zero;
        }
    }
    void playSFX(AudioClip clip)
    {
        float pitchoffset = Random.Range(0f, .4f);
        audioItem.pitch = .8f + pitchoffset;
        audioItem.PlayOneShot(clip);
    }
}
