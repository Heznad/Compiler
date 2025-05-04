using System.Text;

namespace Compiler.Model
{
    public enum TokenCode
    {
        Enum = 1,         // Ключевое слово "enum"
        Class = 2,        // Ключевое слово "class"
        Identifier = 3,    // Идентификатор (имя перечисления, имя типа)
        Space = 4,         // Значащий пробел
        LBrace = 5,        // Открывающая фигурная скобка "{"
        RBrace = 6,        // Закрывающая фигурная скобка "}"
        Comma = 7,         // Запятая ","
        Semicolon = 8,     // Точка с запятой ";"
        Error = 99       // Неизвестный символ/ошибка
    }

    public class Token
    {
        public TokenCode Code { get; set; }
        public string Type { get; set; }
        public string Lexeme { get; set; }
        public int StartPos { get; set; }
        public int EndPos { get; set; }
        public int Line { get; set; }

        public override string ToString()
        {
            
            return $"({Code}) {Type} : '{Lexeme}'";
        }
    }

    public class Scanner
    {
        private string _text;
        private int _pos;       // текущая позиция (сквозная по всему тексту)
        private int _line;      // текущая строка
        private int _linePos;   // позиция в текущей строке
        private List<Token> _tokens;

        public Scanner()
        {
            _tokens = new List<Token>();
        }

        public List<Token> Scan(string text)
        {
            _text = text;
            _pos = 0;
            _line = 1;
            _linePos = 1;
            _tokens.Clear();

            while (!IsEnd())
            {
                char ch = CurrentChar();

                // Используем switch
                switch (ch)
                {
                    // Пропускаем незначащие пробелы, табуляцию и переводы строк
                    case var c when char.IsWhiteSpace(c):
                        if (_tokens.Count > 0)
                        {
                            if (_tokens[_tokens.Count-1].Code == TokenCode.Enum || _tokens[_tokens.Count-1].Code == TokenCode.Class)
                            {
                                AddToken(TokenCode.Space, "пробел", "_");
                            }
                        }
                        Advance();
                        break;

                    // Буква - значит начинаем считывать идентификатор (или ключевое слово)
                    case var c when char.IsLetter(c):
                        ReadIdentifierOrKeyword();
                        break;

                    // Открывающая фигурная скобка
                    case '{':
                        AddToken(TokenCode.LBrace, "открывающая фигурная скобка", "{");
                        Advance();
                        break;

                    // Закрывающая фигурная скобка
                    case '}':
                        AddToken(TokenCode.RBrace, "закрывающая фигурная скобка", "}");
                        Advance();
                        break;

                    // Запятая
                    case ',':
                        AddToken(TokenCode.Comma, "запятая", ",");
                        Advance();
                        break;

                    // Точка с запятой
                    case ';':
                        AddToken(TokenCode.Semicolon, "конец объявления перечисления", ";");
                        Advance();
                        break;

                    // По умолчанию - недопустимый символ
                    default:
                        AddToken(TokenCode.Error, "недопустимый символ", ch.ToString());
                        Advance();
                        break;
                }
            }
            return _tokens;
        }

        public List<Token> CheckEnumConstruction(List<Token> tokens)
        {
            List<Token> errors = new();
            int state = 0;
            foreach(var token in tokens)
            {
                switch (state)
                {
                    case 0:
                        if (token.Code != TokenCode.Enum)
                        {
                            token.Code = TokenCode.Error;
                            token.Type = $"Встречен символ {token.Lexeme}, а ожидался";
                            token.Lexeme = "enum";
                            errors.Add(token);
                            return errors;
                        }
                        else
                        {
                            errors.Add(token);
                            state++;
                        }
                        break;
                    case 1:
                        if(token.Code != TokenCode.Space)
                        {
                            token.Code = TokenCode.Error;
                            token.Type = $"Встречен символ {token.Lexeme}, а ожидался";
                            token.Lexeme = "пробел";
                            errors.Add(token);
                            return errors;
                        }
                        else
                        {
                            errors.Add(token);
                            state++;
                        }
                        break;
                    case 2:
                        if(token.Code != TokenCode.Class)
                        {
                            token.Code = TokenCode.Error;
                            token.Type = $"Встречен символ {token.Lexeme}, а ожидался";
                            token.Lexeme = "class";
                            errors.Add(token);
                            return errors;
                        }
                        else
                        {
                            errors.Add(token);
                            state++;
                        }
                        break;
                    case 3:
                        if(token.Code != TokenCode.Space)
                        {
                            token.Code = TokenCode.Error;
                            token.Type = $"Встречен символ {token.Lexeme}, а ожидался";
                            token.Lexeme = "пробел";
                            errors.Add(token);
                            return errors;
                        }
                        else
                        {
                            errors.Add(token);
                            state++;
                        }
                        break;
                    case 4:
                        if(token.Code != TokenCode.Identifier)
                        {
                            token.Code = TokenCode.Error;
                            token.Type = $"Встречен символ {token.Lexeme}, а ожидался";
                            token.Lexeme = "индетификатор";
                            errors.Add(token);
                            return errors;
                        }
                        else
                        {
                            errors.Add(token);
                            state++;
                        }
                        break;
                    case 5:
                        if (token.Code != TokenCode.LBrace)
                        {
                            token.Code = TokenCode.Error;
                            token.Type = $"Встречен символ {token.Lexeme}, а ожидался";
                            token.Lexeme = "{";
                            errors.Add(token);
                            return errors;
                        }
                        else
                        {
                            errors.Add(token);
                            state++;
                        }
                        break;
                    case 6:
                        if (token.Code != TokenCode.Identifier)
                        {
                            token.Code = TokenCode.Error;
                            token.Type = $"Встречен символ {token.Lexeme}, а ожидался";
                            token.Lexeme = "идентификатор";
                            errors.Add(token);
                            return errors;
                        }
                        else
                        {
                            errors.Add(token);
                            state++;
                        }
                        break;
                    case 7:
                        if (token.Code != TokenCode.Comma && token.Code != TokenCode.RBrace)
                        {
                            token.Code = TokenCode.Error;
                            token.Type = $"Встречен символ {token.Lexeme}, а ожидался";
                            token.Lexeme = ", или }";
                            errors.Add(token);
                            return errors;
                        }
                        else if (token.Code == TokenCode.RBrace)
                        {
                            errors.Add(token);
                            state++;
                        }
                        else if (token.Code == TokenCode.Comma)
                        {
                            errors.Add(token);
                            state--;
                        }
                        break;
                    case 8:
                        if (token.Code != TokenCode.Semicolon)
                        {
                            token.Code = TokenCode.Error;
                            token.Type = $"Встречен символ {token.Lexeme}, а ожидался";
                            token.Lexeme = ";";
                            errors.Add(token);
                            return errors;
                        }
                        else
                        {
                            errors.Add(token);
                            state = 0;
                            return tokens;
                        }
                        break;
                    default:
                        break;
                }
            }
            Token tokenE = new();
            if (state != 0)
            {
                switch (state) {
                    case 1:
                        tokenE = new Token
                        {
                            Code = TokenCode.Space,
                            Type = "ожидался пробел",
                            Lexeme = "_",
                            StartPos = tokens[tokens.Count-1].EndPos+1,
                            EndPos = tokens[tokens.Count - 1].EndPos + 1,
                            Line = tokens[tokens.Count - 1].Line
                        };
                        errors.Add(tokenE);
                        return errors;
                    case 2:
                        tokenE = new Token
                        {
                            Code = TokenCode.Space,
                            Type = "ожидалось ключевое слово",
                            Lexeme = "class",
                            StartPos = tokens[tokens.Count - 1].EndPos + 1,
                            EndPos = tokens[tokens.Count - 1].EndPos + 5,
                            Line = tokens[tokens.Count - 1].Line
                        };
                        errors.Add(tokenE);
                        return errors;
                    case 3:
                        tokenE = new Token
                        {
                            Code = TokenCode.Space,
                            Type = "ожидался пробел",
                            Lexeme = "_",
                            StartPos = tokens[tokens.Count - 1].EndPos + 1,
                            EndPos = tokens[tokens.Count - 1].EndPos + 1,
                            Line = tokens[tokens.Count - 1].Line
                        };
                        errors.Add(tokenE);
                        return errors;
                    case 4:
                        tokenE = new Token
                        {
                            Code = TokenCode.Identifier,
                            Type = "ожидался индетификатор",
                            Lexeme = "identificator",
                            StartPos = tokens[tokens.Count - 1].EndPos + 1,
                            EndPos = tokens[tokens.Count - 1].EndPos + 1,
                            Line = tokens[tokens.Count - 1].Line
                        };
                        errors.Add(tokenE);
                        return errors;
                    case 5:
                        tokenE = new Token
                        {
                            Code = TokenCode.LBrace,
                            Type = "ожидалась левая фигурная скобочка",
                            Lexeme = "{",
                            StartPos = tokens[tokens.Count - 1].EndPos + 1,
                            EndPos = tokens[tokens.Count - 1].EndPos + 1,
                            Line = tokens[tokens.Count - 1].Line
                        };
                        errors.Add(tokenE);
                        return errors;
                    case 6:
                        tokenE = new Token
                        {
                            Code = TokenCode.Identifier,
                            Type = "ожидался индетификатор",
                            Lexeme = "identificator",
                            StartPos = tokens[tokens.Count - 1].EndPos + 1,
                            EndPos = tokens[tokens.Count - 1].EndPos + 1,
                            Line = tokens[tokens.Count - 1].Line
                        };
                        errors.Add(tokenE);
                        return errors;
                    case 7:
                        tokenE = new Token
                        {
                            Code = TokenCode.Identifier,
                            Type = "ожидалась запятая или правая фигурная скобочка",
                            Lexeme = ", или }",
                            StartPos = tokens[tokens.Count - 1].EndPos + 1,
                            EndPos = tokens[tokens.Count - 1].EndPos + 1,
                            Line = tokens[tokens.Count - 1].Line
                        };
                        errors.Add(tokenE);
                        return errors;
                    case 8:
                        tokenE = new Token
                        {
                            Code = TokenCode.Identifier,
                            Type = "ожидалась точка с запятой",
                            Lexeme = ";",
                            StartPos = tokens[tokens.Count - 1].EndPos + 1,
                            EndPos = tokens[tokens.Count - 1].EndPos + 1,
                            Line = tokens[tokens.Count - 1].Line
                        };
                        errors.Add(tokenE);
                        return errors;
                }
            }
            return errors;
        }

        /// <summary>
        /// Считывание идентификатора или ключевого слова
        /// </summary>
        private void ReadIdentifierOrKeyword()
        {
            int startPos = _linePos;
            StringBuilder sb = new StringBuilder();
            char c = CurrentChar();

            // Первый символ идентификатора должен быть английской буквой
            if (!((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z')))
            {
                AddToken(TokenCode.Error, "недопустимый символ", c.ToString(), _linePos, _linePos, _line);
                Advance();
                return;
            }
            sb.Append(c);
            Advance();

            while (!IsEnd())
            {
                c = CurrentChar();
                if ((c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || char.IsDigit(c) || c == '_')
                {
                    sb.Append(c);
                    Advance();
                }
                else
                {
                    break;
                }
            }

            string lexeme = sb.ToString();
            switch (lexeme)
            {
                case "enum":
                    AddToken(TokenCode.Enum, "ключевое слово", lexeme, startPos, _linePos - 1, _line);
                    break;
                case "class":
                    AddToken(TokenCode.Class, "ключевое слово", lexeme, startPos, _linePos - 1, _line);
                    break;
                default:
                    AddToken(TokenCode.Identifier, "идентификатор", lexeme, startPos, _linePos - 1, _line);
                    break;
            }
        }

        #region Вспомогательные методы

        private bool IsEnd()
        {
            return _pos >= _text.Length;
        }

        private char CurrentChar()
        {
            if (IsEnd()) return '\0';
            return _text[_pos];
        }

        private char PeekChar()
        {
            if (_pos + 1 >= _text.Length) return '\0';
            return _text[_pos + 1];
        }

        private void Advance()
        {
            // Если встретили перевод строки, переходим на следующую строку
            if (CurrentChar() == '\n')
            {
                _line++;
                _linePos = 0;
            }
            _pos++;
            _linePos++;
        }

        private void AddToken(TokenCode code, string type, string lexeme)
        {
            AddToken(code, type, lexeme, _linePos, _linePos, _line);
        }

        private void AddToken(TokenCode code, string type, string lexeme, int startPos, int endPos, int line)
        {
            var token = new Token
            {
                Code = code,
                Type = type,
                Lexeme = lexeme,
                StartPos = startPos,
                EndPos = endPos,
                Line = line
            };
            _tokens.Add(token);
        }
        #endregion
    }
}