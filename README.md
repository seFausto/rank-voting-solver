# Rank Voting Solver

## Why?

While watching The Circle on Netflix, as the audience we only get to see some player's ranking, e.g., we know that Shubham ranked Joey in the first place, but we don't know where he ranked Sammie; we know that Sammie ranked Miranda 4th place and Rebecca 5th. So, I wanted to create an application that took these inputs as a puzzle to see what are the possible combinations that give the known Final Ranking.

## How?

The first thing I did was create a quick function that would do the rank voting for a set of inputs. Then I created RankSolver.cs with contains a few classes. To create the solver I'm using a Breath-First approach: Given a rating in a queue, does the resulting rank match the solution? if it doesn't, generate all possible children (a list of ratings with one move each) and add them to a queue, then move on to the next rating.

**Element** - This is the individual vote, an element contains a string the represents the person being voted on and, if the vote is known as a fact (this means that when calculation solutions we don't swap this entry). 

**Ranking** - This contains a single player's ranking of elements. The rank is based on their index position on the list. GetChildren returns a List of Ranking that all have one swap. This is used to calculate the next move. The Swap function swaps the rankings based on the start and swap indexes, except for when the rank is a fact. With this, we generate "children" which are possible solutions.

**Rating**  - Contains the rankings for all players and can generate a Final Ranking that is used to see if this matches the known solution.

# Issues

What I didn't realize when I was building this was the sheer amount of potential solutions that need to be evaluated. Without knowing any vote, there are 6! possible rankings for one player. Then there are 7 players at one point. That means 6!^7, which evaluates to: 100,306,130,042,880,000,000. **One hundred Quintillion** possibilities.

Since this is a brute force solution, it is feasible to find all the combinations that output the expected solution, it will take a very long time.
