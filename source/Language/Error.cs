#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

using System;

namespace PowerStudio.Language
{
    public class Error : IComparable<Error>, IEquatable<Error>
    {
        internal Error( string message, int line, int column, int length, bool isWarning )
        {
            Message = message;
            Line = line;
            Column = column;
            Length = length;
            IsWarning = isWarning;
        }

        public int Column { get; private set; }
        public bool IsWarning { get; private set; }
        public int Length { get; private set; }
        public int Line { get; private set; }
        public string Message { get; private set; }

        #region IComparable<Error> Members

        public int CompareTo( Error error )
        {
            if ( Line < error.Line )
            {
                return -1;
            }
            if ( Line > error.Line )
            {
                return 1;
            }
            if ( Column < error.Column )
            {
                return -1;
            }
            return Column > error.Column ? 1 : 0;
        }

        #endregion

        #region IEquatable<Error> Members

        public bool Equals( Error error )
        {
            if ( ReferenceEquals( null, error ) )
            {
                return false;
            }
            if ( ReferenceEquals( this, error ) )
            {
                return true;
            }
            return error.Column == Column && error.IsWarning == IsWarning && error.Length == Length &&
                   error.Line == Line && Equals( error.Message, Message );
        }

        #endregion

        public override string ToString()
        {
            return string.Format( "Line {0}, column  {1}: {2}", Line, Column, Message );
        }

        public override bool Equals( object obj )
        {
            if ( ReferenceEquals( null, obj ) )
            {
                return false;
            }
            if ( ReferenceEquals( this, obj ) )
            {
                return true;
            }
            if ( obj.GetType() !=
                 typeof (Error) )
            {
                return false;
            }
            return Equals( (Error) obj );
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = Column;
                result = ( result * 397 ) ^ IsWarning.GetHashCode();
                result = ( result * 397 ) ^ Length;
                result = ( result * 397 ) ^ Line;
                result = ( result * 397 ) ^ ( Message != null ? Message.GetHashCode() : 0 );
                return result;
            }
        }
    }
}