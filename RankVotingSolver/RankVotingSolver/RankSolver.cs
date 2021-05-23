using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankVotingSolver
{
    public class RankSolver
    {
        private readonly List<string> Solution;
        private Ratings Ratings;
        private Dictionary<string, int> RatingsCode = new Dictionary<string, int>();
        private Queue<Ratings> RatingsQueue = new Queue<Ratings>();

        public RankSolver(List<string> solution, Ratings initialRatings)
        {
            Solution = solution;
            Ratings = initialRatings;
        }

        public List<Ratings> Solve()
        {
            var result = new List<Ratings>();

            RatingsQueue.Enqueue(Ratings);

            while (RatingsQueue.Any())
            {
                Debug.WriteLine($"Items in RatingsQueue: {RatingsQueue.Count}");
                Debug.WriteLine($"Items in Ratings Code: {RatingsCode.Count}");
                Debug.WriteLine($"Results: {result.Count}");

                var currentRating = RatingsQueue.Dequeue();

                if (RatingsCode.ContainsKey(currentRating.GetCode()))
                    continue;

                if (IsSolved(currentRating))
                {
                    result.Add(currentRating);
                    RatingsCode.ContainsKey(currentRating.GetCode());
                    continue;
                }

                foreach (var r in currentRating.GetChildren())
                {
                    if (RatingsCode.ContainsKey(r.GetCode()))
                        continue;

                    RatingsQueue.Enqueue(r);
                }

                RatingsCode.Add(currentRating.GetCode(), 0);
            }

            return result;
        }

        private bool IsSolved(Ratings ratings)
        {
            return Solution.SequenceEqual(ratings.FinalRanking);
        }
    }


    public class Element
    {
        public string Value { get; set; }
        public bool IsFact { get; set; }
    }

    public class Ranking
    {
        public string Id { get; set; }


        public Ranking()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Ranking(string name)
        {
            Id = name;
        }


        public List<Element> Ranks { get; set; } = new List<Element>();

        public List<Ranking> GetChildren()
        {
            var result = new List<Ranking>();

            for (int startIndex = 0; startIndex < Ranks.Count - 1; startIndex++)
            {
                for (int swapIndex = startIndex + 1; swapIndex < Ranks.Count; swapIndex++)
                {
                    var newRanking = new Ranking();
                    newRanking.Id = Id;

                    newRanking.Ranks.AddRange(Ranks);

                    newRanking.Swap(startIndex, swapIndex);

                    result.Add(newRanking);
                }

            }

            return result;
        }

        private void Swap(int startIndex, int swapIndex)
        {
            if (Ranks[startIndex].IsFact)
                return;
            if (Ranks[swapIndex].IsFact)
                return;

            var temp = Ranks[startIndex];
            Ranks[startIndex] = Ranks[swapIndex];
            Ranks[swapIndex] = temp;
        }

    }

    public class Ratings
    {
        public List<Ranking> Rankings { get; set; } = new List<Ranking>();
        private List<string> finalRanking = new();

        public List<string> FinalRanking
        {
            get
            {
                if (finalRanking.Any())
                    return finalRanking;

                finalRanking = CalculateFinalRanking();

                return finalRanking;
            }

        }

        private List<string> CalculateFinalRanking()
        {
            var rankDictionary = new Dictionary<string, int>();

            foreach (var item in Rankings.First().Ranks)
            {
                rankDictionary.Add(item.Value, 0);
            }

            foreach (var item in Rankings.Skip(1).First().Ranks)
            {
                if (!rankDictionary.ContainsKey(item.Value))
                    rankDictionary.Add(item.Value, 0);
            }

            foreach (var rank in Rankings)
            {
                foreach (var name in rank.Ranks)
                {
                    if (rankDictionary.ContainsKey(name.Value))
                        rankDictionary[name.Value] += rank.Ranks.IndexOf(name);
                }
            }

            return rankDictionary.OrderBy(key => key.Value).Select(x => x.Key).ToList();
        }

        public List<Ratings> GetChildren()
        {
            var result = new List<Ratings>();

            for (int index = 0; index < Rankings.Count; index++)
            {
                var rankChildren = Rankings[index].GetChildren();

                foreach (var item in rankChildren)
                {
                    var newRatings = new Ratings();
                    newRatings.Rankings.Add(item);
                    newRatings.Rankings.AddRange(Rankings.Where(x => x.Id != Rankings[index].Id));

                    result.Add(newRatings);
                }
            }

            return result;
        }

        public string GetCode()
        {
            var result = new StringBuilder();

            foreach (var rank in Rankings.OrderBy(x => x.Id))
            {
                foreach (var item in rank.Ranks)
                {
                    result.Append(item.Value);
                }

            }

            return result.ToString();
        }
    }

}
