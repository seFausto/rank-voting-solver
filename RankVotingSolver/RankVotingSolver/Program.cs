using System;
using System.Collections.Generic;
using System.Linq;


namespace RankVotingSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            //GenerateRakings();

            var solution = new List<string>
             {
                "Shubham",
                "Sammie",
                "Rebecca",
                "Chris",
                "Joey",
                "Miranda",
                "Mercedeze"
            };



            var rating = new Ratings();
            rating.Rankings.Add(
                new Ranking
                {
                    Id = "Shubham",
                    Ranks = new List<Element> //shubham
                    {
                        new Element{Value= "Joey", IsKnownValue =false},
                        new Element{Value= "Rebecca", IsKnownValue =false},
                        new Element{Value= "Sammie", IsKnownValue =true},
                        new Element{Value= "Mercedeze", IsKnownValue =true},
                        new Element{Value= "Miranda", IsKnownValue =false},
                        new Element{Value= "Chris", IsKnownValue =true},

                    }
                }
                );
            rating.Rankings.Add(
                new Ranking
                {
                    Id = "Miranda",
                    Ranks = new List<Element>
                    {
                        new Element{Value= "Joey", IsKnownValue =true},
                        new Element{Value= "Shubham", IsKnownValue =false},
                        new Element{Value= "Sammie", IsKnownValue =true},
                        new Element{Value= "Rebecca", IsKnownValue =false},
                        new Element{Value= "Chris", IsKnownValue =false},
                        new Element{Value= "Mercedeze", IsKnownValue =false},
                    }
                }
                );
            rating.Rankings.Add(
                new Ranking
                {
                    Id = "Joey",
                    Ranks = new List<Element>
                    {
                        new Element{Value= "Shubham", IsKnownValue =false},
                        new Element{Value= "Chris", IsKnownValue =false},
                        new Element{Value= "Miranda", IsKnownValue =true},
                        new Element{Value= "Rebecca", IsKnownValue =false},
                        new Element{Value= "Mercedeze", IsKnownValue =false},
                        new Element{Value= "Sammie", IsKnownValue =false},
                    }
                }
                );
            rating.Rankings.Add(
                           new Ranking
                           {
                               Id = "Chris",
                               Ranks = new List<Element>
                               {
                        new Element{Value= "Mercedeze", IsKnownValue =true},
                        new Element{Value= "Rebecca", IsKnownValue =false},
                        new Element{Value= "Joey", IsKnownValue =false},
                        new Element{Value= "Shubham", IsKnownValue =true},
                        new Element{Value= "Sammie", IsKnownValue =false},
                        new Element{Value= "Miranda", IsKnownValue =false},
                               }
                           }
                           );

            rating.Rankings.Add(
                        new Ranking
                        {
                            Id = "Mercedeze",
                            Ranks = new List<Element>
                            {
                        new Element{Value= "Chris", IsKnownValue =true},
                        new Element{Value= "Shubham", IsKnownValue =false},
                        new Element{Value= "Sammie", IsKnownValue =false},
                        new Element{Value= "Miranda", IsKnownValue =false},
                        new Element{Value= "Rebecca", IsKnownValue =true},
                        new Element{Value= "Joey", IsKnownValue =true},
                        
                            }
                        }
                        );

            rating.Rankings.Add(
            new Ranking
            {
                Id = "Sammie",
                Ranks = new List<Element> //Sammie
                {
                        new Element{Value= "Chris", IsKnownValue =false},
                        new Element{Value= "Rebecca", IsKnownValue =true},
                        new Element{Value= "Joey", IsKnownValue =false},
                        new Element{Value= "Miranda", IsKnownValue =true},
                        new Element{Value= "Mercedeze", IsKnownValue =false},
                        new Element{Value= "Shubham", IsKnownValue =false},
                }
            }
            );

            rating.Rankings.Add(
           new Ranking
           {
               Id = "Rebecca",
               Ranks = new List<Element>
               {
                        new Element{Value= "Shubham", IsKnownValue =true},
                        new Element{Value= "Chris", IsKnownValue =false},
                        new Element{Value= "Joey", IsKnownValue =false},
                        new Element{Value= "Miranda", IsKnownValue =false},
                        new Element{Value= "Sammie", IsKnownValue =false},
                        new Element{Value= "Mercedeze", IsKnownValue =false},
               }
           }
           );


            var rankSolver = new RankSolver(solution, rating);


            var result = rankSolver.Solve();

            Console.WriteLine("Possible configurations for solution");
            int count = 1;
            foreach (var item in solution)
            {
                Console.WriteLine($"{count} - {item}");
                count++;
            }

            foreach (var res in result)
            {
                Console.WriteLine("Solution 1");
                foreach (var item in res.Rankings)
                {
                    Console.WriteLine($"Ranks for {item.Id}");
                    int place = 0;
                    foreach (var rank in item.Ranks)
                    {
                        place++;
                        Console.WriteLine($"{place} - {rank.Value}");
                    }
                }
            }


        }

        private static void GenerateRakings()
        {
            var players = new List<string>
            {
                "Alana", "Antonio", "Mercedeze", "Rebecca", "Chris", "Sammie", "Shubham", "Joey"
            };

            var rankDictionary = new Dictionary<string, int>();

            foreach (var item in players)
            {
                rankDictionary.Add(item, 0);
            }

            var votes = new List<List<string>>();

            for (int i = 0; i < 8; i++)
            {
                var vote = players.Shuffle();
                votes.Add(vote);
            }

            //calculate votes
            foreach (var vote in votes)
            {
                foreach (var name in vote)
                {
                    rankDictionary[name] += vote.IndexOf(name);
                }
            }

            //display votes

            int place = 0;
            foreach (KeyValuePair<string, int> item in rankDictionary.OrderBy(key => key.Value))
            {
                place++;
                Console.WriteLine($"{place} - {item.Key}");
            }
        }
    }

    public static class Helper
    {
        private static Random rng = new Random();

        public static List<T> Shuffle<T>(this List<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
        }

    }

}
