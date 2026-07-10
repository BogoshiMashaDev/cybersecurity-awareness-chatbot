using System;
using System.Collections.Generic;
using System.Text;

namespace CyberSecurityBotPart2
{
    public class QuizService
    {
        private readonly List<QuizQuestion> questions;
        private int currentQuestionIndex;
        private int score;

        public bool IsQuizActive { get; private set; }

        public QuizService()
        {
            questions = new List<QuizQuestion>
            {
                new QuizQuestion
                {
                    Question = "What should you do if you receive an email asking for your password?",
                    Options = new List<string>
                    {
                        "A) Reply with your password",
                        "B) Delete or report the email as phishing",
                        "C) Forward it to friends",
                        "D) Save it for later"
                    },
                    CorrectAnswer = "B",
                    Explanation = "Correct. Legitimate organisations should not ask for your password by email."
                },
                new QuizQuestion
                {
                    Question = "True or False: A strong password should be unique for each account.",
                    Options = new List<string>
                    {
                        "True",
                        "False"
                    },
                    CorrectAnswer = "TRUE",
                    Explanation = "Correct. Reusing passwords can put multiple accounts at risk."
                },
                new QuizQuestion
                {
                    Question = "Which one is a sign of a phishing message?",
                    Options = new List<string>
                    {
                        "A) It creates urgency or fear",
                        "B) It comes from your saved contacts only",
                        "C) It never has links",
                        "D) It always uses perfect grammar"
                    },
                    CorrectAnswer = "A",
                    Explanation = "Correct. Phishing often uses fear or urgency to pressure users."
                },
                new QuizQuestion
                {
                    Question = "What does two-factor authentication help with?",
                    Options = new List<string>
                    {
                        "A) Making the screen brighter",
                        "B) Adding an extra security step when logging in",
                        "C) Deleting old files",
                        "D) Increasing internet speed"
                    },
                    CorrectAnswer = "B",
                    Explanation = "Correct. Two-factor authentication adds another layer of protection."
                },
                new QuizQuestion
                {
                    Question = "True or False: Public Wi-Fi is always safe for online banking.",
                    Options = new List<string>
                    {
                        "True",
                        "False"
                    },
                    CorrectAnswer = "FALSE",
                    Explanation = "Correct. Public Wi-Fi can be unsafe for sensitive activities like online banking."
                },
                new QuizQuestion
                {
                    Question = "What should you check before entering personal details on a website?",
                    Options = new List<string>
                    {
                        "A) Whether the website uses HTTPS",
                        "B) Whether the website has bright colours",
                        "C) Whether the website has many adverts",
                        "D) Whether the website loads slowly"
                    },
                    CorrectAnswer = "A",
                    Explanation = "Correct. HTTPS helps protect data sent between you and the website."
                },
                new QuizQuestion
                {
                    Question = "What is malware?",
                    Options = new List<string>
                    {
                        "A) Helpful software",
                        "B) Harmful software designed to damage or steal information",
                        "C) A type of keyboard",
                        "D) A safe browser update"
                    },
                    CorrectAnswer = "B",
                    Explanation = "Correct. Malware can damage systems or steal personal information."
                },
                new QuizQuestion
                {
                    Question = "True or False: You should download apps only from trusted sources.",
                    Options = new List<string>
                    {
                        "True",
                        "False"
                    },
                    CorrectAnswer = "TRUE",
                    Explanation = "Correct. Trusted sources reduce the risk of downloading malware."
                },
                new QuizQuestion
                {
                    Question = "Which action helps protect your privacy online?",
                    Options = new List<string>
                    {
                        "A) Sharing your ID number publicly",
                        "B) Posting your home address",
                        "C) Reviewing privacy settings",
                        "D) Accepting every friend request"
                    },
                    CorrectAnswer = "C",
                    Explanation = "Correct. Privacy settings help limit who can see your personal information."
                },
                new QuizQuestion
                {
                    Question = "What is social engineering?",
                    Options = new List<string>
                    {
                        "A) Building social media apps",
                        "B) Manipulating people into revealing information",
                        "C) Designing websites",
                        "D) Fixing computer hardware"
                    },
                    CorrectAnswer = "B",
                    Explanation = "Correct. Social engineering tricks people into giving away information."
                },
                new QuizQuestion
                {
                    Question = "True or False: If an offer looks too good to be true, it may be a scam.",
                    Options = new List<string>
                    {
                        "True",
                        "False"
                    },
                    CorrectAnswer = "TRUE",
                    Explanation = "Correct. Scammers often use unrealistic offers to attract victims."
                },
                new QuizQuestion
                {
                    Question = "What should you do if you are cyberbullied?",
                    Options = new List<string>
                    {
                        "A) Reply aggressively",
                        "B) Ignore all safety steps",
                        "C) Save evidence, block the person, and report it",
                        "D) Share your password"
                    },
                    CorrectAnswer = "C",
                    Explanation = "Correct. Saving evidence, blocking, and reporting are safer responses."
                }
            };

            currentQuestionIndex = 0;
            score = 0;
            IsQuizActive = false;
        }

        public string StartQuiz()
        {
            currentQuestionIndex = 0;
            score = 0;
            IsQuizActive = true;

            return "Cybersecurity quiz started!\n\n" +
                   questions[currentQuestionIndex].DisplayQuestion(currentQuestionIndex + 1, questions.Count);
        }

        public string SubmitAnswer(string input)
        {
            if (!IsQuizActive)
            {
                return "The quiz has not started yet. Type 'start quiz' to begin.";
            }

            string answer = input.Trim().ToUpper();

            if (answer == "T")
            {
                answer = "TRUE";
            }

            if (answer == "F")
            {
                answer = "FALSE";
            }

            QuizQuestion currentQuestion = questions[currentQuestionIndex];

            bool isCorrect = answer == currentQuestion.CorrectAnswer.ToUpper();

            string response;

            if (isCorrect)
            {
                score++;
                response = "Correct! ";
            }
            else
            {
                response = $"Incorrect. The correct answer is {currentQuestion.CorrectAnswer}. ";
            }

            response += currentQuestion.Explanation + "\n\n";

            currentQuestionIndex++;

            if (currentQuestionIndex >= questions.Count)
            {
                IsQuizActive = false;

                response += $"Quiz completed! Your final score is {score}/{questions.Count}.\n";

                if (score >= 10)
                {
                    response += "Great job! You are a cybersecurity pro!";
                }
                else if (score >= 7)
                {
                    response += "Good work! You understand many cybersecurity basics.";
                }
                else
                {
                    response += "Keep learning to stay safe online. You can retake the quiz anytime.";
                }

                return response;
            }

            response += questions[currentQuestionIndex].DisplayQuestion(currentQuestionIndex + 1, questions.Count);
            return response;
        }

        public string StopQuiz()
        {
            IsQuizActive = false;
            return "The quiz has been stopped. You can type 'start quiz' to try again.";
        }
    }
}
