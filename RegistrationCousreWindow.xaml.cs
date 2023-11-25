﻿using System;
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
using System.Windows.Shapes;

namespace Academy_fitnessGasanova
{
   
    public partial class RegistrationCousreWindow : Window
    {
        AcademyFitnessBaseEntities1 context;
        public RegistrationCousreWindow(AcademyFitnessBaseEntities1 context, CourseRegistration currentRegistration)
        {
            InitializeComponent();
            this.context = context;
            CmbCourse.ItemsSource = context.Courses.ToList();
            CmbTrainer.ItemsSource = context.Trainers.ToList();
            this.DataContext = currentRegistration;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CheckRegistration())
            { 
                context.SaveChanges();
            this.Close();
            }

        }

        private bool CheckRegistration()
        {
            var reg = this.DataContext as CourseRegistration;
            if (reg.Trainer == null)
            {
                MessageBox.Show("Тренер не выбран");
                return false;
            }
            if (reg.Course == null)
            {
                MessageBox.Show("Курсы  не выбраны");
                return false;
            }
            if (reg.CreatedDate == null)
            {
                MessageBox.Show("Дата не выбрана");
                return false;
            }
            if (!int.TryParse(TxtTotalPoint.Text, out int result))
            {
                MessageBox.Show("Неверные данные");
                return false;
            }
            return true;
        }
    }
}
