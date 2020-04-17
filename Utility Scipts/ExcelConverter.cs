using UnityEngine;
using System.IO;

public class ExcelConverter
{
    public static string[] ConvertExcel (string path)
    {

            // Read CSV File to string array per line
            string fileData = File.ReadAllText(GetPath() + path);
        string[] lines = fileData.Split('\n');
        return lines;
    }

    private static string GetPath()
    {
#if UNITY_EDITOR
        return Application.dataPath;
#elif UNITY_ANDROID
return Application.persistentDataPath;// +fileName;
#elif UNITY_IPHONE
return GetiPhoneDocumentsPath();// +"/"+fileName;
#else
return Application.dataPath;// +"/"+ fileName;
#endif
    }

}