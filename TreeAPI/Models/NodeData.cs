namespace TreeAPI.Models
{
    public class NodeData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public NodeData(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
