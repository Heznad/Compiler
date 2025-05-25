using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compiler.Model
{
    class UrlMatch
    {
        public string Url { get; set; }
        public int Position { get; set; }
    }
    class AvtomatURL
    {
        public List<UrlMatch> FindUrlsWithAutomaton(string text)
        {
            List<UrlMatch> matches = new List<UrlMatch>();
            State currentState = State.Start;
            int urlStartIndex = 0;

            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];

                switch (currentState)
                {
                    case State.Start:
                        if (c == 'h' || c == 'H')
                        {
                            currentState = State.ProtocolH;
                            urlStartIndex = i;
                        }
                        else if (c == 'f' || c == 'F')
                        {
                            currentState = State.ProtocolF;
                            urlStartIndex = i;
                        }
                        break;

                    case State.ProtocolH:
                        if (c == 't' || c == 'T') currentState = State.ProtocolT1;
                        else currentState = State.Start;
                        break;

                    case State.ProtocolT1:
                        if (c == 't' || c == 'T') currentState = State.ProtocolT2;
                        else currentState = State.Start;
                        break;

                    case State.ProtocolT2:
                        if (c == 'p' || c == 'P') currentState = State.ProtocolP;
                        else currentState = State.Start;
                        break;

                    case State.ProtocolP:
                        if (c == 's' || c == 'S') currentState = State.ProtocolS;
                        else if (c == ':') currentState = State.ProtocolColon;
                        else currentState = State.Start;
                        break;

                    case State.ProtocolS:
                        if (c == ':') currentState = State.ProtocolColon;
                        else currentState = State.Start;
                        break;

                    case State.ProtocolF:
                        if (c == 't' || c == 'T') currentState = State.ProtocolT2;
                        else currentState = State.Start;
                        break;

                    case State.ProtocolColon:
                        if (c == '/') currentState = State.ProtocolSlash1;
                        else currentState = State.Start;
                        break;

                    case State.ProtocolSlash1:
                        if (c == '/') currentState = State.ProtocolSlash2;
                        else currentState = State.Start;
                        break;

                    case State.ProtocolSlash2:
                        if (IsDomainChar(c))
                        {
                            currentState = State.Domain;
                        }
                        else
                        {
                            currentState = State.Start;
                        }
                        break;

                    case State.Domain:
                        if (c == '.')
                        {
                            currentState = State.Dot;
                        }
                        else if (!IsDomainChar(c) && c != '/')
                        {
                            currentState = State.Start;
                        }
                        break;

                    case State.Dot:
                        if (IsTldChar(c))
                        {
                            currentState = State.Tld;
                            // Проверяем конец URL
                            if (i + 1 == text.Length || !IsUrlChar(text[i + 1]))
                            {
                                matches.Add(new UrlMatch
                                {
                                    Url = text.Substring(urlStartIndex, i - urlStartIndex + 1),
                                    Position = urlStartIndex
                                });
                                currentState = State.Start;
                            }
                        }
                        else
                        {
                            currentState = State.Start;
                        }
                        break;

                    case State.Tld:
                        if (IsTldChar(c))
                        {
                            // Проверяем конец URL
                            if (i + 1 == text.Length || !IsUrlChar(text[i + 1]))
                            {
                                matches.Add(new UrlMatch
                                {
                                    Url = text.Substring(urlStartIndex, i - urlStartIndex + 1),
                                    Position = urlStartIndex
                                });
                                currentState = State.Start;
                            }
                        }
                        else if (c == '/')
                        {
                            currentState = State.Path;
                        }
                        else if (c == '.')
                        {
                            currentState = State.Dot;
                        }
                        else if (!IsDomainChar(c))
                        {
                            currentState = State.Start;
                        }
                        break;

                    case State.Path:
                        if (!IsUrlChar(c))
                        {
                            matches.Add(new UrlMatch
                            {
                                Url = text.Substring(urlStartIndex, i - urlStartIndex),
                                Position = urlStartIndex
                            });
                            currentState = State.Start;
                        }
                        break;
                }
            }

            // Добавляем URL, если он был в конце текста
            if (currentState == State.Tld || currentState == State.Path)
            {
                matches.Add(new UrlMatch
                {
                    Url = text.Substring(urlStartIndex),
                    Position = urlStartIndex
                });
            }

            return matches;
        }

        // Вспомогательные классы и методы
        private enum State
        {
            Start,
            ProtocolH,
            ProtocolT1,
            ProtocolT2,
            ProtocolP,
            ProtocolS,
            ProtocolF,
            ProtocolColon,
            ProtocolSlash1,
            ProtocolSlash2,
            Domain,
            Dot,
            Tld,
            Path,
            Done,
            Fail
        }

        

        private bool IsDomainChar(char c)
        {
            return char.IsLetterOrDigit(c) || c == '-' || c == '_';
        }

        private bool IsTldChar(char c)
        {
            return char.IsLetter(c);
        }

        private bool IsUrlChar(char c)
        {
            return IsDomainChar(c) || c == '.' || c == '/' || c == '?' || c == '&' || c == '=' || c == '%';
        }
    }
}
