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
    Data,
    Input,
    Error,
    Exception,
    CustomException
}


public class LogManager : MonoBehaviour
{
    // TODO Standardize log output
    public string logFileName;

    public static LogManager logInstance { get; private set; }


    void Awake()
    {
        if (logInstance != null && logInstance != this)
        {
            Destroy(this);
        }
        else
        {
            logInstance = this;
        }

        if (logFileName == "")
        {
            logFileName = "log_test.txt";
        }
        logFileName = Application.dataPath + "/Settings/" + logFileName;

        // test runs
        LogInfo("Logger initiated");

        // DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
    }

    void Update()
    {

    }

    public void LogDebug(string message, bool console = false)
    {
        Log(message, LoggerLevel.Debug);
        if (console) { Debug.Log(message); }
    }

    public void LogData(string message, bool console = false)
    {
        Log(message, LoggerLevel.Data);
        if (console) { Debug.Log(message); }
    }
    public void LogInput(string message, bool console = false)
    {
        Log(message, LoggerLevel.Input);
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
