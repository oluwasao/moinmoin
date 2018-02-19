using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
using TagApp2._0.Entity;
using TagApp2._0.Service;

namespace TagApp2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                LoadDropDowns();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        

        public static readonly string CafePressFilePath = string.Format("{0}\\TagApp\\CafePressTags.csv", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        public static readonly string ZazzleFilePath = string.Format("{0}\\TagApp\\ZazzleTags.csv", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        public static readonly string KeyWordFolderPath = string.Format("{0}\\TagApp", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        string categoryFolder = string.Format("{0}\\TagApp\\settings", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        private void LoadDropDowns()
        {


            string categoryPath = string.Format("{0}\\Category.xml",
               categoryFolder);

            List<Category> categories = Category.LoadCategories(categoryPath);
            foreach (var category in categories)
                cbxPhrases.Items.Add(category.Name.Trim());
        }

        /// <summary>
        /// Loads the text box
        /// </summary>
        private void LoadTextBox(object sender, RoutedEventArgs e)
        {
            try
            {
                lblStatus.Content = "";
                if (!string.IsNullOrEmpty(txtKey.Text))
                {

                    if (string.IsNullOrEmpty(txtEntry.Text))
                        txtEntry.Text += txtKey.Text + " " + cbxPhrases.SelectedValue;
                    else
                        txtEntry.Text += " , " + txtKey.Text + " " + cbxPhrases.SelectedValue;
                }
            }
            catch (Exception ex)
            {
                lblStatus.Content = ex.Message;
                throw ex;
            }

        }

        private void abutton1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string categoryPath = string.Format("{0}\\Category.xml",
               categoryFolder);

                lblStatus.Content = "";
                if (!string.IsNullOrEmpty(atxtAdjective.Text) || !string.IsNullOrEmpty(atxtKey.Text))
                {
                    if (!Directory.Exists(KeyWordFolderPath))
                    {
                        Directory.CreateDirectory(KeyWordFolderPath);
                        //copy category file there.

                    }
                    List<Category> categories = Category.LoadCategories(categoryPath);
                    List<Tag> tags = new List<Tag>();
                    Keyword key = new Keyword(atxtKey.Text.Trim(), atxtAdjective.Text.Trim());
                    foreach (Category c in categories)
                    {
                        Tag t = new Tag() { Category = c, KeyWord = key };
                        tags.Add(t);
                    }
                    Entry entry = new Entry() { Tags = tags };

                    KeyWordLineWriter cafePressWriter = new KeyWordLineWriter(CafePressFilePath, new List<Entry>() { entry }, KeyWordFolderPath);
                    KeyWordLineWriter zazzleWriter = new KeyWordLineWriter(ZazzleFilePath, new List<Entry>() { entry }, KeyWordFolderPath);

                    //write cafe press
                    string cafePressText = cafePressWriter.ReturnCafePressFormat();
                    cafePressWriter.WriteToCsv(cafePressText);

                    //write zazzle
                    string zazzleText = zazzleWriter.ReturnZazzleFormat();
                    zazzleWriter.WriteToCsv(zazzleText);

                }
                alblStatus.Content += "Done! files created at" + Environment.NewLine;
                alblStatus.Content += CafePressFilePath + Environment.NewLine;
                alblStatus.Content += ZazzleFilePath + Environment.NewLine;
            }
            catch (Exception ex)
            {
                lblStatus.Content = ex.Message;
                throw ex;
            }

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string categoryPath = string.Format("{0}\\Category.xml",
               categoryFolder);

                lblStatus.Content = "";
                if (!string.IsNullOrEmpty(txtKey.Text))
                {

                    KeyWordLineWriter cafePressWriter = new KeyWordLineWriter(CafePressFilePath, null, KeyWordFolderPath);
                    KeyWordLineWriter zazzleWriter = new KeyWordLineWriter(ZazzleFilePath, null, KeyWordFolderPath);

                    //write cafe press
                    string cafePressText = KeyWordLineWriter.ReturnCafePressFormat(txtEntry.Text);
                    cafePressWriter.WriteToCsv(cafePressText);

                    //write zazzle
                    string zazzleText = KeyWordLineWriter.ReturnZazzleFormat(txtEntry.Text);
                    zazzleWriter.WriteToCsv(zazzleText);

                }

                lblStatus.Content += "Done! files created at" + Environment.NewLine;
                lblStatus.Content += CafePressFilePath + Environment.NewLine;
                lblStatus.Content += ZazzleFilePath + Environment.NewLine;
            }
            catch (Exception ex)
            {
                lblStatus.Content = ex.Message;
                throw ex;
            }

        }
    }
}
