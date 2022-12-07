using System;
using System.Collections.Generic;

namespace blockchain
{
	enum Command
	{
		Show,
		ShowFull,
		Add,
		Validate,
		Change,
		Exit
	}

	class Program
	{
		static void Main(string[] args)
		{
			List<Note> notes = new List<Note>();

			notes.Add(new Note("Первый блок"));

			string command = "";
			
			Command com = Command.Show;

			while(com != Command.Exit)
			{
				Console.WriteLine("Выбирите действие: ");
				Console.WriteLine("0 - Показать блоки \n1 - Показать полную информацию о блоках \n2 - Добавить блок \n3 - Проверить блоки \n4 - Подменить хеш \n5 - Выход \n");
                command = Console.ReadLine();
                try
				{
					com = (Command)Convert.ToInt32(command); 
				}
				catch(Exception e)
				{
					com = Command.Show;
					Console.WriteLine(e.Message);
				}

				switch(com)
				{
					case Command.Show:
						WriteAllNotes(notes);
						break;

					case Command.ShowFull:
						WriteFullNotes(notes);
						break;

					case Command.Add:
						Console.WriteLine("Введите текст, который будет записан в блок:");
						string text = Console.ReadLine();
						Console.WriteLine();


                        AddNote(text, notes);
						break;

                    case Command.Validate:
                        ValidateNotes(notes);
                        break;

                    case Command.Change:
						int n = ChangeNotes(notes);
						if (n==-1)
						{
							Console.WriteLine("Недостаточно блоков\n");
                        }
						else
						{
							Console.WriteLine($"Предыдущий хеш {n+1} блока успешно изменен\n");
						}
						break;
                   
                    case Command.Exit:
						break;
				}
			}
		}

		static void ValidateNotes(List<Note> notes)
		{
			for(int i = 0; i < notes.Count; i++)
			{
				if(i == 0) 
				{
					Console.WriteLine("0 - Верен");
				}
				else
				{
					if(@notes[i].PreviousHashString == @notes[i - 1].HashString) 
					{
						Console.WriteLine(i + " - Верен");
					}
					else 
					{
						//Console.WriteLine($"\n\n | {notes[i].PreviousHashString} | {notes[i - 1].HashString} | \n\n");
						Console.WriteLine(i + " - Изменен!!!");
					}
				}
			}

			Console.WriteLine("\n");
		}
        static int ChangeNotes(List<Note> notes)
		{
			int n = -1;
			Random rnd = new Random();
			if (notes.Count>0)
			{
				n = rnd.Next(1, notes.Count);
				notes[n].ChangeHash();
			}
			return n;
		}


        static void AddNote(string text, List<Note> notes)
		{
			notes.Add(new Note(text, notes[notes.Count - 1].Hash));
		}

		static void WriteAllNotes(List<Note> notes)
		{
			Console.WriteLine("Ваши блоки: ");

			for(int i = 0; i < notes.Count; i++)
			{
                Console.WriteLine($"*{i+1} блок*");
                WriteNote(notes[i]);
                Console.WriteLine();
            }
		}

		static void WriteFullNotes(List<Note> notes)
		{
			Console.WriteLine("Ваши блоки: ");

			for(int i = 0; i < notes.Count; i++)
			{
                Console.WriteLine($"*{i+1} блок*");
                WriteNoteFull(notes[i]);
                Console.WriteLine();
            }

		}

		static void WriteNote(Note n)
		{
            Console.WriteLine($"Текст: {n.Text}");
        }

		static void WriteNoteFull(Note n)
		{
			Console.WriteLine($"Хеш предыдущего блока:");
            Console.WriteLine(n.PreviousHashString);
            Console.WriteLine($"Текст: {n.Text}");
            Console.WriteLine($"Хеш текущего блока:");
			Console.WriteLine(n.HashString);
            Console.WriteLine($"Время создания:");
			Console.WriteLine(n.TimeCreate);
            Console.WriteLine($"Nonce:");
            Console.WriteLine(n.Nonce);
        }
	}
}
