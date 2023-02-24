using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;


public enum LoggerLevel
{
    Debug,
    Info,
    Error,
    Exception,
    CustomException
}


public class Logger : MonoBehaviour
{
    public string logFileName;

    void Start()
    {
        logFileName = "log_test.txt";

        // test runs
        LogInfo("Logger initiated");
    }

    void Update()
    {
        
    }

    public void LogDebug(string message, bool console = false)
    {
        Log(message, LoggerLevel.Debug);
        if (console) { Debug.Log(message); }
    }

    public void LogException(string message, bool console = false)
    {
        Log(message, LoggerLevel.Exception);
        if (console) { Debug.Log(message); }
    }

    public void LogCustomException(string message, bool console = false)
    {
        Log(message, LoggerLevel.CustomException);
        if (console) { Debug.Log(message); }
    }

    public void LogError(string message, bool console = false)
    {
        Log(message, LoggerLevel.Error);
        if (console) { Debug.Log(message); }
    }

    public void LogInfo(string message, bool console = false)
    {
        Log(message, LoggerLevel.Info);
        if (console) { Debug.Log(message); }
    }

    private void Log(string message, LoggerLevel level)
    {
        using (System.IO.StreamWriter sw = System.IO.File.AppendText(this.logFileName))
        {
            string levelString = $"{level} ".PadRight(17, '-');
            sw.Write($"{level} ");
            sw.WriteLine($"& {DateTime.UtcNow.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture)}");
            string padding = "".PadLeft(10, ' ');
            sw.WriteLine($"{padding}{message}");
            sw.WriteLine($"-----\r\n");
        }
    }
}
