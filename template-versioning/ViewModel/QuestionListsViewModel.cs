using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using entities;
using GalaSoft.MvvmLight.CommandWpf;
using template_versioning.Window;

namespace template_versioning.ViewModel
{
    public class QuestionListsViewModel : MainViewModel
    {
        private QuestionListWindow _questionListWindow;
        public ObservableCollection<QuestionListViewModel> QuestionLists { get; }
        public ObservableCollection<QuestionListViewModel> Templates { get; }
        public QuestionListViewModel SelectedQuestionList { get; set; }
        public QuestionListViewModel SelectedTemplate { get; set; }
        public ICommand OpenQuestionListCommand { get; set; }

        public QuestionListsViewModel(Entities context, QuestionsViewModel questions) : base(context)
        {
            OpenQuestionListCommand = new RelayCommand(OpenQuestionListWindow, CanOpenQuestionListWindow);

            QuestionLists = new ObservableCollection<QuestionListViewModel>(
                context.QuestionLists
                    .Include(ql => ql.QuestionItems)
                    .Include(ql => ql.QuestionItems.Select(qi => qi.Question))
                    .Include(ql => ql.QuestionItems.Select(qi => qi.Answer))
                    .ToList()
                    .Select(ql => new QuestionListViewModel(Context, ql, questions))
                    .ToList()
            );
            Templates = new ObservableCollection<QuestionListViewModel>(
                context.QuestionLists
                    .Where(ql => ql.IsTemplate)
                    .Include(ql => ql.QuestionItems)
                    .Include(ql => ql.QuestionItems.Select(qi => qi.Question))
                    
                    .ToList()
                    .Select(ql => new QuestionListViewModel(Context, ql, questions))
                    .ToList()
            );
        }

        private void OpenQuestionListWindow()
        {
           if(_questionListWindow == null) _questionListWindow = new QuestionListWindow();
           _questionListWindow.Show();
        }

        private bool CanOpenQuestionListWindow()
        {
            return _questionListWindow?.IsEnabled ?? true;
        }
    }
}
