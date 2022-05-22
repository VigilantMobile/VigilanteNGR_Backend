namespace Application.Features.Comments
{
    public class CommentViewModel
    {
        public class CommentRequest
        {
            public string UserComment { get; set; }

            public int SecurityTipId { get; set; }

            public string CommenterId { get; set; }
        }
    }
}
