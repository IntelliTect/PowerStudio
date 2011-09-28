#region License

// 
// Copyright (c) 2011, PowerStudio Project Contributors
// 
// Dual-licensed under the Apache License, Version 2.0, and the Microsoft Public License (Ms-PL).
// See the file LICENSE.txt for details.
// 

#endregion

#region Using Directives

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Host;
using System.Management.Automation.Runspaces;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;

#endregion
namespace PowerStudio.VsExtension.Intellisense
{
    [Name("PowerStudio Completion Source"), Export(typeof(ICompletionSourceProvider)), ContentType(LanguageConfiguration.Name)]
    internal class CompletionSourceProvider : ICompletionSourceProvider
    {
        public ICompletionSource TryCreateCompletionSource(ITextBuffer textBuffer)
        {
            return new CompletionSource(textBuffer);
        }
    }


    public class CompletionSource : ICompletionSource
    {
        private readonly ITextBuffer _TextBuffer;
        
        #region Implementation of IDisposable

        /// <summary>
        /// Determines which <see cref="T:Microsoft.VisualStudio.Language.Intellisense.CompletionSet"/>s should be part of the specified <see cref="T:Microsoft.VisualStudio.Language.Intellisense.ICompletionSession"/>.
        /// </summary>
        /// <param name="session">The session for which completions are to be computed.</param><param name="completionSets">The set of the completionSets to be added to the session.</param>
        /// <remarks>
        /// Each applicable <see cref="M:Microsoft.VisualStudio.Language.Intellisense.ICompletionSource.AugmentCompletionSession(Microsoft.VisualStudio.Language.Intellisense.ICompletionSession,System.Collections.Generic.IList{Microsoft.VisualStudio.Language.Intellisense.CompletionSet})"/> instance will be called in-order to
        ///             (re)calculate a <see cref="T:Microsoft.VisualStudio.Language.Intellisense.ICompletionSession"/>.  <see cref="T:Microsoft.VisualStudio.Language.Intellisense.CompletionSet"/>s can be added to the session by adding
        ///             them to the completionSets collection passed-in as a parameter.  In addition, by removing items from the collection, a
        ///             source may filter <see cref="T:Microsoft.VisualStudio.Language.Intellisense.CompletionSet"/>s provided by <see cref="T:Microsoft.VisualStudio.Language.Intellisense.ICompletionSource"/>s earlier in the calculation
        ///             chain.
        /// </remarks>
        public void AugmentCompletionSession(ICompletionSession session, IList<CompletionSet> completionSets)
        {
            ITextSnapshot snapshot = session.TextView.TextBuffer.CurrentSnapshot;
            SnapshotPoint? triggerPoint = session.GetTriggerPoint( snapshot );

            if ( !triggerPoint.HasValue )
            {
                return;
            }




            ITextSnapshotLine line = snapshot.GetLineFromPosition( triggerPoint.Value );
            string lineString = line.GetText();

            SimpleExpansion expansion;
            expansion = GetExpansions(lineString, triggerPoint.Value );

            if ( expansion == null )
            {
                return;
            }
            List<Completion> completions = new List<Completion>();
            foreach ( string str in expansion.Expansions )
            {
                completions.Add( new Completion( str, str, null, null, null ) );
            }
            SnapshotPoint point = session.TextView.Caret.Position.BufferPosition;
            ITrackingSpan applicableTo =
                    point.Snapshot.CreateTrackingSpan(
                            (Span) new SnapshotSpan( point + expansion.Start, expansion.Length ),
                            SpanTrackingMode.EdgeInclusive );
            completionSets.Add( new CompletionSet( LanguageConfiguration.Name,
                                                   LanguageConfiguration.Name,
                                                   applicableTo,
                                                   completions,
                                                   null ) );
        }

        #endregion

        private static readonly char[] EXPANSION_SEPARATORS = new char[] { '.', ' ' };


        private string AdjustExpansions(string leftWord, ref string[] expansions)
        {
            Func<string, bool> predicate = null;
            string commonWord = null;
            if (!string.IsNullOrEmpty(leftWord) && (expansions != null))
            {
                int startIndex = leftWord.Length - 1;
                do
                {
                    startIndex = leftWord.LastIndexOfAny(EXPANSION_SEPARATORS, startIndex);
                    if (startIndex < 0)
                    {
                        commonWord = null;
                        break;
                    }
                    commonWord = leftWord.Substring(0, startIndex + 1);
                    if (predicate == null)
                    {
                        predicate = delegate(string s)
                        {
                            return s.StartsWith(commonWord, StringComparison.CurrentCultureIgnoreCase);
                        };
                    }
                }
                while (!expansions.All<string>(predicate));
            }
            if (!string.IsNullOrEmpty(commonWord))
            {
                for (int i = 0; i < expansions.Length; i++)
                {
                    expansions[i] = expansions[i].Substring(commonWord.Length);
                }
            }
            return commonWord;
        }


        public SimpleExpansion GetExpansions(string line, int caretIndex)
        {
            int length = caretIndex;
            while (length < line.Length)
            {
                char c = line[length];
                if (char.IsSeparator(c) || char.IsPunctuation(c))
                {
                    break;
                }
                length++;
            }
            int index = caretIndex - 1;
            while ((index >= 0) && !char.IsSeparator(line, index))
            {
                index--;
            }
            index++;
            if (length != line.Length)
            {
                line = line.Substring(0, length);
            }
            string lastWord = line.Substring(index);
            string[] expansions = GetExpansions(line, lastWord);
            if ((expansions != null) && (expansions.Length > 0))
            {
                string leftWord = line.Substring(index, caretIndex - index);
                string str3 = this.AdjustExpansions(leftWord, ref expansions);
                int num3 = !string.IsNullOrEmpty(str3) ? str3.Length : 0;
                return new SimpleExpansion(index + num3, lastWord.Length - num3, expansions);
            }

            return null;
        }

        public string[] GetExpansions(string line, string lastWord)
        {
            bool outputResults = false;
            
            return this.Invoke("$__pc_args=@(); $input|%{$__pc_args+=$_}; TabExpansion $__pc_args[0] $__pc_args[1]; Remove-Variable __pc_args -Scope 0", new [] { line, lastWord }, outputResults).Select<PSObject, string>(delegate(PSObject s)
            {
                return s.ToString();
            }).ToArray<string>();
        }
        public Collection<PSObject> Invoke(string command, [Optional, DefaultParameterValue(null)] object input, [Optional, DefaultParameterValue(true)] bool outputResults)
        {
            if (string.IsNullOrEmpty(command))
            {
                return null;
            }
            using (Pipeline pipeline = this.CreatePipeline(command, outputResults))
            {
                return ((input != null) ? pipeline.Invoke(new object[] { input }) : pipeline.Invoke());
            }
        }
        private Pipeline CreatePipeline(string command, bool outputResults)
        {
            Pipeline pipeline = this.MyRunSpace.CreatePipeline();
            pipeline.Commands.AddScript(command);
            if (outputResults)
            {
                pipeline.Commands.Add("out-default");
                pipeline.Commands[0].MergeMyResults(PipelineResultTypes.Error, PipelineResultTypes.Output);
            }
            return pipeline;
        }

        private Runspace MyRunSpace
        {
            get
            {
                if (_myRunSpace == null)
                {
                    InitialSessionState state = InitialSessionState.CreateDefault();
                    _myRunSpace = RunspaceFactory.CreateRunspace(state);
                    _myRunSpace.Open();
                    Runspace.DefaultRunspace = _myRunSpace;
                }
                return _myRunSpace;
            }


        }

        Runspace _myRunSpace = null;

        public CompletionSource( ITextBuffer textBuffer )
        {
            _TextBuffer = textBuffer;
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            
        }

        #endregion
    }

    public class SimpleExpansion
    {
        public SimpleExpansion(int start, int length, string[] expansions)
        {
            Start = start;
            Length = length;
            Expansions = expansions;
        }

        public string[] Expansions { get; set; }

        public int Length { get; set; }

        public int Start { get; set; }
    }
}