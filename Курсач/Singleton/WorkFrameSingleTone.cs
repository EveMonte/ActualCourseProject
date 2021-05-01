using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Курсач.ViewModels;

namespace Курсач.Singleton
{
    public class WorkFrameSingleTone
    {
        private static WorkFrameSingleTone _instance;
        public WorkframeViewModel WorkframeViewModel { get; set; }
        private WorkFrameSingleTone(WorkframeViewModel viewModel)
        {
            WorkframeViewModel = viewModel;
        }
        public static WorkFrameSingleTone GetInstance(WorkframeViewModel viewModel = null)
        {
            return _instance ?? (_instance = new WorkFrameSingleTone(viewModel));
        }
    }
}
