using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        ProcessTextFiles();
    }

    static void ProcessTextFiles()
    {
        string[] files = { "10.txt", "11.txt", "12.txt", "13.txt", "14.txt", "15.txt", "16.txt", "17.txt", "18.txt", "19.txt", "20.txt", "21.txt", "22.txt", "23.txt", "24.txt", "25.txt", "26.txt", "27.txt", "28.txt", "29.txt" };
        int sum = 0;
        int count = 0;
        var missingFiles = new List<string>();
        var badDataFiles = new List<string>();
        var overflowFiles = new List<string>();

        try
        {
            foreach (var file in files)
            {
                try
                {
                    try
                    {
                        var lines = File.ReadAllLines(file);
                        try
                        {
                            int num1 = int.Parse(lines[0]);
                            int num2 = int.Parse(lines[1]);
                            checked
                            {
                                int product = num1 * num2;
                                sum += product;
                                count++;
                            }
                        }
                        catch
                        {
                            throw new Exception("Bad data in file");
                        }
                    }
                    catch (IndexOutOfRangeException)
                    {
                        throw new Exception("Not enough lines");
                    }
                }
                catch (FileNotFoundException)
                {
                    missingFiles.Add(file);
                }
                catch (Exception ex) when (ex.Message == "Bad data in file")
                {
                    badDataFiles.Add(file);
                }
                catch (OverflowException)
                {
                    overflowFiles.Add(file);
                }
            }

            WriteResults("no_file.txt", missingFiles);
            WriteResults("bad_data.txt", badDataFiles);
            WriteResults("overflow.txt", overflowFiles);

            if (missingFiles.Count > 0 || badDataFiles.Count > 0 || overflowFiles.Count > 0)
                throw new Exception("Failed to create or update one of the result files");

            Console.WriteLine("Середнє арифметичне: " + (sum / count));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
    }

    static void WriteResults(string fileName, List<string> results)
    {
        try
        {
            File.WriteAllLines(fileName, results);
        }
        catch (Exception)
        {
            Console.WriteLine("Помилка при записі файлу: " + fileName);
        }
    }
}
