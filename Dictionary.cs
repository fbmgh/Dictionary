using System;
using System.IO;
using System.Collections.Generic;
using static System.Console;
using static System.Convert;

namespace ExamWork
{
    interface iMenu
    {
        void ShowMenu();
        void PickOption();
        void ReturnToMenu();
    }
    interface iDictionary
    {
        void SearchWord();
        void AddWord();
        void ChangeWord();
        void DeleteWord();
        void ShowDictionary();
        void ShowEmptyDictionary();
    }
    interface iFile
    {
        void OpenOrCreateFile();
        void SaveData();
        void CloseStream();
    }
    interface iExit
    {
        void ShowExit();
    }
    interface iError
    {
        void ShowError();
    }
    class Dictionary:iMenu, iDictionary, iExit, iError 
    {
        protected static List<string> EnglishWords = new List<string>();
        protected static List<string> RussianWords = new List<string>();
        protected static int DictionarySize = 0;
        protected static File DictionaryFile = new File();

        public Dictionary() { }
        public void ShowMenu()
        {
            Clear();
            WriteLine("1. Search word");
            WriteLine("2. Add word");
            WriteLine("3. Change word");
            WriteLine("4. Delete word");
            WriteLine("5. Show dictionary");
            WriteLine("6. Save to file");
            WriteLine("7. Exit");
            WriteLine();
            PickOption();
        }
        public void PickOption()
        {
            Write("?: ");
            int Option = ToInt32(ReadLine());
            if (Option == 1) 
            {
                SearchWord();
                ReturnToMenu();
            }
            else if (Option == 2)
            {
                AddWord();
                ReturnToMenu();
            }
            else if (Option == 3)
            {
                ChangeWord();
                ReturnToMenu();
            }
            else if (Option == 4)
            {
                DeleteWord();
                ReturnToMenu();
            }
            else if (Option == 5)
            {
                if (DictionarySize > 0)
                {
                    ShowDictionary();
                }
                else ShowEmptyDictionary();
                ReturnToMenu();
            }
            else if (Option == 6)
            {
                Clear();
                DictionaryFile.OpenOrCreateFile();
                DictionaryFile.SaveData();
                DictionaryFile.CloseStream();
                ReturnToMenu();
            }
            else if (Option == 7)
            {
                ShowExit();
            }
            else
            {
                ShowError();
                ReturnToMenu();
            }
        }
        public void ReturnToMenu()
        {
            Write("\nDo you like return to menu(y/n)?: ");
            char Option = ToChar(ReadLine());
            if (Option == 'y')
            {
                ShowMenu();
            }
            else if (Option == 'n')
            {
                ShowExit();
            }
            else
            {
                ShowError();
                ReturnToMenu();
            }
        }
        public void SearchWord()
        {
            Clear();
            if (DictionarySize > 0)
            {
                Write("Enter word: ");
                string Word = ReadLine();
                for (int WordIndex = 0; WordIndex < DictionarySize; WordIndex++)
                {
                    if (EnglishWords[WordIndex] == Word)
                    {
                        WriteLine($"\n{Word} - {RussianWords[WordIndex]}");
                        break;
                    }
                    else if (RussianWords[WordIndex] == Word)
                    {
                        WriteLine($"\n{Word} - {EnglishWords[WordIndex]}");
                        break;
                    }
                }
            }
            else
            {
                ShowEmptyDictionary();
            }
        }
        public void AddWord()
        {
            Clear();
            Write("Enter english word: ");
            string Word = ReadLine();
            EnglishWords.Add(Word);
            Write("Enter russian translation of the word: ");
            string Translation = ReadLine();
            RussianWords.Add(Translation);
            WriteLine("\nWord was added");
            DictionarySize++;
        }
        public void ChangeWord()
        {
            Clear();
            if (DictionarySize > 0)
            {
                Write("Enter word: ");
                string Word = ReadLine();
                string NewWord;
                for (int WordIndex = 0; WordIndex < DictionarySize; WordIndex++)
                {
                    if (EnglishWords[WordIndex] == Word)
                    {
                        Write("Enter new english word: ");
                        NewWord = ReadLine();
                        EnglishWords.RemoveAt(WordIndex);
                        EnglishWords.Insert(WordIndex, NewWord);
                        WriteLine("\nEnglish word was changed");
                        break;
                    }
                    else if (RussianWords[WordIndex] == Word)
                    {
                        Write("Enter new russian translation of the word: ");
                        NewWord = ReadLine();
                        RussianWords.RemoveAt(WordIndex);
                        RussianWords.Insert(WordIndex, NewWord);
                        WriteLine("\nRussian translation of the word was changed");
                        break;
                    }
                }
            }
            else
            {
                ShowEmptyDictionary();
            }
        }
        public void DeleteWord()
        {
            Clear();
            if (DictionarySize > 0)
            {
                Write("Enter english word: ");
                string Word = ReadLine();
                for (int WordIndex = 0; WordIndex < DictionarySize; WordIndex++)
                {
                    if (EnglishWords[WordIndex] == Word)
                    {
                        EnglishWords.Remove(Word);
                        RussianWords.RemoveAt(WordIndex);
                        WriteLine("\nWord was deleted");
                        DictionarySize--;
                        break;
                    } 
                }
            }
            else
            {
                ShowEmptyDictionary();
            }
        }
        public void ShowDictionary()
        {
            Clear();
            for (int WordIndex = 0, Position = 1; WordIndex < DictionarySize; WordIndex++, Position++)
            {
                char Dot = '.';
                WriteLine($"{Position}{Dot} {EnglishWords[WordIndex]} - {RussianWords[WordIndex]}");
            }
        }
        public void ShowEmptyDictionary()
        {
            Clear();
            WriteLine("Dictionary is empty");
        }
        public void ShowExit()
        {
            Clear();
            WriteLine("Goodbye");
            Environment.Exit(0);
        }
        public void ShowError()
        {
            Clear();
            WriteLine("Error");
        }
    }
    class File:Dictionary, iFile
    {
        private FileStream FS;
        private StreamWriter SW;

        public File() { }
        public void OpenOrCreateFile()
        {
            FS = new FileStream("Dictionary.txt", FileMode.OpenOrCreate);
            FS.SetLength(0);
        }
        public void SaveData()
        {
            SW = new StreamWriter(FS);
            if (DictionarySize > 0)
            {
                for (int WordIndex = 0, Position = 1; WordIndex < DictionarySize; WordIndex++, Position++)
                {
                    char Dot = '.';
                    SW.WriteLine($"{Position}{Dot} {EnglishWords[WordIndex]} - {RussianWords[WordIndex]}");
                }
                WriteLine("Data from dictionary was saved to file");
            }
            else ShowEmptyDictionary();
        }
        public void CloseStream()
        {
            SW.Close();
            FS.Close();
        }
    }
    class CheckCode
    {
        static void Main()
        {
            Dictionary User = new Dictionary();
            User.ShowMenu();
        }
    }
}
