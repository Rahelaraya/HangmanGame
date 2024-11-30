using System;
using System.Text;
using Spectre.Console;

namespace HangmanGame
{

    class Program
    {
        static void Main()

        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            bool startgame = true;

            while (startgame)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(@"
                 _   _                                            
                | | | |                                           
                | |_| | __ _ _ __   __ _ _ __ ___   __ _ _ __     
                |  _  |/ _` | '_ \ / _` | '_ ` _ \ / _` | '_ \    
                | | | | (_| | | | | (_| | | | | | | (_| | | | |   
                \_| |_/\__,_|_| |_|\__, |_| |_| |_|\__,_|_| |_|   
                             __/ |                        
                            |___/                         
            ");
                Console.ResetColor();

                // Colorful welcome message
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Welcome to the Ultimate Hangman Game! 🎉");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Guess the word by entering one letter at a time.");
                Console.ResetColor();

                string[] wordList = { "batman", "book", "bool", "mafia", "mango", "cat" };
                Random random = new Random();
                string wordToGuess = wordList[random.Next(wordList.Length)];
                char[] guessedWord = new string('_', wordToGuess.Length).ToCharArray();
                List<char> incorrectGuesses = new List<char>();
                int maxAttempts = 6;

                while (incorrectGuesses.Count < maxAttempts && new string(guessedWord) != wordToGuess)
                {
                    Console.WriteLine("\nWord: " + new string(guessedWord));
                    Console.WriteLine($"Incorrect guesses: {string.Join(", ", incorrectGuesses)}");
                    Console.WriteLine($"Attempts remaining: {maxAttempts - incorrectGuesses.Count}");
                    DisplayHangman(incorrectGuesses.Count);
                    Console.Write("Enter a letter: ");

                    string input = Console.ReadLine().ToLower();
                    if (string.IsNullOrEmpty(input) || input.Length != 1)
                    {
                        Console.WriteLine("Please enter a single letter.");
                        continue;
                    }

                    char guess = input[0];

                    if (wordToGuess.Contains(guess))
                    {
                        Console.WriteLine($"Good job! The letter '{guess}' is in the word.");
                        for (int i = 0; i < wordToGuess.Length; i++)
                        {
                            if (wordToGuess[i] == guess)
                            {
                                guessedWord[i] = guess;
                            }
                        }
                    }
                    else
                    {
                        if (!incorrectGuesses.Contains(guess))
                        {
                            incorrectGuesses.Add(guess);
                            Console.WriteLine($"Sorry, the letter '{guess}' is not in the word.");
                        }
                        else
                        {
                            Console.WriteLine($"You already guessed '{guess}'. Try a different letter.");
                        }
                    }
                }

                if (new string(guessedWord) == wordToGuess)
                {
                    Console.WriteLine("\n🎉🎊 🎈 CONGRATULATIONS! 🎈 🎊🎉");
                    Console.WriteLine("🔥🔥 YOU DID IT! 🔥🔥");
                    Console.WriteLine($"🌟 You successfully guessed the word: **{wordToGuess.ToUpper()}** 🌟");
                    Console.WriteLine("💪👏 🏆 AMAZING JOB, YOU WIN! 🏆 👏💪");
                }
                else
                {
                    Console.WriteLine("\n💔💀 GAME OVER! 💀💔");
                    Console.WriteLine("😞😢 SO CLOSE, BUT NOT THIS TIME... 😢😞");
                    Console.WriteLine($"🔤 The word was: **{wordToGuess.ToUpper()}** 🔤");
                    Console.WriteLine("✨💪 Don't give up! Try again and you'll conquer it! 💪✨");
                    DisplayHangman(maxAttempts); // Show full hangman
                }

                // Ask if the user wants to play again
                startgame = AskToPlayAgain();

                if (!startgame)
                {
                    // Farewell message
                    Console.WriteLine("\nThank you for playing the Ultimate Hangman Game! 🎮");
                    Console.WriteLine("We hope to see you again soon. Goodbye! 👋");
                }
            }
        }

        static void DisplayHangman(int stage)
        {
            string[] hangmanStages = new string[]
            {
            @"
  +---+
      |
      |
      |
     ===",
            @"
  +---+
  O   |
      |
      |
     ===",
            @"
  +---+
  O   |
  |   |
      |
     ===",
            @"
  +---+
  O   |
 /|   |
      |
     ===",
            @"
  +---+
  O   |
 /|\  |
      |
     ===",
            @"
  +---+
  O   |
 /|\  |
 /    |
     ===",
            @"
  +---+
  O   |
 /|\  |
 / \  |
     ==="
            };

            Console.WriteLine(hangmanStages[stage]);
        }

        static bool AskToPlayAgain()
        {
            // Create a menu prompt using Spectre.Console
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[green]Would you like to play again?[/]")
                    .AddChoices("Yes", "No")
            );

            // Return true for "Yes", false for "No"
            return choice == "Yes";
        }
    }
}
