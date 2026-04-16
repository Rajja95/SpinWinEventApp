using System.IO;
using System.Text;
using UnityEngine;
using RUS95.SpinWinEventApp.Data;

namespace RUS95.SpinWinEventApp.Systems.Persistence
{
    public class CsvDataSaver
    {
        #region Fields

        private readonly string _filePath;
        private const string Header = "DateTime,Result,Name,Email,Phone";

        #endregion

        #region Constructor

        public CsvDataSaver()
        {
            _filePath = Path.Combine(Application.persistentDataPath, "SpinWinData.csv");
            EnsureFileExists();
        }

        #endregion

        #region Public Methods

        public void Save(GameSessionData data)
        {
            string line = ConvertToCsvLine(data);

#if UNITY_WEBGL
            var saver = new WebGLFileSaver();
            saver.Save(Header + "\n" + line);
#else
    File.AppendAllText(_internalPath, line + "\n", Encoding.UTF8);
#endif
        }

        #endregion

        #region Private Methods

        private void EnsureFileExists()
        {
            if (!File.Exists(_filePath))
            {
                File.WriteAllText(_filePath, Header + "\n");
            }
        }

        private string ConvertToCsvLine(GameSessionData data)
        {
            string dateTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string result = data.IsWin ? "WIN" : "LOSE";

            return $"{dateTime},{result},{Escape(data.Name)},{Escape(data.Email)},{Escape(data.Phone)}";
        }

        private string Escape(string value)
        {
            if (string.IsNullOrEmpty(value))
                return "";

            // Handle commas safely
            if (value.Contains(","))
                return $"\"{value}\"";

            return value;
        }

        #endregion
    }
}