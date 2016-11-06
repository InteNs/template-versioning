using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using entities;

namespace template_versioning.ViewModel
{
    public class QuestionListsViewModel : MainViewModel
    {
        public ObservableCollection<QuestionListViewModel> QuestionLists { get; }
        public ObservableCollection<QuestionListViewModel> Templates { get; }
        public QuestionListViewModel SelectedQuestionList { get; set; }
        public QuestionListViewModel SelectedTemplate { get; set; }

        public QuestionListsViewModel(Entities context) : base(context)
        {
            QuestionLists = new ObservableCollection<QuestionListViewModel>(
                context.QuestionLists
                    .Include(ql => ql.QuestionItems)
                    .Include(ql => ql.QuestionItems.Select(qi => qi.Question))
                    .Include(ql => ql.QuestionItems.Select(qi => qi.Answer))
                    .ToList()
                    .Select(ql => new QuestionListViewModel(Context, ql))
                    .ToList()
            );
            Templates = new ObservableCollection<QuestionListViewModel>(
                context.QuestionLists
                    .Where(ql => ql.IsTemplate)
                    .Include(ql => ql.QuestionItems)
                    .Include(ql => ql.QuestionItems.Select(qi => qi.Question))
                    
                    .ToList()
                    .Select(ql => new QuestionListViewModel(Context, ql))
                    .ToList()
            );
        }
    }
}
