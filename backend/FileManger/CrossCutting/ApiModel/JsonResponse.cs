using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCutting.ApiModel
{
    public class JsonResponse
    {
        public string Type { get; } = "https://tools.ietf.org/html/rfc7231";
        public string Title { get; set; } = "Información.";
        public int Status { get; set; }
        public string TraceId { get; set; }
        public dynamic Errors { get; set; }
        public dynamic Result { get; set; }
    }
}
