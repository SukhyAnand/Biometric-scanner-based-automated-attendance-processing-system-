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
using System.Windows.Shapes;

namespace MEAPS
{
    public partial class SampleData : Window
    {
        public SampleData()
        {
            InitializeComponent();
            InitializeCustomControls();
        }

        public void InitializeCustomControls()
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            List<ComboBoxItem> cbListEmp = new List<ComboBoxItem>();
            foreach (Employee item in context.Employees)
            {
                ComboBoxItem cbiEmp = new ComboBoxItem();
                cbiEmp.Content = item.EmpId;
                cbiEmp.Tag = item.EmpId;
                cbListEmp.Add(cbiEmp);
            }
            cbEmp.ItemsSource = cbListEmp;
            cbEmp.ItemsSource = cbListEmp;

            for (int i = 0; i < 24; i++)
            {
                cbInHrs.Items.Add(i);
                cbOutHrs.Items.Add(i);
            }
            for (int i = 0; i < 60; i++)
            {
                cbInMins.Items.Add(i);
                cbOutMins.Items.Add(i);
            }
            dataGrid.ItemsSource = context.Attendances.ToList();
            dataGrid.Items.Refresh();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Attendance at = new Attendance();
            string entryTime = cbInHrs.Text + ":" + cbInMins.Text;
            string exitTime = cbOutHrs.Text + ":" + cbOutMins.Text;
            ComboBoxItem EmpId = (ComboBoxItem)cbEmp.SelectedItem;
            at.Employee_EmpId = Convert.ToInt32(EmpId.Content);
            at.InTime = TimeSpan.Parse(entryTime);
            at.OutTime = TimeSpan.Parse(exitTime);
            at.Date = Convert.ToDateTime(dtDate.Text);
            MEAPSDbEntities context = new MEAPSDbEntities();
            context.Attendances.Add(at);
            context.SaveChanges();
            dataGrid.ItemsSource = context.Attendances.ToList();
            dataGrid.Items.Refresh();
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            Attendance at = (Attendance)dataGrid.SelectedItem;
            MEAPSDbEntities context = new MEAPSDbEntities();
            Attendance deletable = context.Attendances.SingleOrDefault(x => x.SISOId == at.SISOId);
            context.Attendances.Remove(deletable);
            context.SaveChanges();
            dataGrid.ItemsSource = context.Attendances.ToList();
            dataGrid.Items.Refresh();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).btnLaunchViewer.IsEnabled = true;
        }
    }
}
