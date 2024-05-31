namespace Module.Explorer.Contracts.Tree.Queries.Get
{
    public class TreeViewModel
    {
        public List<CategoryViewModel> Categories { get; set; }
        public List<JobViewModel> Jobs { get; set; }

        public TreeViewModel()
        {
            Categories = new();
            Jobs = new();
        }
    }
}
