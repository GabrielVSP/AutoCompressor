using System;

namespace AutoCompressor
{

    public class Logs
    {

        public static event Action<string> onLogAdded;

        static public void AddLog(string message)
        {
            onLogAdded?.Invoke(message);

        }

    }

}