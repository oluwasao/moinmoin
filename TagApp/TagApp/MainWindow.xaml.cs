using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TagApp.Entity;
using TagApp.Service;
using System.Net.Mail;

namespace TagApp
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
            catch(Exception ex)
            {
                SendException(ex);
            }
        }

        public void SendException(Exception ex)
        {
            string AddressFrom = "lekibaba@gmail.com";
            string NameFrom = "TagApp";
            string AddressTo = "lekibaba@gmail.com";
            string NameTo = "Leke";
            MailAddress objFrom = new MailAddress(AddressFrom, NameFrom);
            MailAddress objTo = new MailAddress(AddressTo, NameTo);
            MailMessage objMessage = new MailMessage(objFrom, objTo);
            objMessage.Subject = "Tag App";

            StringBuilder builder = new StringBuilder();
            builder.AppendLine(ex.Message);
            builder.AppendLine(ex.StackTrace);
            if (ex.InnerException != null)
            {
                builder.AppendLine(ex.InnerException.Message);
                builder.AppendLine(ex.InnerException.StackTrace);
            }


            objMessage.Body = builder.ToString();

            SmtpClient objSmtpClient = new SmtpClient("smtp.gmail.com");
            objSmtpClient.Credentials = new System.Net.NetworkCredential("lekibaba@gmail.com", "redrumjordan23");
            objSmtpClient.EnableSsl = true;

            objSmtpClient.Send(objMessage);

        }

        public static readonly string CafePressFilePath = string.Format("{0}\\TagApp\\CafePressTags.csv", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        public static readonly string ZazzleFilePath = string.Format("{0}\\TagApp\\ZazzleTags.csv", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        public static readonly string KeyWordFolderPath = string.Format("{0}\\TagApp", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
        string categoryFolder = Environment.GetEnvironmentVariable("PROGRAMFILES(X86)") ?? Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
        private void LoadDropDowns()
        {

            
            string categoryPath = string.Format("{0}\\TagApp\\Category.xml",
               categoryFolder.Substring(0, categoryFolder.LastIndexOf("s") + 1));

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
                SendException(ex);
            }

        }

        private void abutton1_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                string categoryPath = string.Format("{0}\\TagApp\\Category.xml",
                   categoryFolder.Substring(0, categoryFolder.LastIndexOf("s") + 1));

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

                    Service.KeyWordLineWriter cafePressWriter = new KeyWordLineWriter(CafePressFilePath,

                                        new List<Entry>() { entry }, KeyWordFolderPath);

                    Service.KeyWordLineWriter zazzleWriter = new KeyWordLineWriter(ZazzleFilePath,

                                        new List<Entry>() { entry }, KeyWordFolderPath);

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
                SendException(ex);
            }

        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                string categoryPath = string.Format("{0}\\TagApp\\Category.xml",
                   categoryFolder.Substring(0, categoryFolder.LastIndexOf("s") + 1));

                lblStatus.Content = "";
                if (!string.IsNullOrEmpty(txtKey.Text))
                {

                    Service.KeyWordLineWriter cafePressWriter = new KeyWordLineWriter(CafePressFilePath, null, KeyWordFolderPath);

                    Service.KeyWordLineWriter zazzleWriter = new KeyWordLineWriter(ZazzleFilePath, null, KeyWordFolderPath);

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
                SendException(ex);
            }

        }
    }
}
