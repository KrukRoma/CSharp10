using System;
using System.Collections;
using System.Collections.Generic;

namespace CSharp10
{
    public enum Genre
    {
        Action,
        Comedy,
        Drama,
        Horror,
        ScienceFiction,
        Fantasy,
        Documentary
    }

    public class Director : ICloneable
    {
        public string Name { get; set; }
        public string Surname { get; set; }

        public Director(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }

        public object Clone()
        {
            return new Director(Name, Surname);
        }

        public override string ToString()
        {
            return $"{Name} {Surname}";
        }
    }

    public class Movie : IComparable<Movie>, ICloneable
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Director Director { get; set; }
        public string Country { get; set; }
        public Genre Genre { get; set; }
        public int Year { get; set; }
        public double Rating { get; set; }

        public Movie(string title, string description, Director director, string country, Genre genre, int year, double rating)
        {
            Title = title;
            Description = description;
            Director = director;
            Country = country;
            Genre = genre;
            Year = year;
            Rating = rating;
        }

        public int CompareTo(Movie other)
        {
            if (other == null) return 1;
            return Title.CompareTo(other.Title);
        }

        public object Clone()
        {
            return new Movie(Title, Description, (Director)Director.Clone(), Country, Genre, Year, Rating);
        }

        public override string ToString()
        {
            return $"{Title} ({Year}), {Genre}, Directed by {Director}, Rating: {Rating}";
        }
    }

    public class MovieComparerByYear : IComparer<Movie>
    {
        public int Compare(Movie x, Movie y)
        {
            if (x == null || y == null)
                return 0;
            return x.Year.CompareTo(y.Year);
        }
    }

    public class MovieComparerByRating : IComparer<Movie>
    {
        public int Compare(Movie x, Movie y)
        {
            if (x == null || y == null) return 0;
            return y.Rating.CompareTo(x.Rating);
        }
    }

    public class Cinema : IEnumerable<Movie>
    {
        private List<Movie> movies = new List<Movie>();

        public void AddMovie(Movie movie)
        {
            movies.Add(movie);
        }

        public void Sort(IComparer<Movie> comparer)
        {
            movies.Sort(comparer);
        }

        public IEnumerator<Movie> GetEnumerator()
        {
            return movies.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public override string ToString()
        {
            string result = "";
            foreach (var movie in movies)
            {
                result += movie.ToString() + "\n";
            }
            return result;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var director1 = new Director("Steven", "Spielberg");
            var director2 = new Director("Christopher", "Nolan");

            var movie1 = new Movie("Jurassic Park", "Dinosaurs in the park", director1, "USA", Genre.ScienceFiction, 1993, 8.1);
            var movie2 = new Movie("Inception", "Dream within a dream", director2, "USA", Genre.Fantasy, 2010, 8.8);
            var movie3 = new Movie("Schindler's List", "Story of Oskar Schindler", director1, "USA", Genre.Drama, 1993, 8.9);

            var cinema = new Cinema();
            cinema.AddMovie(movie1);
            cinema.AddMovie(movie2);
            cinema.AddMovie(movie3);

            Console.WriteLine("Movies sorted by year:");
            cinema.Sort(new MovieComparerByYear());
            Console.WriteLine(cinema);

            Console.WriteLine("Movies sorted by rating:");
            cinema.Sort(new MovieComparerByRating());
            Console.WriteLine(cinema);
        }
    }
}
