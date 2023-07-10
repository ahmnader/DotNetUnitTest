using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace CommonHelpers.Models
{
    public class ApiRequest<T>
    {
        public ApiRequest(T model)
        {
            this.Model = model; 
        }
         

        [Required]
        public T Model { get; set; }
         
    }
}
