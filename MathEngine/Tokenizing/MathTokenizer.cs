using System.Collections.Generic;
using System.Text;
using MathEngine.Models;

namespace MathEngine.Tokenizing
{
    public class MathTokenizer
    {
        public string RawNumerics { get; } = "1234567890.";

        public ExpressionToken Tokenize(MathContext context, string content) => Tokenize(context, new CharacterStream(content));

        public string Alphabet { get; } = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";

        public ExpressionToken Tokenize(MathContext context, CharacterStream content, char readUntil = '\0', bool raiseSingles = true)
        {
            var tokens = new List<ExpressionToken>();
            var currentText = "";

            var lastWasSymbol = true;
            while (!content.EndOfStream)
            {
                var c = content.ReadNext();

                if (c == '(')
                {
                    var subToken = Tokenize(context, content, ')', raiseSingles: false);

                    if (subToken != null)
                    {
                        tokens.Add(subToken);
                    }
                }
                else if (c == readUntil)
                {
                    if (!string.IsNullOrWhiteSpace(currentText))
                    {
                        tokens.Add(new ExpressionToken(currentText));
                    }

                    if (tokens.Count == 1 && raiseSingles)
                    {
                        return tokens[0];
                    }
                    else if (tokens.Count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        return new ExpressionToken(tokens);
                    }
                }
                else if (c == ',' || c == ' ')
                {
                    if (!string.IsNullOrWhiteSpace(currentText))
                    {
                        tokens.Add(new ExpressionToken(currentText));
                        currentText = "";
                    }
                    lastWasSymbol = c == ',';
                }
                else if (RawNumerics.Contains(c))
                {
                    currentText += c;
                    lastWasSymbol = false;
                }
                else if (!Alphabet.Contains(c))
                {
                    if (lastWasSymbol && c == '-' && currentText == "" && RawNumerics.Contains(content.Peak()))
                    {
                        currentText = "-";
                        continue;
                    }

                    if (!string.IsNullOrWhiteSpace(currentText))
                    {
                        tokens.Add(new ExpressionToken(currentText));
                        currentText = "";
                    }
                    tokens.Add(new ExpressionToken(c.ToString()));
                    lastWasSymbol = true;
                }
                else
                {
                    // Place unknown characters in separate tokens.
                    if (!string.IsNullOrWhiteSpace(currentText))
                    {
                        tokens.Add(new ExpressionToken(currentText));
                        currentText = "";
                    }

                    var current = new StringBuilder();
                    current.Append(c);
                    while (true)
                    {
                        var next = content.Peak();
                        if (Alphabet.Contains(next))
                        {
                            current.Append(content.ReadNext());
                        }
                        else
                        {
                            break;
                        }
                    }

                    var block = current.ToString();

                    if (context.Functions.ContainsKey(block))
                    {
                        tokens.Add(new ExpressionToken(block));
                    }
                    else
                    {
                        foreach (var c_ in block)
                        {
                            tokens.Add(new ExpressionToken(c_.ToString()));
                        }
                    }
                    lastWasSymbol = false;
                }
            }

            if (!string.IsNullOrWhiteSpace(currentText))
            {
                tokens.Add(new ExpressionToken(currentText));
            }

            if (tokens.Count == 0)
            {
                return null;
            }
            else if (tokens.Count == 1 && raiseSingles)
            {
                return tokens[0];
            }
            else
            {
                return new ExpressionToken(tokens);
            }
        }
    }
}