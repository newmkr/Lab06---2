using System.Text; // Для класса StringBuilder

namespace Lab06
{
	public class StringTools
	{
		// Содержит большие и маленькие буквы латиницы и кириллицы
		private const string _letters = "йцукенгшщзхъфывапролджэячсмитьбюёЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮЁqwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";

		private const string _digits = "1234567890";

		private const string _dateseparators = @" ./\-";

		private static bool _IsInKeys(char chr, string keys)
		{
			return keys.IndexOf(chr) != -1; ;
		}

		private static bool _IsALetter(char chr)
		{
			return _IsInKeys(chr, _letters);
		}

		private static bool _IsADigit(char chr)
		{
			return _IsInKeys(chr, _digits);
		}

		private static bool _IsADateSeparator(char chr)
		{
			return _IsInKeys(chr, _dateseparators);
		}

		// Внутренний метод реализации LetterCount и SymbolCount. str - строка, где ищем символы. keys - символы, которые мы ищем. inverted - наоборот, искать символы, НЕ содержащиеся в keys
		private static int _CountOverlapsWithKeys(string str, string keys, bool inverted = false)
		{
			int result = 0;
			for (int index = 0; index < str.Length; index++)
			{
				char letter = str[index];
				// Если в строке нет искомой подстроки, IndexOf() вернёт число -1
				if (!inverted)
				{
					if (_IsInKeys(letter, keys)) result++;
				}
				else
				{
					if (!_IsInKeys(letter, keys)) result++;
				}
			}
			return result;
		}

		private static List<int> _GetWordsData(string str, string keys = _letters) // Понятие "слова" может быть заменено, если в keys передать другой набор символов, например цифр
		{
			List<int> result = new List<int>(); /* Мы ещё не знаем, сколько слов будет в итоге, поэтому используем List (коллекцию)
			В result будут храниться индексы начал слов и длины слов.
			[0] - начало первого слова
			[1] - длина первого слова
			[2] - начало второго слова
			...
			*/
			int index = 0;
			while (index < str.Length)
			{
				while (index < str.Length && !_IsInKeys(str[index], keys)) index++; // Ищем следующую букву
				if (index < str.Length) // Продолжаем только если мы в пределах строки
				{
					int wordStartIndex = index; // Первая буква в слове
					index++;
					while (index < str.Length && _IsInKeys(str[index], keys)) index++; /* Ищем следующую небукву
					Сейчас wordStartIndex - первая буква слова, а index - символ "после" последней буквы слова.
					*/
					result.Add(wordStartIndex);
					result.Add(index - wordStartIndex);
				}
			}
			return result;
		}

		private static string[] _GetWords(string str, string keys = _letters)
		{
			List<int> wordsData = _GetWordsData(str, keys);
			string[] result = new string[wordsData.Count / 2]; // Тут нам уже известно, сколько будет в итоге слов. поэтому можно использовать массив
			for (int index = 0; index < wordsData.Count; index += 2)
			{
				result[index / 2] = (str.Substring(wordsData[index], wordsData[index + 1]));
			}
			return result;
		}

		public static int LetterCount(string str)
		{
			return _CountOverlapsWithKeys(str, _letters);
		}

		public static int NonLetterCount(string str)
		{
			return _CountOverlapsWithKeys(str, _letters, true);
		}

		public static int SymbolCount(string str)
		{
			return str.Length;
		}

		public static double AverageWordLength(string str)
		{
			string[] words = _GetWords(str);
			int LetterCount = 0;
			for (int index = 0; index < words.Length; index++)
			{
				LetterCount += words[index].Length;
			}
			return (double)LetterCount / (double)words.Length;
		}

		public static string ReplaceWord(string str, string wordToSeek, string wordToInsert)
		{
			/* Чтобы эффективно изменять строку (неизменяемый тип данных)
			конвертируём её в StringBuilder, который позволяет изменять свой состав динамически
			*/
			StringBuilder sb = new StringBuilder(str);
			List<int> wordsData = _GetWordsData(str);
			// Чтобы индексы не поломались, будем работать с конца:
			for (int index = wordsData.Count - 2; index >= 0; index -= 2)
			{
				int wordStartIndex = wordsData[index];
				int wordLength = wordsData[index + 1];
				string word = str.Substring(wordStartIndex, wordLength);
				if (word.ToLower() == wordToSeek.ToLower()) // Так как регистр нужно игнорировать, ставим ToLower()
				{
					sb.Remove(wordStartIndex, wordLength); // Удаляем слово
					sb.Insert(wordStartIndex, wordToInsert); // Вставляем новое слово
				}
			}
			return sb.ToString(); // "Собираем" строку заново
		}

		public static int SubstringCount(string str, string substr)
		{
			int index = 0;
			int count = 0;
			do
			{
				index = str.IndexOf(substr, index + 1);
				if (index != -1) count++;
			} while (index != -1);
			return count;
		}

		public static bool IsAPalindrome(string inputString)
		{
			string str = inputString.ToLower();
			// Проверку на то, является ли строка словом ИЗ букв, не делаем. Число тоже может быть палиндромом (20.11.02 , 22:22 итд)
			for (int index = 0; index < str.Length / 2; index++) // Проверяем до середины слова (или не включая до символа, находящегося по середине)
			{
				if (str[index] != str[str.Length - index - 1]) return false; // Если символ не равен символу с другой стороны строки, сразу можно сказать что не палиндром и вернуть false
			}
			return true;
		}

		public static bool IsAValidDate(string str)
		{
			// Если это ужасное решение - не вариант, можно ли использовать регулярные выражения для этой задачи?
			if (str.Length > 7 // Если не делать сразу эту проверку то будет исключение при адресации символа
			&&_IsADigit(str[0])
			&& _IsADigit(str[1])
			&& _IsADateSeparator(str[2])
			&& _IsADigit(str[3])
			&& _IsADigit(str[4])
			&& _IsADateSeparator(str[5])
			&& _IsADigit(str[6])
			&& _IsADigit(str[7])
			&& (str.Length == 8 || str.Length == 10 && _IsADigit(str[8]) && _IsADigit(str[9]))
			)
			{
				return true;
			}
			else
			{
				return false;
			}

			// Альренативная имплементация:
			/* /* Получаем данные о нахождении "слов", при этом под "словом" будем считать
			число, то есть подстроку состоящую из цифр * /
			List<int> wordsData = _GetWordsData(str, _numbers);
			// XX.XX.XX    XX.XX.XXXX
			// 01234567    0123456789
			List<int> dateFormat1 = new List<int>() { 0, 2, 3, 2, 6, 2 };
			List<int> dateFormat2 = new List<int>() { 0, 2, 3, 2, 6, 4 };
			/*
			Если число является датой, то информация о словах ВСЕГДА будет
			соответствовать формату 1 (если в доте неполный год)
			или формату 2 (полный год)
			* /
			if (wordsData == dateFormat1 || wordsData == dateFormat2)
			{
				// Формат соответствует. Теперь нужно проверить, правильные ли числа и месяцы
				string[] words = _GetWords(str, _numbers);
				int day = Convert.ToInt32(words[0]);
				int month = Convert.ToInt32(words[1]);
				if (month > 12 || month == 0 || day == 0)
				{
					return false;
				}
				else
				{
					if (month % 2 == 0)
					{
						// Четный месяц: февраль, апрель, 
					}
				}
			}
			else
			{
				return false;
			} */
		}
	}
}