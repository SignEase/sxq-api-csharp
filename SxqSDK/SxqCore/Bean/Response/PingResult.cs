namespace SxqCore.Bean.Response
{
    using System;

    public class PingResult
    {
        private string serviceTime;

        public string ServiceTime
        {
            get
            {
                return this.serviceTime;
            }
            set
            {
                this.serviceTime = value;
            }
        }

    }
}

