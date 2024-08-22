using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;

public class BinaryDataStream 
{
    // Método para guardar un objeto serializado en un archivo binario.
    public static void Save<T>(T serializedObject, string fileName)
    {
        // Define el directorio donde se guardarán los archivos.
        string path = Application.persistentDataPath + "/saves";
        // Crea el directorio si no existe.
        Directory.CreateDirectory(path);

        // Crea un BinaryFormatter para serializar el objeto.
        BinaryFormatter formatter = new BinaryFormatter();
        // Crea un FileStream para escribir el archivo binario.
        FileStream fileStream = new FileStream(path + fileName + ".dat", FileMode.Create);

        try
        {
            // Serializa el objeto y lo escribe en el archivo.
            formatter.Serialize(fileStream, serializedObject);
        }
        catch (SerializationException e)
        {
            // Muestra un mensaje de error en la consola si la serialización falla.
            Debug.Log("Save failed. Error: " + e.Message);
        }
        finally
        {
            // Cierra el flujo del archivo, asegurando que se liberen los recursos.
            fileStream.Close();
        }
    }

    // Método para verificar si un archivo binario con un nombre específico existe.
    public static bool Exist (string fileName)
    {
        // Define la ruta completa del archivo.
        string path = Application.persistentDataPath + "/saves/";
        string fullFileName = fileName + ".dat";
        // Devuelve true si el archivo existe, de lo contrario, false.
        return File.Exists(path + fullFileName);
    }

    // Método para leer y deserializar un objeto desde un archivo binario.
    public static T Read <T>(string fileName)
    {
        // Define la ruta completa del archivo.
        string path = Application.persistentDataPath + "/saves/";
        // Crea un BinaryFormatter para deserializar el objeto.
        BinaryFormatter formatter = new BinaryFormatter();
        // Abre el archivo en modo de lectura.
        FileStream fileStream = new FileStream(path + fileName + ".dat", FileMode.Open);
        // Variable para almacenar el objeto deserializado.
        T returnType = default(T);

        try
        {
            // Deserializa el objeto desde el archivo.
            returnType = (T)formatter.Deserialize(fileStream);
        }
        catch (SerializationException e)
        {
            // Muestra un mensaje de error en la consola si la deserialización falla.
            Debug.Log("Read failed. Error: " + e.Message);
        }
        finally
        {
            // Cierra el flujo del archivo, asegurando que se liberen los recursos.
            fileStream.Close();
        }

        // Devuelve el objeto deserializado.
        return returnType;
    }
}
