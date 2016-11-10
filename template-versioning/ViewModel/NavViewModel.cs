using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using template_versioning.Window;

namespace template_versioning.ViewModel
{
    public class NavViewModel : ViewModelBase
    {
        private QuestionListsWindow _questionListsWindow;
        private QuestionsWindow _questionsWindow;
        private bool _hasOpened;
        public ICommand OpenWindowsCommand { get; set; }

        public NavViewModel()
        {
            _hasOpened = false;
            OpenWindowsCommand = new RelayCommand(OpenWindows, CanOpenWindows);
        }

        private void OpenWindows()
        {
            OpenQuestionsWindow();
            OpenQuestionListsWindow();
            _hasOpened = true;
        }

        private bool CanOpenWindows() { return !_hasOpened;}

        private void OpenQuestionsWindow()
        {
            if(_questionsWindow == null) _questionsWindow = new QuestionsWindow();
            _questionsWindow.Show();
        }
        
         private void OpenQuestionListsWindow()
        {
            if(_questionListsWindow == null) _questionListsWindow = new QuestionListsWindow();
            _questionListsWindow.Show();
        }
    }
}
