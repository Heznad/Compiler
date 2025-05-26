using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;

namespace Compiler.Model
{
    public class SyntaxAnalyzer
    {
        public class TokenData
        {
            public string Type { get; set; }
            public string Value { get; set; }
            public int Position { get; set; }

            public TokenData(string type, string value, int position)
            {
                Type = type;
                Value = value;
                Position = position;
            }
        }

        public class ParseStepData
        {
            public int StepNumber { get; set; }
            public string Rule { get; set; }
            public string Result { get; set; }

            public ParseStepData(int stepNumber, string rule, string result)
            {
                StepNumber = stepNumber;
                Rule = rule;
                Result = result;
            }
        }

        private class Token
        {
            public string Type { get; }
            public string Value { get; }
            public int Position { get; }

            public Token(string type, string value, int position)
            {
                Type = type;
                Value = value;
                Position = position;
            }
        }

        private class ParseStep
        {
            public string Rule { get; }
            public string Result { get; }

            public ParseStep(string rule, string result)
            {
                Rule = rule;
                Result = result;
            }
        }

        private class Lexer
        {
            public List<Token> Tokens { get; } = new List<Token>();
            private readonly string _input;
            private int _position;

            public Lexer(string input)
            {
                _input = input;
                _position = 0;
            }

            public List<Token> Tokenize()
            {
                Tokens.Clear();
                _position = 0;

                while (_position < _input.Length)
                {
                    char current = _input[_position];

                    if (char.IsWhiteSpace(current))
                    {
                        _position++;
                        continue;
                    }

                    if (char.IsDigit(current))
                    {
                        ReadNumber();
                        continue;
                    }

                    if (char.IsLetter(current))
                    {
                        ReadIdentifier();
                        continue;
                    }

                    if (current == '+' || current == '-' || current == '*' || current == '/' || current == '(' || current == ')')
                    {
                        Tokens.Add(new Token("Оператор", current.ToString(), _position));
                        _position++;
                        continue;
                    }

                    throw new Exception($"Неизвестный символ '{current}' в позиции {_position}");
                }

                return Tokens;
            }

            private void ReadNumber()
            {
                int start = _position;
                while (_position < _input.Length && char.IsDigit(_input[_position]))
                {
                    _position++;
                }
                string value = _input.Substring(start, _position - start);
                Tokens.Add(new Token("Число", value, start));
            }

            private void ReadIdentifier()
            {
                int start = _position;
                while (_position < _input.Length && (char.IsLetterOrDigit(_input[_position])))
                {
                    _position++;
                }
                string value = _input.Substring(start, _position - start);
                Tokens.Add(new Token("Идентификатор", value, start));
            }
        }

        private class Parser
        {
            private readonly List<Token> _tokens;
            private int _currentTokenIndex;
            private readonly List<ParseStep> _parseSteps = new List<ParseStep>();

            public Parser(List<Token> tokens)
            {
                _tokens = tokens;
                _currentTokenIndex = 0;
            }

            public List<ParseStep> Parse()
            {
                _parseSteps.Clear();
                _currentTokenIndex = 0;
                ParseE();
                return _parseSteps;
            }

            private void ParseE()
            {
                _parseSteps.Add(new ParseStep("E → TA", "E"));
                ParseT();
                ParseA();
            }

            private void ParseA()
            {
                if (_currentTokenIndex < _tokens.Count && (_tokens[_currentTokenIndex].Value == "+" || _tokens[_currentTokenIndex].Value == "-"))
                {
                    string op = _tokens[_currentTokenIndex].Value;
                    _parseSteps.Add(new ParseStep($"A → {op}TA", $"A-{op}"));
                    _currentTokenIndex++;
                    ParseT();
                    ParseA();
                }
                else
                {
                    _parseSteps.Add(new ParseStep("A → ε", "A-ε"));
                }
            }

            private void ParseT()
            {
                _parseSteps.Add(new ParseStep("T → ОВ", "T"));
                ParseO();
                ParseB();
            }

            private void ParseB()
            {
                if (_currentTokenIndex < _tokens.Count && (_tokens[_currentTokenIndex].Value == "*" || _tokens[_currentTokenIndex].Value == "/"))
                {
                    string op = _tokens[_currentTokenIndex].Value;
                    _parseSteps.Add(new ParseStep($"B → {op}ОВ", $"B-{op}"));
                    _currentTokenIndex++;
                    ParseO();
                    ParseB();
                }
                else
                {
                    _parseSteps.Add(new ParseStep("B → ε", "B-ε"));
                }
            }

            private void ParseO()
            {
                if (_currentTokenIndex >= _tokens.Count)
                    throw new Exception("Ожидался num, id или (E), но достигнут конец ввода");

                var currentToken = _tokens[_currentTokenIndex];

                if (currentToken.Type == "NUMBER")
                {
                    _parseSteps.Add(new ParseStep("O → num", $"O-num({currentToken.Value})"));
                    _currentTokenIndex++;
                }
                else if (currentToken.Type == "IDENTIFIER")
                {
                    _parseSteps.Add(new ParseStep("O → id", $"O-id({currentToken.Value})"));
                    _currentTokenIndex++;
                }
                else if (currentToken.Value == "(")
                {
                    _parseSteps.Add(new ParseStep("O → (E)", "O-("));
                    _currentTokenIndex++;
                    ParseE();
                    if (_currentTokenIndex >= _tokens.Count || _tokens[_currentTokenIndex].Value != ")")
                        throw new Exception("Ожидалась закрывающая скобка ')'");
                    _parseSteps.Add(new ParseStep("", "O-)"));
                    _currentTokenIndex++;
                }
                else
                {
                    throw new Exception($"Ожидался num, id или (E), но получен {currentToken.Type} '{currentToken.Value}'");
                }
            }
        }

        public static (List<TokenData> tokensData, List<ParseStepData> parseStepsData) AnalyzeExpression(string input)
        {
            // Лексический анализ
            var lexer = new Lexer(input);
            var tokens = lexer.Tokenize();

            // Синтаксический анализ
            var parser = new Parser(tokens);
            var parseSteps = parser.Parse();

            // Преобразование данных для возврата
            var tokensData = new List<TokenData>();
            foreach (var token in lexer.Tokens)
            {
                tokensData.Add(new TokenData(token.Type, token.Value, token.Position));
            }

            var parseStepsData = new List<ParseStepData>();
            for (int i = 0; i < parseSteps.Count; i++)
            {
                parseStepsData.Add(new ParseStepData(i + 1, parseSteps[i].Rule, parseSteps[i].Result));
            }

            return (tokensData, parseStepsData);
        }
    }
}
