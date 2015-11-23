namespace GrandTheftApocalpyse.Internal
{
    using System;
    using System.IO;

    /// <summary>
    /// Generic logger class that allows direct logging of anything to a text file
    /// </summary>
    public static class Logger
    {
        public static void Log(object message)
        {
            File.AppendAllText("GrandTheftApocalypse.log", DateTime.Now + " : " + message + Environment.NewLine);
        }
    }
}
