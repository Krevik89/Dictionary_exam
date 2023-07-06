using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary_exam
{
    internal class DictProcessor
    {
        private Dictionary<string, List<string>> _dictionary;
        private DictFile dictFile;

        public DictProcessor()
        {
            dictFile = new DictFile();
            _dictionary = new Dictionary<string, List<string>>();
        }

        public void LoadDictionary(string path) => _dictionary = dictFile.LoadDict(path);

        public void SaveDictionary(string path)
        {
            DictFile dictFile = new DictFile();
            dictFile.SaveDictionary(path, _dictionary);
        }

        public void SaveNewDictionary(string path) => dictFile.SaveNewDictionary(path, _dictionary);

        public void ShowDictionary()
        {
            Console.WriteLine("Словарь:");
            foreach (KeyValuePair<string, List<string>> pair in _dictionary)
            {
                Console.WriteLine($"{pair.Key} - {string.Join(", ", pair.Value)}");
            }
        }

        public void AddWord(string word, List<string> translations)
        {
            if (_dictionary.ContainsKey(word))
            {
                Console.WriteLine($"Слово '{word}' уже существует в словаре.");
                return;
            }
            _dictionary.Add(word, translations);
            Console.WriteLine($"Слово '{word}' добавлено в словарь.");
        }

        public void Remove(string word)
        {
            if (!_dictionary.ContainsKey(word))
            {
                Console.WriteLine($"Слово '{word}' не существует в словаре.");
                return;
            }
            else
            {
                Console.WriteLine("1.Удалить слово: \n2.Удалить перевод:");
                switch (Int32.Parse(Console.ReadLine()))
                {
                    case 1:
                        _dictionary.Remove(word);
                        Console.WriteLine($"Слово  '{word}' удалено из словаря.");
                        return;
                    case 2:
                        if (_dictionary[word].Count == 1)
                        {
                            Console.WriteLine("Нельзя удалить единственный перевод");
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Введите перевод который нужно удалить");
                            string trans = Console.ReadLine();
                            bool containsValue2 = _dictionary[word].Contains(trans);
                            if (containsValue2)
                            {
                                _dictionary[word].Remove(trans);
                            }
                            else
                            {
                                Console.WriteLine($"Такого перевода для слова {word} не найдено");
                            }
                        }
                        break;
                }
            }
        }

        public void EditWord(string word)
        {
            if (!_dictionary.ContainsKey(word))
            {
                Console.WriteLine($"Слово '{word}' не существует в словаре.");
                return;
            }
            else
            {
                Console.WriteLine("1.Изменить слово: \n2.Изменить перевод:");
                switch (Int32.Parse(Console.ReadLine()))
                {
                    case 1:
                        if (_dictionary.ContainsKey(word))
                        {
                            // Создание нового ключа и копирование списка значений со старого ключа
                            Console.WriteLine("Введите новое слово");
                            string newKey = Console.ReadLine();
                            List<string> values = new List<string>(_dictionary[word]);
                            // Удаление записи со старым ключом
                            _dictionary.Remove(word);
                            // Добавление записи с новым ключом и скопированным списком значений
                            _dictionary.Add(newKey, values);
                            Console.WriteLine($"Слово {word} успешно изменено на {newKey}");
                        }
                        return;
                    case 2:
                        Console.WriteLine("Введите перевод который нужно изменить");
                        string trans = Console.ReadLine();
                        bool containsValue2 = _dictionary[word].Contains(trans);
                        if (containsValue2)
                        {
                            string newtrans = Console.ReadLine();
                            List<string> values = _dictionary[word];

                            int index = values.IndexOf(trans);
                            if (index != -1)
                            {
                                // Замена "value2" на "newvalue2" по индексу
                                values[index] = newtrans;
                            }
                            Console.WriteLine($"Перевод для слова '{word}' успешно изменен.");
                        }
                        else
                        {
                            Console.WriteLine($"Такого перевода для слова {word} не найдено");
                        }
                        break;
                }
            }
        }
    }
}
