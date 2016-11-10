using System.Data.Entity;
using System.Linq;
using entities;

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
            Question = new Question
            {
                Version = 1,
                Id = NextId
            };
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
            this.Reload();
            Context.Questions.Add(newQuestion);
            Context.SaveChanges();
            return new QuestionViewModel(Context, newQuestion);
        }

        public QuestionViewModel Duplicate()
        {
            var newQuestion = new Question
            {
                Description = Question.Description,
                Id = NextId,
                Version = 1
            };
            this.Reload();
            Context.Questions.Add(newQuestion);
            Context.SaveChanges();
            return new QuestionViewModel(Context, newQuestion);
        }

        public void Reload()
        {
            Description = (string)Context.Entry(Question).OriginalValues["Description"];
        }

        public void Create()
        {
            Context.Entry(Question).State = EntityState.Added;
            Context.SaveChanges();
        }

        private int NextId => Context.Questions.Any() ? Context.Questions.Max(q => q.Id) + 1 : 1;

        public void Disable()
        {
            Question.IsActive = false;
            Context.Entry(Question).State = EntityState.Modified;
            Context.SaveChanges();
        }
    }
}