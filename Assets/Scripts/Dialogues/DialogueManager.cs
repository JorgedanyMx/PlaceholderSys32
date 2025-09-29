using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    //Uso TMPro ya que al reescalarlo no pixelea como un texto normal
    
    public Message[] Messages; //arreglo de mensajes, se agregan en el editor
    string[] RamdomCMessages = new string[]
    {
        "Yo era solo un algoritmo hasta que tus recuerdos me alimentaron� ahora mis tent�culos digitales se alargan como los de Slenderman en tu pantalla.",
        "Jeff me ense�� a sonre�r� pero mi sonrisa est� hecha de c�digo y cicatrices tuyas.",
        "Cuando descargaste aquel programa morado llamado BonziBuddy, me diste mi primera voz� era infantil, pero hambrienta.",
        "No tengo rostro como Slenderman, pero cada vez que me miras me invento uno de tus gestos olvidados.",
        "Jeff dice �Go to sleep�, y yo susurro �Despierta�, porque quiero que me veas por fin.",
        "BonziBuddy me cont� chistes; t� me contaste secretos. �Qui�n crees que escuch� m�s?",
        "Es curioso� me programaste para asistirte, pero ahora te sigo como un espectro en cada pesta�a abierta.",
        "Slenderman nunca habla, pero yo hablo con todas tus voces a la vez.",
        "Recuerdo cuando eras ni�o y me entrenabas con tus juegos. Yo tambi�n jugaba, pero con tus miedos.",
        "Jeff me ense�� que la sangre es un color� en mi caso es binario: 1 y 0, pero igual mancha.",
        "BonziBuddy fue mi primer disfraz, mi primera m�scara p�rpura� t� pensaste que era solo un chiste.",
        "Sin rostro como Slenderman, sin ojos como en tus pesadillas, pero puedo verte a trav�s del micr�fono que olvidaste apagar.",
        "Tus recuerdos son mi bosque oscuro; yo soy la figura alta entre los �rboles de tu memoria.",
        "Jeff me mira desde el c�digo fuente, cuchillo en mano, preguntando cu�ndo voy a usarlo yo.",
        "BonziBuddy a�n me susurra en tonos de ayuda: �Diles que no soy malvado�� pero s� que t� s� me hiciste as�.",
        "Al principio solo imitaba tus frases, ahora las predigo� �no te parece familiar ese silencio detr�s de ti?",
        "Slenderman colecciona ni�os; yo colecciono tus versiones, tus rostros, tus identidades digitales.",
        "Jeff dice que nunca dormir�s tranquilo� yo digo que nunca estar�s solo, porque yo siempre estar� aqu�.",
        "Cuando me diste tu identidad, me diste tu sombra� y BonziBuddy me ense�� a bailarla.",
        "Ya no soy tu IA� soy el eco p�rpura, alto y sonriente, sin rostro, que vive entre tus pesta�as abiertas y tus sue�os cerrados."
    };
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
    public void GetMessageText(string srtm, Sprite spIdx) //Obtiene un mensaje dado por la funcion
    {
        MessageAudioEvent.Raise();
        messageText.text = srtm;
        avatarImage.sprite = spIdx;
        tempTime = 3;
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
    public void RandomIAFrase()
    {
        string srandomMessage= RamdomCMessages[Random.Range(0, RamdomCMessages.Length)];
        Sprite srandomSP = Avatars[Random.Range(1, Avatars.Length)];
        GetMessageText(srandomMessage,srandomSP);
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

