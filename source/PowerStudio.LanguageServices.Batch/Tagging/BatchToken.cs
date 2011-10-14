#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

namespace PowerStudio.LanguageServices.Batch.Tagging
{
    public class BatchToken
    {
        public string Text { get; set; }
        public BatchTokenType TokenType { get; set; }
        public int Start { get; set; }

        public int Length
        {
            get { return string.IsNullOrEmpty(Text) ? 0 : Text.Length; }
        }
    }
}
