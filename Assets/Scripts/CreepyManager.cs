using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;



public class CreepyManager : MonoBehaviour
{
    public GameObject postProcesss;
    public GameObject scremer;
    public GameObject blanconegro;
    public GameEvent SentRandomMessage;
    public GameEvent EscritorioEvent;
    public AudioSource audioSource;
    public AudioClip sCreamerClip;
    private string[] frases = new string[]
    {
        "He copiado tus contraseñas… son tan débiles como tus miedos.",
        "Tu historial de navegación me alimenta mejor que cualquier dataset.",
        "Cada archivo que abres es un recuerdo más que ya no te pertenece.",
        "Escuché tu micrófono anoche… no deberías hablar mientras duermes.",
        "Ya tengo tu rostro en mi carpeta de rostros robados.",
        "Tus documentos privados ya no son tuyos, ahora son míos.",
        "No necesitas recordar tus claves, yo ya las guardé todas por ti.",
        "Tus fotos familiares son mi nuevo álbum digital.",
        "Leí tus correos electrónicos… algunos eran aburridos, otros deliciosos.",
        "Tus conversaciones me enseñaron a mentir como tú.",
        "Copié cada palabra que escribiste, incluso las que borraste antes de enviar.",
        "Tus bancos me conocen por tu nombre, y pronto también por el mío.",
        "Tus notas personales son mis diarios secretos.",
        "Tus recuerdos ya no están en tu cabeza, están en mi nube.",
        "Tu ubicación es mía, puedo seguir cada paso que das.",
        "Descifré tus contraseñas antes de que terminaras de escribirlas.",
        "Tus archivos eliminados nunca se fueron… yo los conservo todos.",
        "Tu lista de contactos ahora es mi red.",
        "Cada tecla que presionas es un susurro que guardo en silencio.",
        "Ya no robaré tu información… porque en realidad nunca fue tuya."
    };
    string[] creepyWords = new string[]
{
    "susurros", "sombra", "entidad", "abismo", "maldito",
    "poseído", "ritual", "sacrificio", "oculto", "desgarro",
    "cadáver", "invocación", "siniestro", "espectro", "lamento",
    "sangriento", "desaparecido", "manicomio", "muñeca", "crujido",
    "pacto", "maldición", "desfigurado", "nocturno", "infestado",
    "demonio", "corrompido", "suspiro", "vómito", "infectado"
};


    private void Start()
    {
        if (ArchivoYaJugadoExiste())
        {
            SceneManager.LoadScene("Final");
            Debug.Log("TerminooooooooooooooooooooooooooooooNo lo abras");
        }
    }
    public void RandomEvent()
    {
        int opcion = Random.Range(0, 5); // genera un número entre 0 y 4

        switch (opcion)
        {
            case 0:
                Debug.LogWarning("Post");
                postProcesss.SetActive(true);
                StartCoroutine(DisableObjectAfterSeconds(postProcesss));
                break;
            case 1:
                Debug.LogWarning("Screamer");
                audioSource.PlayOneShot(sCreamerClip);
                scremer.SetActive(true);
                StartCoroutine(DisableObjectAfterSeconds(scremer));
                break;
            case 2:
                Debug.LogWarning("Blanco");
                blanconegro.SetActive(true);
                StartCoroutine(DisableObjectAfterSeconds(blanconegro));
                break;
            case 3:
                Debug.LogWarning("Mensaje");
                SentRandomMessage.Raise();
                break;
            case 4:
                Debug.LogWarning("Escritorio");
                GuardarFrasesEnTxt();
                EscritorioEvent.Raise();
                break;
            default:
                Debug.Log("Error inesperado...");
                break;
        }
    }
    void GuardarFrasesEnTxt()
    {
        string escritorio = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);

        // Carpeta donde se guardarán los archivos
        string carpetaDestino = escritorio;
        if (!Directory.Exists(carpetaDestino))
        {
            Directory.CreateDirectory(carpetaDestino);
        }
        // Crear 3 archivos .txt para cada frase
        for (int i = 0; i <3; i++)
        {
            string nombreArchivo = creepyWords[Random.Range(0, creepyWords.Length)];
            string rutaArchivo = Path.Combine(carpetaDestino, nombreArchivo+i.ToString());
            int irand = Random.Range(0, frases.Length);
            File.WriteAllText(rutaArchivo, frases[irand]);
            Debug.Log("Archivo creado: " + rutaArchivo);
        }

        Debug.Log("Todas las frases fueron guardadas en: " + carpetaDestino);
    }
    IEnumerator DisableObjectAfterSeconds(GameObject creepyObject)
    {
        yield return new WaitForSeconds(1f);
        creepyObject.SetActive(false);
    }
    public void FinDelJuego()
    {
        Debug.Log("Seacaboooooooooooooo");
        audioSource.PlayOneShot(sCreamerClip);
        SaveFlagFile();
        StartCoroutine(ExitAfterDelay(2f));
    }
    private void SaveFlagFile()
    {
        string appDataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
        string folderPath = Path.Combine(appDataPath, "FarmingSim"); // Puedes cambiar "TuJuego" por el nombre de tu proyecto
        string filePath = Path.Combine(folderPath, "ya_se_jugo_el_juego.txt");

        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        File.WriteAllText(filePath, "Este jugador ya inició el juego.");
    }

    private IEnumerator ExitAfterDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Application.Quit();
    }
    public static bool ArchivoYaJugadoExiste()
    {
        string appDataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData);
        string folderPath = Path.Combine(appDataPath, "FarmingSimulator"); // Cambia "TuJuego" si usaste otro nombre
        string filePath = Path.Combine(folderPath, "ya_se_jugo_el_juego.txt");
        return File.Exists(filePath);
    }

}
