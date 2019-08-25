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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //global declarations
        int minTabIndex = 0, maxTabIndex = 7;
        //end of global declarations
        private void RefreshDataGrids()
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            dataGridEmployees.ItemsSource = context.Employees.ToList();
            dataGridEmployees.Items.Refresh();
            dataGridHolidays.ItemsSource = context.Holidays.ToList();
            dataGridHolidays.Items.Refresh();
            dataGridExcepHolidays.ItemsSource = context.ExceptionAttendances.ToList();
            dataGridExcepHolidays.Items.Refresh();
            dataGridExcepTime.ItemsSource = context.TimeExceptions.ToList();
            dataGridExcepTime.Items.Refresh();
            dataGridLeaveApp.ItemsSource = context.LeaveApplications.ToList();
            dataGridLeaveApp.Items.Refresh();
            dataGridLeaveStat.ItemsSource = context.LeaveStatuses.ToList();
            dataGridLeaveStat.Items.Refresh();
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            //initialize employee list
            MEAPSDbEntities context = new MEAPSDbEntities();
            List<ComboBoxItem> cbListEmp = new List<ComboBoxItem>();
            foreach (Employee item in context.Employees)
            {
                ComboBoxItem cbiEmp = new ComboBoxItem();
                cbiEmp.Content = item.EmpId;
                cbiEmp.Tag = item.EmpId;
                cbListEmp.Add(cbiEmp);
            }
            cbAEEmp.ItemsSource = cbListEmp;
            cbLEEmp.ItemsSource = cbListEmp;
            cbReportEmp.ItemsSource = cbListEmp;

            //initialize time selectors
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
            cbInHrs.SelectedIndex = 9;
            cbInMins.SelectedIndex = 50;
            cbOutHrs.SelectedIndex = 17;
            cbOutMins.SelectedIndex = 25;

            //initialize year selectors
            int lastyear = Int32.Parse(System.DateTime.Now.Year.ToString()) + 5;
            int firstyear = Int32.Parse(System.DateTime.Now.Year.ToString()) - 5;

            for (int i = firstyear; i <= lastyear; i++)
            {
                cbLeaveStatMonthlyYear.Items.Add(i);
                cbReportYears.Items.Add(i);
            }
            cbLeaveStatMonthlyYear.SelectedIndex = 5;
            cbReportMonths.SelectedIndex = 1;
            cbReportYears.SelectedIndex = 5;
            try
            {
                cbAEEmp.SelectedIndex = 0;
                cbLEEmp.SelectedIndex = 0;
                cbLeaveType.SelectedIndex = 0;
                cbReportEmp.SelectedIndex = 0;
            }
            catch (Exception)
            {
                
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            RefreshDataGrids();
            MEAPSDbEntities context = new MEAPSDbEntities();
            TabCtrl t = context.TabCtrls.FirstOrDefault();
            if (t == null)
            {
                t = new TabCtrl();
                t.selectedIndex = "0";
                context.TabCtrls.Add(t);
                context.SaveChanges();
            }
            btnLaunchViewer.Visibility = Visibility.Hidden;
            tabCtrlMain.SelectedIndex = Int32.Parse(t.selectedIndex);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (tabCtrlMain.SelectedIndex > minTabIndex)
            {
                tabCtrlMain.SelectedIndex--;
            }
        }

        private void btnLaunchUploader_Click(object sender, RoutedEventArgs e)
        {
            btnLaunchUploader.IsEnabled = false;
            Uploader upwin = new Uploader();
            upwin.Show();
        }

        private void btnLaunchViewer_Click(object sender, RoutedEventArgs e)
        {
            btnLaunchViewer.IsEnabled = false;
            SampleData sd = new SampleData();
            sd.Show();
        }

        private void checkBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void btnEmpAdd_Click(object sender, RoutedEventArgs e)
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            Employee em = new Employee();
            em.EmpId = Int32.Parse(txtEmpId.Text);
            em.Name = txtEmpName.Text;
            em.Dept = txtDept.Text;
            em.CL = Double.Parse(txtCL.Text);
            em.ML = Double.Parse(txtML.Text);
            em.EL = Double.Parse(txtEL.Text);
            em.LWP = Double.Parse(txtLWP.Text);
            em.JoiningDate = Convert.ToDateTime(dtJoiningDate.Text);
            context.Employees.Add(em);
            context.SaveChanges();
            RefreshDataGrids();
        }

        private void btnEmpDelSelected_Click(object sender, RoutedEventArgs e)
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            Employee em = (Employee)dataGridEmployees.SelectedItem;
            Employee deletable = context.Employees.SingleOrDefault(x => x.EmpId == em.EmpId);
            context.Employees.Remove(deletable);
            context.SaveChanges();
            RefreshDataGrids();
        }

        private void btnEmpUpdate_Click(object sender, RoutedEventArgs e)
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            Employee em = (Employee)dataGridEmployees.SelectedItem;
            Employee updatable = context.Employees.SingleOrDefault(x => x.EmpId == em.EmpId);
            updatable.EmpId = int.Parse(txtEmpId.Text);
            updatable.Name = txtEmpName.Text;
            updatable.Dept = txtDept.Text;
            updatable.CL = Double.Parse(txtCL.Text);
            updatable.ML = Double.Parse(txtML.Text);
            updatable.EL = Double.Parse(txtEL.Text);
            updatable.JoiningDate = Convert.ToDateTime(dtJoiningDate.Text);
            updatable.LWP = Double.Parse(txtLWP.Text);
            context.SaveChanges();
            RefreshDataGrids();
        }
        private void btnClr_Click(object sender, RoutedEventArgs e)
        {
            txtEmpId.Text = "";
            txtEmpName.Text = "";
            txtDept.Text = "";
            dtJoiningDate.Text = "";
            txtCL.Text = "";
            txtML.Text = "";
            txtEL.Text = "";
            txtLWP.Text = "";
        }

        private void btnFill_Click(object sender, RoutedEventArgs e)
        {
            Employee em = (Employee)dataGridEmployees.SelectedItem;
            txtEmpId.Text = em.EmpId.ToString();
            txtEmpName.Text = em.Name;
            txtDept.Text = em.Dept;
            dtJoiningDate.Text = em.JoiningDate.ToShortDateString();
            txtCL.Text = em.CL.ToString();
            txtML.Text = em.ML.ToString();
            txtEL.Text = em.EL.ToString();
            txtLWP.Text = em.LWP.ToString();
        }

        private void btnHolidaysAdd_Click(object sender, RoutedEventArgs e)
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            Holiday h = new Holiday();
            h.Date = (DateTime)calHolidays.SelectedDate;
            h.Comment = txtHolidayComment.Text;
            context.Holidays.Add(h);
            context.SaveChanges();
            RefreshDataGrids();
        }

        private void btnHolidaysDel_Click(object sender, RoutedEventArgs e)
        {
            Holiday h = (Holiday)dataGridHolidays.SelectedItem;
            MEAPSDbEntities context = new MEAPSDbEntities();
            Holiday deletable = context.Holidays.SingleOrDefault(x => x.Date == h.Date);
            context.Holidays.Remove(deletable);
            context.SaveChanges();
            RefreshDataGrids();
        }

        private void btnAEAdd_Click(object sender, RoutedEventArgs e)
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            ExceptionAttendance ae = new ExceptionAttendance();
            ComboBoxItem EmpId = (ComboBoxItem)cbAEEmp.SelectedItem;
            ae.EmpId = Convert.ToInt32(EmpId.Content);
            ae.Date = Convert.ToDateTime(dtAEDate.Text);
            ae.Remarks = txtAERemarks.Text;
            context.ExceptionAttendances.Add(ae);
            context.SaveChanges();
            RefreshDataGrids();
        }

        private void btnAEDel_Click(object sender, RoutedEventArgs e)
        {
            ExceptionAttendance ae = (ExceptionAttendance)dataGridExcepHolidays.SelectedItem;
            MEAPSDbEntities context = new MEAPSDbEntities();
            ExceptionAttendance deletable = context.ExceptionAttendances.SingleOrDefault(x => ((x.EmpId == ae.EmpId) && (x.Date == ae.Date)));
            context.ExceptionAttendances.Remove(deletable);
            context.SaveChanges();
            RefreshDataGrids();
        }

        private void btnTEAdd_Click(object sender, RoutedEventArgs e)
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            TimeException te = new TimeException();
            te.Date = Convert.ToDateTime(dtTEDate.Text);
            string entryTime = cbInHrs.Text + ":" + cbInMins.Text;
            string exitTime = cbOutHrs.Text + ":" + cbOutMins.Text;
            te.InTime = TimeSpan.Parse(entryTime);
            te.OutTime = TimeSpan.Parse(exitTime);
            context.TimeExceptions.Add(te);
            context.SaveChanges();
            RefreshDataGrids();
        }

        private void btnTEDel_Click(object sender, RoutedEventArgs e)
        {
            TimeException te = (TimeException)dataGridExcepTime.SelectedItem;
            MEAPSDbEntities context = new MEAPSDbEntities();
            TimeException deletable = context.TimeExceptions.SingleOrDefault(x => x.Date == te.Date);
            context.TimeExceptions.Remove(deletable);
            context.SaveChanges();
            RefreshDataGrids();
        }

        private void btnLEAdd_Click(object sender, RoutedEventArgs e)
        {
            LeaveApplication la = new LeaveApplication();
            la.Date = Convert.ToDateTime(dtLEDate.Text);
            la.EmployeeEmpId = Int32.Parse(cbLEEmp.Text);
            la.Type = cbLeaveType.Text;
            la.Remarks = txtLERemarks.Text;
            MEAPSDbEntities context = new MEAPSDbEntities();
            context.LeaveApplications.Add(la);
            context.SaveChanges();
            RefreshDataGrids();
        }

        private void btnLEDel_Click(object sender, RoutedEventArgs e)
        {
            LeaveApplication la = (LeaveApplication)dataGridLeaveApp.SelectedItem;
            MEAPSDbEntities context = new MEAPSDbEntities();
            LeaveApplication deletable = context.LeaveApplications.SingleOrDefault(x => x.Date == la.Date);
            context.LeaveApplications.Remove(deletable);
            context.SaveChanges();
            RefreshDataGrids();
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            DateTime StartDate = Convert.ToDateTime(dtRangeStart.Text);
            DateTime EndDate = Convert.ToDateTime(dtRangeEnd.Text);
            MEAPS.BusinessLogic.BulkAttendanceProcessor.calculate(StartDate, EndDate);
            RefreshDataGrids();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            TabCtrl t = context.TabCtrls.First();
            MessageBoxResult result = MessageBox.Show("Save position?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                t.selectedIndex = tabCtrlMain.SelectedIndex.ToString();
            }
            else
            {
                t.selectedIndex = "0";
            }
            context.SaveChanges();
            Application.Current.Shutdown();
        }

        private void btnLeaveStatTruncate_Click(object sender, RoutedEventArgs e)
        {
            MEAPSDbEntities context = new MEAPSDbEntities();
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE[LeaveStatuses]");
            context.SaveChanges();
            RefreshDataGrids();
        }

        private void btnLeaveStatMonthlyRefresh_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridLeaveStat.Items.Count <= 1)
            {
                MessageBoxResult alertbox = MessageBox.Show("Please generate daily attendance reports first.", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                int date = Int32.Parse(cbLeaveStatMonthlyYear.SelectedItem.ToString());
                dataGridLeaveStatMonthly.ItemsSource = BusinessLogic.MonthlyAttendanceHandler.Calculate(date);
            }

        }

        private void btnShowMessage_Click(object sender, RoutedEventArgs e)
        {
            int c = 0;
            if (rdChoice1.IsChecked == true)
            {
                c = 1;
            }
            if (rdChoice2.IsChecked == true)
            {
                c = 2;
            }

            int EmpId = Int32.Parse(cbReportEmp.Text);
            MEAPSDbEntities context = new MEAPSDbEntities();
            Employee em = context.Employees.SingleOrDefault(x => x.EmpId == EmpId);

            switch (c)
            {
                case 1:                    
                    string result = "Employee Id: " + em.EmpId + "\nEmployee Name: " + em.Name + "\nRemaining CL: " + em.CL + "\nRemaining ML: " + em.ML + "\nRemaining EL: " + em.EL + "\nLeaves without Pay: " + em.LWP;
                    MessageBoxResult res = MessageBox.Show(result, "Result", MessageBoxButton.OK, MessageBoxImage.Information); 
                    break;

                case 2:
                    double sumCL = 0, sumML = 0, sumEL = 0, sumLWP = 0;
                    if (dataGridLeaveStat.Items.Count <= 1)
                    {
                        MessageBoxResult alertbox = MessageBox.Show("Please generate daily attendance reports first.", "Warning", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        List<LeaveStatus> ls_emp = (from x in context.LeaveStatuses where x.EmployeeEmpId == em.EmpId select x).ToList();
                        if (cbReportMonths.SelectedIndex == 0)
                        {                            
                            List<LeaveStatus> ls_emp_mon = (from x in ls_emp where x.Date.Year == int.Parse(cbReportYears.Text) select x).ToList();
                            sumCL = ls_emp_mon.Sum(x => ((x.FCL ?? 0) + (0.5 * (x.HCL ?? 0)) + (0.25 * ((x.QCL_In ?? 0) + (x.QCL_Out ?? 0)))));
                            sumML = ls_emp_mon.Sum(x => ((x.FML ?? 0) + (0.5 * (x.HML ?? 0))));
                            sumEL = ls_emp_mon.Sum(x => (x.EL ?? 0));
                            sumLWP = ls_emp_mon.Sum(x => x.LWP);
                            string result2 = "Employee Id: " + em.EmpId + "\nEmployee Name: " + em.Name + "\nTotal CL: " + sumCL + "\nTotal ML: " + sumML + "\nTotal EL: " + sumEL + "\nTotal Leaves without Pay: " + sumLWP;
                            MessageBoxResult res2 = MessageBox.Show(result2, "Result", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            int month = cbReportMonths.SelectedIndex;
                            List<LeaveStatus> ls_emp_mon = (from x in ls_emp where x.Date.Month == month && x.Date.Year == int.Parse(cbReportYears.Text) select x).ToList();
                            sumCL = ls_emp_mon.Sum(x => ((x.FCL ?? 0) + (0.5 * (x.HCL ?? 0)) + (0.25 * ((x.QCL_In ?? 0) + (x.QCL_Out ?? 0)))));
                            sumML = ls_emp_mon.Sum(x => ((x.FML ?? 0) + (0.5 * (x.HML ?? 0))));
                            sumEL = ls_emp_mon.Sum(x => (x.EL ?? 0));
                            sumLWP = ls_emp_mon.Sum(x => x.LWP);
                            string result2 = "Employee Id: " + em.EmpId + "\nEmployee Name: " + em.Name + "\nTotal CL: " + sumCL + "\nTotal ML: " + sumML + "\nTotal EL: " + sumEL + "\nTotal Leaves without Pay: " + sumLWP;
                            MessageBoxResult res2 = MessageBox.Show(result2, "Result", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

        private void chkDev_Click(object sender, RoutedEventArgs e)
        {
            if (chkDev.IsChecked == true)
            {
                btnLaunchViewer.Visibility = Visibility.Visible;
            }
            else
            {
                btnLaunchViewer.Visibility = Visibility.Hidden;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (tabCtrlMain.SelectedIndex < maxTabIndex)
            {
                tabCtrlMain.SelectedIndex++;
            }
        }
    }
}
