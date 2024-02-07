using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using CNSys;
using System.Linq;
using EAU.Web.Models;
using CNSys.Data;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.DependencyInjection;

namespace EAU.Web.Mvc
{
    /// <summary>
    /// Базов Api контролер.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : Controller
    {
        private IMapper _mapper;

        #region Properties

        protected IMapper Mapper
        {
            get
            {
                return _mapper != null ? _mapper : _mapper = HttpContext.RequestServices.GetRequiredService<IMapper>();
            }
        }

        #endregion

        #region Public Methods

        protected IActionResult BadRequest(OperationResult operResult)
        {
            return BadRequest(operResult, null, null);
        }

        protected IActionResult BadRequest(OperationResult operResult, string errorCode, string errorMessage)
        {
            if (operResult.OperationResultType != OperationResultTypes.CompletedWithError)
                throw new ArgumentException("OperationResultType must be CompletedWithError.");

            if (operResult.Errors?.Any() != true)
                throw new ArgumentException("No error in OperationResult.");

            var error = new ApiError();

            if (!string.IsNullOrEmpty(errorCode) || !string.IsNullOrEmpty(errorMessage))
            {
                error.Code = errorCode;
                error.Message = errorMessage;
            }
            else if (operResult.Errors.Count == 1)
            {
                MapApiError(operResult.Errors.Single(), ref error);
            }
            else
            {
                error.Code = EAUErrorCodes.ValidationError.ToString();
            }

            if (operResult.Errors.Count > 1 || !string.IsNullOrEmpty(errorCode) || !string.IsNullOrEmpty(errorMessage))
            {
                error.InnerErrors = new List<ApiError>();

                foreach (var err in operResult.Errors)
                {
                    var innerError = new ApiError();
                    MapApiError(err, ref innerError);
                    error.InnerErrors.Add(innerError);
                }
            }

            TryLocalize(error);
            return new BadRequestObjectResult(error);
        }

        protected IActionResult BadRequest(string errorCode, string errorMessage)
        {
            var error = new ApiError()
            {
                Code = errorCode,
                Message = errorMessage
            };

            TryLocalize(error);
            return new BadRequestObjectResult(error);

        }

        protected IActionResult OperationResult(OperationResult result)
        {
            if (result.IsSuccessfullyCompleted)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result);
            }
        }

        protected IActionResult OperationResult<T>(OperationResult<T> result)
        {
            if (result.IsSuccessfullyCompleted)
            {
                return Ok(result.Result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        protected IActionResult OperationResult<TSource, TTarget>(OperationResult<TSource> result)
        {
            if (result.IsSuccessfullyCompleted)
            {
                return Ok(Mapper.Map<TTarget>(result.Result));
            }
            else
            {
                return BadRequest(result);
            }
        }

        protected IActionResult Result<T>(T value)
        {
            if (value == null || (value is ICollection && ((ICollection)value).Count == 0))
                return Ok();
            else
                return Ok(value);
        }

        protected IActionResult PagedResult<T>(T value, CNSys.Data.PagedDataState pagedState)
        {
            return PagedResult(value, pagedState.Count);
        }

        protected IActionResult PagedResult<T>(T value, int? count)
        {
            if (value == null || (value is ICollection && ((ICollection)value).Count == 0))
            {
                if (count.HasValue)
                {
                    Request.HttpContext.Response.Headers.Add("count", count.ToString());
                }

                return Ok();
            }
            else
            {
                if (count.HasValue)
                {
                    Request.HttpContext.Response.Headers.Add("count", count.ToString());
                }

                return Ok(value);
            }
        }

        protected IActionResult PagedResult<TSource, TTarget>(TSource value, PagedDataState pagedState)
        {
            return PagedResult<TSource, TTarget>(value, pagedState.Count);
        }

        protected IActionResult PagedResult<TSource, TTarget>(TSource value, int? count)
        {
            if (value == null)
                return Ok();
            else
            {
                return PagedResult(Mapper.Map<TTarget>(value), count);
            }
        }

        #endregion

        #region Helpers

        private void MapApiError(IError error, ref ApiError apiError)
        {
            apiError.Code = error.Code;
            apiError.Message = error.Message;
        }

        private void TryLocalize(ApiError error)
        {
            var errorMessageLocalized = GetStringLocalizer()[error.Code];
            if (!string.IsNullOrEmpty(errorMessageLocalized))
            {
                error.Message = errorMessageLocalized;
            }
            if (error.InnerErrors != null)
            {
                foreach (var err in error.InnerErrors)
                {
                    var errMessageLocalized = GetStringLocalizer()[err.Code];
                    if (!string.IsNullOrEmpty(errMessageLocalized))
                    {
                        err.Message = errMessageLocalized;
                    }
                }
            }
        }

        private IStringLocalizer GetStringLocalizer()
        {
            return HttpContext.RequestServices.GetService<IStringLocalizer>();
        }

        #endregion
    }
}
