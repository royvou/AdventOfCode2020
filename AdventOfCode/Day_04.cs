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
            return _input.Split(Environment.NewLine + Environment.NewLine).Select(x => x.Replace(Environment.NewLine, String.Empty)).Where(x =>
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
            return _input.Split(Environment.NewLine + Environment.NewLine).Select(x => x.Replace(Environment.NewLine, " ")).Where(x =>
            {
                return _requiredKeys.All(key => x.Contains(key + ":"));
            }).Where(x =>
            {
                var allFields = x.SplitSpace();

                return allFields.All(currentField =>
                {
                    var currentFieldKey = currentField.Substring(0, 3);
                    var currentFieldKeyValue = currentField.Substring(4);

                    var success = currentFieldKey switch
                    {
                        "byr" => int.Parse(currentFieldKeyValue) >= 1920 && int.Parse(currentFieldKeyValue) <= 2002,
                        "iyr" => int.Parse(currentFieldKeyValue) >= 2010 && int.Parse(currentFieldKeyValue) <= 2020,
                        "eyr" => int.Parse(currentFieldKeyValue) >= 2020 && int.Parse(currentFieldKeyValue) <= 2030,
                        /*"hgt" => fieldValue[^2] switch
                        {
                            "cm" => int.Parse(fieldValue) >= 150 && int.Parse(fieldValue) <= 193,
                            "in" => int.Parse(fieldValue.Substring(0, fieldValue.Length -2)) >= 59 && int.Parse(regexMatch.Groups[2].Value) <= 76
                            _ => false
                        },*/
                        "hgt" => currentFieldKeyValue.Substring(currentFieldKeyValue.Length - 2) switch
                        {
                            "in" => int.Parse(currentFieldKeyValue.Substring(0, currentFieldKeyValue.Length - 2)) >= 59 &&
                                    int.Parse(currentFieldKeyValue.Substring(0, currentFieldKeyValue.Length - 2)) <= 76,
                            "cm" => int.Parse(currentFieldKeyValue.Substring(0, currentFieldKeyValue.Length - 2)) >= 150 &&
                                    int.Parse(currentFieldKeyValue.Substring(0, currentFieldKeyValue.Length - 2)) <= 193,
                            _ => false
                        },
                        "hcl" => Regex.IsMatch(currentFieldKeyValue, "^(#[0-9a-f]{6})$"),
                        "ecl" => Regex.IsMatch(currentFieldKeyValue, "^(amb|blu|brn|gry|grn|hzl|oth)$"),
                        "pid" => Regex.IsMatch(currentFieldKeyValue, "^([0-9]{9})$"),
                        "cid" => true,
                        _ => false
                    };

                    return success;
                });

            }).Count().ToString();
        }
    }
}