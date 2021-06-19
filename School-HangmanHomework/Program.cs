using System;
using System.IO;
using System.Text;

namespace School_HangmanHomework
{
    class Program
    {
        static void Main()
        {
            StartHangman();
        }

        static void StartHangman()
        {
            string secretWord = GetRandomWordFromTextFile();

            StringBuilder incorrectLetters = new ();

            char[] correctLetters = new char[secretWord.Length];
            Array.Fill(correctLetters, '_');

            Console.WriteLine("Hello! The game has started.");

            int userGuessesLeft = 10;
            bool userWonGame = false;

            while (userGuessesLeft > 0 && !userWonGame)
            {
                Console.Clear();

                ShowInfoForUser(correctLetters, incorrectLetters, userGuessesLeft);

                Console.WriteLine("Guess a letter or a word:");
                string userInput = Console.ReadLine().ToLower();

                Console.WriteLine();

                if (userInput.Length == secretWord.Length)
                {
                    if (userInput.Equals(secretWord))
                    {
                        userWonGame = true;
                    }
                    else
                    {
                        userGuessesLeft--;

                        Console.WriteLine($"{userInput} is wrong.");
                        Console.WriteLine("Press any key to continue.");

                        Console.ReadKey();
                    }
                }
                else if (userInput.Length == 1)
                {
                    if (secretWord.Contains(userInput))
                    {
                        for (int i = 0; i < secretWord.Length; i++)
                        {
                            char secretWordLetter = secretWord[i];
                            char userLetter = userInput[0];

                            if (secretWordLetter.Equals(userLetter))
                            {
                                correctLetters[i] = userLetter;
                            }
                        }

                        StringBuilder correctLettersString = new ();
                        foreach (char c in correctLetters)
                        {
                            correctLettersString.Append(c);
                        }

                        if (correctLettersString.Equals(secretWord))
                        {
                            userWonGame = true;
                        }
                    }
                    else
                    {
                        if (incorrectLetters.ToString().Contains(userInput))
                        {
                            Console.WriteLine($"'{userInput}' is already there. Try again.");

                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                        }
                        else
                        {
                            userGuessesLeft--;

                            incorrectLetters.Append(userInput);
                            Console.WriteLine($"'{userInput}' is wrong.");

                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Only a whole word or one letter can be written.");

                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
            }

            Console.WriteLine();

            if (userWonGame)
            {
                UserWonGame(secretWord, incorrectLetters);
            }
            else
            {
                UserLostGame(secretWord, incorrectLetters, correctLetters);
            }

            RestartGameQuestion();
        }

        // Helper methods
        static string GetRandomWordFromTextFile()
        {
            string[] arrayOfWords = ReadFromTextFile();

            Random random = new ();
            int randomWordIndex = random.Next(arrayOfWords.Length);

            string usedWord = arrayOfWords[randomWordIndex];

            return usedWord;
        }

        static string[] ReadFromTextFile()
        {
            string pathWithEnv = @"%USERPROFILE%\Desktop\ManyWords.txt";
            string filePath = Environment.ExpandEnvironmentVariables(pathWithEnv);

            StreamReader textReader = new (filePath);
            using (textReader)
            {
                string[] stringArray = textReader.ReadLine().Split(',');

                return stringArray;
            }
        }

        static void ShowInfoForUser(char[] correctLetters, StringBuilder incorrectLetters, int userGuessesLeft)
        {
            Console.WriteLine("Correct letters:");
            Console.WriteLine(correctLetters);
            Console.WriteLine();

            Console.WriteLine("Incorrect letters:");
            Console.WriteLine(incorrectLetters);
            Console.WriteLine();

            Console.WriteLine($"{userGuessesLeft} guesses left.");
        }

        static void UserWonGame(string secretWord, StringBuilder incorrectLetters)
        {
            Console.Clear();

            Console.WriteLine("Right word:");
            Console.WriteLine(secretWord);
            Console.WriteLine();

            Console.WriteLine("Incorrect letters:");
            Console.WriteLine(incorrectLetters);
            Console.WriteLine();

            Console.WriteLine("You won!");
            Console.WriteLine();
        }

        static void UserLostGame(string secretWord, StringBuilder incorrectLetters, char[] correctLetters)
        {
            Console.Clear();

            Console.WriteLine("Right word:");
            Console.WriteLine(secretWord);
            Console.WriteLine();

            Console.WriteLine("Correct letters:");
            Console.WriteLine(correctLetters);
            Console.WriteLine();

            Console.WriteLine("Incorrect letters:");
            Console.WriteLine(incorrectLetters);
            Console.WriteLine();

            Console.WriteLine("You lost!");
            Console.WriteLine();
        }

        static void RestartGameQuestion()
        {
            Console.WriteLine("Do you want to play again? y/n:");
            Console.Write("Choice: ");
            string userChoice = Console.ReadLine();

            if (userChoice.Length > 1)
            {
                Console.WriteLine();
                Console.WriteLine("You can only choose: y/n");

                RestartGameQuestion();
            }

            if (userChoice.Equals("y", StringComparison.OrdinalIgnoreCase))
            {
                StartHangman();
            }
            else if (userChoice.Equals("n", StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Exiting.");
                Environment.Exit(0);

                Console.WriteLine();
            }
        }
    }
}
