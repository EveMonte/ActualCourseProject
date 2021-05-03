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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Курсач.Singleton;
using Курсач.ViewModels;

namespace Курсач.UserControls
{
    /// <summary>
    /// Логика взаимодействия для FullInfo.xaml
    /// </summary>
    public partial class FullInfo : UserControl
    {
        public FullInfo()
        {
            InitializeComponent();
            DataContext = FullInfoViewModelSingleTone.GetInstance(new FullInfoViewModel()).FullInfoViewModel;

        }
    }
}
