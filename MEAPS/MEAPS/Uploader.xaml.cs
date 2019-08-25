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
using System.IO;
using Microsoft.Win32;
using System.Data;

namespace MEAPS
{
    /// <summary>
    /// Interaction logic for Uploader.xaml
    /// </summary>
    public partial class Uploader : Window
    {

        private void LogString(string logdata)
        {
            lblMsg.Content = lblMsg.Content + "\n" + logdata;
        }

        public Uploader()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dialog = new OpenFileDialog();
            dialog.DefaultExt = ".csv";
            dialog.Filter = "Comma Seperated Values (.csv)|*.csv";
            Nullable<bool> result = dialog.ShowDialog();

            if (result==true)
            {
                string filename = dialog.FileName;
                txtPath.Text = filename;
            }

            LogString("File path loaded.");
            LogString(dialog.FileName);
        }


        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            string filepath = txtPath.Text;
            DataTable csvFileData = new DataTable();
            if (chkClear.IsChecked == true)
            {
                MEAPSDbEntities context = new MEAPSDbEntities();
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [CSVDatas]");
                LogString("Cleared old records.");
            }
            LogString("Converting CSV to DataTable...");
            try
            {
                 csvFileData = CsvToDb.CsvToDataTable.ConvertCsvtoDataTable(filepath);
            }
            catch (Exception ex)
            {
                LogString(ex.Message);
            }

            LogString("Inserting DataTable to Database...");
            try
            {
                CsvToDb.DataTabletoDB.InsertDataIntoSQLServerUsingSQLBulkCopy(csvFileData);
            }
            catch (Exception ex)
            {
                LogString(ex.Message);
            }
            LogString("Done!");
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ((MainWindow)Application.Current.MainWindow).btnLaunchUploader.IsEnabled = true;
        }

        private void btnClean_Click(object sender, RoutedEventArgs e)
        {
            MEAPS.BusinessLogic.CsvDataCleaner.Clean();
        }
    }
}
