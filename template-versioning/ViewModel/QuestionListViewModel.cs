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

        public QuestionItemViewModel SelectedQuestionItem { get; set; }

        private readonly QuestionsViewModel _questions;
        public ObservableCollection<QuestionItemViewModel> QuestionItems { get; set; }
        public ICommand RemoveQuestionCommand => SelectedQuestionItem?.DestroyCommand;
        public ICommand AddQuestionCommand { get; set; }

        public QuestionListViewModel(Entities context, QuestionList questionList, QuestionsViewModel questions) : base(context)
        {
            AddQuestionCommand = new RelayCommand(AddQuestion);
            QuestionList = questionList;
            _questions = questions;
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

        private void AddQuestion()
        {
            var newItem = new QuestionItem { QuestionList = QuestionList, Question = _questions.SelectedQuestion.Question};
            Context.QuestionItems.Add(newItem);
            QuestionItems.Add(new QuestionItemViewModel(Context, newItem));
            Context.SaveChanges();
        }
    }
}
