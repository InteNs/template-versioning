using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using entities;
using GalaSoft.MvvmLight.CommandWpf;
using lib;

namespace template_versioning.ViewModel
{
    public class QuestionsViewModel : MainViewModel
    {
        public ObservableCollection<QuestionViewModel> Questions { get; set; }
        public QuestionViewModel SelectedQuestion { get; set; }
        public ICommand UpdateQuestionCommand { get; set; }
        public ICommand DuplicateQuestionCommand { get; set; }
        public ICommand CreateQuestionCommand { get; set; }
        public ICommand DisableQuestionCommand { get; set; }
        public ICommand ReloadQuestionCommand { get; set; }

        public QuestionsViewModel(Entities context) : base(context)
        {
            UpdateQuestionCommand = new RelayCommand<QuestionViewModel>(UpdateQuestion, CanSaveQuestion);
            DuplicateQuestionCommand = new RelayCommand<QuestionViewModel>(DuplicateQuestion, CanSaveQuestion);
            CreateQuestionCommand = new RelayCommand(CreateQuestion);
            DisableQuestionCommand = new RelayCommand(DisableQuestion);
            ReloadQuestionCommand = new RelayCommand(ReloadQuestion);

            GetData();
        }

        private void GetData()
        {
            var questions = from q in Context.Questions
                            where !(from qq in Context.Questions
                                    where qq.Id == q.Id && qq.Version > q.Version
                                    select qq).Any() && q.IsActive
                            select q;

            Questions = new ObservableCollection<QuestionViewModel>(questions.ToList().Select(q => new QuestionViewModel(Context, q)).ToList());
        }

        private void UpdateQuestion(QuestionViewModel q)
        {
            var newQuestion = q.Update();
            if (newQuestion == null) return;
            Questions.Add(newQuestion);
            Questions.Remove(q);
        }

        private void DisableQuestion()
        {
            SelectedQuestion.Disable();
            Questions.Remove(SelectedQuestion);
        }

        private void ReloadQuestion()
        {
            SelectedQuestion.Reload();
        }

        private void DuplicateQuestion(QuestionViewModel q)
        {
            var newQuestion = q.Duplicate();
            if (newQuestion == null) return;
            Questions.Add(newQuestion);
        }

        private bool CanSaveQuestion(QuestionViewModel q)
        {
            return q?.Description?.Replace(" ", "").Length > 0;
        }

        private void CreateQuestion()
        {
            var newQuestion = new QuestionViewModel(Context);
            Questions.Add(newQuestion);
        }
    }
}
