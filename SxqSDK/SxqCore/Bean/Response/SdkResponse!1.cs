namespace SxqCore.Bean.Response
{
    using System;

    public class SdkResponse<T>
    {
        private int code;
        private bool success;
        private string message;
        private T result;

        public bool Success
        {
            get
            {
                return this.success;
            }
            set
            {
                this.success = value;
            }
        }

        public int Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
            }
        }

        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
            }
        }

        public T Result
        {
            get
            {
                return this.result;
            }
            set
            {
                this.result = value;
            }
        }

    }
}

