using AIGenerator.Data;
using AIGenerator.DTOs;
using AIGenerator.Interface;
using AIGenerator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.SemanticKernel;
using System.Text.Json;
using System.Text.RegularExpressions;


namespace AIGenerator.Services;

public class CvParserService : ICVParser
{
    private readonly Kernel _kernel;

    public CvParserService(Kernel kernel)
    {
        _kernel = kernel;
    }

    public async Task<Resume> ParseCvAsync(string rawText)
    {
        var prompt = @"
Extract CV info and return valid JSON only. Rules:
- Use yyyy or yyyy/mm/dd for dates.
- Use null (no quotes) for missing numbers (e.g., GPA).
- Use null for missing data.
- Use true/false for booleans, not strings.
- generate a reasonable professional summary(Bio) based on the available data.
- summary should be 4–5 line .and Use present tense for skills, past for achievements. No 'I' or name. Keep it concise and professional.
- Output JSON only, no extra text.
- skill type is Soft or Technical and write every skill alone.
- in experience if he still working endDate= Present
- in exsperience if he didn't mention the title or multiple expectid duties fill it from the information you have 
- rewrite duties in reasonable professional
- userPrompt = just the paragraph

{
  ""firstName"": string,
  ""lastName"": string,
  ""email"": string,
  ""phoneNumber"": string,
  ""dateOfBirth"": string,
  ""address"": string,
  ""summary"": string,
  ""title"": string,
  ""userPrompt"": string,
""githubLink"": string,
""facebookLink"": string,
""linkedInLink"": string,
  ""educations"": [
    {
      ""collegeName"": string,
      ""degreeType"": string,
      ""majorName"": string,
      ""startDate"": string,
      ""endDate"": string,
      ""GPA"": double
    }
  ],
  ""experiences"": [
    {
      ""title"": string,
      ""companyName"": string,
      ""startDate"": string,
      ""endDate"": string,
      ""isCurrent"": boolean,
      ""duties"": [string]
    }
  ],
  ""skills"": [
    {
      ""skillName"": string,
      ""skillType"": string
    }
  ],
  ""languages"": [
    {
      ""languageName"": string,
      ""level"": string
    }
  ],
  ""certificates"": [
    {
      ""providerName"": string,
      ""topicName"": string,
      ""startDate"": string,
      ""endDate"": string,
      ""GPA"": double
    }
  ]
}
CV TEXT:
{{$input}}

JSON:
";


        var extractFunction = _kernel.CreateFunctionFromPrompt(prompt);
        var result = await _kernel.InvokeAsync(extractFunction, new()
        {
            ["input"] = rawText
        });

        var json = result.ToString();

        // Replace GPA values that are empty strings with null
        json = Regex.Replace(json, @"""GPA""\s*:\s*""[^""]*""", @"""GPA"": null", RegexOptions.IgnoreCase);
        Resume? resume;

        try
        {
            resume = JsonSerializer.Deserialize<Resume>(json, new JsonSerializerOptions
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

        return resume!;
    }
}