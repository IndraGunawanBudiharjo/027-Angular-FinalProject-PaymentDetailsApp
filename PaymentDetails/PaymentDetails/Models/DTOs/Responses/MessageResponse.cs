using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentDetails.Models.DTOs.Responses
{
    public class MessageResponse
    {
        public string message { get; set; }
        public PaymentDetail item { get; set; }
    }
}
