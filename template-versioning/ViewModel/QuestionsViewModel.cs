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
        public ObservableCollection<QuestionViewModel> Questions { get; }
        public QuestionViewModel SelectedQuestion { get; set; }
        public ICommand UpdateQuestionCommand { get; set; }
        public ICommand DuplicateQuestionCommand { get; set; }
        public ICommand CreateQuestionCommand { get; set; }
        public ICommand DisableQuestionCommand { get; set; }

        public QuestionsViewModel(Entities context) : base(context)
        {
            UpdateQuestionCommand = new RelayCommand(UpdateQuestion, CanSaveQuestion);
            DuplicateQuestionCommand = new RelayCommand(DuplicateQuestion, CanSaveQuestion);
            CreateQuestionCommand = new RelayCommand(CreateQuestion);
            DisableQuestionCommand = new RelayCommand(DisableQuestion);

            var questions = from q in context.Questions
                where !(from qq in context.Questions
                    where qq.Id == q.Id && qq.Version > q.Version
                    select qq).Any()
                select q;

            Questions = new ObservableCollection<QuestionViewModel>(questions.ToList().Select(q => new QuestionViewModel(context, q)).ToList());
        }

        private void UpdateQuestion()
        {
            var newQuestion = SelectedQuestion.Update();
            if (newQuestion == null) return;
            Questions.Add(newQuestion);
            Questions.Remove(SelectedQuestion);
        }

        private void DisableQuestion()
        {
            SelectedQuestion.Disable();
            Questions.Remove(SelectedQuestion);
        }

        private void DuplicateQuestion()
        {
            var newQuestion = SelectedQuestion.Duplicate();
            if (newQuestion == null) return;
            Questions.Add(newQuestion);
        }

        private bool CanSaveQuestion()
        {
            return SelectedQuestion?.Description?.Replace(" ", "").Length > 0;
        }

        private void CreateQuestion()
        {
            var newQuestion = new QuestionViewModel(Context);
            Questions.Add(newQuestion);
        }
    }
}
