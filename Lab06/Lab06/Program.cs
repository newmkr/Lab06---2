namespace Lab06
{
    class Program
    {
        public static void Main()
        {
            Console.WriteLine("Лабораторная работа 03. Выполнил студент 6104-020302D Круглов Данил");
            //string inputString = "Съешь ещё этих мягких французских булочек, да выпей же чаю.";
            string inputString = "Съешь ещё эих мягких французских булочек, да выпей же магаданского чаю";
            Console.WriteLine("Исходная строка: " + inputString + "\n");
            Console.WriteLine(StringTools.ReplaceWord(inputString, "гриша", "Пидор"));
            Console.Write("Кол-во букв: ");
            Console.WriteLine(StringTools.LetterCount(inputString));
            Console.Write("Кол-во прочих символов: ");
            Console.WriteLine(StringTools.NonLetterCount(inputString));
            Console.Write("Длина строки в целом: ");
            Console.WriteLine(StringTools.SymbolCount(inputString));
            Console.Write("Средняя длина слова: ");
            Console.WriteLine(StringTools.AverageWordLength(inputString));
            Console.Write("Заменить слово \"{0}\" на \"{1}\": ", "да", "нет");
            Console.WriteLine(StringTools.ReplaceWord(inputString, "да", "нет"));
            Console.Write("Вхождений подстроки \"{0}\": ", "да");
            Console.WriteLine(StringTools.SubstringCount(inputString, "да"));
            if (StringTools.IsAPalindrome(inputString))
            {
                Console.WriteLine("Строка является палиндромом.");
            }
            else
            {
                Console.WriteLine("Строка не является палиндромом.");
            }
            if (StringTools.IsAValidDate(inputString))
            {
                Console.WriteLine("Строка является датой.");
            }
            else
            {
                Console.WriteLine("Строка не является датой.");
            }
        }
    }
}