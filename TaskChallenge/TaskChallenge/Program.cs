using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;


struct People
{
    //challenge required information
    public int BirthYear;
    public int DeathYear;
}

namespace TaskChallenge
{
    class Program
    {
        //min year
        public static int minYear = 1900;
        //max year
        public static int maxYear = 2000;
        //101 years available to be alive
        public static int[] numberAlive = new int[101];

        public static int personIndex = 0;

        //offset for array size to years
        public static int arrayOffset = maxYear - numberAlive.Length;

        public static Random rand = new Random();
        static void AddPerson(People referencePerson, List<People> population)
        {
            referencePerson.BirthYear = rand.Next(minYear, maxYear - 1);
            referencePerson.DeathYear = rand.Next(referencePerson.BirthYear + 1, maxYear);
            population.Add(referencePerson);
        }

        static void Main(string[] args)
        {
            //used to see how long the code take to run
            Stopwatch timer = new Stopwatch();
            //create now so a new creation isnt required per person
            People addThisPerson = new People();
            //List of given people
            List<People> population = new List<People>();
            //creates random people and appends them to the population list
            for (long numOfPeople = 0; numOfPeople < 5; numOfPeople++)
            {
                AddPerson(addThisPerson, population);
            }
            //now creation is done lets start the actual application
            Console.WriteLine("Press Any Key to Continue");
            Console.ReadKey();
            Console.Clear();
            //after creation start the timer
            timer.Start();
            //for each person in the population list tally the years the were alive and then put that into the numberAlive array
            foreach (var alivePersons in population)
            {
                AddLifeSpan(alivePersons);
            }
            //Now check the tally to get the years with the Highest Population
            FindYearWithHighestPopulation();
            //stop timer and grab the codes duration
            timer.Stop();
            long duration = timer.ElapsedMilliseconds;
            //Console.WriteLine(duration);
            //just to see the results
            Console.WriteLine("Press Any Key to Exit");
            Console.ReadKey();
        }
        public static List<int> FindYearWithHighestPopulation()
        {
            //list in case multiple years have a tie for number of people alive
            Console.ResetColor();
            List<int> returnThis = new List<int>();
            //keeps track of the highest number of people alive
            int highestNum = 0;
            for (int i = 0; i < numberAlive.Length; i++)
            {
                if (numberAlive[i] > highestNum)
                {
                    returnThis.Clear();
                    highestNum = numberAlive[i];
                    returnThis.Add(i + arrayOffset);
                }
                else if (numberAlive[i] == highestNum)
                {
                    returnThis.Add(i + arrayOffset);
                }
            }

            //-1 is need to be as an array index for said number
            foreach (var item in returnThis)
            {
                Console.SetCursorPosition(item-arrayOffset -1, personIndex);
                Console.Write("-");
            }
            Console.WriteLine();
            Console.Write("Highest Years : ");
            foreach (var item in returnThis)
            {
                Console.Write(item + "  ");
            }
            Console.WriteLine();
            Console.WriteLine("UnderScored Columns are the highest Years(in white)");
            Console.WriteLine("NUM Alive : " + highestNum);
            return returnThis;
        }



        public static void AddLifeSpan(People persons)
        {
            //check the persons relevant birth year to start and then go to death year as long as it is within the range of the array
            for (int i = 0; i  < 101; i++)
            {
                //person isnt bon yet
                if (i < persons.BirthYear - arrayOffset-1)
                {
                    //set dead color and draw graph
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(i, personIndex);
                    Console.Write(i % 10);
                }
                //person is alive
                else if (i < numberAlive.Length && i <= persons.DeathYear - arrayOffset-1)
                {
                    //now make the 'i' or year storeable in the array limits 0-100 must be less than 101
                    numberAlive[i]++;
                    //set alive color and draw graph
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(i, personIndex);
                    Console.Write('-');
                }
                //person has died
                else
                {
                    //person has died output the year's tens spot
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(i, personIndex);
                    Console.Write(i);
                }
            }
            Console.Write(" BY"+persons.BirthYear + " DY" + persons.DeathYear);
            Console.WriteLine();
            personIndex++;
        }
    }
}
