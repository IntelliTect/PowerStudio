#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

using System.Collections.Generic;

namespace PowerStudio.Language
{
    public partial class Scanner
    {
        public Scanner( IEnumerable<string> lines )
        {
            SetSource( new List<string>( lines ) );
        }

        public Scanner( string line )
        {
            SetSource( new List<string> { line } );
        }
    }
}