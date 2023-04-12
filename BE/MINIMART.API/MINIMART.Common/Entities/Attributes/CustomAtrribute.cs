namespace MINIMART.Common.Entities.Attributes
{
    public class CustomAtrribute
    {
        /// <summary>
        /// Custom Attribute cho các field
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
        public class CDisplayName : Attribute
        {
            /// <summary>
            /// Tên hiển thị
            /// </summary>
            public string DisplayName { get; set; }

            /// <summary>
            /// Hàm khởi tạo
            /// </summary>
            /// <param name="displayName">Tên hiển thị</param>
            public CDisplayName(string displayName)
            {
                this.DisplayName = displayName;
            }
        }

        /// <summary>
        /// Custom Attribute cho Regex Attribute
        /// </summary>
        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
        public class CRegularExpression : Attribute
        {
            /// <summary>
            /// Regex
            /// </summary>
            public string Regex { get; set; }

            /// <summary>
            /// Hàm khởi tạo
            /// </summary>
            /// <param name="regex">Regex</param>
            public CRegularExpression(string regex)
            {
                this.Regex = regex;
            }
        }
    }
}
