using Microsoft.AspNetCore.Mvc;
using Hors;
using System.Text.Json.Nodes;

namespace DateParserService.Controllers
{
    [ApiController]
    [Route("date")]
    public class DateController : ControllerBase
    {
        [HttpPost]
        public IActionResult ParseDate([FromBody] JsonObject json)
        {
            var textIn = json["text"]?.ToString();
            if (string.IsNullOrEmpty(textIn))
            {
                return BadRequest("Text field is missing or empty.");
            }

            var hors = new HorsTextParser();
            var result = hors.Parse(textIn, DateTime.Now);

            var textResult = result.Text; // -> string: "полить цветы"
            var formatTextResult = result.TextWithTokens; // -> string: "полить цветы {0}"
            var dateResult = result.Dates[0].DateFrom; // -> DateTime: 2025-01-10 12:00:00

            if (result.Dates.Count > 0)
            {
                var formattedDate = dateResult.ToString("yyyy-MM-dd HH:mm");
                var response = new
                {
                    textResult = textResult,
                    formatTextResult = formatTextResult,
                    formattedDate = formattedDate
                };
                return Ok(response);
            }
            else
            {
                return BadRequest("Failed to parse the date.");
            }
        }
    }
}
