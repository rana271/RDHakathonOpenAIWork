using Hakaton.Services;
using Hakaton.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Hakaton.Controllers
{
    [ApiController]
    [Route("api/prompt")]
   
    public class OpenAiController : Controller
    {
        private readonly IOpenAiService _openAiService;
        private readonly IPromptService _promptService;

        public OpenAiController(IOpenAiService openAiService, IPromptService promptService)
        {
            _openAiService = openAiService;
            _promptService = promptService; 
        }
        [HttpPost("generate")]
        public async Task<IActionResult> GeneratePrompt([FromBody] GenerateRequest request)
        {

            
            var filePathString=MockDataUtility.GetMockDataFromFile("ConfigPrompt.json");
            
            // Get the prompt from the JSON file
            var prompt = await _promptService.GetPromptFromFileAsync(filePathString);

            if (prompt==null)
            {
                return BadRequest("Prompt is missing or empty.");
            }

           
            foreach (var item in prompt.PlatformSupportTeam.Categories)
            {
                foreach (var question in item.Questions)
                {
                if(question.QuestionText== request.Context)
                    {
                        var promptResponse = await _openAiService.GetOpenAiResponseAsync(question.QuestionText);
                        return Ok(new { promptResponse });
                    }
                    else if(question.QuestionText != request.Context)
                    {
                        continue;
                    }
                    else
                    {
                        return Ok("Invaid Prompt not lsited");
                    }
                }
            }
            return Ok("Invaid Prompt not lsited");

        }
        public class GenerateRequest
        {
            public string Context { get; set; }
        }
    }
}
