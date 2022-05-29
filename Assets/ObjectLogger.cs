using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ObjectLogger : MonoBehaviour
{
    public string Directory;
    public string Name;

    private TextWriter writer;

    // Temporary fix... always start the logger; need to change this to have one event start all logging objects
    public void Start()
    {
        Begin();
    }

    // Start is called before the first frame update
    public void Begin()
    {
        OnDestroy();
        int postfix = 0;
        string filename = "";
        do
        {
            filename = Path.Combine(Directory, $"{DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss")}_{Name}_{postfix}.csv");
        } while (File.Exists(filename));
        writer = new StreamWriter(filename);
    }

    // Update is called once per frame
    void Update()
    {
        if (writer != null)
        {
            writer.WriteLine($"{DateTime.Now.Ticks},{transform.position.x},{transform.position.y},{transform.position.z},{transform.rotation.x},{transform.rotation.y},{transform.rotation.z},{transform.rotation.w}");
            writer.Flush();
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
