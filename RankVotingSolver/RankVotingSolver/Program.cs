using System;
using System.Collections.Generic;
using System.Linq;


namespace RankVotingSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            var solution = new List<string>
             {
               "Joey",
               "Shubham",
               "Sammie",
               "Chris",
               "Rebecca",
               "Ed"
            };

            var rating = new Ratings();
            rating.Rankings.Add(
            new Ranking
            {
                Id = "Ed",
                Ranks = new List<Element> //shubham
                {
                    new Element{Value= "Joey", IsFact =false},
                    new Element{Value= "Chris", IsFact =true},
                    new Element{Value= "Shubham", IsFact =false},
                    new Element{Value= "Sammie", IsFact =true},
                    new Element{Value= "Ed", IsFact =false}
                }
            });

            rating.Rankings.Add(
            new Ranking
            {
                Id = "Shubham",
                Ranks = new List<Element> //shubham
                {
                    new Element{ Value= "Joey", IsFact = false },
                    new Element{ Value= "Rebecca", IsFact = true },
                    new Element{ Value= "Sammie", IsFact = false },
                    new Element{ Value= "Mercedeze", IsFact = false },
                    new Element{ Value= "Chris", IsFact = false }
                }
            });

            rating.Rankings.Add(
            new Ranking
            {
                Id = "Joey",
                Ranks = new List<Element>
                {
                    new Element{Value= "Sammie", IsFact =true},
                    new Element{Value= "Shubham", IsFact = true},
                    new Element{Value= "Chris", IsFact =false},
                    new Element{Value= "Ed", IsFact =true},
                    new Element{Value= "Rebecca", IsFact =false},                    
                }
            });

            rating.Rankings.Add(
            new Ranking
            {
                Id = "Chris",
                Ranks = new List<Element>
                {
                    new Element{Value= "Joey", IsFact =true},
                    new Element{Value= "Rebecca", IsFact =false},
                    new Element{Value= "Shubham", IsFact =false},
                    new Element{Value= "Sammie", IsFact =false},
                    new Element{Value= "Ed", IsFact =true},
                }
            });


            rating.Rankings.Add(
            new Ranking
            {
                Id = "Sammie",
                Ranks = new List<Element> //Sammie
                {
                    new Element{Value= "Chris", IsFact =false},
                    new Element{Value= "Joey", IsFact =false},
                    new Element{Value= "Rebecca", IsFact =true},
                    new Element{Value= "Ed", IsFact =true},
                    new Element{Value= "Shubham", IsFact =false},
                }
            });

            rating.Rankings.Add(
            new Ranking
            {
                Id = "Rebecca",
                Ranks = new List<Element>
                {
                    new Element{Value= "Shubham", IsFact =true},
                    new Element{Value= "Chris", IsFact =false},
                    new Element{Value= "Sammie", IsFact =false},
                    new Element{Value= "Joey", IsFact =true},
                    new Element{Value= "Ed", IsFact =false},
                }
            });


            var rankSolver = new RankSolver(solution, rating);
            var result = rankSolver.Solve();

            Console.WriteLine("Possible configurations for solution");
            int count = 1;
            foreach (var item in solution)
            {
                Console.WriteLine($"{count} - {item}");
                count++;
            }

            int solutionCount = 0;
            foreach (var res in result)
            {
                solutionCount++;
                Console.WriteLine($"Solution {solutionCount}");
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
               "Joey", "Shubham", "Sammie", "Chris", "Rebecca"
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
