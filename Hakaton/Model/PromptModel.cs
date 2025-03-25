// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using Newtonsoft.Json;

public class Category
{
    [JsonProperty("category")]
    public string Category1 { get; set; }

    [JsonProperty("questions")]
    public List<Question> Questions { get; set; }
}

public class PlatformSupportTeam
{
    [JsonProperty("categories")]
    public List<Category> Categories { get; set; }
}

public class Question
{
    [JsonProperty("questionId")]
    public int QuestionId { get; set; }

    [JsonProperty("questionText")]
    public string QuestionText { get; set; }

    [JsonProperty("questionType")]
    public string QuestionType { get; set; }
}

public class RootPrompt
{
    [JsonProperty("PlatformSupportTeam")]
    public PlatformSupportTeam PlatformSupportTeam { get; set; }
}

