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