using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTracker_Khaled
{
    // acts as Base class
    public class AssignmentType
    {
        // grade weights
        public static double quizWeight = 0.30d;
        public static double homeworkWeight = 0.50d;
        public static double examWeight = 0.20d;

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public char LetterGrade { get; set; }
        public int NumericGrade { get; set; }

        public AssignmentType()
        {
            Name = string.Empty;
            Description = string.Empty;
            Date = DateTime.Today;
            LetterGrade = 'F';
            NumericGrade = 0;
        }

        public static double CalculateHomework(int hwTotals, int hwAmts)
        {
            // this calculates the average hw grade
            var hwAvg = hwTotals / hwAmts;

            // applies weight
            double weightedHW = hwAvg * homeworkWeight;

            return weightedHW;
        }

        public static double CalculateQuiz(int quizTotals, int quizAmts)
        {
            // this calculates the average quiz grade
            var quizAvg = quizTotals / quizAmts;

            // applies weight
            double weightedQuiz = quizAvg * quizWeight;

            return weightedQuiz;
        }

        public static double CalculateExam(int examTotals, int examAmts)
        {
            // this calculates the average exam grade
            var examAvg = examTotals / examAmts;

            // applies weight
            double weightedExam = examAvg * examWeight;

            return weightedExam;
        }

        public static double AverageGrade(double hwAvg, double quizAvg, double examAvg)
        {
            var avg = hwAvg + quizAvg + examAvg;

            return avg;
        }

        public override string ToString()
        {
            return $"Assignment Name: {Name}\nDescription: {Description}\nDue Date: {Date}\nGrade: {NumericGrade} ({LetterGrade})\n";
        }
    }
}
