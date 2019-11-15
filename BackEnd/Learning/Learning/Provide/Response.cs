using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Learning.Provide
{
    /// <summary>
    /// 前后端通信实体基类
    /// </summary>

    [DataContract]
    public abstract class BaseResponse
    {
        public static readonly int SuccessCode = 10000;

        /// <summary>
        /// 返回消息
        /// </summary>
        [DataMember]
        public string Message { get; set; }

        /// <summary>
        /// 返回代码
        /// </summary>
        [DataMember]
        public int Code { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        [DataMember]
        public object DeveloperMessage { get; set; }

    }

    /// <summary>
    /// 
    /// </summary>
    public class SuccessResponse<T> : BaseResponse
    {
        /// <summary>
        /// 返回类型
        /// </summary>
        [DataMember]
        public T Data { get; set; }

        [DataMember]
        public Pager Paper { get; set; }

        public SuccessResponse(int code, string message, T data)
        {
            Code = code;
            Message = message;
            Data = data;
        }

        public SuccessResponse(T data)
            : this(SuccessCode, WarningMessage.MessageDic[SuccessCode], data)
        {

        }
    }
    public class SuccessResponse : SuccessResponse<object>
    {
        public SuccessResponse(int code, string message, object data)
            : base(code, message, data)
        {

        }

        public SuccessResponse(object data)
            : base(data)
        {

        }

        public SuccessResponse()
            : base(null)
        {

        }
    }

    /// <summary>
    /// 成功列表对象
    /// </summary>
    public class SuccessListResponse<T> : BaseResponse
    {
        /// <summary>
        /// 返回类型
        /// </summary>
        [DataMember]
        public T List { get; set; }

        [DataMember]
        public Pager Pager { get; set; }

        public SuccessListResponse(int code, string message, T list, Pager pager)
        {

            Code = code;
            Message = message;
            List = list;
            Pager = pager;
        }

        public SuccessListResponse(T list, Pager pager)
            : this(SuccessCode, WarningMessage.MessageDic[SuccessCode], list, pager)
        {

        }
    }

    public class zidingyi : BaseResponse
    {

    }


    [KnownType(typeof(ErrorResponse))]
    public class ErrorResponse : BaseResponse
    {
        /// <summary>
        /// 正式环境
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        public ErrorResponse(int code, string message)
        {

            Code = code;
            Message = message;
        }
        /// <summary>
        /// 开发环境，返回错误信息
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="developerMessage"></param>
        public ErrorResponse(int code, string message, object developerMessage)
        {
            Code = code;
            Message = message;
            DeveloperMessage = developerMessage;
        }

        public ErrorResponse(Warning warning)
            : this(warning.Code, warning.Message)
        {

        }
    }

}
