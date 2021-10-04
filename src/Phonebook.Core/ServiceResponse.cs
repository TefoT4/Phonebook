﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.Core
{
    /* This class can be expanded in any way.
     */
    public class ServiceResponse<T>
    {
        public ServiceResponse()
        {
            IsSuccessful = true;
            Errors = new List<string>();
        }

        public T Entity { get; set; }

        public dynamic Tag { get; set; }
        
        public bool IsSuccessful { get; set; }

        public List<string> Errors { get; set; }
    }
}
