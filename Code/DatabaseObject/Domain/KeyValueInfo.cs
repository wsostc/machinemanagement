using System.IO;

namespace DatabaseObject.Domain
{
    public class KeyValueInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class FileStreamInfo
    {
        public string FileName { get; set; }
        public Stream Stream { get; set; }
    }
}
