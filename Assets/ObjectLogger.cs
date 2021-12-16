using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class Utils {
    public static StreamWriter CreateStreamWriter(string Directory, string Name, string Postfix, bool IncludeDate)
    {
        int prefix = 0;
        string filename = "";
        do
        {
            if (IncludeDate)
            {
                filename = Path.Combine(Directory, $"{Name}_{prefix}_{DateTime.Now.ToString("MM-dd-yyyy_H-mm-ss")}.{Postfix}.csv");
            }
            else
            {
                filename = Path.Combine(Directory, $"{Name}_{prefix}.{Postfix}.csv");
            }
        } while (File.Exists(filename));
        return new StreamWriter(filename);
    }
}

public class ObjectLogger : MonoBehaviour
{
    public string Directory;
    public string Name;
    public string Postfix;

    private TextWriter writer;

    public int Records { get; private set; }
    public string LastEntry { get; private set; }

    // Start is called before the first frame update
    public void Begin()
    {
        OnDestroy();
        writer = Utils.CreateStreamWriter(Directory, Name, Postfix, false);
        Records = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (writer != null)
        {
            LastEntry = $"{DateTime.Now.Ticks} {transform.position.x} {transform.position.y} {transform.position.z} {transform.rotation.x} {transform.rotation.y} {transform.rotation.z} {transform.rotation.w}";
            writer.WriteLine(LastEntry);
            writer.Flush();
            Records++;
        }
    }

    private void OnDestroy()
    {
        if (writer != null)
        {
            writer.Dispose();
            writer = null;
        }
    }
}
