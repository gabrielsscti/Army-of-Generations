using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
public class FileDataManager
{
    public const string DAY   = "Dia.csv";
    public const string MONTH = "Mes.csv";
    public const string YEAR  = "Ano.csv";

    public static Dictionary<int, Attributes> GetAttributes(string time){
        if ((time != DAY) && (time != MONTH) && (time != YEAR)){
            Debug.Log("Invalid String!");
            return null;
        }

        var res = new Dictionary<int, Attributes>();
        using (var reader = new StreamReader("Assets/Data/" + time)){
            while(!reader.EndOfStream){
                var values        = reader.ReadLine().Split(',');
                var days          = values[0];
                var advantageName = values[1];
                var attributes    = values[2];
                
                var attributesArray = attributes.Split('|');
                var attributeObj    = new Attributes(advantageName, int.Parse(attributesArray[0]), int.Parse(attributesArray[1]), int.Parse(attributesArray[2]), int.Parse(attributesArray[3]));
                
                foreach (string day in days.Split('-')){
                    res.Add(int.Parse(day), attributeObj);
                }
            }
        }
        return res;
    }
}
