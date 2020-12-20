using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode
{
    public class Day_18 : BaseDay
    {
        public Day_18()
        {
        }

        public Day_18(string input) : base(input)
        {
        }


        public override string Solve_1()
        {
            var inputOperations = _input.Replace("(", "( ").Replace(")", " )");
            return inputOperations.SplitNewLine().Select(x => ExecuteOperations(x.SplitSpace())).Sum().ToString();
        }

        private static long ExecuteOperations(IList<string> inputOperations)
        {
            var result = 0L;

            var activeOperation = Day18Operator.Plus;
            var parenthesesCount = 0;
            var tmpOps = new List<string>();
            long? longValue = null;
            for (var index = 0; index < inputOperations.Count; index++)
            {
                var operation = inputOperations[index];
                Day18Operator tmpOp;
                switch (operation)
                {
                    case "+" when parenthesesCount == 0:
                        tmpOp = Day18Operator.Plus;
                        activeOperation = tmpOp;
                        break;
                    case "*" when parenthesesCount == 0:
                        tmpOp = Day18Operator.Mulitply;
                        activeOperation = tmpOp;
                        break;
                    case { } str when long.TryParse(str, out var tmpInt):
                        longValue = tmpInt;
                        tmpOp = Day18Operator.Number;
                        break;
                    case "(":
                        tmpOp = Day18Operator.StartParentheses;
                        parenthesesCount += 1;
                        break;
                    case ")":
                        tmpOp = Day18Operator.EndParentheses;
                        parenthesesCount -= 1;
                        break;
                    default:
                        tmpOp = Day18Operator.None;
                        break;
                }

                if (
                    parenthesesCount > 0
                )
                    tmpOps.Add(operation);

                if (tmpOp == Day18Operator.EndParentheses && parenthesesCount == 0)
                {
                    //Gather all operations to execute;  
                    longValue = ExecuteOperations(tmpOps.Skip(1).ToList());
                    tmpOps.Clear();
                }

                if (longValue.HasValue && parenthesesCount == 0)
                {
                    result = activeOperation switch
                    {
                        Day18Operator.Plus => result + longValue.Value,
                        Day18Operator.Mulitply => result * longValue.Value,
                        _ => throw new ArgumentOutOfRangeException(),
                    };
                    longValue = default;
                }
            }

            return result;
        }

        public override string Solve_2()
        {
            var inputOperations = _input.Replace("(", "( ").Replace(")", " )");

            return inputOperations.SplitNewLine().Select(x =>
            {
                var ops = x.SplitSpace().ToList();
                ConvertPriority(ref ops);
                return ExecuteOperations(ops);
            }).Sum().ToString();
        }

        private static void ConvertPriority(ref List<string> inputOperations)
        {
            for (var i = inputOperations.Count - 1; i > 0; i--)
            {
                var currentOperation = inputOperations[i];

                if (currentOperation == "+")
                {
                    //Add parentheses before/after
                    var skip = false;
                    if (inputOperations[i + 1] == "(" && inputOperations[i - 1] != ")")
                    {
                        inputOperations.Insert(inputOperations.Count.Clamp(0, inputOperations.Count), ")");
                        inputOperations.Insert((i - 1).Clamp(0, inputOperations.Count), "(");
                        i--;
                        skip = true;
                    }

                    if (i - 1 >= 0 && inputOperations[i - 1] == ")" && inputOperations[i + 1] == "(")
                    {
                        inputOperations.Insert(i, ")");
                        inputOperations.Insert(0, "(");
                        i--;
                        skip = true;
                    }


                    if (i - 1 >= 0 && inputOperations[i - 1] == ")" && inputOperations[i + 1] != "(")
                    {
                        for (int y = i; y >= 0; y--)
                        {
                            int parenth = 1;
                            var tmpOp = inputOperations[y];
                            if (tmpOp == ")")
                                parenth++;
                            else if (tmpOp == "(")
                                parenth--;


                            if (parenth == 0)
                            {
                                inputOperations.Insert(i, ")");
                                inputOperations.Insert(y, "(");

                                break;
                            }
                        }


                        i--;
                        skip = true;
                    }

                    if (!skip)
                    {
                        inputOperations.Insert((i + 2).Clamp(0, inputOperations.Count), ")");
                        inputOperations.Insert((i - 1).Clamp(0, inputOperations.Count), "(");
                        i--;
                    }
                }
            }
        }
    }


    public enum Day18Operator
    {
        None,
        Plus,
        Mulitply,
        Number,
        StartParentheses,
        EndParentheses,
    }
}