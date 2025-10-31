﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shared.Error_Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string ErrorMessage { get; set; }=string.Empty;     
        
        public IEnumerable<string>? Errors { get; set; }    
        override public string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
