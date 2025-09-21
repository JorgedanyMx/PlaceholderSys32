using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;


public class TestPCInteraction : MonoBehaviour
{
    string desktopPath = "";
    void Awake()
    {
        desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        SaveTxt();
    }
    // Start is called before the first frame update
    private void OnEnable()
    {
        Debug.Log("Usuario del SO:"+GetUser());
    }

    private String GetUser()
    {
        string user = Environment.UserName;
        return user;
    }
    private void SaveImage()
    {
        // Ruta al escritorio
        string filePath = Path.Combine(desktopPath, "screenshot.png");

        // Capturar la pantalla
        Texture2D tex = ScreenCapture.CaptureScreenshotAsTexture();
        byte[] bytes = tex.EncodeToPNG();
        File.WriteAllBytes(filePath, bytes);

        Debug.Log("Imagen guardada en: " + filePath);

        // Liberar memoria
        Destroy(tex);
    }
    private void SaveTxt()
    {
        string path = desktopPath + "/Jajaesuntexto.txt";
        string linea = "Nueva entrada: " + System.DateTime.Now+"\nEste es un crype file";

        File.AppendAllText(path, linea + "\n"); // añade al final
    }

    private void ReadText(){
        string path = Application.persistentDataPath + "/miArchivo.txt";
        if (File.Exists(path)){
            string contenido = File.ReadAllText(path);
            Debug.Log("Contenido: " + contenido);
        }
        else{
            Debug.LogWarning("El archivo no existe en: " + path);
        }
    }

}
