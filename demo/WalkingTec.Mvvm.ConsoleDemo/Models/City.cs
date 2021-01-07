using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace WalkingTec.Mvvm.Demo.Models
{
    public class City :TreePoco<City>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new int ID { get; set; }

        public new int? ParentId { get; set; }

        [Display(Name = "名称")]
        [Required(ErrorMessage = "名称是必填项")]
        public string Name { get; set; }

        [Display(Name = "test")]
        public string Test { get; set; }

    }
}
