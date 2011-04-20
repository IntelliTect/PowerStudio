#region Using Directives

using Microsoft.VisualStudio.Text.Adornments;
using Microsoft.VisualStudio.Text.Tagging;

#endregion

namespace PowerStudio.VsExtension.Tagging
{
    public class ErrorTokenTag : TokenTag, IErrorTag
    {
        /// <summary>
        /// Initializes a new instance of a <see cref="T:Microsoft.VisualStudio.Text.Tagging.ErrorTag"/> of the specified type.
        /// 
        /// </summary>
        /// <param name="toolTipContent">The tooltip content to display. May be null.</param>
        /// <param name="errorType">The type of error to use.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="errorType"/> is null.</exception>
        public ErrorTokenTag( string toolTipContent, string errorType )
        {
            ErrorType = errorType;
            ToolTipContent = toolTipContent;
        }

        /// <summary>
        /// Initializes a new instance of a <see cref="T:Microsoft.VisualStudio.Text.Tagging.ErrorTag"/> of the specified type with no tooltip content.
        /// 
        /// </summary>
        /// <param name="toolTipContent">The tooltip content to display. May be null.</param>
        public ErrorTokenTag( string toolTipContent )
                : this( toolTipContent, PredefinedErrorTypeNames.SyntaxError )
        {
        }

        #region IErrorTag Members

        /// <summary>
        /// Gets the type of error to use.
        /// 
        /// </summary>
        public string ErrorType { get; private set; }

        /// <summary>
        /// Gets the content to use when displaying a tooltip for this error.
        ///             This property may be null.
        /// 
        /// </summary>
        public object ToolTipContent { get; private set; }

        #endregion
    }
}