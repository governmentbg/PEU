using EAU.Common;
using EAU.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Net;
using System.Threading.Tasks;

namespace EAU.Web.Mvc
{
    /// <summary>
    /// Реализация на интерфейс IActionResult за работа с грешки.
    /// </summary>
    public class ErrorActionResult : IActionResult
    {
        #region Fileds

        private readonly Exception _exception;
        private readonly IStringLocalizer _stringLocalizer;

        #endregion

        #region Constructor

        public ErrorActionResult(Exception exception, IStringLocalizer stringLocalizer)
        {
            _exception = exception;
            _stringLocalizer = stringLocalizer;
        }

        #endregion

        #region Public Methods

        public Task ExecuteResultAsync(ActionContext context)
        {
            int statusCode = (int)StatusCodes.Status500InternalServerError;
            ApiError apiError = null;

            if (_exception != null)
            {
                var (apiErrorParsed, httpStatusCode) = GetError(_exception);

                statusCode = (int)httpStatusCode;
                apiError = apiErrorParsed;
            }

            var objectResult = new ObjectResult(apiError) { StatusCode = statusCode };

            return objectResult.ExecuteResultAsync(context);
        }

        #endregion

        #region Helpers

        private (ApiError, HttpStatusCode) GetError(Exception ex)
        {
            string labelCode = null;
            if (TryGetSqlException(ex, out Microsoft.Data.SqlClient.SqlException sqlException))
            {
                var dbCode = sqlException.Message.Substring(0, 10).Trim();
                labelCode = GetResourceCodeFromDbErrorCode(dbCode);
            }

            var error = new ApiError();
            HttpStatusCode httpStatusCode;

            if (!string.IsNullOrEmpty(labelCode))
            {
                error.Code = labelCode;
                httpStatusCode = HttpStatusCode.BadRequest;
            }
            else
            {
                if (ex is NoDataFoundException || ex.InnerException is NoDataFoundException)
                {
                    NoDataFoundException dataEx = ex is NoDataFoundException ? (NoDataFoundException)ex : (NoDataFoundException)ex.InnerException;

                    error.Code = dataEx.ObjectType == "DocumentProcess" ? "GL_ProcessExists_L" : "GL_NO_DATA_FOUND_L";
                    httpStatusCode = HttpStatusCode.NotFound;
                }
                else if (ex is AccessDeniedException || ex.InnerException is AccessDeniedException)
                {
                    error.Code = "GL_NO_RESOURCE_ACCESS_E";
                    httpStatusCode = HttpStatusCode.Forbidden;
                }
                else
                {
                    error.Code = "GL_SYSTEM_UNAVAILABLE_E";
                    httpStatusCode = HttpStatusCode.InternalServerError;
                }                
            }

            error.Message = _stringLocalizer[error.Code];

            return (error, httpStatusCode);
        }

        private static bool TryGetSqlException(Exception ex, out Microsoft.Data.SqlClient.SqlException sqlException)
        {
            Exception currentExCheck = ex;
            sqlException = null;

            while (currentExCheck != null)
            {
                if (currentExCheck is Microsoft.Data.SqlClient.SqlException)
                {
                    sqlException = (Microsoft.Data.SqlClient.SqlException)currentExCheck;
                    return true;
                }

                currentExCheck = currentExCheck.InnerException;
            }
            return false;
        }

        private static string GetResourceCodeFromDbErrorCode(string errorCode)
        {
            if (errorCode == DbErrorCodes.EmailAlreadyRegistered) return "GL_EMAIL_IS_ALREADY_REGISTERED_E";
            else if (errorCode == DbErrorCodes.UsernameAlreadyRegistered) return "GL_USR_0011_E";
            else if (errorCode == DbErrorCodes.AisCinAlreadyRegistered) return "GL_CIN_IS_ALREADY_REGISTERED_E";
            else if (errorCode == DbErrorCodes.IBANAlreadyRegistered) return "GL_IBAN_IS_ALREADY_REGISTERED_E";
            else if (errorCode == DbErrorCodes.DuplicateDocumentProcess) return "GL_PROCESSEXISTS_L";

            return null;
        }

        #endregion
    }
}
