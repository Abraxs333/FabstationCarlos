using UnityEngine;

public static class LogTools
{
    public enum LogType { Angle, Rotation, Minimap, Input, UI, GameManager, StateEvent }

    /// <summary>
    /// Prints a formatted log message with the script name and log type.
    /// </summary>
    public static void Print(object caller, LogType logType, string message)
    {
        string scriptName = caller.GetType().Name; // Extracts the calling script name
        string logPrefix = $"[{logType}] ({scriptName}) → ";

        switch (logType)
        {
           
            default:
                Debug.Log(logPrefix + message);
                break;
        }
    }
}