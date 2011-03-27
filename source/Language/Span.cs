#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

namespace PowerStudio.Language
{
    public class Span
    {
        public int EndColumn;
        public int EndLine;
        public int Length;
        public int StartColumn;
        public int StartLine;
        public int StartPosition;

        public Span( int startLine, int startColumn, int endLine, int endColumn, int startPosition, int length )
        {
            StartLine = startLine;
            StartColumn = startColumn;
            EndLine = endLine;
            EndColumn = endColumn;
            StartPosition = startPosition;
            Length = length;
        }

        public static Span Merge( Span lhs, Span rhs )
        {
            return new Span( lhs.StartLine, lhs.StartColumn, rhs.EndLine, rhs.EndColumn, lhs.StartPosition, rhs.StartPosition - lhs.StartPosition + rhs.Length );
        }
    }
}