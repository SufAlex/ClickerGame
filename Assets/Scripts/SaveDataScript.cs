using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

//[Serializable]
class SaveDataScript
{ 
    public static List<Tuple<string,int>> records = new List<Tuple<string, int>>();

    static public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.OpenWrite(Application.persistentDataPath 
                                         + "/MySaveData.dat"); 
        
        bf.Serialize(file, records);
        file.Close();
        //Debug.Log("Records saved!");
    }
    
    static public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/MySaveData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.OpenRead(Application.persistentDataPath 
                                            + "/MySaveData.dat");
        
            records = (List<Tuple<string,int>>)bf.Deserialize(file);
        
            file.Close();
            //Debug.Log("Records loaded!");
        }

    }
    static public void AddRecord(int score)
    {
        records.Add(Tuple.Create(DateTime.Now.ToString(),score));
    }
    
    static public void PrintToLog()
    {
        //Debug.Log("Records:");
        //Debug.Log("persistentDataPath:" + Application.persistentDataPath);
        foreach (var rec in records)
        {
            Debug.Log(rec.Item1 + " " + rec.Item2);
        }
    }
    
    static public void PrintToText(Text t)
    {
        t.text = "";
        foreach (var rec in records)
        {
            t.text += rec.Item1 + " " + rec.Item2 + "\n";
        }
    }
}
