using System;

namespace Domain
{
    public class Image : BaseEntity
    {
        public string Name { get; set; } = "";
        public string Extension { get; set; } = "";
        public byte[] Content { get; set; } = null!;
    }
}