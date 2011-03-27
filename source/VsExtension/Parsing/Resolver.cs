/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
This code is licensed under the Visual Studio SDK license terms.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/

using System.Collections.Generic;

namespace PowerStudio.VsExtension.Parsing
{
    public class Resolver : IAstResolver
    {
        #region IAstResolver Members

        public IList<Declaration> FindCompletions( object result, int line, int col )
        {
            return new List<Declaration>();
        }

        public IList<Declaration> FindMembers( object result, int line, int col )
        {
            // ManagedMyC.Parser.AAST aast = result as ManagedMyC.Parser.AAST;
            var members = new List<Declaration>();

            //foreach (string state in aast.startStates.Keys)
            //    members.Add(new Declaration(state, state, 0, state));

            return members;
        }

        public string FindQuickInfo( object result, int line, int col )
        {
            return "unknown";
        }

        public IList<Method> FindMethods( object result, int line, int col, string name )
        {
            return new List<Method>();
        }

        #endregion
    }
}