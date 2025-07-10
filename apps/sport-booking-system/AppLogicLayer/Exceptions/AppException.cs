using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLogicLayer
{
    public class AppException : Exception
    {
        public string? ErrorCode { get; set; }
        public string? TableName { get; set; }
        public override string Message { get; }
        public new Exception? InnerException { get; }

        public AppException(string message, string? tableName = null, string? errorCode = null, Exception? innerException = null)
        {
            InnerException = innerException;

            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrWhiteSpace(message))
            {
                Message = "";
                return;
            }
                

            sb.AppendLine(message);
            if (!string.IsNullOrWhiteSpace(tableName))
                sb.AppendLine($"Table: {tableName}");

            if (!string.IsNullOrWhiteSpace(errorCode))
                sb.AppendLine($"Error Code: {errorCode}");

            Message = sb.ToString();
        }
    }
}
