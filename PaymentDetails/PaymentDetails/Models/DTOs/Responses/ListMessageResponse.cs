using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentDetails.Models.DTOs.Responses
{
    public class ListMessageResponse
    {
        public string message { get; set; }
        public List<PaymentDetail> item { get; set; }
    }
}
