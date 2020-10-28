using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkTracker_Khaled
{
    class Program : AssignmentType
    {
        static void Main(string[] args)
        {
            // to be entered into by user
            var asgName = string.Empty;
            var desc = string.Empty;
            var grade = 0;
            var dueDate = DateTime.Today;
            char letterGrade;
            var asgList = new List<AssignmentType>();

            // to be used in calculating avg grade
            var hwTotals = 0;
            var quizTotals = 0;
            var examTotals = 0;

            // number of each assignments
            var hwAmt = 0;
            var quizAmt = 0;
            var examAmt = 0;

            // used in calculating course avg
            double avgHW = 0.0d;
            double avgQuiz = 0.0d;
            double avgExam = 0.0d;
            double avgTotal = 0.0d;

            Console.WriteLine("This is an assignment and course grade tracker.\n");
            DisplayMenu();

            while (true)
            {
                var input = Console.ReadLine();
                var addAnother = Console.ReadLine();

                if (input.Equals("1"))
                {
                    do
                    {
                        Console.WriteLine("Enter the homework name: ");
                        asgName = Console.ReadLine();

                        Console.WriteLine("Enter the homework description: ");
                        desc = Console.ReadLine();

                        Console.WriteLine("Enter the date the homework was due (mm-dd-yyyy): ");
                        dueDate = DateTime.ParseExact(Console.ReadLine(), "MM-dd-yyyy", null);

                        Console.WriteLine("Enter the numeric grade you received for the homework: ");
                        grade = int.Parse(Console.ReadLine());
                        hwTotals += grade;
                        ++hwAmt;

                        letterGrade = LetterEquivalent(grade);

                        asgList.Add(new Homework
                        {
                            Name = asgName,
                            Description = desc,
                            Date = dueDate,
                            NumericGrade = grade,
                            LetterGrade = letterGrade
                        });

                        Console.WriteLine("Enter another homework assignment? \'yes\' or \'no.\'");
                        addAnother = Console.ReadLine();
                    }
                    while (addAnother.Equals("yes") || addAnother.Equals("Yes") || addAnother.Equals("YES"));

                    // if user does not wish to enter another assignment of the same type
                    if (!addAnother.Equals("yes") || !addAnother.Equals("Yes") || !addAnother.Equals("YES"))
                        DisplayMenu();
                    continue;
                }
                else if (input.Equals("2"))
                {
                    do
                    {
                        Console.WriteLine("Enter the quiz name: ");
                        asgName = Console.ReadLine();

                        Console.WriteLine("Enter the quiz description: ");
                        desc = Console.ReadLine();

                        Console.WriteLine("Enter the date the quiz was due (mm-dd-yyyy): ");
                        dueDate = DateTime.ParseExact(Console.ReadLine(), "MM-dd-yyyy", null);

                        Console.WriteLine("Enter the numeric grade you received for the quiz: ");
                        grade = int.Parse(Console.ReadLine());
                        quizTotals += grade;
                        ++quizAmt;

                        letterGrade = LetterEquivalent(grade);

                        asgList.Add(new Quizzes
                        {
                            Name = asgName,
                            Description = desc,
                            Date = dueDate,
                            NumericGrade = grade,
                            LetterGrade = letterGrade
                        });

                        Console.WriteLine("Enter another quiz? \'yes\' or \'no.\'");
                        addAnother = Console.ReadLine();

                    }
                    while (addAnother.Equals("yes") || addAnother.Equals("Yes") || addAnother.Equals("YES"));

                    if (!addAnother.Equals("yes") || !addAnother.Equals("Yes") || !addAnother.Equals("YES"))
                        DisplayMenu();
                    continue;
                }
                else if (input.Equals("3"))
                {
                    do
                    {
                        Console.WriteLine("Enter the exam name: ");
                        asgName = Console.ReadLine();

                        Console.WriteLine("Enter the exam description: ");
                        desc = Console.ReadLine();

                        Console.WriteLine("Enter the date the exam was due (mm-dd-yyyy): ");
                        dueDate = DateTime.ParseExact(Console.ReadLine(), "MM-dd-yyyy", null);

                        Console.WriteLine("Enter the numeric grade you received for the exam: ");
                        grade = int.Parse(Console.ReadLine());
                        examTotals += grade;
                        ++examAmt;

                        letterGrade = LetterEquivalent(grade);

                        asgList.Add(new Exams
                        {
                            Name = asgName,
                            Description = desc,
                            Date = dueDate,
                            NumericGrade = grade,
                            LetterGrade = letterGrade
                        });

                        Console.WriteLine("Enter another exam? \'yes\' or \'no.\'");
                        addAnother = Console.ReadLine();
                    }
                    while (addAnother.Equals("yes") || addAnother.Equals("Yes") || addAnother.Equals("YES"));

                    if (!addAnother.Equals("yes") || !addAnother.Equals("Yes") || !addAnother.Equals("YES"))
                        DisplayMenu();
                    continue;
                }
                else if (input.Equals("4"))
                {
                    Console.WriteLine("Here are the assignments used to calculate your course average: ");
                    Console.WriteLine("HOMEWORK: ");
                    asgList.Where(t => t is Homework).ToList().ForEach(t => Console.WriteLine(t.ToString()));
                    Console.WriteLine("QUIZZES: ");
                    asgList.Where(t => t is Quizzes).ToList().ForEach(t => Console.WriteLine(t.ToString()));
                    Console.WriteLine("EXAMS: ");
                    asgList.Where(t => t is Exams).ToList().ForEach(t => Console.WriteLine(t.ToString()));

                    Console.WriteLine("Here is your overall course average: ");

                    foreach (var asg in asgList)
                    {
                        if (asg is Homework)
                            avgHW = CalculateHomework(hwTotals, hwAmt);
                        else if (asg is Quizzes)
                            avgQuiz = CalculateQuiz(quizTotals, quizAmt);
                        else if (asg is Exams)
                            avgExam = CalculateExam(examTotals, examAmt);
                    }

                    avgTotal = AverageGrade(avgHW, avgQuiz, avgExam);

                    Console.WriteLine($"{avgTotal} ({LetterEquivalent((int)avgTotal)})");
                    Console.ReadLine();
                    Console.WriteLine("Thank you for using the assignment and course grade tracker.\n");
                    break;
                }
                else if (input.Equals("5"))
                {
                    Console.WriteLine("Thank you for using the assignment and course grade tracker.\n");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid selection, try again.\n");
                    DisplayMenu();
                    continue;
                }
            } // end while

            Console.ReadLine();

        } // end main

        static void DisplayMenu()
        {
            Console.WriteLine("Please enter the number next to the action you wish to perform.");
            Console.WriteLine("1. enter new homework\n" +
                              "2. enter new quiz\n" +
                              "3. enter new exam\n" +
                              "4. view course average\n" +
                              "5. exit program\n");
        } // end DisplayMenu

        static char LetterEquivalent(int grade)
        {
            char letter;
            if (grade >= 90)
                letter = 'A';
            else if (grade >= 80 && grade <= 89)
                letter = 'B';
            else if (grade >= 70 && grade <= 79)
                letter = 'C';
            else if (grade >= 60 && grade <= 69)
                letter = 'D';
            else
                letter = 'F';

            return letter;
        } // end LetterEquivalent
    }
}
