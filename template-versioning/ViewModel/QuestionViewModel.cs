using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using entities;
using GalaSoft.MvvmLight.CommandWpf;

namespace template_versioning.ViewModel
{
    public class QuestionViewModel : MainViewModel
    {
        public Question Question { get; }
        public QuestionViewModel(Entities context, Question question) : base(context)
        {
            Question = question;
        }

        public QuestionViewModel(Entities context) : base(context)
        {
            Question = new Question {Number = NextNumber, Version = 1};
        }

        public int Number
        {
            get { return Question.Number; }
            set
            {
                Question.Number = value;
                RaisePropertyChanged();
            }
        }

        public string Description
        {
            get { return Question.Description; }
            set
            {
                Question.Description = value;
                RaisePropertyChanged();
            }
        }

        public decimal Version
        {
            get { return Question.Version; }
            set
            {
                Question.Version = value;
                RaisePropertyChanged();
            }
        }

        public QuestionViewModel Update()
        {
            switch (Context.Entry(Question).State)
            {
                    case EntityState.Unchanged:
                    return null;
                    case EntityState.Detached:
                    Create();
                    return this;
            }
            var newQuestion = new Question
            {
                Description = Question.Description,
                Number = Question.Number,
                Version = Question.Version + 1
            };
            Context.Entry(Question).Reload();
            Context.Questions.Add(newQuestion);
            Context.SaveChanges();
            return new QuestionViewModel(Context, newQuestion);
        }

        public QuestionViewModel Duplicate()
        {
            var newQuestion = new Question
            {
                Description = Question.Description,
                Number = NextNumber,
                Version = 1
            };
            Description = (string) Context.Entry(Question).OriginalValues["Description"];
            Context.Questions.Add(newQuestion);
            Context.SaveChanges();
            return new QuestionViewModel(Context, newQuestion);
        }

        public void Create()
        {
            Context.Questions.Add(Question);
            Context.SaveChanges();
        }

        private int NextNumber => Context.Questions.Max(q => q.Number) + 1;
    }
}