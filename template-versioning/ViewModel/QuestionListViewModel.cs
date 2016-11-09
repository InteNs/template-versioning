using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entities;

namespace template_versioning.ViewModel
{
    public class QuestionListViewModel : MainViewModel
    {
        public QuestionList QuestionList { get; }

        public QuestionListViewModel(Entities context, QuestionList questionList) : base(context)
        {
            QuestionList = questionList;
        }

        public string Description
        {
            get { return QuestionList.Description; }
            set
            {
                QuestionList.Description = value;
                RaisePropertyChanged();
            }
        }

        public int Id
        {
            get { return QuestionList.Id; }
            set
            {
                QuestionList.Id = value;
                RaisePropertyChanged();
            }
        }

        public bool IsTemplate
        {
            get { return QuestionList.IsTemplate; }
            set
            {
                QuestionList.IsTemplate = value;
                RaisePropertyChanged();
            }
        }
    }
}
