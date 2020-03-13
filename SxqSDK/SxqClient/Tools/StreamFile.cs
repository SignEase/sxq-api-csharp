namespace SxqClient.Tools
{
    using System;
    using System.IO;

    public class StreamFile : IFileItem
    {
        public static string MIME_TYPE_DEFAULT = "application/octet-stream";
        public static int READ_BUFFER_SIZE = 0x1000;
        private string fileName;
        private Stream stream;
        private string mimeType;
        private static string DEFAULT_FILE_NAME = "streamFile";

        public StreamFile(Stream stream)
        {
            this.fileName = DEFAULT_FILE_NAME;
            this.stream = stream;
            this.mimeType = MIME_TYPE_DEFAULT;
        }

        public StreamFile(string fileName, Stream stream)
        {
            this.fileName = fileName;
            this.stream = stream;
            this.mimeType = MIME_TYPE_DEFAULT;
        }

        public StreamFile(string fileName, Stream stream, string mimeType)
        {
            this.fileName = fileName;
            this.stream = stream;
            this.mimeType = mimeType;
        }

        public long GetFileLength() => 
            0L;

        public string GetFileName() => 
            this.fileName;

        public string GetMimeType()
        {
            if (this.mimeType == null)
            {
                return MIME_TYPE_DEFAULT;
            }
            return this.mimeType;
        }

        public bool IsValid() => 
            ((this.stream != null) && !string.IsNullOrEmpty(this.fileName));

        public void Write(ref Stream output)
        {
            try
            {
                long length = this.stream.Length;
                byte[] buffer = new byte[READ_BUFFER_SIZE];
                int count = 0;
                while (length > 0L)
                {
                    count = this.stream.Read(buffer, 0, READ_BUFFER_SIZE);
                    length -= count;
                    output.Write(buffer, 0, count);
                }
            }
            finally
            {
                if (this.stream != null)
                {
                    this.stream.Close();
                }
            }
        }
    }
}

