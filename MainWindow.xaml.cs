using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Academy_fitnessGasanova
{
  
    public partial class MainWindow : Window
    {
        AcademyFitnessBaseEntities1 context;
        public MainWindow()
        {
            InitializeComponent();
            context = new AcademyFitnessBaseEntities1();
            CmbSelectTrainer.ItemsSource = context.Trainers.ToList();       
            RegistrationGrid.ItemsSource = context.CourseRegistrations.ToList();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        { 
            var newRegistration = new CourseRegistration();
            context.CourseRegistrations.Add(newRegistration);
            var win = new RegistrationCousreWindow(context, newRegistration);
            win.ShowDialog();
            RegistrationGrid.ItemsSource = context.CourseRegistrations.ToList();

        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var row = RegistrationGrid.SelectedItem as CourseRegistration;
            if (row == null)
            {
                MessageBox.Show("Строка не выбрана");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Вы уыерены", "Вопрос", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                context.CourseRegistrations.Remove(row);
                context.SaveChanges();
                RegistrationGrid.ItemsSource = context.CourseRegistrations.ToList();
            }
        }

        private void CmbSelectTrainer_SlectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbSelectTrainer.SelectedItem == null) return;
            var currentTrainer = (Trainer)CmbSelectTrainer.SelectedItem;
            List<CourseRegistration> ListTrainer=context.CourseRegistrations.ToList();
            RegistrationGrid.ItemsSource = ListTrainer.Where(x => x.Trainer == currentTrainer).ToList();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationGrid.ItemsSource = context.CourseRegistrations.ToList();  
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Button EditButton = sender as Button;
            var currentRegistratioon = EditButton.DataContext as CourseRegistration;
            var win = new RegistrationCousreWindow(context, currentRegistratioon);
            win.ShowDialog();
        }

        private void FiltrButton_Click(object sender, RoutedEventArgs e) { 
            RegistrationGrid.ItemsSource = context.CourseRegistrations.Where(x => x.IsDone == true).ToList();
            MessageBoxResult result = MessageBox.Show("Вернуть все записи?", "Вопрос", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                RegistrationGrid.ItemsSource = context.CourseRegistrations.ToList();
            }
        }
    }
}
