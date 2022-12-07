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
			List<Node> notes = new List<Node>();

			notes.Add(new Node("������ ����"));

			string command = "";
			
			Command com = Command.Show;

			while(com != Command.Exit)
			{
				Console.WriteLine("�������� ��������: ");
				Console.WriteLine("0 - �������� ����� \n1 - �������� ������ ���������� � ������ \n2 - �������� ���� \n3 - ��������� ����� \n4 - ��������� ��� \n5 - ����� \n");
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
						Console.WriteLine("������� �����, ������� ����� ������� � ����:");
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
							Console.WriteLine("������������ ������\n");
                        }
						else
						{
							Console.WriteLine($"���������� ��� {n+1} ����� ������� �������\n");
						}
						break;
                   
                    case Command.Exit:
						break;
				}
			}
		}

		static void ValidateNotes(List<Node> notes)
		{
			for(int i = 0; i < notes.Count; i++)
			{
				if(i == 0) 
				{
					Console.WriteLine("0 - �����");
				}
				else
				{
					if(@notes[i].PreviousHashString == @notes[i - 1].HashString) 
					{
						Console.WriteLine(i + " - �����");
					}
					else 
					{
						//Console.WriteLine($"\n\n | {notes[i].PreviousHashString} | {notes[i - 1].HashString} | \n\n");
						Console.WriteLine(i + " - �������!!!");
					}
				}
			}

			Console.WriteLine("\n");
		}
        static int ChangeNotes(List<Node> notes)
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


        static void AddNote(string text, List<Node> notes)
		{
			notes.Add(new Node(text, notes[notes.Count - 1].Hash));
		}

		static void WriteAllNotes(List<Node> notes)
		{
			Console.WriteLine("���� �����: ");

			for(int i = 0; i < notes.Count; i++)
			{
                Console.WriteLine($"*{i+1} ����*");
                WriteNote(notes[i]);
                Console.WriteLine();
            }
		}

		static void WriteFullNotes(List<Node> notes)
		{
			Console.WriteLine("���� �����: ");

			for(int i = 0; i < notes.Count; i++)
			{
                Console.WriteLine($"*{i+1} ����*");
                WriteNoteFull(notes[i]);
                Console.WriteLine();
            }

		}

		static void WriteNote(Node n)
		{
            Console.WriteLine($"�����: {n.Text}");
        }

		static void WriteNoteFull(Node n)
		{
			Console.WriteLine($"��� ����������� �����:");
            Console.WriteLine(n.PreviousHashString);
            Console.WriteLine($"�����: {n.Text}");
            Console.WriteLine($"��� �������� �����:");
			Console.WriteLine(n.HashString);
            Console.WriteLine($"����� ��������:");
			Console.WriteLine(n.TimeCreate);
            Console.WriteLine($"Nonce:");
            Console.WriteLine(n.Nonce);
        }
	}
}
