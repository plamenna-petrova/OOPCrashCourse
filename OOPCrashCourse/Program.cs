using System;
using System.Collections.Generic;
using System.Linq;

namespace OOPCrashCourse
{
    public abstract class Person
    {
        private string id;

        private DateTime createdOn; 

        public DateTime? modifiedOn;

        public string firstName;

        public string lastName;

        public Person(): base()
        {
            Id = Guid.NewGuid().ToString().Substring(0, 7);
            CreatedOn = DateTime.Now;
        }

        public Person(string firstName, string lastName) 
            : base()
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public string Id
        {
            get
            {
                return id;
            }
            private set
            {
                id = value;
            }
        }

        public string FirstName
        {
            get
            {
                return firstName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Invalid country name");
                }

                firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return lastName;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Invalid country name");
                }

                lastName = value;
            }
        }

        public DateTime CreatedOn
        {
            get 
            {
                return createdOn;
            }
            private set
            {
                createdOn = value;
            }
        }

        public DateTime? ModifiedOn
        {
            get
            {
                return modifiedOn;
            }
            set
            {
                modifiedOn = value;
            }
        }

        public override string ToString()
        {
            return $"Info : First Name - {FirstName}, Last Name : {LastName}";
        }
    }

    public class Actor: Person
    {
        public int age;

        private string whereabouts;

        private DateTime birthdate;

        private double turnover;

        private string biography;

        public Actor(): base()
        {

        }

        public Actor(string firstName, string lastName): base(firstName, lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public Actor(string firstName, string lastName, int age)
            : this(firstName, lastName)
        {
            this.age = age;
        }

        public Actor(string firstName, string lastName, int age, double turnover)
            : this(firstName, lastName, age)
        {
            this.turnover = turnover;
        }

        public Actor(
            string firstName, string lastName, int age, 
            double turnover, Country country, bool isPopular
        )
            : this(firstName, lastName, age, turnover)
        {
            Country = country;
            IsPopular = isPopular;
        }

        public int Age 
        { 
            get 
            {
                return age;
            } 
            set
            {
                if (value < 1 && value > 110)
                {
                    throw new ArgumentOutOfRangeException($"Invalid {this.GetType().Name}");
                }

                age = value;
            }
        }

        public string Whereabouts { get => whereabouts; set => whereabouts = value; }

        public DateTime BirthDate { get => birthdate; set => birthdate = value; }

        public double Turnover { get => turnover; set => turnover = value; }

        public string Biography { get => biography; set => biography = value; }

        public bool IsPopular { get; set; }

        public Country Country { get; set; }

        public override string ToString()
        {
            return base.ToString() + $"Age: {Age}, Whereabouts: {Whereabouts}, " +
                $"Birthdate: {BirthDate}, Turnover: {Turnover}, " +
                $"Is Popular : {(IsPopular ? "Yes" : "No")}";
        }
    }

    public class Country
    {
        private string name;

        private int population;

        public Country()
        {

        }

        public Country(string name)
        {
            Name = name;
            //Actors = new List<Actor>();
        }

        //public string Name { get; set; }

        public string Name
        {
            get
            {
                return name;
            }
            // try with private set
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Invalid country name");
                }

                name = value;
            }
        }

        public int Population
        {
            get
            {
                return population;
            }
            // try with private set
            set
            {
                population = value;
            }
        }

        //public List<Actor> Actors { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Country usa = new Country("Usa");
            Country germany = new Country("Germany");
            Country spain = new Country("Spain");
            Country australia = new Country()
            {
                Name = "Australia"
            };
            Country uk = new Country
            {
                Name = "UK"
            };

            uk.Population = 500000;

            Console.WriteLine($"{uk.Name} + - + {uk.Population}");

            List<Country> countriesInitialList = new List<Country>()
            {
                usa,
                germany,
                spain,
                australia,
                uk
            };

            // with a traditional list

            //List<string> moreCountriesNames = new List<string>()
            //{
            //    "Portugal", "Greece", "Finnland", "Romania", "Japan"
            //};

            // try also with a HashSet of unique strings

            HashSet<string> moreCountriesNames = new HashSet<string>()
            {
                "Portugal", "Greece", "Finnland", "Romania", "Japan"
            };

            List<Country> moreCountriesList = new List<Country>();

            // with a traditional list

            //moreCountriesNames.ForEach(x =>
            //{
            //    moreCountriesList.Add(new Country(x));
            //});

            foreach (var countryName in moreCountriesNames)
            {
                moreCountriesList.Add(new Country(countryName));

                // or
                //moreCountriesList.Add(new Country
                //{
                //    Name = countryName
                //});
            }

            var concatenatedListOfCountries = countriesInitialList
                .Concat(moreCountriesList)
                .ToList();

            var countriesStartingWithU = concatenatedListOfCountries
                .Where(c => c.Name.ToLower().StartsWith("u"))
                .ToList();

            foreach (var country in countriesStartingWithU)
            {
                Console.WriteLine($"Country: {country.Name}");
            }

            var countriesWithMoreThanSixCharacters = concatenatedListOfCountries
                    .Any(c => c.Name.Length > 6);

            if (countriesWithMoreThanSixCharacters)
            {
                Console.WriteLine("Yes, there are");
                var targetCountries = concatenatedListOfCountries
                    .Where(c => c.Name.Length > 6)
                    .ToList();

                foreach (var country in targetCountries)
                {
                    Console.WriteLine($"{country.Name}");
                }
            }
            else
            {
                Console.WriteLine("No there aren't");
            }

            Actor firstActor = new Actor();
            //firstActor.id = "1221";
            //firstActor.createdOn = DateTime.UtcNow;
            firstActor.FirstName = "Finn";
            firstActor.LastName = "Wolfhard";
            firstActor.Age = 20;
            firstActor.BirthDate = new DateTime(2022, 10, 8);
            firstActor.Biography = "Mike Wheeler in Stranger Things";
            firstActor.Turnover = 10000000000;
            firstActor.Country = concatenatedListOfCountries[0];
            firstActor.Whereabouts = firstActor.Country.Name + " " 
                +"The Address of the actor - Hawkins";
            firstActor.IsPopular = true;

            var countryOfTheFirstActor = concatenatedListOfCountries
                .FirstOrDefault(c => c.Name == firstActor.Country.Name);

            //countryOfTheFirstActor.Actors.Add(firstActor);

            string firstActorInformation = firstActor.ToString();
            Console.WriteLine(firstActorInformation);

            // add two more actors by following this pattern
            // move the Person class into a folder, named Abstraction
            // move the Actor and Country classes into a separate file under this directory
            // add more actors using the overloaded constructors
            // rewrite the whole logic using only auto properties
            // use the debugger

            // a few more examples

            Actor secondActor = new Actor("Sadie", "Sink");
            secondActor.Age = 21;
            secondActor.BirthDate = new DateTime(2001, 5, 17);
            secondActor.Biography = "Running up that Hill and that's enough";
            secondActor.Turnover = 500000;
            secondActor.Country = concatenatedListOfCountries.ElementAt(0);
            secondActor.Whereabouts = secondActor.Country.Name + " " 
                +"Hawkings, was in California for a time";
            secondActor.IsPopular = true;

            var countryOfTheSecondActor = concatenatedListOfCountries
                .FirstOrDefault(c => c.Name == secondActor.Country.Name);

            //countryOfTheSecondActor.Actors.Add(secondActor);

            string secondActorInformation = secondActor.ToString();
            Console.WriteLine(secondActorInformation);

            Actor thirdActor = new Actor("Milly Bobby", "Brown", 19);
            thirdActor.BirthDate = new DateTime(2001, 5, 4);
            thirdActor.Biography = "Eleven in Stranger Things";
            thirdActor.Turnover = 1500000;
            thirdActor.Country = concatenatedListOfCountries.ElementAt(0);
            thirdActor.Whereabouts = thirdActor.Country.Name + " " 
                +"Hawkings Lab, then Hawkings, then California";
            thirdActor.IsPopular = true;

            var countryOfTheThirdActor = concatenatedListOfCountries
                .FirstOrDefault(c => c.Name == thirdActor.Country.Name);

            //countryOfTheThirdActor.Actors.Add(thirdActor);

            string thirdActorInformation = thirdActor.ToString();
            Console.WriteLine(thirdActorInformation);

            Actor fourthActor = new Actor("Gaten", "Matarazzo", 20, 700000);
            fourthActor.BirthDate = new DateTime(2001, 5, 4);
            fourthActor.Biography = "Eleven in Stranger Things";
            fourthActor.Country = concatenatedListOfCountries.ElementAt(0);
            fourthActor.Whereabouts = fourthActor.Country.Name + " "
                + "lives in Hawkins";
            fourthActor.IsPopular = false;

            var countryOfTheFourthActor = concatenatedListOfCountries
                .FirstOrDefault(c => c.Name == fourthActor.Country.Name);

            //countryOfTheFourthActor.Actors.Add(fourthActor);

            string fourthActorInformation = fourthActor.ToString();
            Console.WriteLine(fourthActorInformation);

            Actor fifthActor = new Actor("Gaten", "Matarazzo", 20, 700000, concatenatedListOfCountries[0], false);
            fifthActor.BirthDate = new DateTime(2001, 5, 4);
            fifthActor.Biography = "Eleven in Stranger Things";
            fifthActor.Country = concatenatedListOfCountries.ElementAt(0);
            fifthActor.Whereabouts = fifthActor.Country.Name + " "
                + "lives in Hawkins";

            var countryOfTheFifthActor = concatenatedListOfCountries
                .FirstOrDefault(c => c.Name == fifthActor.Country.Name);

            //countryOfTheFifthActor.Actors.Add(fifthActor);

            string fifthActorInformation = fifthActor.ToString();
            Console.WriteLine(fifthActorInformation);
        }
    }
}
