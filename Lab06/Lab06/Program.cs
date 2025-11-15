namespace Lab06
{
	class Program
	{
		public static void Main()
		{
			Console.WriteLine("Лабораторная работа 03. Выполнил студент 6104-020302D Круглов Данил");
			int menuInput;
			do
			{
				//string inputString = "Съешь ещё эих мягких французских булочек, да выпей же магаданского чаю";
				Console.WriteLine("Введите исходную строку:");
				string inputString = Console.ReadLine();
				Console.WriteLine();
				Console.WriteLine("Исходная строка: " + inputString + "\n");
				Console.Write("Кол-во букв: ");
				Console.WriteLine(StringTools.LetterCount(inputString));
				Console.Write("Кол-во прочих символов: ");
				Console.WriteLine(StringTools.NonLetterCount(inputString));
				Console.Write("Длина строки в целом: ");
				Console.WriteLine(StringTools.SymbolCount(inputString));
				Console.Write("Средняя длина слова: ");
				Console.WriteLine(StringTools.AverageWordLength(inputString));
				Console.WriteLine("Введите слово, которое нужно заменить:");
				string wordToSeek = Console.ReadLine();
				Console.WriteLine("Введите новое слово:");
				string wordToInsert = Console.ReadLine();
				Console.Write("Заменить слово \"{0}\" на \"{1}\": ", wordToSeek, wordToInsert);
				Console.WriteLine(StringTools.ReplaceWord(inputString, wordToSeek, wordToInsert));
				Console.WriteLine("Введите подстроку, вхождения которой В ИСХОДНОЙ СТРОКЕ нужно подсчитать (с учётом регистра):");
				string substringToCount = Console.ReadLine();
				Console.Write("Вхождений подстроки \"{0}\": ", substringToCount);
				Console.WriteLine(StringTools.SubstringCount(inputString, substringToCount));
				if (StringTools.IsAPalindrome(inputString))
				{
					Console.WriteLine("Исходная строка является палиндромом.");
				}
				else
				{
					Console.WriteLine("Исходная строка не является палиндромом.");
				}
				if (StringTools.IsAValidDate(inputString))
				{
					Console.WriteLine("Исходная строка является датой.");
				}
				else
				{
					Console.WriteLine("Исходная строка не является датой.");
				}
				Console.WriteLine();
				Console.WriteLine("Демонстрация окончена. Выберите опцию:\n1 - Начать заново\n\n0 - Завершение работы");
				menuInput = GetOption(0, 1);
				if (menuInput == 1) Console.Clear();
			} while (menuInput != 0);
		}

		static int GetOption(int lowerBound, int upperBound)
		{
			int option = 0; // Задаём дефолт значение чтобы компилятор не ругался
			bool success = false;
			do
			{
				try
				{
					option = Convert.ToInt32(Console.ReadLine());
					success = option < lowerBound || option > upperBound;
				}
				catch (FormatException)
				{
					Console.WriteLine("Ожидается целое число от {0} до {1}", lowerBound, upperBound);
				}
			} while (!success);
			return option;
		}
	}
}