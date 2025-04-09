using System.Collections.Generic;
using System.Text;

namespace Compiler.Model
{
    public enum TokenCode
    {
        Enum = 1,         // Ключевое слово "enum"
        Class = 2,        // Ключевое слово "class"
        Identifier = 3,    // Идентификатор (имя перечисления, имя типа)
        LBrace = 4,        // Открывающая фигурная скобка "{"
        RBrace = 5,        // Закрывающая фигурная скобка "}"
        Comma = 6,         // Запятая ","
        Semicolon = 7,     // Точка с запятой ";"
        ScopeResolution = 8, // Оператор разрешения области видимости "::" (для C++ scoped enums)
        Unknown = 99       // Неизвестный символ/ошибка
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
            /*return $"[{Line}:{StartPos}-{EndPos}] ({Code}) {Type} : '{Lexeme}'";*/
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

                    // Двоеточие (возможно, часть "::")
                    case ':':
                        if (PeekChar() == ':')
                        {
                            AddToken(TokenCode.ScopeResolution, "оператор разрешения области видимости", "::");
                            Advance();
                            Advance(); // Пропускаем оба двоеточия
                        }
                        else
                        {
                            AddToken(TokenCode.Unknown, "неизвестный символ", ":"); // одиночное двоеточие
                            Advance();
                        }
                        break;

                    // По умолчанию - недопустимый символ
                    default:
                        AddToken(TokenCode.Unknown, "недопустимый символ", ch.ToString());
                        Advance();
                        break;
                }
            }

            return _tokens;
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
                AddToken(TokenCode.Unknown, "недопустимый символ", c.ToString(), _linePos, _linePos, _line);
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