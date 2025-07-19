using AIGenerator.Interface;
using AIGenerator.Models;
using Microsoft.SemanticKernel;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace AIGenerator.Services
{
    public class PorfolioParserService : IPortfolioParser
    {
        private readonly Kernel _kernel;

        public PorfolioParserService(Kernel kernel)
        {
            _kernel = kernel;
        }

        public async Task<Portfolio> ParsePortfolioAsync(string rawText)
        {
            var prompt = @"
Extract Portfolio info and return valid JSON only. Rules:
- Use yyyy or yyyy/mm/dd for dates.
- Use null (no quotes) for missing numbers (e.g., GPA).
- Use null for missing data.
- Use true/false for booleans, not strings.
- generate a reasonable professional summary(Bio) based on the available data.
- summary should be 4–5 line .and Use present tense for skills, past for achievements. No 'I' or name. Keep it concise and professional.
- Output JSON only, no extra text.

{
  ""firstName"": string,
  ""secondName"": string,
  ""thirdName"": string,
  ""lastName"": string,
  ""email"": string,
  ""phoneNumber"": string,
  ""dateOfBirth"": string,
  ""address"": string,
  ""summary"": string,
  ""title"": string,
  ""linkedInLink"": string,
  ""facebookLink"": string,
  ""githubLink"": string,
  

}
Portfolio TEXT:
{{$input}}

JSON:
"
            ;


            var extractFunction = _kernel.CreateFunctionFromPrompt(prompt);
            var result = await _kernel.InvokeAsync(extractFunction, new()
            {
                ["input"] = rawText
            });

            var json = result.ToString();

          
            Portfolio? portfolio;

            try
            {
                portfolio = JsonSerializer.Deserialize<Portfolio>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Deserialization error: " + ex.Message);
                Console.WriteLine("Offending JSON: " + json);
                throw;
            }



            return portfolio!;
        }
    }
    }

