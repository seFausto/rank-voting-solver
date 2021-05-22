# Rank Voting Solver

## Why?

While watching The Circle on netflix, as the audience we only get to see some player's ranking, e.g., we know that Shubham ranked Joey in first place, but we don't know where he ranked Sammie; we know that Sammie ranked Miranda 4th place and Rebecca 5th. So, I wanted to create an application that took these inputs as a puzzle to see what are the possible combinations that give the known Final Ranking.

## How?

The first thing I did was create a quick function that would do the rank voting for a set of inputs. Then I created RankSolver.cs with contains a few classes. To create the solver I'm using a Breath-First approach: Given a rating in a queue, does the resulting rank match the solution? if it doens't, generate all possible children (a list of ratings with one move each) and add them to a queue, then move on to the next rating.

**Element** - This is the individual vote, an element contains a string the represents the person being voted on and, if the vote is know as a fact (this means that when calculation solutions we don't swap this entry). 

**Ranking** - This is contains a single players ranking of elements. The rank is based on their index position on the list. GetChildren returns a List of Ranking that all have one swap. This is used to calculate the next move. The Swap function swaps the rankings based on the start and swap indexes, except for when the rank is a fact. With this we generate "children" which are possible soluions.

**Rating**  - Contains all the rankings and can generate a Final Ranking that is used to see if this matches the known solution.

