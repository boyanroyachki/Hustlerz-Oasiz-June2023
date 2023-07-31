using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HustlerzOasiz.Web.ViewModels.Job
{
	public class JobDeleteViewModel
	{
        public string Title { get; set; } = null!;

        public string Location { get; set; } = null!;

        public string Details { get; set; } = null!;

        public DateTime DatePosted { get; set; }
    }
}
