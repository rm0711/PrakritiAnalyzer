# Prakruti Analyzer App

## Description

The Prakruti Analyzer App is a desktop application built using C# and WPF that helps users determine their Ayurvedic body type (dosha). Through a multiple-choice quiz based on Ayurvedic principles, the app calculates users' dominant doshas — Vata, Pitta, and Kapha — and presents personalized results. The app also includes user authentication and stores data locally in JSON format.

## Personal Information 
- Name - Rohit Mahadik
- Enrollment No. - 202203103510183
- Class - 7D

## Features

- User authentication with registration and login
- Interactive 12-question MCQ quiz
- Dosha analysis with primary and secondary results
- Local storage of user info and quiz results for persistence
- Validation to ensure the quiz is completely answered before submission

## Data Storage

- User login credentials (usernames, emails, passwords) are stored securely and permanently in a local JSON text file named `users.txt`.
- Prakriti quiz results including dosha scores and timestamps are separately stored in another local JSON text file named `quiz_results.txt`.
- Both files reside in a designated `data` folder or specified path, ensuring organized and persistent data handling within the application.

## Installation Steps

1. Ensure you have [Visual Studio](https://visualstudio.microsoft.com/) installed with support for .NET desktop development.
2. Clone or download the project repository:
3. Open the solution `.sln` file in Visual Studio.

## How to Run

1. Open the `PrakrutiAnalyzer` solution in Visual Studio.
2. Build the solution to restore dependencies.
3. Run the project (F5) to launch the WPF application.
4. Register or login with user credentials.
5. Complete the Prakruti quiz and view your dosha analysis.

## Technologies Used

- C#  
- Windows Presentation Foundation (WPF)  
- JSON for data storage (`System.Text.Json`)
