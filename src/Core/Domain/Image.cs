using System;

namespace Domain
{
    public class Image : BaseEntity
    {
        public byte[] Content { get; set; } = null!;
    }
}