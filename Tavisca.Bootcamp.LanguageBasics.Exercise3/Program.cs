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
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");

            Console.WriteLine(result);
        }

        public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
                      
            int[] calorie = new int[protein.Length];
            for(int index = 0; index < protein.Length; index++)
            {
                calorie[index] = fat[index] * 9 + protein[index] * 5 + carbs[index] * 5;

            }
            int[] results = new int[dietPlans.Length];
            for(int index = 0; index < dietPlans.Length; index++)
            {
                List<int> allowedItems = new List<Int32>();
                char[] plans = dietPlans[index].ToCharArray();
                for (int k = 0; k < protein.Length; k++)
                {
                    allowedItems.Add(k);

                }
                for (int secondIndex = 0; secondIndex < plans.Length; secondIndex++)
                {
                    int min, max;
                    switch (plans[secondIndex])
                    {
                        case 'P':
                            max = GetValue(protein, allowedItems);
                            allowedItems = GetAllowedItems(protein, allowedItems, max);
                            break;
                        case 'p':
                            min = GetValue(protein, allowedItems, "min");
                            allowedItems = GetAllowedItems(protein, allowedItems, min);
                            break;
                        case 'C':
                            max = GetValue(carbs, allowedItems);
                            allowedItems = GetAllowedItems(carbs, allowedItems, max);
                            break;
                        case 'c':
                            min = GetValue(carbs, allowedItems, "min");
                            allowedItems = GetAllowedItems(carbs, allowedItems, min);
                            break;
                        case 'F':
                            max = GetValue(fat, allowedItems);
                            allowedItems = GetAllowedItems(fat, allowedItems, max);
                            break;
                        case 'f':
                            min = GetValue(fat, allowedItems, "min");
                            allowedItems = GetAllowedItems(fat, allowedItems, min);
                            break;
                        case 'T':
                            max = GetValue(calorie, allowedItems);
                            allowedItems = GetAllowedItems(calorie, allowedItems, max);
                            break;
                        case 't':
                            min = GetValue(calorie, allowedItems, "min");
                            allowedItems = GetAllowedItems(calorie, allowedItems, min);
                            break;
                    }
                  
                }
                results[index] = allowedItems[0];
            }
            return results;
        }

        public static int GetValue(int[] arr,List<int> allowedItems,string op = "max")
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            if (op.Equals("max"))
            {
                for(int index = 0; index < allowedItems.Count; index++)
                {
                    if (max < arr[allowedItems[index]])
                    {
                        max = arr[allowedItems[index]];
                    }
                }
                return max;
            }
            else
            {
                for (int index = 0; index < allowedItems.Count; index++)
                {
                    if (min > arr[allowedItems[index]])
                    {
                        min = arr[allowedItems[index]];
                    }
                }
                return min;
            }
            
        }

        public static List<int> GetAllowedItems(int[] arr,List<int> allowedItems,int val)
        {
            List<int> tempAllowed = new List<int>();
            for(int index = 0; index < allowedItems.Count; index++)
            {
                if (arr[allowedItems[index]] == val)
                {
                    tempAllowed.Add(allowedItems[index]);
                }
            }
            return tempAllowed;
        }
    }
}
