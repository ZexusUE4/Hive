using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Hive.Models.ViewModels
{
    public class TaskCreateVM
    {
        public TaskCreateVM()
        {
            SelectedAssignees = new List<string>();
        }
        public Task Task { get; set; }

        [Required(ErrorMessage = "Please select at least one assignee")]
        public List<string> SelectedAssignees { get; set; }
    }
}