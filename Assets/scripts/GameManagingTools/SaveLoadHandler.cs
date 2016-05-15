using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;

public class SaveLoadHandler : MonoBehaviour {

    
    public PlayerData LocalCopyOfData;
    public bool IsSceneBeingLoaded = false;

    public void SaveData()
    {
        if (!Directory.Exists("Saves"))
            Directory.CreateDirectory("Saves");

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Create("Saves/save.sv");

        LocalCopyOfData = PlayerData.Instance;

        formatter.Serialize(saveFile, LocalCopyOfData);

        saveFile.Close();
    }

    public void LoadData()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream saveFile = File.Open("Saves/save.sv", FileMode.Open);

        LocalCopyOfData = (PlayerData)formatter.Deserialize(saveFile);

        saveFile.Close();
    }
}
