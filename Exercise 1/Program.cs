using System;
using System.Text.RegularExpressions;

namespace Exercise_1
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            // Print today's date 
            Console.WriteLine("Today's date: " + getDateWithoutTime());
            Console.Write(Environment.NewLine);
            // Print days elsapsed since this year, running day not included 
            Console.WriteLine("Elapsed days since start of the year: " + getDaysElsapsedSinceStartYear());
            Console.Write(Environment.NewLine);
            // Print days elsapsed since this year, running day is included 
            Console.WriteLine("Elapsed days since start of the year and running day included: " + getDaysElsapsedSinceStartYear(true));
            Console.Write(Environment.NewLine);

            // get next leapYear starts with Tuesday
            int yr = getNextLeapYearStartsWithTuesday(2066);
            if (yr == 0)
            {
                Console.WriteLine("Didn't get next leap year starts with Tuestday within the searched range");
                Console.Write(Environment.NewLine);
            }
            if (yr != 0)
            {
                Console.WriteLine("Next leap year starts with Tuestday within the searched range is: " + yr);
                Console.Write(Environment.NewLine);
            }

            // ask email address and validate it
            Console.Write("Enter you're email address: ");

            string emailAddress = Console.ReadLine();
            if (isEmailAddressValid(emailAddress))
            {
                Console.Write($"Entered '{emailAddress}' email address is in the valid format!");
                Console.Write(Environment.NewLine);
                Console.Write(Environment.NewLine);
            }
            if (!isEmailAddressValid(emailAddress))
            {
                Console.Write($"Entered: '{emailAddress}' email address is NOT in the valid format!");
                Console.Write(Environment.NewLine);
                Console.Write(Environment.NewLine);
            }

            // ask user input
            Console.Write("Enter numbers separed with the comma, dot counts as a decimal separator \n" +
                          "both negative and postive numbers are allowed: ");
            var numbers = Console.ReadLine();

            // convert user input to string array
            string[] numbersArray = getArrayFromString(numbers);
            // validate the user input

            if (validateNumbersInput(numbersArray))
            {
                // get average from those numbers
                // convert to double array before
                double[] dobuleNumbersArray = convertStringArrayToDoubleArray(numbersArray);
                double average = getAverageOfNumbers(dobuleNumbersArray);
                // print avg
                Console.WriteLine("You're input average is: " + average);
            }
            if (!validateNumbersInput(numbersArray))
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine("Numbers are inserted in the wrong way! \n" +
                                  "Enter numbers separed with the comma like this -> 5.6, 4, -7, -6.1... \n" +
                                      "- dot counts as a decimal separator \n" +
                                      "- both negative and postive numbers are allowed!");
            }

            Console.ReadKey(true);

            double[] convertStringArrayToDoubleArray(string[] arr)
            {
                double[] doubleArray = new double[arr.Length];
                try
                {
                    for (int i = 0; i < arr.Length; i++)
                    {
                        doubleArray[i] = Convert.ToDouble(arr[i]);
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message +
                                      "Numbers are inserted in the wrong way! \n" +
                                      "Enter numbers separed with the comma like this -> 5.6, 4, -7, -6.1... \n" +
                                      "- dot counts as a decimal separator \n" +
                                      "- both negative and postive numbers are allowed!");
                }
                return doubleArray;
            }

            bool validateNumbersInput(string[] str)
            {
                // Negative and positive number are allowed
                // Dot as a separator point is allowed
                var allowedCharacters = new Regex(@"^-?[0-9\.]+$");

                for (int i = 0; i < str.Length; i++)
                {
                    // Trim possible spaces
                    str[i] = str[i].Trim();
                    // If some number in the array not correct then return false
                    if (!allowedCharacters.IsMatch(str[i]))
                    {
                        return false;
                    }
                }

                return true;
            }

            string getDateWithoutTime()
            {
                var date = DateTime.Now.ToString("yyyy.MM.dd");
                return date;
            }

            int getDaysElsapsedSinceStartYear(bool runningDayIncluded = false)
            {
                // initialize this year
                var thisYear = "01-01-" + DateTime.Now.ToString("yyyy");

                // parse start date and put in DateTime object
                DateTime startDate = DateTime.Parse(thisYear);

                // get the current DateTime
                DateTime now = DateTime.Now;

                // get the TimeSpan of the difference
                TimeSpan elapsed = now.Subtract(startDate);
                var daysAgo = elapsed.TotalDays;

                // round down days to get elapsed days (running day is not included)
                var daysElapsed = Math.Floor(daysAgo);

                // days are always integers
                int days = Convert.ToInt32(daysElapsed);

                // if running day is included as a elsapsed day, then +1 to days
                if (runningDayIncluded)
                {
                    days = days + 1;
                }

                return days;
            }

            // did my own custom leapYear checker, it was more fun this way..
            bool isLeapYear(int year)
            {
                // years divisible by 4 are leap years
                if (year % 4 != 0)
                {
                    return false;
                }
                // excpept centrury's, that are divisible by 400 are leap years, others century's are not
                if (year % 4 == 0 && year % 100 == 0 && year % 400 != 0)
                {
                    return false;
                }

                return true;
            }

            int getNextLeapYearStartsWithTuesday(int untilYear = 2222)
            {
                // if years doesn't exist return 0                
                int year = 0;
                // get a start year, including this year
                int startYear = Convert.ToInt32(
                                DateTime.Now.ToString("yyyy"));


                for (int y = startYear; y <= untilYear; y++)
                {
                    if (isLeapYear(y))
                    {
                        // check if first day of leap year is a Tuesday
                        DateTime dateValue = new DateTime(y, 1, 1);
                        if (dateValue.ToString("dddd") == "Tuesday")
                        {
                            year = y;
                            return year;
                        }
                    }
                }

                return year;
            }

            bool isEmailAddressValid(string email, bool valid = false)
            {
                if (email.Contains("@") && email.Contains("."))
                {
                    valid = true;
                    return valid;
                }

                return valid;
            }

            string[] getArrayFromString(string str)
            {
                var separator = ",";
                String[] elements = Regex.Split(str, separator);
                return elements;
            }

            double getAverageOfNumbers(double[] nums)
            {
                var sum = 0.0;
                var avg = 0.0;

                for (int i = 0; i < nums.Length; i++)
                {
                    sum += nums[i];
                }

                avg = sum / nums.Length;
                return avg;
            }
        }
    }
}
