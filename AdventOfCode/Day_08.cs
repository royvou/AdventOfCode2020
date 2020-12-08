using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using AoCHelper;

namespace AdventOfCode
{
    public class Day_08 : BaseDay
    {
        private string _input;

        public Day_08()
        {
            _input = File.ReadAllText(InputFilePath);
        }

        public override string Solve_1()
        {
            var records = _input.SplitNewLine().Select(ParseInstruction).ToList();

            var index = 0;
            var acc = 0;
            var visitedIndexes = new HashSet<int>();

            while (true)
            {
                if (visitedIndexes.Contains(index))
                {
                    break;
                }
                visitedIndexes.Add(index);

                var record = records[index];
                switch (record.Type)
                {
                    case InstructionType.acc: 
                        acc += record.TypeModifer;
                        index += 1;
                        break;
                    case  InstructionType.jmp:
                        index += record.TypeModifer;
                        break;
                    case InstructionType.nop:
                        index += 1;
                        break;        
                }

            }

            return acc.ToString();
        }

        private Instruction ParseInstruction(string arg)
        {
            var type = Enum.Parse<InstructionType>(arg.Substring(0, 3));
            var modifier = int.Parse(arg.Substring(4));
            return new Instruction(type, modifier);
        }

        public override string Solve_2()
        {
            throw new NotImplementedException();
        }
        
        
    }

    public record Instruction(InstructionType Type, int TypeModifer);

    public enum InstructionType
    {
        nop,
        acc,
        jmp
    }
}