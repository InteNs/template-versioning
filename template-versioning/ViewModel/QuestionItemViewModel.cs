using System.Data.Entity;
using System.Linq;
using System.Windows.Input;
using entities;
using GalaSoft.MvvmLight.CommandWpf;

namespace template_versioning.ViewModel
{
    public class QuestionItemViewModel : MainViewModel
    {
        public QuestionItem QuestionItem { get; }

        public ICommand DestroyCommand { get; set; }

        public QuestionItemViewModel(Entities context, QuestionItem questionItem) : base(context)
        {
            QuestionItem = questionItem;
            DestroyCommand = new RelayCommand(Destroy, CanDestroy);
        }

        public int QuestionId => QuestionItem.QuestionId;

        public int QuestionVersion => QuestionItem.QuestionVersion;

        public string QuestionDescription => QuestionItem.Question.Description;

        public string Answer => QuestionItem.Answer.Value;

        public void Destroy()
        {
            Context.QuestionItems.Remove(QuestionItem);
            Context.SaveChanges();
        }

        public bool CanDestroy()
        {
            return QuestionItem.AnswerId != null;
        }
    }
}