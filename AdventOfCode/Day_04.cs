using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_04 : BaseDay
    {
        private string _input;

        public Day_04()
        {
            _input = File.ReadAllText(InputFilePath);
           
        }

        public override string Solve_1()
        {
           var  _requiredKeys = new[]
            {

                "byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid",
                //"cid" //  Missing CID is fine :)
            };
            return _input.SplitDoubleNewLine().Select(x => x.Replace(Environment.NewLine, String.Empty)).Where(x =>
            {
               return _requiredKeys.All(key => x.Contains(key + ":"));
            }).Count().ToString();
        }
        
        public override string Solve_2()
        {
            var  _requiredKeys = new[]
            {
                "byr",
                "iyr",
                "eyr",
                "hgt",
                "hcl",
                "ecl",
                "pid",
                //"cid" //  Missing CID is fine :)
            };
            return _input.SplitDoubleNewLine().Select(x => x.Replace(Environment.NewLine, " "))
                .Where(x =>
            {
                return _requiredKeys.All(key => x.Contains(key + ":"));
            })
                .Where(x =>
            {
                var allFields = x.SplitSpace();

                return allFields.All(currentField =>
                {
                    var currentFieldKey = currentField. Substring(0, 3);
                    var currentFieldKeyValue = currentField.Substring(4);

                    var success = currentFieldKey switch
                    {
                        "byr" => Regex.IsMatch(currentFieldKeyValue, "^[1][9][2-9][0-9]$|^[2][0][0][0-2]$", RegexOptions.Compiled),
                        "iyr" => Regex.IsMatch(currentFieldKeyValue, "^[2][0][1][0-9]$|^2020$",RegexOptions.Compiled),
                        "eyr" => Regex.IsMatch(currentFieldKeyValue, "^[2][0][2][0-9]$|^2030$",RegexOptions.Compiled),
                        "hgt" => Regex.IsMatch(currentFieldKeyValue, "^[1][5-8][0-9]cm$|^[1][9][0-3]cm$|^59in$|^[6][0-9]in$|^[7][0-6]in$",RegexOptions.Compiled),
                        "hcl" => Regex.IsMatch(currentFieldKeyValue, "^(#[0-9a-f]{6})$",RegexOptions.Compiled),
                        "ecl" => Regex.IsMatch(currentFieldKeyValue, "^(amb|blu|brn|gry|grn|hzl|oth)$",RegexOptions.Compiled),
                        "pid" => Regex.IsMatch(currentFieldKeyValue, "^([0-9]{9})$", RegexOptions.Compiled),
                        "cid" => true,
                        _ => false
                    };

                    return success;
                });

            }).Count().ToString();
        }
    }
}