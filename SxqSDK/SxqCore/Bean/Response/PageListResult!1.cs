namespace SxqCore.Bean.Response
{
    using System.Collections.Generic;

    public class PageListResult<T>
    {
        private List<T> list;
        private int? totalCount;

        public int? TotalCount
        {
            get
            {
                return this.totalCount;
            }
            set
            {
                this.totalCount = value;
            }
        }

        public List<T> List
        {
            get
            {
                return this.list;
            }
            set
            {
                this.list = value;
            }
        }
    }
}

