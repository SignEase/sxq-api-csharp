namespace SxqClient.Tools
{
    using SxqClient.Http;
    using System;

    public interface IHttpRequest
    {
        HttpParamers GetHttpParamers();
        string GetRequestPath();
    }
}

