using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class saver 
{
    

    public static void SavePlayer (GameManager player)
    {
        string path = Application.persistentDataPath + "/player.leaderboards";
        BinaryFormatter formatter = new BinaryFormatter();

        FileStream stream = new FileStream(path, FileMode.Create);


        PlayerData data = new PlayerData(player);

        formatter.Serialize(stream, data);
        stream.Close();

        /*if (!File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();

            FileStream stream = new FileStream(path, FileMode.Create);


            PlayerData data = new PlayerData(player);

            formatter.Serialize(stream, data);
            stream.Close();
        }*/
        /*else if (File.Exists(path)) 
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = formatter.Deserialize(stream) as PlayerData;

            FileStream addition = new FileStream(path, FileMode.Append);

            data = new PlayerData(player);
            formatter.Serialize(stream, data);
            stream.Close();
        }*/
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.leaderboards";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else 
        {
            Debug.LogError("Save file not found in" + path);
            return null;
        }
    }
}
