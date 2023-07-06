using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary_exam
{
    internal class DictFile
    {
        private string _pathDir = Directory.GetCurrentDirectory();
        public List<string> GetDictionaryPaths() => Directory.GetFiles(_pathDir, "*.txt").ToList();
        //метод для загрузки словаря
        public Dictionary<string, List<string>> LoadDict(string path)
        {
            if (!File.Exists(path))
            {
                return new Dictionary<string, List<string>>();
            }
            Dictionary<string, List<string>> dict = new Dictionary<string, List<string>>();
            using (StreamReader sr = new StreamReader(path))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(':');
                    string key = parts[0];
                    string[] values = parts[1].Split(',');
                    List<string> valueList = new List<string>();
                    foreach (string value in values)
                    {
                        valueList.Add(value);
                    }
                    dict.Add(key, valueList);
                }
            }
            return dict;
        }
        //метод для сохранения жмякается пользователем
        public void SaveDictionary(string path, Dictionary<string, List<string>> dictionary)
        {
            using (StreamWriter writer = new StreamWriter(path + ".txt", false))
            {
                foreach (KeyValuePair<string, List<string>> entry in dictionary)
                {
                    string key = entry.Key;
                    List<string> values = entry.Value;       
                    string valueString = string.Join(",", values);
                    writer.WriteLine($"{key}: {valueString}");
                }
            }
        }
        //метод для сохранения в другой файл
        public void SaveNewDictionary(string path, Dictionary<string, List<string>> dictionary)
        {
            using (StreamWriter writer = new StreamWriter(path + ".txt"))
            {
                foreach (KeyValuePair<string, List<string>> entry in dictionary)
                {
                    string key = entry.Key;
                    List<string> values = entry.Value;
                    string valueString = string.Join(",", values);
                    writer.WriteLine($"{key}: {valueString}");
                }
            }
        }
    }
}
