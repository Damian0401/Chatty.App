using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Message
{
    public class DeleteMessageRequestDto
    {
        public Guid MessageId { get; set; }
    }
}