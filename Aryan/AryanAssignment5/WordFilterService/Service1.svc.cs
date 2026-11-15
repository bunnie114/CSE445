
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace WordFilterService
{
    public class Service1 : IService1
    {
        /// <summary>
        /// Removes HTML/XML tags, attributes, punctuation,
        /// and common stop words from input text.
        /// </summary>
        /// <param name="str">
        /// Input text or webpage HTML content.
        /// </param>
        /// <returns>
        /// A string containing the remaining meaningful words.
        /// </returns>
        public string WordFilter(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return "";
            }

            // Remove script and style sections.
            string cleanedText = Regex.Replace(
                str,
                @"<\s*(script|style)\b[^>]*>[\s\S]*?<\s*/\s*\1\s*>",
                " ",
                RegexOptions.IgnoreCase
            );

            // Remove HTML/XML tags and their attributes.
            cleanedText = Regex.Replace(
                cleanedText,
                @"<[^>]*>",
                " "
            );

            // Convert HTML entities to normal characters.
            cleanedText = WebUtility.HtmlDecode(cleanedText);

            // Remove punctuation and special characters.
            cleanedText = Regex.Replace(
                cleanedText,
                @"[^\p{L}\p{N}]+",
                " "
            );

            // List of common stop words.
            HashSet<string> stopWords = new HashSet<string>(
                StringComparer.OrdinalIgnoreCase)
            {
                "a", "an", "the", "is", "are", "am",
                "was", "were", "be", "been", "being",
                "in", "on", "at", "to", "for", "of",
                "and", "or", "but", "with", "by",
                "from", "as", "it", "its", "this",
                "that", "these", "those", "he",
                "she", "they", "we", "you", "i",
                "me", "my", "your", "our", "their",
                "his", "her", "them", "us", "do",
                "does", "did", "has", "have", "had",
                "will", "would", "can", "could",
                "should", "not", "so", "if", "then",
                "than", "there", "here", "about",
                "into", "up", "down", "out"
            };

            // Split the cleaned text into words.
            string[] words = cleanedText.Split(
                new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries
            );

            List<string> filteredWords = new List<string>();

            // Keep only meaningful words.
            foreach (string word in words)
            {
                if (!stopWords.Contains(word))
                {
                    filteredWords.Add(word);
                }
            }

            // Return the filtered text.
            return string.Join(" ", filteredWords);
        }
    }
}
