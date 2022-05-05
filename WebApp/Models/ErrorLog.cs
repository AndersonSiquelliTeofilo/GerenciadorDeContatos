using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class ErrorLog
    {
        public int Id { get; set; }

        public DateTime DateOccurred { get; set; }

        [MaxLength(100)]
        public string ClasseName { get; set; }

        [MaxLength(100)]
        public string Action { get; set; }

        public string StackTrace { get; set; }

        public string InnerException { get; set; }
    }
}