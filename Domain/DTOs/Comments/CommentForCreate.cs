namespace Domain.DTOs.Comments
{
    public class CommentForCreate
    {
        public string UserComment { get; set; }

        public int SecurityTipId { get; set; }

        public string CommenterId { get; set; }
    }
}
