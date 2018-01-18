
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace EventPlanner
{
    public class OrganizeSchedule
    {
        public List<AttendeeVote> attendeeVotes;

        public OrganizeSchedule(List<AttendeeVote> attendeeVotes)
        {
            this.attendeeVotes = attendeeVotes;
        }
        public List<AttendeeVote> TabulateVotes()
        {
            List<AttendeeVote> compiledVotes = AddVotes();
            List<AttendeeVote> organizedVotes = RankVotes(compiledVotes);
            return organizedVotes;
        }

        public List<AttendeeVote> AddVotes()
        {
            List<AttendeeVote>compiledVotes = new List<AttendeeVote>();
            for (int i = 0; i < (attendeeVotes.Count); i++)
            {
                AttendeeVote voteOne = attendeeVotes[i];
                AttendeeVote voteTotal = new AttendeeVote(voteOne.show, voteOne.points);
                for (int k = i+1; k < (attendeeVotes.Count); k++)
                {
                    
                    AttendeeVote voteTwo = attendeeVotes[k];
                    if(voteOne.show == voteTwo.show)
                    {
                        voteTotal.points += voteTwo.points;
                        voteTwo.points = 0;
                    }
                }
                if (voteTotal.points > 0)
                {
                    compiledVotes.Add(voteTotal);
                }
            }
            return compiledVotes;
        }

        public List<AttendeeVote> RankVotes(List<AttendeeVote> compiledVotes)
        {
            for(int i = 0; i<compiledVotes.Count; i++)
                for (int k = 0; k < compiledVotes.Count-1; k++)
                {
                    if ((compiledVotes[k].points < compiledVotes[k+1].points))
                    {
                        AttendeeVote showOne = compiledVotes[k];
                        AttendeeVote showTwo = compiledVotes[k+1];
                        compiledVotes[k+1] = showOne;
                        compiledVotes[k] = showTwo;
                    }
                }

            return compiledVotes;
        }
            

    }
}


