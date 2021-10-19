using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveManager
{
    public static SaveState state;
    public static string file = "savedata.text";

    private static string _path = Application.persistentDataPath + "/" + file;

    public static GameObject player;

    public static void Save(SaveState state)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = File.Create(_path);
        binaryFormatter.Serialize(fileStream, state);
        fileStream.Close();
    }
    public static SaveState Load()
    {
        if (File.Exists(_path))
        {
            try
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                FileStream fileStream = File.Open(_path, FileMode.Open);
                SaveState state = (SaveState)binaryFormatter.Deserialize(fileStream);
                return state;
            }
            catch(SerializationException)
            {
                Debug.Log("Failed to Load");
            }
        }
        else
        {
            Debug.Log("File not Found");
        }
        return null;
    }
}
