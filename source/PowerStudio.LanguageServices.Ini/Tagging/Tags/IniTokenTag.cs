#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using PowerStudio.LanguageServices.Tagging.Tags;

#endregion

namespace PowerStudio.LanguageServices.Ini.Tagging.Tags
{
    public class IniTokenTag : TokenTag<IniToken>
    {
        public IniTokenTag( IniToken token )
        {
            Token = token;
        }
    }
}