using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Application.Common
{
    public record Error(string code,string Description , ErrorType type = ErrorType.Failure)
    {
        public static Error Failure(string code="General.Failure" , string Description = "General Failure Desc")
            => new Error(code , Description , ErrorType.Failure);

        public static Error Validation(string code = "General.Validation", string Description = "General Validation Desc")
            => new Error(code, Description, ErrorType.Validation);

        public static Error Forbidden(string code = "General.Forbidden", string Description = "General Forbidden Desc")
            => new Error(code, Description, ErrorType.Forbidden);

        public static Error UnAuthorized(string code = "General.UnAuthorized", string Description = "General UnAuthorized Desc")
            => new Error(code, Description, ErrorType.UnAuthorized);

        public static Error NotFound(string code = "General.NotFound", string Description = "General NotFound Desc")
            => new Error(code, Description, ErrorType.NotFound);

        public static Error Conflict(string code = "General.Conflict", string Description = "General Conflict Desc")
            => new Error(code, Description, ErrorType.Conflict);

        public static Error InValidCredentails(string code = "General.InValidCredentails", string Description = "General InValidCredentails Desc")
            => new Error(code, Description, ErrorType.InValidCredentails);


    }
}
