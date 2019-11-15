namespace Rems.Domain.Entities
{
    public class ResearcherList
    {
        public int ResearcherListId { get; set; }

        public int? ResearcherId { get; set; }

        public int? ExperimentId { get; set; }        

        public virtual Experiment Experiment { get; set; }
        public virtual Researcher Researcher { get; set; }


    }
}
