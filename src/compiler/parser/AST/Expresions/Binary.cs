namespace Parsing;

public abstract class Binary : Expression
    {
        public Expression? Right { get; set; }
        public Expression? Left { get; set; }
        public Binary(int line, int position) : base(line, position){}
    }