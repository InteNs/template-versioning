/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:template_versioning"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using System.ComponentModel.Design;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using entities;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace template_versioning.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<Entities, Entities>();
            SimpleIoc.Default.Register<QuestionListViewModel>();
            SimpleIoc.Default.Register<QuestionListsViewModel>();
            SimpleIoc.Default.Register<QuestionsViewModel>();
            SimpleIoc.Default.Register<NavViewModel>();
        }

        public NavViewModel Main => ServiceLocator.Current.GetInstance<NavViewModel>();
        public Entities DbContext => ServiceLocator.Current.GetInstance<Entities>(); 
        public QuestionListsViewModel QuestionLists => ServiceLocator.Current.GetInstance<QuestionListsViewModel>();
        public QuestionListViewModel QuestionList => QuestionLists.SelectedTemplate;
        public QuestionsViewModel Questions => ServiceLocator.Current.GetInstance<QuestionsViewModel>();

        public static void Cleanup()
        {
            ServiceLocator.Current.GetInstance<Entities>().Dispose();
        }
    }
}