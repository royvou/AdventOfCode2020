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
                return (long) EvaluateExpression(new Queue<char>(x.ToCharArray().Where(c => c != ' ')),
                    new Dictionary<char, int>() {['+'] = 0, ['*'] = 1});
            }).Sum().ToString();
        }

        private long ExecuteOperation(Stack<long> stash, char op)
            => op switch
            {
                '+' => stash.Pop() + stash.Pop(),
                '*' => stash.Pop() * stash.Pop(),
                _ => throw new NotSupportedException()
            };

        private long EvaluateExpression(Queue<char> expression, Dictionary<char, int> precedence)
        {
            var stash = new Stack<long>();
            var ops = new Stack<char>();

            while (expression.Count > 0)
            {
                var c = expression.Dequeue();
                if (c >= '0' && c <= '9')
                {
                    stash.Push((long) char.GetNumericValue(c));
                }
                else if (c == '(')
                {
                    stash.Push(EvaluateExpression(expression, precedence));
                }
                else if (c == ')')
                {
                    break;
                }
                else if (ops.Count == 0 || precedence[c] < precedence[ops.Peek()])
                {
                    ops.Push(c);
                }
                else
                {
                    stash.Push(ExecuteOperation(stash, ops.Pop()));
                    ops.Push(c);
                }
            }

            while (ops.Count > 0)
            {
                stash.Push(ExecuteOperation(stash, ops.Pop()));
            }


            return stash.Peek();
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