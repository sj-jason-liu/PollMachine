using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem
{
    public static void SaveData (DataInputPanel data)
    {
        BinaryFormatter formatter = new BinaryFormatter(); //create a new binary formatter to store data
        string path = Application.persistentDataPath + "/data.atomylist"; //save to application location
        FileStream stream = new FileStream(path, FileMode.Create); //create filestream to create a file

        ListData listData = new ListData(data); //create a data to write into file

        formatter.Serialize(stream, listData); //serialize the file into binary file
        stream.Close(); //close file stream
    }

    public static ListData LoadData()
    {
        string path = Application.persistentDataPath + "/data.atomylist";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            
            ListData listData = formatter.Deserialize(stream) as ListData;
            stream.Close();

            return listData;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
