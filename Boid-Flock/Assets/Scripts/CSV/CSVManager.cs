using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CSVManager : MonoBehaviour
{
    public static CSVManager Instance { get; private set; }

    //[SerializeField] private string fileName = "";
    //string _filename;
    public bool hasInit = false;
    TextWriter tw;


    void Awake()
    {
      if(Instance != null && Instance != this) { Destroy(this); }
      else { Instance = this; }

      //_filename = Application.dataPath + "/" + fileName + ".csv";
      
    }


    public void InitAndWriteCSV(DataList file)
    {
        string _filename = Application.dataPath + "/" + file.filename + ".csv";

        if (!file.hasInit)
        {
            tw = new StreamWriter(_filename, false);
            tw.Write(file.heading);
            tw.WriteLine("");
            tw.Close();
            file.hasInit = true;
        }

        tw = new StreamWriter(_filename, true);
        //if(file.hasValue) tw.WriteLine(file.objectname + "," + file.value1 + "," + file.value2);
        if (file.isJustString) tw.WriteLine(file.objectname + "," + file.objectinteracted + "," + file.value2);
        tw.Close();
    }


    public void WriteCSV(string filename, string heading, double value, float timeElapsed)
    {
        filename = Application.dataPath + "/New Data/" + filename + ".csv";

        if (!hasInit) { 
            tw = new StreamWriter(filename, false);
            tw.WriteLine(heading);
            tw.Close();
            hasInit = true;
        }
        
        tw = new StreamWriter(filename, true);
        tw.WriteLine(value + "," + timeElapsed);
        tw.Close();
    }
    
}
