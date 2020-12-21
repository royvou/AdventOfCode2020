using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_21 : BaseDay
    {
        public Day_21()
        {
        }

        public Day_21(string input) : base(input)
        {
        }

        public override string Solve_1()
        {
            var foods = ParseFoods(_input);

            Dictionary<Day21Allergen, HashSet<Day21Ingredient>> possiblePosionMapping = new();
            foreach (var food in foods)
            {
                foreach (var allergene in food.Allergens)
                {
                    if (possiblePosionMapping.TryGetValue(allergene, out var possibleIngredient))
                    {
                        possibleIngredient.RemoveWhere(pi => !food.Ingredient.Contains(pi));
                    }
                    else
                    {
                        possiblePosionMapping.Add(allergene, new HashSet<Day21Ingredient>(food.Ingredient));
                    }
                }
            }

            var allIngredients = new HashSet<Day21Ingredient>(foods.SelectMany(x => x.Ingredient));
            var allPossiblePosion = new HashSet<Day21Ingredient>(possiblePosionMapping.Values.SelectMany(x => x).Distinct());
            var safeIngredient = new HashSet<Day21Ingredient>(allIngredients.Where(ig => !allPossiblePosion.Contains(ig)));

            return foods.SelectMany(x => x.Ingredient).Count(ig => safeIngredient.Contains(ig)).ToString();
        }

        private static List<Day21Food> ParseFoods(string input)
        {
            var lines = input.SplitNewLine();
            return lines.Select(x =>
            {
                var matches = Regex.Match(x, @"(.*) \(contains (.*)\)");
                return new Day21Food(
                    matches.Groups[1].Value.SplitSpace().Select(y => new Day21Ingredient(y)).ToArray(),
                    matches.Groups[2].Value.Split(",", StringSplitOptions.TrimEntries).Select(y => new Day21Allergen(y)).ToArray());
            }).ToList();
        }

        public override string Solve_2()
        {
            throw new NotImplementedException();
        }

        public record Day21Food(Day21Ingredient[] Ingredient, Day21Allergen[] Allergens);

        public record Day21Ingredient(string Name);

        public record Day21Allergen(string Name);
    }
}