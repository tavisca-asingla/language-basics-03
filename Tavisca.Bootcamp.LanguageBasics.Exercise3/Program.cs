using System;
using System.Collections.Generic;
using System.Linq;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            //Console.Write($"Expected = [{string.Join(", ", expected)}]\n");
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");

            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
            // Add your code here.
            //throw new NotImplementedException();
            
            int[] calorie = new int[protein.Length];
            for(int i = 0; i < protein.Length; i++)
            {
                calorie[i] = fat[i] * 9 + protein[i] * 5 + carbs[i] * 5;

            }
            //Console.Write($"Calorie = [{string.Join(", ", calorie)}]\n");
            int[] exp = new int[dietPlans.Length];
            for(int i = 0; i < dietPlans.Length; i++)
            {
                List<int> allowed = new List<Int32>();
                char[] plans = dietPlans[i].ToCharArray();
                for (int k = 0; k < protein.Length; k++)
                {
                    allowed.Add(k);

                }
                for (int j = 0; j < plans.Length; j++)
                {
                    if (plans[j].Equals('P'))
                    {
                        int max = getValue(protein, allowed);
                        allowed = getAllowed(protein, allowed, max);
                        
                    }
                    else if (plans[j].Equals('p'))
                    {
                        int min = getValue(protein, allowed,"min");
                        allowed = getAllowed(protein, allowed, min);
                    }
                    else if (plans[j].Equals('C'))
                    {
                        int max = getValue(carbs, allowed);
                        allowed = getAllowed(carbs, allowed, max);
                    }
                    else if (plans[j].Equals('c'))
                    {
                        int min = getValue(carbs, allowed,"min");
                        allowed = getAllowed(carbs, allowed,min);
                    }
                    else if (plans[j].Equals('F'))
                    {
                        int max = getValue(fat, allowed);
                        allowed = getAllowed(fat, allowed, max);
                    }
                    else if (plans[j].Equals('f'))
                    {
                        int min = getValue(fat, allowed,"min");
                        allowed = getAllowed(fat, allowed, min);
                    }
                    else if (plans[j].Equals('T'))
                    {
                        int max = getValue(calorie, allowed);
                        allowed = getAllowed(calorie, allowed, max);
                    }
                    else if (plans[j].Equals('t'))
                    {
                        int min = getValue(calorie, allowed,"min");
                        allowed = getAllowed(calorie, allowed, min);
                    }
                }
                exp[i] = allowed[0];
            }
            //Console.Write($"Result = [{string.Join(", ", exp)}]\n");
            return exp;
        }

        public static int getValue(int[] arr,List<int> allowed,string op = "max")
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            if (op.Equals("max"))
            {
                for(int i = 0; i < allowed.Count; i++)
                {
                    if (max < arr[allowed[i]])
                    {
                        max = arr[allowed[i]];
                    }
                }
                return max;
            }
            else
            {
                for (int i = 0; i < allowed.Count; i++)
                {
                    if (min > arr[allowed[i]])
                    {
                        min = arr[allowed[i]];
                    }
                }
                return min;
            }
            
        }

        public static List<int> getAllowed(int[] arr,List<int> allowed,int val)
        {
            List<int> tempAllowed = new List<int>();
            for(int i = 0; i < allowed.Count; i++)
            {
                if (arr[allowed[i]] == val)
                {
                    tempAllowed.Add(allowed[i]);
                }
            }
            return tempAllowed;
        }
    }
}
