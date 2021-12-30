using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlogFsn.Areas.Admin.Models
{
    public class Compo_ListGridRolesViewModel
    {
        //dd
        public string Id { get; set; }
        public string ParentId { get; set; }

        [Display(Name = "PageName")]
        public string PageName { get; set; }
        public int Sort { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
        public bool HasChild { get; set; }
    }
}
