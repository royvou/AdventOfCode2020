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
            var acc = RunSimulation(records);

            return acc.Accumulator.ToString();
        }

        private static (bool InfiniteLoop, int Accumulator) RunSimulation(List<Instruction> records)
        {
            var index = 0;
            var acc = 0;
            var visitedIndexes = new HashSet<int>();

            while (true)
            {
                if (visitedIndexes.Contains(index))
                {
                    return (InfiniteLoop: true, Accumulator: acc);//break;)
                }

                if (index == (records.Count - 1))
                {
                    return (InfiniteLoop: false, Accumulator: acc);//break;)
                }

                visitedIndexes.Add(index);

                var record = records[index];
                switch (record.Type)
                {
                    case InstructionType.Acc:
                        acc += record.TypeModifer;
                        index += 1;
                        break;
                    case InstructionType.Jmp:
                        index += record.TypeModifer;
                        break;
                    case InstructionType.Nop:
                        index += 1;
                        break;
                }
            }

            return default;
        }

        private Instruction ParseInstruction(string arg)
        {
            var type = Enum.Parse<InstructionType>(arg.Substring(0, 3), true);
            var modifier = int.Parse(arg.Substring(4));
            return new Instruction(type, modifier);
        }

        public override string Solve_2()
        {
            var records = _input.SplitNewLine().Select(ParseInstruction).ToList();

            var result =Enumerable.Range(0, records.Count).Select(x =>
            {
                //Filter 
                var record = records[x];
                if (record.Type == InstructionType.Acc)
                    return null;

                var newList = new List<Instruction>(records);
                var newRecord = record with { 
                    Type = record.Type switch {
                        InstructionType.Jmp => InstructionType.Nop, 
                        InstructionType.Nop => InstructionType.Jmp,
                        //InstructionType.Acc => ,
                    _ => throw new ArgumentOutOfRangeException()
                }};
                newList[x] = newRecord;
                return newList;
            }).Where(x => x is not null).Select(x =>  RunSimulation(x)).FirstOrDefault(x => !x.InfiniteLoop);

            return result.Accumulator.ToString();
        }
        
        
    }

    public record Instruction(InstructionType Type, int TypeModifer);

    public enum InstructionType
    {
        Nop,
        Acc,
        Jmp
    }
}