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
            Question = new Question {Version = 1};
        }

        public int Id
        {
            get { return Question.Id; }
            set
            {
                Question.Id = value;
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

        public int Version
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
                Id = Question.Id,
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

        public void Disable()
        {
            Question.IsActive = false;
            Context.Entry(Question).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}