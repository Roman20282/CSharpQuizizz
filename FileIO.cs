using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSharpQuizizz
{
    
        public class FileIO
        {
            static int qwestionCount;                       
            static int variantsOfAnsverCount;              
            static string answ;
            static string qwescion_x;
            static int numberOfCorectAnswer;
            static int selectedResponse;
            static int uncorectAnswersCount;
            static int corectAnswersCount;
            static double result;
            static bool Exit;

            private static void ShowProgramTitle()
            {
                Console.Clear();
                Console.WriteLine("**********************************CSharpQuizizz*******************************************");
                Console.WriteLine("|=========================================================================================|\n");
                Console.WriteLine("             For end of work, type: \"exit\".");
                Console.WriteLine("|=========================================================================================|\n\n\n");
            }
            public static void ShowQwestion(string qwescion_x)
            {
                Console.WriteLine("     {0}                                                         \n", qwescion_x);
                Console.WriteLine();
                Console.WriteLine("|=========================================================================================|\n");
            }
            public static void ShowVariantsOfAnswers(int j, string answ)
            {
                Console.WriteLine("|  {0})     {1}                                                         \n", j, answ);
                Console.WriteLine("|=========================================================================================|\n");
            }

            public static void OpenFile()
            {
                var pathForTest = Directory.GetCurrentDirectory() + @"\test.txt";

                if (!File.Exists(Convert.ToString(pathForTest)))
                {
                    Console.WriteLine("Error 404. File not found.   ");
                    System.Threading.Thread.Sleep(8000);
                }
                else
                {
                    ReadQwestion(pathForTest.ToString());
                }
            }
            static void ReadQwestion(string pathToFile)
            {
                Exit = false;
                while (!Exit)
                {
                        FileStream fin;
                            try
                            {
                                fin = new FileStream(pathToFile, FileMode.Open);
                                Console.WriteLine("File opening...");
                                System.Threading.Thread.Sleep(2000);
                            }
                            catch (IOException exc)
                            {
                                Console.WriteLine("Error open file:" + exc.Message);
                                return;
                            }
                        StreamReader fstr_in = new StreamReader(fin);
                        string s;
                        ShowProgramTitle();
                        numberOfCorectAnswer = 0;
                        uncorectAnswersCount = 0;
                        corectAnswersCount = 0;
                        try
                        {

                            s = fstr_in.ReadLine().Substring(3);
                            qwestionCount = Convert.ToInt32(s);

                            s = fstr_in.ReadLine().Substring(3); //s = s.Substring(3);
                            variantsOfAnsverCount = Convert.ToInt32(s);

                            s = fstr_in.ReadLine();

                            for (int i = 0; i < qwestionCount; i++)
                            {
                                qwescion_x = fstr_in.ReadLine();
                                ShowQwestion(qwescion_x);
                                for (int j = 0; j < variantsOfAnsverCount; j++)
                                {

                                    answ = fstr_in.ReadLine();
                                    ShowVariantsOfAnswers(j + 1, answ);
                                }

                                numberOfCorectAnswer = Convert.ToInt32(fstr_in.ReadLine().Substring(15));
                                s = fstr_in.ReadLine();
                                PromptToMakeChoise();
                                
                                Console.WriteLine();
                                Console.WriteLine();

                            }
                            ShowResults();
                        }
                        catch (IOException exc)
                        {
                            Console.WriteLine("Error input/output:\n" + exc.Message);
                        }
                        finally
                        {
                            fstr_in.Close();
                        }
                }
            }
            public static void PromptToMakeChoise()
            {
                    string ch;
                    selectedResponse = 0;
                    Console.Write("Make your choice :   "); ch = Console.ReadLine();
                    if (ch == "exit") return;
                    else
                    {
                        bool success = int.TryParse(ch, out selectedResponse);
                        CheckAnswer(selectedResponse);
                        
                    }
            }
            public static void CheckAnswer(int tempAnswer)
            {
                if (tempAnswer == numberOfCorectAnswer) corectAnswersCount++;
                else uncorectAnswersCount++;
            }

            public static void ShowResults()
            {
                result = ((double)corectAnswersCount / qwestionCount) * 100;
                Console.WriteLine("Corect Answer: {0}", corectAnswersCount);
                Console.WriteLine("Uncorect Answer: {0}", uncorectAnswersCount);
                Console.WriteLine("Avarage result: {0}", Math.Round(result, 2));
                Console.WriteLine("\n\n\n");
                
                Console.WriteLine("Would you like to take the test again? If not type: \"exit\"");
                string ch = Console.ReadLine();
                if (ch == "exit")  Exit = true;
            }
        }
}
