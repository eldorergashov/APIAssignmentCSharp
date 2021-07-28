using System;
using System.Collections.Generic;
using System.Text;

namespace APITesting.Types
{
    public class ResponseData
    {
        public string RequestId { get; set; }
        public int HTTPStatusCode { get; set; }
        public int RetryAttempts { get; set; }

    }
}
