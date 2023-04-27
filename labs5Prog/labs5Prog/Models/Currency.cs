namespace labs5Prog.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public List<Course> Courses { get; set; } = new(); 

    }

}
