namespace QuanLyVanTai.BLL.DTOs
{
    public class ChatMessageDto
    {
        public string Role { get; set; } = "user"; // "user", "model", "assistant", "system"
        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }

    public class AiChatbotConfig
    {
        public string Provider { get; set; } = "Gemini"; // "Gemini" hoặc "OpenAI"
        public string ApiKey { get; set; } = string.Empty;
        public string Model { get; set; } = "gemini-1.5-flash"; // gemini-1.5-flash, gemini-2.0-flash, gpt-4o-mini
        public string? CustomApiUrl { get; set; }
        public double Temperature { get; set; } = 0.7;
        public int MaxTokens { get; set; } = 1000;
    }

    public class AiChatbotResponse
    {
        public bool Success { get; set; } = true;
        public string ResponseText { get; set; } = string.Empty;
        public string ProviderUsed { get; set; } = "Gemini";
        public bool IsFallback { get; set; } = false;
        public string? ErrorMessage { get; set; }
    }
}
