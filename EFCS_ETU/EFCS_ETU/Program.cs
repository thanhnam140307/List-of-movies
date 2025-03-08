

using System;
using System.Diagnostics;
using System.Numerics;
using System.Reflection;
using System.Text;
using TP3;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class Program
{
    public const string MOVIES_FILE = "File/movies.csv";
    public static readonly string[] Categories = { "Action", "Drama", "Comedy", "SciFi", "Horror" };
    public static readonly string[] Studios = { "Universal Pictures", "Paramount", "Warner Bros", "Sony Pictures" };

    public const string INVALID_VALUE_MESSAGE = "Invalid value, please retry : ";
    public const string TITLE = " Movie App ";
    public const string TITLE_LINE = "*";
    public const int MIN_MOVIE_YEAR = 1978;
    public const int MAX_MOVIE_YEAR = 2024;

    public static int MIN_MOVIE_CATEGORY = 0;

    public static int MIN_MOVIE_RATING = 0;
    public static int MAX_MOVIE_RATING = 10;

    public static int MIN_MOVIE_STUDIO = 0;

    public const string QUIT = "0";
    public const string ADD_MOVIE = "1";
    public const string REMOVE_MOVIES_BY_INDEX = "2";
    public const string LIST_MOVIES = "3";
    public const string REMOVE_MOVIES_BY_YEAR = "4";
    public const string REMOVE_MOVIES_BY_RATING = "5";
    public const string SORT_MOVIES_BY_RATING = "6";


    public static void Main(string[] args)
    {
        //Retrieves an array of movies stored in a file
        Movie[] allMovies = ReadMoviesFromFile(MOVIES_FILE);
        bool canEnd = false;

        do
        {
            PresentTitle(TITLE, TITLE_LINE);
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("Menu Options");
            Console.WriteLine("============");
            Console.WriteLine("0) Quit");
            Console.WriteLine("1) Add a movie");
            Console.WriteLine("2) Remove a movie");
            Console.WriteLine("3) List all movies");
            Console.WriteLine("4) Erase a movies by year");
            Console.WriteLine("5) Erase a movies by rating");
            Console.WriteLine("6) Sort a movie by rating");
            Console.WriteLine(" ");
            Console.Write("Your choice: ");
            string inputChoice = Console.ReadLine();

            do
            {
                if (inputChoice == QUIT)
                {
                    canEnd = true;
                    break;
                }

                else if (inputChoice == ADD_MOVIE)
                {
                    Movie newMovies = AddMovie();
                    allMovies = AppendMovie(allMovies, newMovies);
                    ClearChoice();
                    break;

                }
                else if (inputChoice == REMOVE_MOVIES_BY_INDEX)
                {
                    ListMovies(allMovies);
                    int index = InputIndex(allMovies);
                    allMovies = RemoveMovie(allMovies, index);
                    ClearChoice();
                    break;

                }
                else if (inputChoice == LIST_MOVIES)
                {
                    ListMovies(allMovies);
                    ClearChoice();
                    break;

                }
                else if (inputChoice == REMOVE_MOVIES_BY_YEAR)
                {
                    ListMovies(allMovies);
                    int year = InputYear(allMovies);
                    allMovies = RemoveMoviesByYear(allMovies, year);
                    ClearChoice();
                    break;

                }
                else if (inputChoice == REMOVE_MOVIES_BY_RATING)
                {
                    ListMovies(allMovies);
                    int rating = InputRating(allMovies);
                    allMovies = RemoveMoviesByRating(allMovies, rating);
                    ClearChoice();
                    break;

                }
                else if (inputChoice == SORT_MOVIES_BY_RATING)
                {
                    allMovies = SortMoviesByRating(allMovies);
                    ListMovies(allMovies);
                    ClearChoice();
                    break;
                }

                else
                {
                    WriteError(INVALID_VALUE_MESSAGE);
                    inputChoice = Console.ReadLine();
                }
            }
            while (true);
        }
        while (!canEnd);
    }
    public static void PresentTitle(string title, string titleLine)
    {
        string barTitle = "";
        string bar = "";

        for (int i = 0; i < Console.WindowWidth; i++)
            bar += titleLine;

        for (int i = 0; i < Console.WindowWidth - title.Length; i++)
        {
            barTitle += titleLine;

            if (i == (Console.WindowWidth - title.Length) / 2)
                barTitle += title;
        }

        WriteConsole(bar);
        WriteConsole(barTitle);
        WriteConsole(bar);
    }

    public static Movie AddMovie()
    {
        Movie newMovie = new Movie();

        Console.Write("Enter title: ");
        string inputTitle = Console.ReadLine();

        while (inputTitle.Length == 0)
        {
            WriteError(INVALID_VALUE_MESSAGE);
            inputTitle = Console.ReadLine();
        }

        newMovie.Title = inputTitle;

        int year = 0;
        Console.Write($"Enter a year between {MIN_MOVIE_YEAR} and {MAX_MOVIE_YEAR}: ");
        string inputYear = Console.ReadLine();
        bool isYearNumber = int.TryParse(inputYear, out year);
        newMovie.Year = ReturnMovieProperties(isYearNumber, inputYear, year, MIN_MOVIE_YEAR, MAX_MOVIE_YEAR);

        int category = 0;
        Console.Write("Available categories: ");
        Console.WriteLine(WriteCategoryAndStudio(Categories));
        Console.Write("Enter category: ");
        string inputCategory = Console.ReadLine();
        bool isCategoryNumber = int.TryParse(inputCategory, out category);
        newMovie.Category = ReturnMovieProperties(isCategoryNumber, inputCategory, category, MIN_MOVIE_CATEGORY, Categories.Length - 1);

        int rating = 0;
        Console.Write($"Enter rating between {MIN_MOVIE_RATING} and {MAX_MOVIE_RATING}: ");
        string inputRating = Console.ReadLine();
        bool isRatingNumber = int.TryParse(inputRating, out rating);
        newMovie.Rating = ReturnMovieProperties(isRatingNumber, inputRating, rating, MIN_MOVIE_RATING, MAX_MOVIE_RATING);

        int studio = 0;
        Console.Write("Available studios: ");
        Console.WriteLine(WriteCategoryAndStudio(Studios));
        Console.Write("Enter studio: ");
        string inputStudio = Console.ReadLine();
        bool isStudioNumber = int.TryParse(inputStudio, out studio);
        newMovie.Studio = ReturnMovieProperties(isStudioNumber, inputStudio, studio, MIN_MOVIE_STUDIO, Studios.Length - 1);

        return newMovie;
    }

    public static int InputIndex(Movie[] allMovies)
    {
        int index = 0;
        Console.Write("Enter movie index to delete: ");
        string inputIndex = Console.ReadLine();
        bool isNumber = int.TryParse(inputIndex, out index);

        while (allMovies.Length != 0)
        {
            if (index < 0 || index >= allMovies.Length || !isNumber)
            {
                WriteError(INVALID_VALUE_MESSAGE);
                inputIndex = Console.ReadLine();
                isNumber = int.TryParse(inputIndex, out index);
            }
            else
                break;
        }

        return index;
    }

    public static void ListMovies(Movie[] allMovies)
    {
        string data = string.Format("   {0,-20} {1,-10} {2,-10} {3,-10} {4,-10} \n", "Title", "Year", "Category", "Rating", "Studio");
        data += string.Format("   {0,-20} {1,-10} {2,-10} {3,-10} {4,-10} \n", "=====", "====", "========", "======", "========");

        for (int i = 0; i < allMovies.Length; i++)
        {
            string title = allMovies[i].Title;
            int year = allMovies[i].Year;
            string categoryName = ReturnCategoryAndStudio(allMovies[i].Category, Categories);
            int rating = allMovies[i].Rating;
            string studioName = ReturnCategoryAndStudio(allMovies[i].Studio, Studios);

            data += string.Format("{0,-2} {1,-20} {2,-10} {3,-12} {4,-8} {5,-10} \n", i + "-", title, year, categoryName, rating, studioName);
        }

        Console.WriteLine($"\n{data}");
    }

    public static int InputYear(Movie[] allMovies)
    {
        bool isYearInIndex = false;
        int year = 0;
        Console.Write("Enter year to delete: ");
        string inputYear = Console.ReadLine();
        bool isNumber = int.TryParse(inputYear, out year);

        while (allMovies.Length != 0)
        {
            for (int i = 0; i < allMovies.Length; i++)
                if (year == allMovies[i].Year)
                    isYearInIndex = true;

            if (!isYearInIndex || !isNumber)
            {
                WriteError(INVALID_VALUE_MESSAGE);
                inputYear = Console.ReadLine();
                isNumber = int.TryParse(inputYear, out year);
            }
            else
                break;
        }

        return year;
    }

    public static int InputRating(Movie[] allMovies)
    {
        int rating = 0;
        Console.Write("Enter rating to delete: ");
        string inputIndex = Console.ReadLine();
        bool isNumber = int.TryParse(inputIndex, out rating);

        while (allMovies.Length != 0)
        {
            if (!isNumber)
            {
                WriteError(INVALID_VALUE_MESSAGE);
                inputIndex = Console.ReadLine();
                isNumber = int.TryParse(inputIndex, out rating);
            }
            else
                break;
        }

        return rating;
    }

    public static Movie[] SortMoviesByRating(Movie[] allMovies)
    {
        int count = 0;
        Movie value;

        do
        {
            count = 0;

            for (int i = 0; i < allMovies.Length - 1; i++)
            {
                if (allMovies[i].Rating > allMovies[i + 1].Rating)
                {
                    value = allMovies[i];
                    allMovies[i] = allMovies[i + 1];
                    allMovies[i + 1] = value;
                    count = 1;
                }
            }
        }
        while (count > 0);

        return allMovies;
    }

    public static Movie[] RemoveMovie(Movie[] allMovies, int index)
    {
        int j = 0;

        if (index < 0 || index >= allMovies.Length)
            return allMovies;

        Movie[] newMovietab = new Movie[allMovies.Length - 1];

        for (int i = 0; i < allMovies.Length; i++)
        {
            if (i != index)
            {
                newMovietab[j] = allMovies[i];
                j++;
            }
        }

        return newMovietab;
    }

    public static Movie[] RemoveMoviesByYear(Movie[] allMovies, int year)
    {
        int[] tabElementsRemove = new int[allMovies.Length];

        for (int i = 0; i < allMovies.Length; i++)
            tabElementsRemove[i] = allMovies[i].Year;

        Movie[] newMovietab = RemoveByProperty(allMovies, year, tabElementsRemove);

        return newMovietab;
    }

    public static Movie[] RemoveMoviesByRating(Movie[] allMovies, int rating)
    {
        int[] tabElementsRemove = new int[allMovies.Length];

        for (int i = 0; i < allMovies.Length; i++)
            tabElementsRemove[i] = allMovies[i].Rating;

        Movie[] newMovietab = RemoveByProperty(allMovies, rating, tabElementsRemove);

        return newMovietab;
    }

    public static string ReturnCategoryAndStudio(int categoryOrStudio, string[] tab)
    {
        string name = string.Empty;

        for (int i = 0; i < tab.Length; i++)
            if (i == categoryOrStudio)
                name = tab[i];

        return name;
    }

    public static int ReturnMovieProperties(bool isNumber, string inputNumber, int number, int min, int max)
    {
        while (!isNumber || number < min || number > max)
        {
            WriteError(INVALID_VALUE_MESSAGE);
            inputNumber = Console.ReadLine();
            isNumber = int.TryParse(inputNumber, out number);
        }

        return number;
    }

    public static Movie[] AppendMovie(Movie[] allMovies, Movie newMovie)
    {
        Movie[] newMovietab = new Movie[allMovies.Length + 1];

        for (int i = 0; i < allMovies.Length; i++)
            newMovietab[i] = allMovies[i];

        newMovietab[allMovies.Length] = newMovie;

        return newMovietab;
    }

    public static Movie[] RemoveByProperty(Movie[] allMovies, int valueToRemove, int[] tabElementsRemove)
    {
        int counter = 0;
        int j = 0;

        for (int i = 0; i < tabElementsRemove.Length; i++)
            if (valueToRemove == tabElementsRemove[i])
                counter++;

        Movie[] newMovietab = new Movie[allMovies.Length - counter];

        for (int i = 0; i < allMovies.Length; i++)
        {
            if (tabElementsRemove[i] != valueToRemove)
            {
                newMovietab[j] = allMovies[i];
                j++;
            }
        }

        return newMovietab;
    }

    public static string WriteCategoryAndStudio(string[] tab)
    {
        string phrase = "";

        for (int i = 0; i < tab.Length; i++)
            phrase += $"{i}-{tab[i]} ";

        return phrase;
    }

    public static void ClearChoice()
    {
        Console.Write("Press any key to return to main menu... ");
        Console.ReadLine();
        Console.Clear();
    }

    #region Console Display

    //Allows to display an error text in red
    private static void WriteError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    //Allows you to display text in the color of your choice, gray by default
    private static void WriteConsole(string text, ConsoleColor color = ConsoleColor.Green)
    {
        ConsoleColor oldColor = Console.ForegroundColor;
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ForegroundColor = oldColor;
    }
    #endregion


    #region File Reading
    //Functions given for reading movies stored in a file
    //The goal is to give you some movies at the start to work on the display

    //Important: The movies you add will not be saved between executions
    private static Movie[] ReadMoviesFromFile(string fileName)
    {
        List<Movie> allMovies = new List<Movie>();
        string[] allLines = ReadFile(fileName);
        for (int i = 0; i < allLines.Length && !string.IsNullOrEmpty(allLines[i]); i++)
        {
            string[] currentLine = allLines[i].Split(",", StringSplitOptions.RemoveEmptyEntries);
            Movie newMovie = new Movie();
            newMovie.Title = currentLine[0];
            newMovie.Year = int.Parse(currentLine[1]);
            newMovie.Category = int.Parse(currentLine[2]);
            newMovie.Rating = int.Parse(currentLine[3]);
            newMovie.Studio = int.Parse(currentLine[4]);
            allMovies.Add(newMovie);
        }
        return allMovies.ToArray();
    }
    public static string[] ReadFile(string fileName)
    {
        StreamReader reader = new StreamReader(fileName, System.Text.Encoding.UTF8);
        List<string> allLines = new List<string>();

        // Read all lines from the file and add them to the list
        while (!reader.EndOfStream)
        {
#pragma warning disable CS8600 // Conversion of a literal or a potentially null value to a non-nullable type
            string line = reader.ReadLine();
#pragma warning restore CS8600 // Conversion of a literal or a potentially null value to a non-nullable type
            allLines.Add(line);
        }

        reader.Close();

        return allLines.ToArray();
    }

    #endregion
}