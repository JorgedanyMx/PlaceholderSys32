using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //Uso TMPro ya que al reescalarlo no pixelea como un texto normal
    
    public Message[] Messages; //arreglo de mensajes, se agregan en el editor
    public Message[] RamdomCMessages; //arreglo de mensajes, se agregan en el editor

    public Sprite[] Avatars; //Same as above, pero con sprites
    public Image avatarImage; //Elemento UI de la imagen
    public TextMeshProUGUI messageText; //Elemento UI del dialogo
    public TextMeshProUGUI avatarName; //Elemento UI del nombre del personaje

    public RectTransform backgroundBox;
    private float tempTime;
    public GameObject messageInPosition;
    public GameObject messageOutPosition;
    
    public LeanTweenType easeInType;
    public LeanTweenType easeOutType;
    public GameObject button;

    [Header("Manager Del Juego")]
    [SerializeField] GameEvent startEvent;
    public GameObject[] UIItems;
    public PlayerData playerData;
    public GameEvent MessageAudioEvent;

    private void Start()
    {
        startEvent.Raise();
    }
    public void RandomCreepyEvent()
    {
        if (!playerData.isCreepy)
            playerData.isCreepy=true;
        GetRandomMessage();
    }
    public void GetRandomMessage() //Obtiene un mensaje al azar, mismo index se usa para mostrar el avatar correspondiente
    {
        var index = Random.Range(0, Messages.Length);
        var message = Messages[index];
        messageText.text = message.MessageString;
        avatarImage.sprite = Avatars[index];
        //avatarName.text = Messages[index].characterName; //Mismo indice de mensaje debe corresponder a la imagen a mostrar
        tempTime = Messages[index].time;
        ShowMessage();
    }

    public void ShowMessage()
    {
        LeanTween.move(backgroundBox.gameObject,messageInPosition.transform,1.0f).setEase(easeInType).setOnComplete(HideMessage);
    }
    public void GetMessage(Dialogs dialog)
    {
        MessageAudioEvent.Raise();
        var index = Random.Range(0, Messages.Length);
        messageText.text = dialog.text;
        avatarImage.sprite = dialog.sprite;
        //avatarName.text = Messages[index].characterName; //Mismo indice de mensaje debe corresponder a la imagen a mostrar
        tempTime = dialog.time;
        ShowMessage();
    }
    public void GetIdxMessage(int indexDiag) //Obtiene un mensaje dado por la funcion
    {
        MessageAudioEvent.Raise();
        var message = Messages[indexDiag];
        messageText.text = message.MessageString;
        //avatarImage.sprite = Avatars[idxSp];
        //avatarName.text = Messages[index].characterName; //Mismo indice de mensaje debe corresponder a la imagen a mostrar
        tempTime = Messages[indexDiag].time;
        if (message.buttonMessage.Length>0)
        {
            
        }
        ShowMessage();
    }
    public void SetIdSprite(int idSp)
    {
        avatarImage.sprite = Avatars[idSp];
    }
    public void GetIdxMessage(int indexDiag,Sprite sp) //Obtiene un mensaje dado por la funcion
    {
        MessageAudioEvent.Raise();
        var message = Messages[indexDiag];
        messageText.text = message.MessageString;
        avatarImage.sprite = sp;
        tempTime = Messages[indexDiag].time;
        if (message.buttonMessage.Length > 0)
        {

        }
        ShowMessage();
    }
    public void HideMessage()
    {
        LeanTween.delayedCall(3.0f,
            () =>
            {
                //Debug.Log("Now Hiding Message");
                LeanTween.move(backgroundBox.gameObject, messageOutPosition.transform, 1.0f).setEase(easeOutType);
            });
    }   
    public void UnlockFarmItemUI(int idx)
    {
        UIItems[idx].SetActive(true);
    }
    public void NoMoney()
    {
        if (!playerData.isCreepy)
        {
            //GetIdxMessage(1);
            GetIdxMessage(1,Avatars[playerData.currentCreepyLevel]);//Mensaje que no tienes nada, felipe
        }
        else
        {
            //GetIdxMessage(2);
            GetIdxMessage(2, Avatars[playerData.currentCreepyLevel]);//Mensaje que no tienes nada, enojao
        }
    }
    public void UnlockApple()
    {
        UIItems[0].SetActive(true);
    }
    public void UnlockSandia()
    {
        UIItems[1].SetActive(true);
    }
}
/*
 Msg 1: NoDineroBueno
Msg 2: NoDineroMalo
Msg 3:  Primer objeto malo
Msg 4: Segundo Objeto Malo
 */

[System.Serializable]
public class Message //Contiene el mensaje a mostrar asi como el nombre del personaje (por si decidimos cursearlo)
{
    public int time;
    public string characterName;
    public string MessageString;
    public string[] buttonMessage;
}

