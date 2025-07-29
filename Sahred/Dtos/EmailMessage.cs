namespace Shared.Dtos
{
    public class EmailMessage
    {
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

        public List<EmailAttachment> Attachments { get; set; } = new();

        public bool IsHtml { get; set; } = true;
    }
}
