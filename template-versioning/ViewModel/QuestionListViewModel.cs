using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using entities;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace template_versioning.ViewModel
{
    public class QuestionListViewModel : MainViewModel
    {
        public QuestionList QuestionList { get; }

        public QuestionViewModel QuestionToAdd { get; set; }

        public ObservableCollection<QuestionViewModel> Questions { get; set; }
        public QuestionItemViewModel SelectedQuestionItem { get; set; }
        public ObservableCollection<QuestionItemViewModel> QuestionItems { get; set; }
        public ICommand RemoveQuestionCommand { get; set; }
        public ICommand AddQuestionCommand { get; set; }

        public QuestionListViewModel(Entities context, QuestionList questionList, ObservableCollection<QuestionViewModel> questions ) : base(context)
        {
            AddQuestionCommand = new RelayCommand(AddQuestion);
            RemoveQuestionCommand = new RelayCommand(RemoveQuestion);
            QuestionList = questionList;
            Questions = questions;
            QuestionItems = new ObservableCollection<QuestionItemViewModel>(QuestionList.QuestionItems.Select(qi => new QuestionItemViewModel(Context, qi)));
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

        private void RemoveQuestion()
        {
            SelectedQuestionItem.Destroy();
            QuestionItems.Remove(SelectedQuestionItem);
        }
        private void AddQuestion()
        {
            if (QuestionToAdd == null) return;
            if (QuestionItems.Select(q => q.QuestionId).Contains(QuestionToAdd.Id)) return;
            var newQ = new QuestionItem {Question = QuestionToAdd.Question, QuestionList = QuestionList};
            Context.QuestionItems.Add(newQ);
            Context.SaveChanges();
            QuestionItems.Add(new QuestionItemViewModel(Context, newQ));
        }
    }
}
