using System;
using System.Collections.Generic;
using System.Linq;

/*
 * This is one of my very first C# console applications */

namespace BlackjackLab
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("*****Welcome To Console BlackJack!*****");
                Console.WriteLine("Press any key to play: ");
                Console.ReadKey();
                Game myGame = new Game();
                myGame.ComputerTurn = true;
                myGame.ComputerMove();
                myGame.ComputerMove();
                Console.WriteLine("Press any key to continue: ");
                Console.ReadKey();
                myGame.ComputerTurn = false;
                myGame.UserTurn = true;
                myGame.UserMove();
                Console.WriteLine("Press any key to continue: ");
                Console.ReadKey();
                myGame.UserMove();
                myGame.UserTurn = false;
                do
                {
                    Console.WriteLine("Would you like to HIT or STAY? ");
                    string userInput = Console.ReadLine();
                    if ((userInput.ToUpper()) == "HIT")
                    {
                        myGame.UserTurn = true;
                        myGame.UserMove();
                        myGame.UserTurn = false;                                       
                    } 
                    else if ((userInput.ToUpper()) == "STAY")
                    {
                        while (myGame.ComputerScore < 17 && myGame.UserScore != 21)
                        {
                            myGame.ComputerTurn = true;
                            myGame.ComputerMove();
                            myGame.ComputerTurn = false;
                        }
                        myGame.Stay();
                    }
                    else if ((userInput.ToUpper()) == "EXIT")
                        Environment.Exit(1);
                    else
                    {
                        Console.WriteLine("Invalid input, press any key to close");
                        Console.ReadKey();
                        Environment.Exit(1);
                    }

                }
                while (myGame.ComputerScore < 21 && myGame.UserScore < 21);
                {
                    if (myGame.UserScore == 21 && myGame.ComputerScore < 21)
                    {
                        Console.WriteLine("Player has {0} points", myGame.UserScore);
                        Console.WriteLine("Computer has {0} points", myGame.ComputerScore);
                        Console.WriteLine("BLACKJACK, PLAYER wins!");
                        Console.WriteLine("Press any key to exit: ");
                        Console.ReadKey();
                        Environment.Exit(1);
                    }
                    else if (myGame.ComputerScore == 21 && myGame.UserScore < 21)
                    {
                        Console.WriteLine("Player has {0} points", myGame.UserScore);
                        Console.WriteLine("Computer has {0} points", myGame.ComputerScore);
                        Console.WriteLine("BLACKJACK, COMPUTER takes the win!");
                        Console.WriteLine("Press any key to exit: ");
                        Console.ReadKey();
                        Environment.Exit(1);
                    }
                    else if (myGame.UserScore > 21)
                    {
                        Console.WriteLine("Player has {0} points", myGame.UserScore);
                        Console.WriteLine("Computer has {0} points", myGame.ComputerScore);
                        Console.WriteLine("PLAYER BUSTS!");
                        Console.WriteLine("Sorry, COMPUTER wins!");
                        Console.WriteLine("Press any key to exit: ");
                        Console.ReadKey();
                        Environment.Exit(1);
                    }
                    else if (myGame.ComputerScore > 21)
                    {
                        Console.WriteLine("Player has {0} points", myGame.UserScore);
                        Console.WriteLine("Computer has {0} points", myGame.ComputerScore);
                        Console.WriteLine("COMPUTER BUSTS!");
                        Console.WriteLine("Congratulations, PLAYER wins!");
                        Console.WriteLine("Press any key to exit: ");
                        Console.ReadKey();
                        Environment.Exit(1);
                    }
                    else
                    {
                        Console.WriteLine("There's been an ERROR, neither player has been counted as winning.");
                        Console.WriteLine("Press any key to exit: ");
                        Console.ReadKey();
                        Environment.Exit(1);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception found in Program.Main(): " + e.Message);
                Console.WriteLine("Press any key to close program: ");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
    }


    class Card
    {
        public static string[] validSuits = new string[] { "Hearts", "Spades", "Diamonds", "Clubs" };
        public static string[] validRanks = new string[] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };

        public static string[] ValidSuits()
        {
            return validSuits;
        }
        public static string[] ValidRanks()
        {
            return validRanks;
        }

        public string Suit { get; set; }
        public string Rank { get; set; }
        public ConsoleColor ForegroundColor { get; set; }
        public bool UserTurn { get; set; }
        public bool ComputerTurn { get; set; }
        public int UserScore { get; set; }
        public int ComputerScore { get; set; }


        public Card(string suit, string rank)
        {
            try
            {
                if (!validSuits.Contains(suit) || !validRanks.Contains(rank))
                {
                    Console.WriteLine("The Card Details Are Invalid!");
                    Console.Write("Press any key to exit: ");
                    Console.ReadKey();
                    Environment.Exit(1);
                }
                else if (suit == "Hearts" || suit == "Diamonds")
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    this.Suit = suit;
                    this.Rank = rank;
                }
                else if (suit == "Spades" || suit == "Clubs")
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    this.Suit = suit;
                    this.Rank = rank;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception found in Card.Card(): " + e);
                Console.WriteLine("Press any key to close program: ");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }

        public int GetValue()
        {
            try
            {
                if (this.Rank == "Ace")
                {
                    if (this.ComputerTurn == true)
                    {
                        if (this.ComputerScore <= 5)
                            return 11;
                        else
                            return 1;
                    }
                    else
                    {
                        Console.WriteLine("An ace can be worth either 1 or 11 points");
                        Console.WriteLine("Would you like this to be 1 point or 11 points? Enter 1, 11: ");
                        string userInput = Console.ReadLine();
                        int userInt;
                        int.TryParse(userInput, out userInt);

                        if (userInt == 1 || (userInput.ToUpper()) == "ONE")
                            return 1;
                        else if (userInt == 11 || (userInput.ToUpper()) == "ELEVEN")
                            return 11;
                        else
                        {
                            Console.WriteLine("Invalid input, value will default to 1 point");
                            return 1;
                        }
                    }
                }
                else if (this.Rank == "Jack" || this.Rank == "Queen" || this.Rank == "King")
                    return 10;
                else
                    return int.Parse(this.Rank);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception found in Card.GetValue(): " + e.Message);
                Console.WriteLine("Press any key to close program: ");
                Console.ReadKey();
                Environment.Exit(1);
                return 1;
            }
        }
        public string GetFace()
        {
            try
            {
                string rankSuit = this.Rank + " of " + this.Suit;
                switch (rankSuit)
                {
                    //Hearts
                    case "Ace of Hearts":
                        return " ____________\n|Ace       <3|\n|            |\n|            |\n|   Hearts   |\n|            |\n|            |\n|<3       Ace|\n|____________|\n";
                    case "2 of Hearts":
                        return " ____________\n| 2        <3|\n|            |\n|            |\n|   Hearts   |\n|            |\n|            |\n|<3        2 |\n|____________|\n";
                    case "3 of Hearts":
                        return " ____________\n| 3        <3|\n|            |\n|            |\n|   Hearts   |\n|            |\n|            |\n|<3        3 |\n|____________|\n";
                    case "4 of Hearts":
                        return " ____________\n| 4        <3|\n|            |\n|            |\n|   Hearts   |\n|            |\n|            |\n|<3        4 |\n|____________|\n";
                    case "5 of Hearts":
                        return " ____________\n| 5        <3|\n|            |\n|            |\n|   Hearts   |\n|            |\n|            |\n|<3        5 |\n|____________|\n";
                    case "6 of Hearts":
                        return " ____________\n| 6        <3|\n|            |\n|            |\n|   Hearts   |\n|            |\n|            |\n|<3        6 |\n|____________|\n";
                    case "7 of Hearts":
                        return " ____________\n| 7        <3|\n|            |\n|            |\n|   Hearts   |\n|            |\n|            |\n|<3        7 |\n|____________|\n";
                    case "8 of Hearts":
                        return " ____________\n| 8        <3|\n|            |\n|            |\n|   Hearts   |\n|            |\n|            |\n|<3        8 |\n|____________|\n";
                    case "9 of Hearts":
                        ;
                        return " ____________\n| 9        <3|\n|            |\n|            |\n|   Hearts   |\n|            |\n|            |\n|<3        9 |\n|____________|\n";
                    case "10 of Hearts":
                        return " ____________\n| 10       <3|\n|            |\n|            |\n|   Hearts   |\n|            |\n|            |\n|<3       10 |\n|____________|\n";
                    case "Jack of Hearts":
                        return " ____________\n| Jack     <3|\n|            |\n|            |\n|   Hearts   |\n|            |\n|            |\n|<3     Jack |\n|____________|\n";
                    case "Queen of Hearts":
                        return " ____________\n| Queen    <3|\n|            |\n|            |\n|   Hearts   |\n|            |\n|            |\n|<3    Queen |\n|____________|\n";
                    case "King of Hearts":
                        return " ____________\n| King     <3|\n|            |\n|            |\n|   Hearts   |\n|            |\n|            |\n|<3     King |\n|____________|\n";
                    //Diamonds
                    case "Ace of Diamonds":
                        return " ____________\n|Ace       <>|\n|            |\n|            |\n|  Diamonds  |\n|            |\n|            |\n|<>       Ace|\n|____________|\n";
                    case "2 of Diamonds":
                        return " ____________\n| 2        <>|\n|            |\n|            |\n|  Diamonds  |\n|            |\n|            |\n|<>        2 |\n|____________|\n";
                    case "3 of Diamonds":
                        return " ____________\n| 3        <>|\n|            |\n|            |\n|  Diamonds  |\n|            |\n|            |\n|<>        3 |\n|____________|\n";
                    case "4 of Diamonds":
                        return " ____________\n| 4        <>|\n|            |\n|            |\n|  Diamonds  |\n|            |\n|            |\n|<>        4 |\n|____________|\n";
                    case "5 of Diamonds":
                        return " ____________\n| 5        <>|\n|            |\n|            |\n|  Diamonds  |\n|            |\n|            |\n|<>        5 |\n|____________|\n";
                    case "6 of Diamonds":
                        return " ____________\n| 6        <>|\n|            |\n|            |\n|  Diamonds  |\n|            |\n|            |\n|<>        6 |\n|____________|\n";
                    case "7 of Diamonds":
                        return " ____________\n| 7        <>|\n|            |\n|            |\n|  Diamonds  |\n|            |\n|            |\n|<>        7 |\n|____________|\n";
                    case "8 of Diamonds":
                        return " ____________\n| 8        <>|\n|            |\n|            |\n|  Diamonds  |\n|            |\n|            |\n|<>        8 |\n|____________|\n";
                    case "9 of Diamonds":
                        return " ____________\n| 9        <>|\n|            |\n|            |\n|  Diamonds  |\n|            |\n|            |\n|<>        9 |\n|____________|\n";
                    case "10 of Diamonds":
                        return " ____________\n| 10       <>|\n|            |\n|            |\n|  Diamonds  |\n|            |\n|            |\n|<>       10 |\n|____________|\n";
                    case "Jack of Diamonds":
                        return " ____________\n| Jack     <>|\n|            |\n|            |\n|  Diamonds  |\n|            |\n|            |\n|<>     Jack |\n|____________|\n";
                    case "Queen of Diamonds":
                        return " ____________\n| Queen    <>|\n|            |\n|            |\n|  Diamonds  |\n|            |\n|            |\n|<>    Queen |\n|____________|\n";
                    case "King of Diamonds":
                        return " ____________\n| King     <>|\n|            |\n|            |\n|  Diamonds  |\n|            |\n|            |\n|<>     King |\n|____________|\n";
                    //Clubs
                    case "Ace of Clubs":
                        return " ____________\n|Ace      <^>|\n|            |\n|            |\n|    Clubs   |\n|            |\n|            |\n|<^>      Ace|\n|____________|\n";
                    case "2 of Clubs":
                        return " ____________\n| 2       <^>|\n|            |\n|            |\n|    Clubs   |\n|            |\n|            |\n|<^>       2 |\n|____________|\n";
                    case "3 of Clubs":
                        return " ____________\n| 3       <^>|\n|            |\n|            |\n|    Clubs   |\n|            |\n|            |\n|<^>       3 |\n|____________|\n";
                    case "4 of Clubs":
                        return " ____________\n| 4       <^>|\n|            |\n|            |\n|    Clubs   |\n|            |\n|            |\n|<^>       4 |\n|____________|\n";
                    case "5 of Clubs":
                        return " ____________\n| 5       <^>|\n|            |\n|            |\n|    Clubs   |\n|            |\n|            |\n|<^>       5 |\n|____________|\n";
                    case "6 of Clubs":
                        return " ____________\n| 6       <^>|\n|            |\n|            |\n|    Clubs   |\n|            |\n|            |\n|<^>       6 |\n|____________|\n";
                    case "7 of Clubs":
                        return " ____________\n| 7       <^>|\n|            |\n|            |\n|    Clubs   |\n|            |\n|            |\n|<^>       7 |\n|____________|\n";
                    case "8 of Clubs":
                        return " ____________\n| 8       <^>|\n|            |\n|            |\n|    Clubs   |\n|            |\n|            |\n|<^>       8 |\n|____________|\n";
                    case "9 of Clubs":
                        return " ____________\n| 9       <^>|\n|            |\n|            |\n|    Clubs   |\n|            |\n|            |\n|<^>       9 |\n|____________|\n";
                    case "10 of Clubs":
                        return " ____________\n| 10      <^>|\n|            |\n|            |\n|    Clubs   |\n|            |\n|            |\n|<^>      10 |\n|____________|\n";
                    case "Jack of Clubs":
                        return " ____________\n| Jack    <^>|\n|            |\n|            |\n|    Clubs   |\n|            |\n|            |\n|<^>    Jack |\n|____________|\n";
                    case "Queen of Clubs":
                        return " ____________\n| Queen   <^>|\n|            |\n|            |\n|    Clubs   |\n|            |\n|            |\n|<^>   Queen |\n|____________|\n";
                    case "King of Clubs":
                        return " ____________\n| King    <^>|\n|            |\n|            |\n|    Clubs   |\n|            |\n|            |\n|<^>    King |\n|____________|\n";
                    //Spades
                    case "Ace of Spades":
                        return " ____________\n|Ace       8>|\n|            |\n|            |\n|   Spades   |\n|            |\n|            |\n|8>       Ace|\n|____________|\n";
                    case "2 of Spades":
                        return " ____________\n| 2        8>|\n|            |\n|            |\n|   Spades   |\n|            |\n|            |\n|8>        2 |\n|____________|\n";
                    case "3 of Spades":
                        return " ____________\n| 3        8>|\n|            |\n|            |\n|   Spades   |\n|            |\n|            |\n|8>        3 |\n|____________|\n";
                    case "4 of Spades":
                        return " ____________\n| 4        8>|\n|            |\n|            |\n|   Spades   |\n|            |\n|            |\n|8>        4 |\n|____________|\n";
                    case "5 of Spades":
                        return " ____________\n| 5        8>|\n|            |\n|            |\n|   Spades   |\n|            |\n|            |\n|8>        5 |\n|____________|\n";
                    case "6 of Spades":
                        return " ____________\n| 6        8>|\n|            |\n|            |\n|   Spades   |\n|            |\n|            |\n|8>        6 |\n|____________|\n";
                    case "7 of Spades":
                        return " ____________\n| 7        8>|\n|            |\n|            |\n|   Spades   |\n|            |\n|            |\n|8>        7 |\n|____________|\n";
                    case "8 of Spades":
                        return " ____________\n| 8        8>|\n|            |\n|            |\n|   Spades   |\n|            |\n|            |\n|8>        8 |\n|____________|\n";
                    case "9 of Spades":
                        return " ____________\n| 9        8>|\n|            |\n|            |\n|   Spades   |\n|            |\n|            |\n|8>        9 |\n|____________|\n";
                    case "10 of Spades":
                        return " ____________\n| 10       8>|\n|            |\n|            |\n|   Spades   |\n|            |\n|            |\n|8>       10 |\n|____________|\n";
                    case "Jack of Spades":
                        return " ____________\n| Jack     8>|\n|            |\n|            |\n|   Spades   |\n|            |\n|            |\n|8>     Jack |\n|____________|\n";
                    case "Queen of Spades":
                        return " ____________\n| Queen    8>|\n|            |\n|            |\n|   Spades   |\n|            |\n|            |\n|8>    Queen |\n|____________|\n";
                    case "King of Spades":
                        return " ____________\n| King     8>|\n|            |\n|            |\n|   Spades   |\n|            |\n|            |\n|8>     King |\n|____________|\n";
                    default:
                        return "Value Not Found, ERROR!";

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception found in Card.GetFace(): " + e.Message);
                Console.WriteLine("Press any key to close program: ");
                Console.ReadKey();
                Environment.Exit(1);
                return "exiting";
            }
        }


    }
    class Deck
    {
        public List<string> Cards { get; set; }
        public Random RandomGenerator { get; set; }

        public Deck()
        {
            try
            {
                this.Cards = new List<string>();
                this.RandomGenerator = new Random();
                foreach (var suit in Card.ValidSuits())
                {
                    foreach (var rank in Card.ValidRanks())
                    {
                        Cards.Add(rank + " of " + suit);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception found in Deck.Deck(): " + e.Message);
                Console.WriteLine("Press any key to close program: ");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
        public Card DrawCard()
        {
            try
            {
                List<string> drawnCards = new List<string>();
                string suit;
                string rank;
                string[] parts;
                string myCardName;
                string[] myCard = new string[1];
                do
                {
                    int randomNumber = RandomGenerator.Next(0, this.Cards.Count);
                    myCard[0] = Cards.ElementAt(randomNumber);
                }
                while (drawnCards.Contains(myCard[0]));
                {
                    drawnCards.Add(myCard[0]);
                    this.Cards.Remove(myCard[0]);
                    parts = myCard[0].Split(new char[] { ' ' });
                    suit = parts[2];
                    rank = parts[0];
                    myCardName = rank + " of " + suit;
                }

                Card drawnCard = new Card(suit, rank);
                return drawnCard;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception found in Deck.DrawCard(): " + e.Message);
                Console.WriteLine("Press any key to close program: ");
                Console.ReadKey();
                Environment.Exit(1);
                string one = " ";
                string two = " ";
                Card exit = new Card(one, two);
                return exit;
            }
        }

    }

    class Game
    {
        public bool UserWon { get; set; }
        public bool ComputerWon { get; set; }
        public int UserScore { get; set; }
        public int ComputerScore { get; set; }
        public Deck Deck { get; set; }
        public bool UserTurn { get; set; }
        public bool ComputerTurn { get; set; }


        public Game()
        {
            try
            {
                this.UserWon = false;
                this.ComputerWon = false;
                this.UserTurn = false;
                this.ComputerTurn = false;
                this.UserScore = 0;
                this.ComputerScore = 0;
                Deck = new Deck();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception found in Game.Game(): " + e.Message);
                Console.WriteLine("Press any key to close program: ");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
    
        public void Stay()
        {
            try
            {
                if (this.UserScore == 21 && this.ComputerScore < 21)
                {
                    Console.WriteLine("Player has {0} points", this.UserScore);
                    Console.WriteLine("Computer has {0} points", this.ComputerScore);
                    Console.WriteLine("BLACKJACK!, PLAYER wins!");
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                    Environment.Exit(1);
                }
                else if (this.ComputerScore == 21 && this.UserScore < 21)
                {
                    Console.WriteLine("Player has {0} points", this.UserScore);
                    Console.WriteLine("Computer has {0} points", this.ComputerScore);
                    Console.WriteLine("Sorry, COMPUTER got BLACKJACK and takes the win!");
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                    Environment.Exit(1);
                }
                else if (this.UserScore > 21)
                {
                    Console.WriteLine("Player has {0} points", this.UserScore);
                    Console.WriteLine("Computer has {0} points", this.ComputerScore);
                    Console.WriteLine("PLAYER BUSTS!");
                    Console.WriteLine("Sorry, COMPUTER wins!");
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                    Environment.Exit(1);
                }
                else if (this.ComputerScore > 21)
                {
                    Console.WriteLine("Player has {0} points", this.UserScore);
                    Console.WriteLine("Computer has {0} points", this.ComputerScore);
                    Console.WriteLine("COMPUTER BUSTS!");
                    Console.WriteLine("Congratulations, PLAYER wins!");
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                    Environment.Exit(1);
                }
                else if (this.ComputerScore < 21 && this.UserScore < 21 && this.ComputerScore > this.UserScore)
                {
                    Console.WriteLine("Player has {0} points", this.UserScore);
                    Console.WriteLine("Computer has {0} points", this.ComputerScore);
                    Console.WriteLine("Sorry, COMPUTER wins!");
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                    Environment.Exit(1);
                }
                else if (this.UserScore < 21 && this.ComputerScore < 21 && this.UserScore > this.ComputerScore)
                {
                    Console.WriteLine("Player has {0} points", this.UserScore);
                    Console.WriteLine("Computer has {0} points", this.ComputerScore);
                    Console.WriteLine("Congratulations, PLAYER wins!");
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                    Environment.Exit(1);
                }
                else if (this.ComputerScore == this.UserScore)
                {
                    Console.WriteLine("Player has {0} points", this.UserScore);
                    Console.WriteLine("Computer has {0} points", this.ComputerScore);
                    Console.WriteLine("PUSH! It's a tie!");
                    Console.WriteLine("Press any key to exit: ");
                    Console.ReadKey();
                    Environment.Exit(1);
                }
                else
                {
                    Console.WriteLine("Player has {0} points", this.UserScore);
                    Console.WriteLine("Computer has {0} points", this.ComputerScore);
                    Console.WriteLine("There's been some kind of ERROR!");
                    Console.WriteLine("Press any key to exit");
                    Console.ReadKey();
                    Environment.Exit(1);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception found in Game.Stay(): " + e.Message);
                Console.WriteLine("Press any key to close program: ");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
        public void ComputerMove()
        {
            try
            {
                Card myCard = Deck.DrawCard();
                myCard.ComputerTurn = true;
                myCard.ComputerScore = this.ComputerScore;
                string suit = myCard.Suit;
                string rank = myCard.Rank;
                string myCardName = rank + " of " + suit;
                int myCardValue = myCard.GetValue();
                string myCardFace = myCard.GetFace();
                this.ComputerScore += myCardValue;
                Console.WriteLine("The computer has drawn a card");
                myCard.ComputerTurn = false;

                if (this.ComputerScore == 21)
                    this.ComputerWon = true;
                else if (this.ComputerScore > 21)
                    this.UserWon = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception found in Game.ComputerMove(): " + e.Message);
                Console.WriteLine("Press any key to close program: ");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
        public void UserMove()
        {
            try
            {
                Card myCard = Deck.DrawCard();
                myCard.UserTurn = true;
                myCard.UserScore = this.UserScore;
                string suit = myCard.Suit;
                string rank = myCard.Rank;
                string myCardName = rank + " of " + suit;
                int myCardValue = myCard.GetValue();
                string myCardFace = myCard.GetFace();
                this.UserScore += myCardValue;
                Console.WriteLine("The player drew a {0}, and now has {1} points", myCardName, this.UserScore);
                Console.WriteLine(myCardFace);
                myCard.UserTurn = false;

                if (this.UserScore == 21)
                    this.UserWon = true;
                else if (this.UserScore > 21)
                    this.ComputerWon = true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception found in Game.UserMove(): " + e.Message);
                Console.WriteLine("Press any key to close program: ");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
    }
}


