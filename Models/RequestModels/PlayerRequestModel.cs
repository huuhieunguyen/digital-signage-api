namespace CMS.Models.RequestModels
{
    public class PlayerRequestModel
    {
        public Player Player { get; set; }
        public IEnumerable<int> LabelIds { get; set; }
    }
}