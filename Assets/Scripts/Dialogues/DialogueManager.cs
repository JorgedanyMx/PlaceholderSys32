using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //Uso TMPro ya que al reescalarlo no pixelea como un texto normal
    
    public Message[] Messages; //arreglo de mensajes, se agregan en el editor
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

    [SerializeField] GameEvent startEvent;

    private void Start()
    {
        startEvent.Raise();
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
        Debug.Log("Showing Message");
    }
    public void GetMessage(Dialogs dialog)
    {
        var index = Random.Range(0, Messages.Length);
        messageText.text = dialog.text;
        avatarImage.sprite = dialog.sprite;
        //avatarName.text = Messages[index].characterName; //Mismo indice de mensaje debe corresponder a la imagen a mostrar
        tempTime = dialog.time;
        ShowMessage();
    }
    public void HideMessage()
    {
        LeanTween.delayedCall(3.0f,
            () =>
            {
                Debug.Log("Now Hiding Message");
                LeanTween.move(backgroundBox.gameObject, messageOutPosition.transform, 1.0f).setEase(easeOutType);
            });
    }   
}

[System.Serializable]
public class Message //Contiene el mensaje a mostrar asi como el nombre del personaje (por si decidimos cursearlo)
{
    public int time;
    public string characterName;
    public string MessageString;    
}

