using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSecurityBotPart2
{
    public class QuizQuestion
    {
        public string Question { get; set; } = "";
        public List<string> Options { get; set; } = new List<string>();
        public string CorrectAnswer { get; set; } = "";
        public string Explanation { get; set; } = "";

        public string DisplayQuestion(int questionNumber, int totalQuestions)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"Question {questionNumber} of {totalQuestions}");
            builder.AppendLine(Question);
            builder.AppendLine();

            foreach (string option in Options)
            {
                builder.AppendLine(option);
            }

            builder.AppendLine();
            builder.AppendLine("Type your answer, for example: A, B, C, D, True, or False.");

            return builder.ToString();
        }

    }
}
