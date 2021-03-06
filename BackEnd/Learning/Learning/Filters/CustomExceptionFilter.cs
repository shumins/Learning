﻿using Learning.Provide;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Learning.Filters
{
    /// <summary>
    /// 全局异常过滤器
    /// </summary>
    public class CustomExceptionFilter : IExceptionFilter
    {
        readonly ILoggerFactory _loggerFactory;
        readonly IHostingEnvironment _env;

        public CustomExceptionFilter(ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            _loggerFactory = loggerFactory;
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            var logger = _loggerFactory.CreateLogger(context.Exception.TargetSite.ReflectedType);

            logger.LogError(new EventId(context.Exception.HResult),
            context.Exception,
            context.Exception.Message);

            var json = new ErrorResponse(0, "未知错误,请重试");
            //判断是否开发环境
            if (_env.IsDevelopment())
            {
                json.DeveloperMessage = context.Exception;
            }
            context.Result = new ApplicationErrorResult(json);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.ExceptionHandled = true;
        }
    }

    public class ApplicationErrorResult : ObjectResult
    {
        public ApplicationErrorResult(object value) : base(value)
        {
            StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }

    //    public class ErrorResponse
    //    {
    //        public ErrorResponse(string msg)
    //    {
    //        Message = msg;
    //    }
    //    public string Message { get; set; }
    //    public object DeveloperMessage { get; set; }
    //}
}
