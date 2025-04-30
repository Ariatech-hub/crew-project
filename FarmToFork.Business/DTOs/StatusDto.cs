using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FarmToFork.Business.DTOs
{
    public class StatusDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Code { get; set; } = null!;
        public int IsCustomerStatus { get; set; }
        public int IsCustomerGrainCycleStatus { get; set; }
    }
}
