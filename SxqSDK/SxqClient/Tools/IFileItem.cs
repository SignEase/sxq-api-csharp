namespace SxqClient.Tools
{
    using System;
    using System.IO;

    public interface IFileItem
    {
        long GetFileLength();
        string GetFileName();
        string GetMimeType();
        bool IsValid();
        void Write(ref Stream output);
    }
}

