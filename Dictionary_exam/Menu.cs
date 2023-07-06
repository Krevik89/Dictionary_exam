using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dictionary_exam
{
    internal class Menu
    {
        private string[] _menuMain = { "Создать", "Посмотреть словари", "Выход" };
        private string[] _menuDict = { "Посмотреть сожержимое словаря", "Добавить слово-перевод", "Изменить перевод-слово",
        "Удалить слово-перевод","Сохранить изменения", "Сохранить словарь в другой файл", "Вернутся назад" };
        private string _dictName;
        private DictFile _dictFile;
        private DictProcessor _dictProcessor;

        public Menu()
        {
            _dictFile = new DictFile();
            _dictProcessor = new DictProcessor();
        }

        public void Run()
        {
            Console.WriteLine("Добро пожаловать в приложение словари!");
            while (true)
            {
                switch (ShowMenu(_menuMain))
                {
                    case 1:
                        Console.Clear();
                        CreateDictionary();
                        break;
                    case 2:
                        Console.Clear();
                        ViewDictionaries();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Пока!");
                        return;
                }
            }
        }
        private int ShowMenu(string[] menu)
        {
            Console.WriteLine();
            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {menu[i]}");
            }
            Console.Write("Выберите пункт: ");
            return Int32.Parse(Console.ReadLine());
        }
        private void CreateDictionary()
        {
            Console.Write("Введите название словаря: ");
            _dictName = Console.ReadLine();
            string path = Directory.GetCurrentDirectory() + "\\" + _dictName + ".txt";
            if (File.Exists(path))
            {
                Console.WriteLine($"Словарь '{_dictName}' с таким именем уже существует.");
                return;
            }
            File.Create(_dictName + ".txt").Close(); 
            Console.WriteLine($"Словарь '{_dictName}' успешно создан.");
            DictionaryMenu(path);
        }
        private void ViewDictionaries()
        {
            Console.WriteLine("Выберите словарь:");
            List<string> dictPaths = _dictFile.GetDictionaryPaths();

            if (dictPaths == null || dictPaths.Count == 0)
            {
                Console.WriteLine("Нет словарей для просмотра.");
                return;
            }

            for (int i = 0; i < dictPaths.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Path.GetFileNameWithoutExtension(dictPaths[i])}");
            }
            Console.Write("Введите свой выбор: ");
            int selected = Int32.Parse(Console.ReadLine());
            string path = dictPaths[selected - 1];

            _dictProcessor.LoadDictionary(path);
            DictionaryMenu(path);
        }
        private void DictionaryMenu(string path)
        {
            while (true)
            {
                int selected = ShowMenu(_menuDict);
                switch (selected)
                {
                    case 1:
                        Console.Clear();
                        _dictProcessor.ShowDictionary();
                        break;
                    case 2:
                        Console.Clear();
                        AddWord();
                        break;
                    case 3:
                        Console.Clear();
                        EditWord();
                        break;
                    case 4:
                        Console.Clear();
                        Remove();
                        break;
                    case 5:
                        Console.Clear();
                        _dictProcessor.SaveDictionary(_dictName);
                        break;
                    case 6:
                        Console.Clear();
                        SaveAsNewDictionary();
                        break;
                    case 7:
                        return;
                }
            }
        }
        private void AddWord()
        {
            Console.Write("Введите слово: ");
            string word = Console.ReadLine();
            Console.Write("Введите переводы слова (через запятую): ");
            string translationsInput = Console.ReadLine();
            List<string> translations = new List<string>();
            if (!string.IsNullOrEmpty(translationsInput))
            {
                translations = new List<string>(translationsInput.Split(','));
            }
            _dictProcessor.AddWord(word, translations);
        }
        private void EditWord()
        {
            Console.Write("Введите слово для удаления: ");
            _dictProcessor.EditWord(Console.ReadLine());
        }
        private void Remove()
        {
            Console.Write("Введите слово для удаления: ");
            _dictProcessor.Remove(Console.ReadLine());
        }
        private void SaveAsNewDictionary()
        {
            Console.Write("Введите название файла куда сохранить: ");
            string path = Console.ReadLine();
            if (string.IsNullOrEmpty(path))
            {
                Console.WriteLine("Имя файла не может быть пустым.");
                return;
            }
            if (File.Exists(path))
            {
                Console.WriteLine($"Словарь '{path}' уже существует.");
                return;
            }
            _dictProcessor.SaveNewDictionary(path);
            Console.WriteLine($"Новый словарь '{path}' успешно сохранен.");
        }
    }
}
