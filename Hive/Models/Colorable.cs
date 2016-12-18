using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hive.Models
{
    public abstract class Colorable
    {
        public string Color { get; set; }
    }
}