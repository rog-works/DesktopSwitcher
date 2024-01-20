using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DesktopSwitcher
{
    class Logger
    {
        enum Levels
        {
            Debug,
            Info,
            Warning,
            Error,
        }

        private static Logger instance = null;
        private List<string> lines = new List<string>();
        private bool alwaysFlush = true;

        public static Logger Get
        {
            get
            {
                if (Logger.instance == null)
                {
                    Logger.instance = new Logger();
                }

                return Logger.instance;
            }
        }

        public static void Debug(string line)
        {
            Logger.Get.Log(Logger.Levels.Debug, line);
        }

        public static void Info(string line)
        {
            Logger.Get.Log(Logger.Levels.Info, line);
        }

        public static void Warning(string line)
        {
            Logger.Get.Log(Logger.Levels.Warning, line);
        }

        public static void Error(string line)
        {
            Logger.Get.Log(Logger.Levels.Error, line);
        }

        private void Log(Levels level, string line)
        {
            var date = DateTime.Now.ToString("yyyy:MM:dd-HH:mm:ss");
            this.lines.Add($"{date} [{level}] {line}");
            Console.WriteLine($"[{level}] {line}");

            if (this.alwaysFlush)
            {
                this.FlushImpl();
                this.lines.Clear();
            }
        }

        public static void Flush()
        {
            Logger.Get.FlushImpl();
        }

        public void FlushImpl()
        {
            var path = Application.ExecutablePath;
            var dir = Path.GetDirectoryName(path);
            var logpath = Path.Combine(dir, "app.log");
            File.AppendAllLines(logpath, this.lines);
        }
    }
}
