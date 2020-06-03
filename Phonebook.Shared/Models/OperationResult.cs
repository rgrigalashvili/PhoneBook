using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.Shared.Models
{
    public class OperationResult
    {
        public bool IsSuccess { get; protected set; }
        public string ErrorMessage { get; protected set; }
        public OperationResult(bool IsSuccess, string ErrorMessage)
        {
            this.IsSuccess = IsSuccess;
            this.ErrorMessage = ErrorMessage;
        }
    }
}
