using PrakritiAnalyzer.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace PrakritiAnalyzer
{
    public partial class AnalyzeWindow : Window
    {
        private readonly string quizFile = @"D:\WPF_Intern\PrakritiAnalyzer\data\quiz_results.txt";
        private readonly string username;

        public AnalyzeWindow(string loggedUser)
        {
            InitializeComponent();
            username = loggedUser;
            var directory = Path.GetDirectoryName(quizFile);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            if (!File.Exists(quizFile))
                File.WriteAllText(quizFile, "[]");
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            string[] groups = new string[] {
                "Skin", "Build", "Hair", "Eyes", "Mindset", "Memory",
                "Emotions", "Diet", "Sleep", "Energy", "Weather", "Stress"
            };

            bool allAnswered = true;

            foreach (string group in groups)
            {
                bool groupAnswered = QuizGrid.Children
                    .OfType<StackPanel>()
                    .SelectMany(sp => sp.Children.OfType<RadioButton>())
                    .Any(rb => rb.GroupName == group && rb.IsChecked == true);

                if (!groupAnswered)
                {
                    allAnswered = false;
                    break;
                }
            }

            SubmitButton.IsEnabled = allAnswered;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            int vata = 0, pitta = 0, kapha = 0;

            foreach (var rb in QuizGrid.Children.OfType<StackPanel>()
                         .SelectMany(sp => sp.Children.OfType<RadioButton>()))
            {
                if (rb.IsChecked == true)
                {
                    switch (rb.Tag)
                    {
                        case "Vata": vata++; break;
                        case "Pitta": pitta++; break;
                        case "Kapha": kapha++; break;
                    }
                }
            }

            var result = new[] { ("Vata", vata), ("Pitta", pitta), ("Kapha", kapha) }
                            .OrderByDescending(x => x.Item2).ToList();

            var quizResult = new QuizResult
            {
                Username = username,
                PrimaryDosha = result[0].Item1,
                SecondaryDosha = result[1].Item1,
                VataCount = vata,
                PittaCount = pitta,
                KaphaCount = kapha,
                CompletedAt = DateTime.Now
            };

            // Read file safely
            string jsonText = File.ReadAllText(quizFile);
            if (string.IsNullOrWhiteSpace(jsonText))
            {
                jsonText = "[]";
            }

            var results = JsonSerializer.Deserialize<List<QuizResult>>(jsonText) ?? new List<QuizResult>();
            results.Add(quizResult);

            try
            {
                var json = JsonSerializer.Serialize(results, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(quizFile, json);
                MessageBox.Show("Result saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving result: " + ex.Message);
            }

            ResultText.Text = $"Primary Dosha: {result[0].Item1}\nSecondary Dosha: {result[1].Item1}\n\nResult Saved Permanently!";
        }
    }
}
