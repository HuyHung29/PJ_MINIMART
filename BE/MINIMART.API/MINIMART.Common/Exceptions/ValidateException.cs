using System.Collections;

namespace MINIMART.Common.Exceptions
{
    /// <summary>
    /// Exception validate dữ liệu
    /// </summary>
    public class ValidateException : Exception
    {
        #region Property
        /// <summary>
        /// Thông báo lỗi
        /// </summary>
        private string ValidateMessage;

        /// <summary>
        /// Mảng các lỗi
        /// </summary>
        private Dictionary<string, string> Errors { get; set; }
        #endregion

        #region Contructor
        public ValidateException(string validateMessage, Dictionary<string, string> errors)
        {
            this.ValidateMessage = validateMessage;
            this.Errors = errors;
        }
        #endregion

        #region Method
        public override IDictionary Data => Errors;

        public override string Message => ValidateMessage;
        #endregion
    }
}
