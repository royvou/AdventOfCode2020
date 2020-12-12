using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    public abstract class BaseDay : AoCHelper.BaseDay
    {
        protected string _input;

        public BaseDay()
        {
            _input = File.ReadAllText(InputFilePath);

        }

        public BaseDay(string input)
        {
            _input = input;
        }
    }
}