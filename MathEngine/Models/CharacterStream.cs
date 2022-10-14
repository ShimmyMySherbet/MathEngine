namespace MathEngine.Models
{
    public class CharacterStream
    {
        public string Input { get; }

        public int Index { get; set; } = 0;

        public bool EndOfStream => Index >= Input.Length;

        public CharacterStream(string content)
        {
            Input = content;
        }

        public char ReadNext()
        {
            if (EndOfStream)
            {
                return '\0';
            }

            var c = Input[Index];
            Index++;
            return c;
        }

        public char Peak()
        {
            if (EndOfStream)
            {
                return '\0';
            }
            return Input[Index];
        }


    }
}