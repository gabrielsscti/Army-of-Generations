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
        Debug.Log(Application.streamingAssetsPath);
        using (var reader = new StreamReader(Application.streamingAssetsPath + "/Data/" + time)){
            while(!reader.EndOfStream){
                var values        = reader.ReadLine().Split(',');
                var days          = values[0];
                var advantageName = values[1];
                var attributes    = values[2];
                
                var attributesArray = attributes.Split('|');
                var attributeObj    = new Attributes(advantageName, int.Parse(attributesArray[0]), int.Parse(attributesArray[1]), int.Parse(attributesArray[2]), int.Parse(attributesArray[3]));
                
                var parsedDays = days.Split('-');
                if (parsedDays.Length > 1){
                    
                    for (int i = int.Parse(parsedDays[0]); i<=int.Parse(parsedDays[1]); i++)
                        res.Add(i, attributeObj);
                }
                else
                    res.Add(int.Parse(days), attributeObj);
            }
        }
        return res;
    }
}
