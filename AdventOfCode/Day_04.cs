using System;
using System.IO;
using System.Linq;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_04 : BaseDay
    {
        private string _input;
        private string[] _requiredKeys;

        public Day_04()
        {
            _input = File.ReadAllText(InputFilePath);
            _requiredKeys = new[]
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
        }

        public override string Solve_1()
        {
            return _input.Split(Environment.NewLine + Environment.NewLine).Select(x => x.Replace(Environment.NewLine, String.Empty)).Where(x =>
            {
               return _requiredKeys.All(key => x.Contains(key + ":"));
            }).Count().ToString();
        }


        public override string Solve_2()
        {
            throw new NotImplementedException();
        }
    }
}