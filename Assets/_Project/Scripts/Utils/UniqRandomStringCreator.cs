using UnityEngine;

public static class UniqRandomStringCreator 
{
    public static string GetUniqString()
    {
        string key = Random.Range(1000, 9999) + "-" + Random.Range(1000, 9999) + "-" + 
                     Random.Range(1000, 9999) + "-" + Random.Range(0, 9) + "-";
        return key;
    }
}
