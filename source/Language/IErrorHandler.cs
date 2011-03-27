#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

using QUT.Gppg;

namespace PowerStudio.Language
{
    public interface IErrorHandler
    {
        int NumberOfErrors { get; }
        int NumberOfWarnings { get; }
        void AddError( string message, int line, int column, int length, int severity );
        void AddError( string message, int line, int column, int length );
        void AddError( string message, LexLocation lexLocation, int severity );
        void AddWarning( string message, int line, int column, int length );
    }
}