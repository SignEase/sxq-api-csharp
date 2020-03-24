using System;
namespace SxqCore.Bean.Contract
{
    public class ContractVariable
    {
        public const string TYEP_TEXT = "TEXT";         //文本类型
        public const string TYEP_NUMBER = "NUMBER";     //数字类型
        public const string TYEP_DATE = "DATE";         //时间类型

        public const string CATEGORY_AMOUNT = "AMOUNT"; //金额

        public const string ALT_SELECT = "SELECT";      //下拉框
        public const string ALT_RADIO = "RADIO";        //单选框
        public const string ALT_CHECKOUT = "CHECKOUT";  //多选框
       

        // 唯一标识符
        private int id;
        // 变量名称 （合同变量标签）
        private string label;
        // 模板变量的内容
        private string content;
        // 内容类型
        private string contentType;
        // 内容类别
        private string category;
        // 变量定位 X 轴
        private double positionX;
        // 变量定位 Y 轴
        private double positionY;
        // 页码
        private int pageNumber;
 
        // 备选内容，可能是html 片段的下拉框，多选框，也有可能是其他内容
        private string alternativeContent;
        // 这个变量的操作用户id
        private long operationUserId;
        // 是否已使用过了
        private bool used;

        /** 扩展字段-暂未启用
        // 该签章变量的签署阶段（在哪个阶段添加）
        private string signPhase;
        **/


        public ContractVariable()
        {
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }


        public string Label
        {
            get
            {
                return this.label;
            }
            set
            {
                this.label = value;
            }
        }

        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
            }
        }

        public string ContentType
        {
            get
            {
                return this.contentType;
            }
            set
            {
                this.contentType = value;
            }
        }

        public double PositionX
        {
            get
            {
                return this.positionX;
            }
            set
            {
                this.positionX = value;
            }
        }

        public double PositionY
        {
            get
            {
                return this.positionY;
            }
            set
            {
                this.positionY = value;
            }
        }

        public int PageNumber
        {
            get
            {
                return this.pageNumber;
            }
            set
            {
                this.pageNumber = value;
            }
        }

        public string Category
        {
            get
            {
                return this.category;
            }
            set
            {
                this.category = value;
            }
        }

        public string AlternativeContent
        {
            get
            {
                return this.alternativeContent;
            }
            set
            {
                this.alternativeContent = value;
            }
        }

        public long OperationUserId
        {
            get
            {
                return this.operationUserId;
            }
            set
            {
                this.operationUserId = value;
            }
        }

        public bool Used
        {
            get
            {
                return this.used;
            }
            set
            {
                this.used = value;
            }
        }

    }
}
