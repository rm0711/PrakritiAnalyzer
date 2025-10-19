using System;

namespace PrakritiAnalyzer.models
{
    public class QuizResult
    {
        public string Username { get; set; }
        public string PrimaryDosha { get; set; }
        public string SecondaryDosha { get; set; }
        public int VataCount { get; set; }
        public int PittaCount { get; set; }
        public int KaphaCount { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
