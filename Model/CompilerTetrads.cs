using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Compiler.Model
{
    class CompilerTetrads
    {
        private int tempCounter = 1;
        public string ParseExpression(string input, List<Tetrad> tetrads)
        {
            input = input.Replace(" ", "");
            int pos = 0;
            return E(input, ref pos, tetrads);
        }

        private string E(string input, ref int pos, List<Tetrad> tetrads)
        {
            string temp1 = T(input, ref pos, tetrads);
            return A(input, ref pos, tetrads, temp1);
        }

        private string A(string input, ref int pos, List<Tetrad> tetrads, string temp1)
        {
            if (pos >= input.Length) return temp1;

            char op = input[pos];
            if (op == '+' || op == '-')
            {
                pos++;
                string temp2 = T(input, ref pos, tetrads);
                string result = GetTempVar();
                tetrads.Add(new Tetrad(op.ToString(), temp1, temp2, result));
                return A(input, ref pos, tetrads, result);
            }
            return temp1;
        }

        private string T(string input, ref int pos, List<Tetrad> tetrads)
        {
            string temp1 = O(input, ref pos, tetrads);
            return B(input, ref pos, tetrads, temp1);
        }

        private string B(string input, ref int pos, List<Tetrad> tetrads, string temp1)
        {
            if (pos >= input.Length) return temp1;

            char op = input[pos];
            if (op == '*' || op == '/')
            {
                pos++;
                string temp2 = O(input, ref pos, tetrads);
                string result = GetTempVar();
                tetrads.Add(new Tetrad(op.ToString(), temp1, temp2, result));
                return B(input, ref pos, tetrads, result);
            }
            return temp1;
        }

        private string O(string input, ref int pos, List<Tetrad> tetrads)
        {
            if (pos >= input.Length)
                throw new Exception("Ожидается идентификатор или выражение в скобках");

            if (input[pos] == '(')
            {
                pos++;
                string temp = E(input, ref pos, tetrads);
                if (pos >= input.Length || input[pos] != ')')
                    throw new Exception("Ожидается закрывающая скобка");
                pos++;
                return temp;
            }

            if (input[pos] == '-') // Унарный минус
            {
                pos++;
                string operand = O(input, ref pos, tetrads);
                string result = GetTempVar();
                tetrads.Add(new Tetrad("minus", operand, null, result));
                return result;
            }

            if (char.IsLetter(input[pos]))
            {
                string id = input[pos].ToString();
                pos++;
                while (pos < input.Length && char.IsLetterOrDigit(input[pos]))
                {
                    id += input[pos];
                    pos++;
                }
                return id;
            }

            throw new Exception($"Неожиданный символ: {input[pos]}");
        }

        private string GetTempVar()
        {
            return $"t{tempCounter++}";
        }
    }
    public class Tetrad
    {
        public string Op { get; set; }
        public string Arg1 { get; set; }
        public string Arg2 { get; set; }
        public string Result { get; set; }

        public Tetrad(string op, string arg1, string arg2, string result)
        {
            Op = op;
            Arg1 = arg1;
            Arg2 = arg2;
            Result = result;
        }
    }
}
