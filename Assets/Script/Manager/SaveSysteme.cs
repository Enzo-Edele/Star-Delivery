using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSysteme : MonoBehaviour
{
    public static void Save(GameManager gameData, int emplacement)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/data" + emplacement + ".save";
        FileStream stream = new FileStream(path, FileMode.Create);

        SaveData data = new SaveData(gameData);

        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static SaveData LoadData(int emplacement)
    {
        string path = Application.persistentDataPath + "/data" + emplacement + ".save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();

            return data;
        }
        else
        {
            Debug.LogError("No save file found in " + path);
            return null;
        }
    }
}
