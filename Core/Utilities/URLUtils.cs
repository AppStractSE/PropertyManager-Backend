using System.Text;
using System.Text.RegularExpressions;

namespace Core.Utilities
{
    public static class URLUtils
    {

        public static string GenerateSlug(string input)
        {
            // Normalize the input string to NFKD form and remove diacritics
            string normalizedInput = input.Normalize(NormalizationForm.FormKD);
            normalizedInput = new string(normalizedInput
                .Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                .Select(c => char.ToLower(c))
                .ToArray());
            // Replace spaces with hyphens
            normalizedInput = normalizedInput.Trim().Replace(' ', '-');
            return normalizedInput;
        }
    }
}
