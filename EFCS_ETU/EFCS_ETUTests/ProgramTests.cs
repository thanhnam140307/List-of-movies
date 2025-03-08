using Microsoft.VisualStudio.TestTools.UnitTesting;
using TP3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        #region RemoveMoviesByRating
        [TestMethod()]
        public void t_01_RemoveMoviesByRating_FromEmptyArray_ShouldReturnEmptyArray()
        {
            // Arrange
            // Prepare an empty array of Movies
            Movie[] allMovies = new Movie[0];
            const int ANY_RATING_TO_DELETE = 2;

            // Act
            // Try to remove a rating from an empty Movies array
            Movie[] result = Program.RemoveMoviesByRating(allMovies, ANY_RATING_TO_DELETE);

            // Assert
            // The function returned an empty array of Movies, as expected
            CollectionAssert.AreEqual(new Movie[0], result);
        }

        [TestMethod()]
        public void t_02_RemoveMoviesByRating_FromSingleElementRatingNotPresent_ShouldReturnSameArray()
        {
            // Arrange
            // Prepare an array with 1 Movie
            const int ANY_RATING = 2;
            Movie[] allMovies = new Movie[1] { new Movie() };
            allMovies[0].Year = 2024;
            allMovies[0].Rating = ANY_RATING + 1;
            allMovies[0].Category = 0;
            allMovies[0].Studio = 0;
            allMovies[0].Title = "ANY_TITLE";

            // Act
            // Try to remove a rating that is not in our array
            Movie[] result = Program.RemoveMoviesByRating(allMovies, ANY_RATING);

            // Assert
            // The function returned the same array of Movies intact, as expected 
            CollectionAssert.AreEqual(allMovies, result);
        }

        [TestMethod()]
        public void t_03_RemoveMoviesByRating_FromSingleElementRatingPresent_ShouldReturnEmptyArray()
        {
            // Arrange
            // Prepare an array with 1 Movie
            const int ANY_RATING = 2;
            Movie[] allMovies = new Movie[1] { new Movie() };
            allMovies[0].Year = 2024;
            allMovies[0].Rating = ANY_RATING;
            allMovies[0].Category = 0;
            allMovies[0].Studio = 0;
            allMovies[0].Title = "ANY_TITLE";

            // Act
            // Try to remove a rating that is in our array
            Movie[] result = Program.RemoveMoviesByRating(allMovies, ANY_RATING);

            // Assert
            // The function removed the movie and returned an empty array of Movies, as expected
            CollectionAssert.AreEqual(new Movie[0], result);
        }

        [TestMethod()]
        public void t_04_RemoveMoviesByRating_FromMultipleElementsRatingPresentAtBeginning_ShouldRemoveRating()
        {
            // Arrange
            // Prepare an array with 10 Movies
            const int RATING = 0;
            const int NB_Movies = 9;
            Movie[] allMovies = new Movie[NB_Movies];
            Movie[] expectedMovies = new Movie[0];

            for (int i = 0; i < NB_Movies; i++)
            {
                allMovies[i] = new Movie();
                allMovies[i].Year = 2024;
                allMovies[i].Rating = i / 3;
                allMovies[i].Category = 0;
                allMovies[i].Studio = 0;
                allMovies[i].Title = $"Rating{i / 3}";
                if (allMovies[i].Rating != RATING)
                {
                    expectedMovies = Append(expectedMovies, allMovies[i]);
                }
            }

            // Act
            // Try to remove a rating that is in our array at the beginning
            Movie[] result = Program.RemoveMoviesByRating(allMovies, RATING);

            // Assert
            // The function removed the Movies, as expected
            Assert.IsTrue(CompareArraysByTitles(result, expectedMovies));
        }

        [TestMethod()]
        public void t_05_RemoveMoviesByRating_FromMultipleElementsRatingPresentInTheMiddle_ShouldRemoveRating()
        {
            // Arrange
            // Prepare an array with 10 Movies
            const int RATING = 1;
            const int NB_Movies = 9;
            Movie[] allMovies = new Movie[NB_Movies];
            Movie[] expectedMovies = new Movie[0];
            for (int i = 0; i < NB_Movies; i++)
            {
                allMovies[i] = new Movie();
                allMovies[i].Year = 2024;
                allMovies[i].Rating = i / 3;
                allMovies[i].Category = 0;
                allMovies[i].Studio = 0;
                allMovies[i].Title = $"Rating{i / 3}";
                if (allMovies[i].Rating != RATING)
                {
                    expectedMovies = Append(expectedMovies, allMovies[i]);
                }
            }

            // Act
            // Try to remove a rating that is in our array in the middle
            Movie[] result = Program.RemoveMoviesByRating(allMovies, RATING);

            // Assert
            // The function removed the Movie and returned an array of Movies as expected
            Assert.IsTrue(CompareArraysByTitles(result, expectedMovies));
        }

        [TestMethod()]
        public void t_06_RemoveMoviesByRating_FromMultipleElementsRatingPresentAtTheEnd_ShouldRemoveRating()
        {
            // Arrange
            // Prepare an array with 10 Movies
            const int RATING = 2;
            const int NB_Movies = 9;
            Movie[] allMovies = new Movie[NB_Movies];
            Movie[] expectedMovies = new Movie[0];
            for (int i = 0; i < NB_Movies; i++)
            {
                allMovies[i] = new Movie();
                allMovies[i].Year = 2024;
                allMovies[i].Rating = i / 3;
                allMovies[i].Category = 0;
                allMovies[i].Studio = 0;
                allMovies[i].Title = $"Rating{i / 3}";
                if (allMovies[i].Rating != RATING)
                {
                    expectedMovies = Append(expectedMovies, allMovies[i]);
                }
            }

            // Act
            // Try to remove a rating that is in our array at the end
            Movie[] result = Program.RemoveMoviesByRating(allMovies, RATING);

            // Assert
            // The function removed the Movie and returned an array of Movies as expected
            Assert.IsTrue(CompareArraysByTitles(result, expectedMovies));
        }
        #endregion

        #region RemoveMoviesByYear
        [TestMethod()]
        public void t_07_RemoveMoviesByYear_FromEmptyArray_ShouldReturnEmptyArray()
        {
            // Same test as t_01, with the year
            // Arrange
            Movie[] allMovies = new Movie[0];
            const int ANY_YEAR_TO_DELETE = 2002;

            // Act
            Movie[] result = Program.RemoveMoviesByYear(allMovies, ANY_YEAR_TO_DELETE);

            // Assert
            CollectionAssert.AreEqual(new Movie[0], result);
        }
        [TestMethod()]
        public void t_08_RemoveMoviesByYear_FromSingleElementYearNotPresent_ShouldReturnSameArray()
        {
            // Same test as t_02, with the year
            // Arrange
            const int ANY_YEAR = 2;
            Movie[] allMovies = new Movie[1] { new Movie() };
            allMovies[0].Year = ANY_YEAR + 1;
            allMovies[0].Rating = 0;
            allMovies[0].Category = 0;
            allMovies[0].Studio = 0;
            allMovies[0].Title = "ANY_TITLE";

            // Act
            Movie[] result = Program.RemoveMoviesByYear(allMovies, ANY_YEAR);

            // Assert
            CollectionAssert.AreEqual(allMovies, result);
        }
        [TestMethod()]
        public void t_09_RemoveMoviesByYear_FromSingleElementYearPresent_ShouldReturnEmptyArray()
        {
            // Same test as t_03, with the year
            // Arrange
            const int ANY_YEAR = 2002;
            Movie[] allMovies = new Movie[1] { new Movie() };
            allMovies[0].Year = ANY_YEAR;
            allMovies[0].Rating = 0;
            allMovies[0].Category = 0;
            allMovies[0].Studio = 0;
            allMovies[0].Title = "ANY_TITLE";

            // Act
            Movie[] result = Program.RemoveMoviesByYear(allMovies, ANY_YEAR);

            // Assert
            CollectionAssert.AreEqual(new Movie[0], result);
        }

        [TestMethod()]
        public void t_10_RemoveMoviesByYear_FromMultipleElementsYearPresentAtBeginning_ShouldRemoveYear()
        {
            // Same test as t_04, with the year
            // Arrange
            const int YEAR = 1999;
            const int NB_Movies = 9;
            Movie[] allMovies = new Movie[NB_Movies];
            Movie[] expectedMovies = new Movie[0];
            for (int i = 0; i < NB_Movies; i++)
            {
                allMovies[i] = new Movie();
                allMovies[i].Year = YEAR + i / 3;
                allMovies[i].Rating = i / 3;
                allMovies[i].Category = 0;
                allMovies[i].Studio = 0;
                allMovies[i].Title = $"YEAR{i / 3}";
                if (allMovies[i].Year != YEAR)
                {
                    expectedMovies = Append(expectedMovies, allMovies[i]);
                }
            }
            // Act
            Movie[] result = Program.RemoveMoviesByYear(allMovies, YEAR);

            // Assert
            Assert.IsTrue(CompareArraysByTitles(result, expectedMovies));
        }

        [TestMethod()]
        public void t_11_RemoveMoviesByYear_FromMultipleElementsYearPresentInTheMiddle_ShouldRemoveYear()
        {
            // Same test as t_05, with the year
            // Arrange
            const int YEAR = 2000;
            const int NB_Movies = 9;
            Movie[] allMovies = new Movie[NB_Movies];
            Movie[] expectedMovies = new Movie[0];
            for (int i = 0; i < NB_Movies; i++)
            {
                allMovies[i] = new Movie();
                allMovies[i].Year = YEAR + i / 3;
                allMovies[i].Rating = i / 3 + 1;
                allMovies[i].Category = 0;
                allMovies[i].Studio = 0;
                allMovies[i].Title = $"YEAR{i / 3}";
                if (allMovies[i].Year != YEAR + 1)
                {
                    expectedMovies = Append(expectedMovies, allMovies[i]);
                }
            }
            // Act
            Movie[] result = Program.RemoveMoviesByYear(allMovies, YEAR + 1);

            // Assert
            Assert.IsTrue(CompareArraysByTitles(result, expectedMovies));
        }
        [TestMethod()]
        public void t_12_RemoveMoviesByYear_FromMultipleElementsYearPresentAtTheEnd_ShouldRemoveYear()
        {
            // Same test as t_06, with the year
            // Arrange
            const int YEAR = 1999;
            const int NB_Movies = 9;
            Movie[] allMovies = new Movie[NB_Movies];
            Movie[] expectedMovies = new Movie[0];
            for (int i = 0; i < NB_Movies; i++)
            {
                allMovies[i] = new Movie();
                allMovies[i].Year = YEAR + i / 3;
                allMovies[i].Rating = i / 3 + 1;
                allMovies[i].Category = 0;
                allMovies[i].Studio = 0;
                allMovies[i].Title = $"YEAR{i / 3}";
                if (allMovies[i].Year != YEAR + 2)
                {
                    expectedMovies = Append(expectedMovies, allMovies[i]);
                }
            }
            // Act
            Movie[] result = Program.RemoveMoviesByYear(allMovies, YEAR + 2);

            // Assert
            Assert.IsTrue(CompareArraysByTitles(result, expectedMovies));
        }
        #endregion

        #region RemoveMovie
        [TestMethod()]
        public void t_13_RemoveMovie_FromEmptyArray_ShouldReturnEmptyArray()
        {
            // Arrange
            // Create an empty array of movies
            Movie[] allMovies = new Movie[0];
            const int ANY_INDEX = 99;

            // Act
            // Try to remove a non-existent index
            Movie[] result = Program.RemoveMovie(allMovies, ANY_INDEX);

            // Assert
            // The function returned an empty array of Movies, as expected
            CollectionAssert.AreEqual(new Movie[0], result);
        }

        [TestMethod()]
        public void t_14_RemoveMovie_FromSingleElementMovieNotPresent_ShouldReturnSameArray()
        {
            // Arrange
            // Create a movie
            const int INDEX = 99;
            const int ANY_YEAR = 2002;
            Movie[] allMovies = new Movie[1] { new Movie() };
            allMovies[0].Year = ANY_YEAR;
            allMovies[0].Rating = 0;
            allMovies[0].Category = 0;
            allMovies[0].Studio = 0;
            allMovies[0].Title = "ANY_TITLE";

            // Act
            // Try to remove an index that is not present
            Movie[] result = Program.RemoveMovie(allMovies, INDEX);

            // Assert
            // The array returned is intact
            CollectionAssert.AreEqual(allMovies, result);
        }

        [TestMethod()]
        public void t_15_RemoveMovie_FromSingleElementMoviePresent_ShouldReturnEmptyArray()
        {
            // Arrange
            // Create a movie
            const int INDEX = 0;
            const int ANY_YEAR = 2002;
            Movie[] allMovies = new Movie[1] { new Movie() };
            allMovies[0].Year = ANY_YEAR;
            allMovies[0].Rating = 0;
            allMovies[0].Category = 0;
            allMovies[0].Studio = 0;
            allMovies[0].Title = "ANY_TITLE";

            // Act
            // Try to remove a present index in the array
            Movie[] result = Program.RemoveMovie(allMovies, INDEX);

            // Assert
            // The array returned is empty
            CollectionAssert.AreEqual(new Movie[0], result);
        }

        [TestMethod()]
        public void t_16_RemoveMovie_FromMultipleElementsIndexAtBeginning_ShouldRemoveMovie()
        {
            // Arrange
            // Prepare an array with 10 Movies
            const int NB_Movies = 10;
            Movie[] allMovies = new Movie[NB_Movies];
            Movie[] expectedMovies = new Movie[0];
            const int INDEX = 0;
            for (int i = 0; i < NB_Movies; i++)
            {
                allMovies[i] = new Movie();
                allMovies[i].Year = 2024;
                allMovies[i].Rating = i / 3 + 1;
                allMovies[i].Category = 0;
                allMovies[i].Studio = 0;
                allMovies[i].Title = "ANY_TITLE";
                if (i != INDEX)
                {
                    expectedMovies = Append(expectedMovies, allMovies[i]);
                }
            }
            // Act
            // Try to remove the index at the beginning
            Movie[] result = Program.RemoveMovie(allMovies, INDEX);

            // Assert
            // The function removed the movie from the array
            Assert.IsTrue(CompareArraysByTitles(result, expectedMovies));
        }

        [TestMethod()]
        public void t_17_RemoveMovie_FromMultipleElementsIndexInTheMiddle_ShouldRemoveMovie()
        {
            // Arrange
            // Prepare an array with 10 Movies
            const int NB_Movies = 10;
            Movie[] allMovies = new Movie[NB_Movies];
            Movie[] expectedMovies = new Movie[0];
            const int INDEX = NB_Movies / 2;
            for (int i = 0; i < NB_Movies; i++)
            {
                allMovies[i] = new Movie();
                allMovies[i].Year = 2024;
                allMovies[i].Rating = i / 3 + 1;
                allMovies[i].Category = 0;
                allMovies[i].Studio = 0;
                allMovies[i].Title = "ANY_TITLE";
                if (i != INDEX)
                {
                    expectedMovies = Append(expectedMovies, allMovies[i]);
                }
            }
            // Act
            // Try to remove a movie in the middle of the array
            Movie[] result = Program.RemoveMovie(allMovies, INDEX);

            // Assert
            // The function removed the movie from the array
            Assert.IsTrue(CompareArraysByTitles(result, expectedMovies));
        }

        [TestMethod()]
        public void t_18_RemoveMovie_FromMultipleElementsIndexAtEnd_ShouldRemoveMovie()
        {
            // Arrange
            // Prepare an array with 10 Movies
            const int NB_Movies = 10;
            Movie[] allMovies = new Movie[NB_Movies];
            Movie[] expectedMovies = new Movie[0];
            const int INDEX = NB_Movies - 1;
            for (int i = 0; i < NB_Movies; i++)
            {
                allMovies[i] = new Movie();
                allMovies[i].Year = 2024;
                allMovies[i].Rating = i / 3 + 1;
                allMovies[i].Category = 0;
                allMovies[i].Studio = 0;
                allMovies[i].Title = "ANY_TITLE";
                if (i != INDEX)
                {
                    expectedMovies = Append(expectedMovies, allMovies[i]);
                }
            }
            // Act
            // Try to remove the movie at the end of the array
            Movie[] result = Program.RemoveMovie(allMovies, INDEX);

            // Assert
            // The function removed the movie from the array
            Assert.IsTrue(CompareArraysByTitles(result, expectedMovies));
        }
        #endregion

        #region SortMoviesByRating
        [TestMethod()]
        public void t_19_SortMovies_MultipleElementArrayDescending_ShouldSortArray()
        {
            // Arrange
            // Prepare an array with 10 Movies with ratings from 10 to 1
            const int NB_Movies = 10;
            Movie[] allMovies = new Movie[NB_Movies];
            Movie[] expectedMovies = new Movie[0];
            for (int i = 0; i < NB_Movies; i++)
            {
                allMovies[i] = new Movie();
                allMovies[i].Year = 2024;
                allMovies[i].Rating = NB_Movies - i;
                allMovies[i].Category = 0;
                allMovies[i].Studio = 0;
                allMovies[i].Title = "ANY_TITLE";

                expectedMovies = PrePend(expectedMovies, allMovies[i]);
            }
            // Act
            // Sort the array by rating
            Program.SortMoviesByRating(allMovies);

            // Assert
            // The function sorted the movies by rating as expected
            Assert.IsTrue(CompareArraysByTitles(allMovies, expectedMovies));
        }

        [TestMethod()]
        public void t_20_SortMovies_MultipleElementArrayAscending_ShouldPreserveArray()
        {
            // Arrange
            // Prepare an array with 10 Movies with ratings from 1 to 10
            const int NB_Movies = 10;
            Movie[] allMovies = new Movie[NB_Movies];
            Movie[] expectedMovies = new Movie[0];
            for (int i = 0; i < NB_Movies; i++)
            {
                allMovies[i] = new Movie();
                allMovies[i].Year = 2024;
                allMovies[i].Rating = NB_Movies - i;
                allMovies[i].Category = 0;
                allMovies[i].Studio = 0;
                allMovies[i].Title = "ANY_TITLE";

                expectedMovies = Append(expectedMovies, allMovies[i]);
            }
            // Act
            // Sort the array by rating
            Program.SortMoviesByRating(allMovies);

            // Assert
            // The array remains intact, as expected
            Assert.IsTrue(CompareArraysByTitles(allMovies, expectedMovies));
        }

        [TestMethod()]
        public void t_21_SortMovies_SingleElementArrayAscending_ShouldPreserveArray()
        {
            // Arrange
            // Prepare an array with 1 Movie with rating 5
            Movie[] allMovies = new Movie[0];
            Movie[] expectedMovies = new Movie[0];
            Movie movie = new Movie();
            movie.Year = 2024;
            movie.Rating = 5;
            movie.Category = 0;
            movie.Studio = 0;
            movie.Title = "ANY_TITLE";
            allMovies = Append(allMovies, movie);
            expectedMovies = Append(expectedMovies, movie);

            // Act
            // Sort the array by rating
            Program.SortMoviesByRating(allMovies);

            // Assert
            // The array remains intact, as expected
            Assert.IsTrue(CompareArraysByTitles(allMovies, expectedMovies));
        }

        [TestMethod()]
        public void t_22_SortMovies_FromEmptyArray_ShouldReturnEmptyArray()
        {
            // Arrange
            // Prepare an empty array of movies
            Movie[] allMovies = new Movie[0];
            Movie[] expectedMovies = new Movie[0];

            // Act
            // Sort the array by rating
            Program.SortMoviesByRating(allMovies);

            // Assert
            // The array remains empty, as expected
            Assert.IsTrue(CompareArraysByTitles(allMovies, expectedMovies));
        }
        #endregion


        //Utility functions for testing
        private Movie[] Append(Movie[] array, Movie movie)
        {
            Movie[] newArray = new Movie[array.Length + 1];
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[i];
            }
            newArray[array.Length] = movie;

            return newArray;

        }

        private Movie[] PrePend(Movie[] array, Movie movie)
        {
            Movie[] newArray = new Movie[array.Length + 1];
            for (int i = newArray.Length - 1; i > 0; i--)
            {
                newArray[i] = array[i - 1];
            }
            newArray[0] = movie;

            return newArray;

        }

        private bool CompareArraysByTitles(Movie[] firstArray, Movie[] secondArray)
        {
            bool areEqual = true;
            int i = 0;
            if (firstArray.Length != secondArray.Length)
            {
                areEqual = false;
            }
            else
            {
                while (i < firstArray.Length)
                {
                    if (firstArray[i].Title != secondArray[i].Title)
                    {
                        areEqual = false;
                        break;
                    }
                    i++;
                }
            }

            return areEqual;
        }
    }
}