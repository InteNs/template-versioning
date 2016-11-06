using System.Collections;
using entities;
using GalaSoft.MvvmLight;

namespace template_versioning.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        protected Entities Context { get; set; }
        public MainViewModel(Entities context)
        {
            Context = context;
        }
    }
}