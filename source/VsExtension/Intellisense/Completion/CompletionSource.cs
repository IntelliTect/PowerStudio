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
using System.Linq;
using System.Management.Automation;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;

#endregion

namespace PowerStudio.VsExtension.Intellisense.Completion
{
    internal class CompletionSource : ICompletionSource
    {
        private static readonly IEnumerable<string> BuiltInCompletions;
        private readonly ITextBuffer _Buffer;
        private readonly CompletionSourceProvider _SourceProvider;
        private List<Microsoft.VisualStudio.Language.Intellisense.Completion> _Completions;
        private bool _IsDisposed;

        static CompletionSource()
        {
            var keywords = new List<string>
                           {
                                   "begin",
                                   "break",
                                   "catch",
                                   "class",
                                   "continue",
                                   "data",
                                   "define",
                                   "do",
                                   "dynamicparam",
                                   "else",
                                   "elseif",
                                   "end",
                                   "exit",
                                   "filter",
                                   "finally",
                                   "for",
                                   "foreach",
                                   "from",
                                   "function",
                                   "if",
                                   "in",
                                   "param",
                                   "process",
                                   "return",
                                   "switch",
                                   "throw",
                                   "trap",
                                   "try",
                                   "until",
                                   "using",
                                   "var",
                                   "while"
                           };
            var variables = new List<string>
                            {
                                    "global:",
                                    "local:",
                                    "private:",
                                    "script:",
                                    "$$",
                                    "$?",
                                    "$_",
                                    "$args",
                                    "$ConsoleFileName",
                                    "$Error",
                                    "$Event",
                                    "$EventSubscriber",
                                    "$ExecutionContext",
                                    "$false",
                                    "$foreach",
                                    "$Home",
                                    "$Host",
                                    "$input",
                                    "$LastExitCode",
                                    "$matches",
                                    "$MyInvocation",
                                    "$NestedPromptLevel",
                                    "$null",
                                    "$PID",
                                    "$PsBoundParameters",
                                    "$PsCmdlet",
                                    "$PsCulture",
                                    "$PsDebugContext",
                                    "$PsHome",
                                    "$PsScriptRoot",
                                    "$PsUICulture",
                                    "$PsVersionTable",
                                    "$Pwd",
                                    "$Sender",
                                    "$ShellID",
                                    "$SourceArgs",
                                    "$SourceEventArgs",
                                    "$switch",
                                    "$this",
                                    "$true"
                            };

            var preferenceVariables = new List<string>
                                      {
                                              "$ConfirmPreference",
                                              "$DebugPreference",
                                              "$ErrorActionPreference",
                                              "$ErrorView",
                                              "$FormatEnumerationLimit",
                                              "$MaximumAliasCount",
                                              "$MaximumDriveCount",
                                              "$MaximumErrorCount",
                                              "$MaximumFunctionCount",
                                              "$MaximumHistoryCount",
                                              "$MaximumVariableCount",
                                              "$OFS",
                                              "$OutputEncoding",
                                              "$ProgressPreference",
                                              "$VerbosePreference",
                                              "$WarningPreference",
                                              "$WhatIfPreference"
                                      };

            var cmdlets = new List<string>
                          {
                                  "ForEach-Object",
                                  "Where-Object",
                                  "Add-Content",
                                  "Add-PSSnapIn",
                                  "Get-Content",
                                  "Set-Location",
                                  "Set-Location",
                                  "Clear-Content",
                                  "Clear-Host",
                                  "Clear-History",
                                  "Clear-Item",
                                  "Clear-ItemProperty",
                                  "Clear-Host",
                                  "Clear-Variable",
                                  "Compare-Object",
                                  "Copy-Item",
                                  "Copy-Item",
                                  "Copy-Item",
                                  "Copy-ItemProperty",
                                  "Convert-Path",
                                  "Disable-PSBreakpoint",
                                  "Remove-Item",
                                  "Compare-Object",
                                  "Get-ChildItem",
                                  "Enable-PSBreakpoint",
                                  "Write-Output",
                                  "Export-Alias",
                                  "Export-Csv",
                                  "Export-PSSession",
                                  "Remove-Item",
                                  "Enter-PSSession",
                                  "Exit-PSSession",
                                  "Format-Custom",
                                  "Format-List",
                                  "ForEach-Object",
                                  "Format-Table",
                                  "Format-Wide",
                                  "Get-Alias",
                                  "Get-PSBreakpoint",
                                  "Get-Content",
                                  "Get-ChildItem",
                                  "Get-Command",
                                  "Get-PSCallStack",
                                  "Get-PSDrive",
                                  "Get-History",
                                  "Get-Item",
                                  "Get-Job",
                                  "Get-Location",
                                  "Get-Member",
                                  "Get-Module",
                                  "Get-ItemProperty",
                                  "Get-Process",
                                  "Group-Object",
                                  "Get-PSSession",
                                  "Get-PSSnapIn",
                                  "Get-Service",
                                  "Get-Unique",
                                  "Get-Variable",
                                  "Get-WmiObject",
                                  "Get-History",
                                  "Get-History",
                                  "Invoke-Command",
                                  "Invoke-Expression",
                                  "Invoke-History",
                                  "Invoke-Item",
                                  "Import-Alias",
                                  "Import-Csv",
                                  "Import-Module",
                                  "Import-PSSession",
                                  "powershell_ise.exe",
                                  "Invoke-WMIMethod",
                                  "Stop-Process",
                                  "Out-Printer",
                                  "Get-ChildItem",
                                  "help",
                                  "mkdir",
                                  "Measure-Object",
                                  "Move-Item",
                                  "New-PSDrive",
                                  "Move-Item",
                                  "Move-ItemProperty",
                                  "Move-Item",
                                  "New-Alias",
                                  "New-PSDrive",
                                  "New-Item",
                                  "New-Module",
                                  "New-PSSession",
                                  "New-Variable",
                                  "Out-GridView",
                                  "Out-Host",
                                  "Pop-Location",
                                  "Get-Process",
                                  "Push-Location",
                                  "Get-Location",
                                  "Invoke-History",
                                  "Remove-PSBreakpoint",
                                  "Receive-Job",
                                  "Remove-Item",
                                  "Remove-PSDrive",
                                  "Rename-Item",
                                  "Remove-Item",
                                  "Remove-Job",
                                  "Remove-Item",
                                  "Remove-Item",
                                  "Remove-Module",
                                  "Rename-Item",
                                  "Rename-ItemProperty",
                                  "Remove-ItemProperty",
                                  "Remove-PSSession",
                                  "Remove-PSSnapin",
                                  "Remove-Variable",
                                  "Resolve-Path",
                                  "Remove-WMIObject",
                                  "Start-Job",
                                  "Set-Alias",
                                  "Start-Process",
                                  "Start-Service",
                                  "Set-PSBreakpoint",
                                  "Set-Content",
                                  "Select-Object",
                                  "Set-Variable",
                                  "Set-Item",
                                  "Set-Location",
                                  "Start-Sleep",
                                  "Sort-Object",
                                  "Set-ItemProperty",
                                  "Stop-Job",
                                  "Stop-Process",
                                  "Stop-Service",
                                  "Start-Process",
                                  "Set-Variable",
                                  "Set-WMIInstance",
                                  "Tee-Object",
                                  "Get-Content",
                                  "Where-Object",
                                  "Wait-Job",
                                  "Write-Output"
                          };

            var aliases = new List<string>
                          {
                                  "ac",
                                  "asnp",
                                  "cat",
                                  "cd",
                                  "chdir",
                                  "clc",
                                  "clear",
                                  "clhy",
                                  "cli",
                                  "clp",
                                  "cls",
                                  "clv",
                                  "compare",
                                  "copy",
                                  "cp",
                                  "cpi",
                                  "cpp",
                                  "cvpa",
                                  "dbp",
                                  "del",
                                  "diff",
                                  "dir",
                                  "ebp",
                                  "echo",
                                  "epal",
                                  "epcsv",
                                  "epsn",
                                  "erase",
                                  "etsn",
                                  "exsn",
                                  "fc",
                                  "fl",
                                  "foreach",
                                  "ft",
                                  "fw",
                                  "gal",
                                  "gbp",
                                  "gc",
                                  "gci",
                                  "gcm",
                                  "gcs",
                                  "gdr",
                                  "ghy",
                                  "gi",
                                  "gjb",
                                  "gl",
                                  "gm",
                                  "gmo",
                                  "gp",
                                  "gps",
                                  "group",
                                  "gsn",
                                  "gsnp",
                                  "gsv",
                                  "gu",
                                  "gv",
                                  "gwmi",
                                  "h",
                                  "history",
                                  "icm",
                                  "iex",
                                  "ihy",
                                  "ii",
                                  "ipal",
                                  "ipcsv",
                                  "ipmo",
                                  "ipsn",
                                  "ise",
                                  "iwmi",
                                  "kill",
                                  "lp",
                                  "ls",
                                  "man",
                                  "md",
                                  "measure",
                                  "mi",
                                  "mount",
                                  "move",
                                  "mp",
                                  "mv",
                                  "nal",
                                  "ndr",
                                  "ni",
                                  "nmo",
                                  "nsn",
                                  "nv",
                                  "ogv",
                                  "oh",
                                  "popd",
                                  "ps",
                                  "pushd",
                                  "pwd",
                                  "r",
                                  "rbp",
                                  "rcjb",
                                  "rd",
                                  "rdr",
                                  "ren",
                                  "ri",
                                  "rjb",
                                  "rm",
                                  "rmdir",
                                  "rmo",
                                  "rni",
                                  "rnp",
                                  "rp",
                                  "rsn",
                                  "rsnp",
                                  "rv",
                                  "rvpa",
                                  "rwmi",
                                  "sajb",
                                  "sal",
                                  "saps",
                                  "sasv",
                                  "sbp",
                                  "sc",
                                  "select",
                                  "set",
                                  "si",
                                  "sl",
                                  "sleep",
                                  "sort",
                                  "sp",
                                  "spjb",
                                  "spps",
                                  "spsv",
                                  "start",
                                  "sv",
                                  "swmi",
                                  "tee",
                                  "type",
                                  "where",
                                  "wjb",
                                  "write"
                          };
            BuiltInCompletions =
                    keywords.Union( variables ).Union( preferenceVariables ).Union( cmdlets ).Union( aliases );
        }

        public CompletionSource( CompletionSourceProvider sourceProvider, ITextBuffer textBuffer )
        {
            _SourceProvider = sourceProvider;
            _Buffer = textBuffer;
        }

        #region Implementation of IDisposable

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            if ( !_IsDisposed )
            {
                GC.SuppressFinalize( this );
                _IsDisposed = true;
            }
        }

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
        public void AugmentCompletionSession( ICompletionSession session, IList<CompletionSet> completionSets )
        {
            completionSets.Clear();

            _Completions = new List<Microsoft.VisualStudio.Language.Intellisense.Completion>();
            foreach ( string completion in BuiltInCompletions )
            {
                _Completions.Add( new Microsoft.VisualStudio.Language.Intellisense.Completion( completion,
                                                                                               completion,
                                                                                               null,
                                                                                               null,
                                                                                               null ) );
            }

            // TODO: This declaration set is a quick impl. It does not take scope or order in file into consideration
            string text = _Buffer.CurrentSnapshot.GetText();
            Collection<PSParseError> errors;
            Collection<PSToken> tokens = PSParser.Tokenize( text, out errors );
            bool nextIsMethodName = false;
            foreach ( PSToken psToken in tokens )
            {
                if ( nextIsMethodName )
                {
                    nextIsMethodName = false;
                    var completion = new Microsoft.VisualStudio.Language.Intellisense.Completion(
                            psToken.Content, psToken.Content, null, null, null );
                    _Completions.Add( completion );
                    continue;
                }

                if ( psToken.Type == PSTokenType.Keyword &&
                     string.Equals( psToken.Content, "function" ) )
                {
                    nextIsMethodName = true;
                }
                if ( psToken.Type ==
                     PSTokenType.Variable )
                {
                    var completion = new Microsoft.VisualStudio.Language.Intellisense.Completion(
                            "$" + psToken.Content, "$" + psToken.Content, null, null, null );
                    _Completions.Add( completion );
                }
            }

            completionSets.Add( new CompletionSet(
                                        "Tokens",
                                        //the non-localized title of the tab
                                        "Tokens",
                                        //the display title of the tab
                                        FindTokenSpanAtPosition( session.GetTriggerPoint( _Buffer ), session ),
                                        _Completions.Distinct( new CompletionComparer() ),
                                        null ) );
        }

        #endregion

        private ITrackingSpan FindTokenSpanAtPosition( ITrackingPoint point, ICompletionSession session )
        {
            SnapshotPoint currentPoint = ( session.TextView.Caret.Position.BufferPosition ) - 1;
            ITextStructureNavigator navigator =
                    _SourceProvider.NavigatorService.GetTextStructureNavigator( _Buffer );
            TextExtent extent = navigator.GetExtentOfWord( currentPoint );
            return currentPoint.Snapshot.CreateTrackingSpan( extent.Span, SpanTrackingMode.EdgeInclusive );
        }

        #region Nested type: CompletionComparer

        private class CompletionComparer : IEqualityComparer<Microsoft.VisualStudio.Language.Intellisense.Completion>
        {
            #region Implementation of IEqualityComparer<in Completion>

            /// <summary>
            /// Determines whether the specified objects are equal.
            /// </summary>
            /// <returns>
            /// true if the specified objects are equal; otherwise, false.
            /// </returns>
            /// <param name="x">The first object of type <paramref name="T"/> to compare.</param><param name="y">The second object of type <paramref name="T"/> to compare.</param>
            public bool Equals( Microsoft.VisualStudio.Language.Intellisense.Completion x,
                                Microsoft.VisualStudio.Language.Intellisense.Completion y )
            {
                return string.Equals( x.DisplayText, y.DisplayText, StringComparison.OrdinalIgnoreCase );
            }

            /// <summary>
            /// Returns a hash code for the specified object.
            /// </summary>
            /// <returns>
            /// A hash code for the specified object.
            /// </returns>
            /// <param name="obj">The <see cref="T:System.Object"/> for which a hash code is to be returned.</param><exception cref="T:System.ArgumentNullException">The type of <paramref name="obj"/> is a reference type and <paramref name="obj"/> is null.</exception>
            public int GetHashCode( Microsoft.VisualStudio.Language.Intellisense.Completion obj )
            {
                return obj.DisplayText.GetHashCode();
            }

            #endregion
        }

        #endregion
    }
}