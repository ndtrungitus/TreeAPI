namespace TreeAPI.Models
{
    public class AddNoteRequest
    {
        public NodeData NodeData { get; set; }

        public int ParentId { get; set; }

        public int Index { get; set; }

        public AddNoteRequest()
        {

        }
    }
}
