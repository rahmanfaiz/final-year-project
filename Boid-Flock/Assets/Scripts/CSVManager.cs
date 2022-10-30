using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVManager : MonoBehaviour
{
    public static CSVManager Instance { get; private set; }

    [SerializeField] private string fileName = "";
    string filename;
    bool hasInit = false;
    TextWriter tw;

    void Awake()
    {
      if(Instance != null && Instance != this) { Destroy(this); }
      else { Instance = this; }

      filename = Application.dataPath + "/" + fileName + ".csv";
      
    }

    public void WriteCSV(string heading, string name, float value)
    {
        if (!hasInit) { 
            tw = new StreamWriter(filename, false);
            tw.WriteLine(heading);
            tw.Close();
            hasInit = true;
        }

        tw = new StreamWriter(filename, true);
        tw.WriteLine(name + "," + value);
        tw.Close();
    }
    
}
