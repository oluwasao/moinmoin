using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using TagApp.Entity;

namespace TagApp.Service
{
    public class KeyWordLineWriter
    {
        private List<Entry> _entries;
        public List<Entry> Entries { get { return _entries; } }
        private string _filePath;
        private string _folderPath;
        public KeyWordLineWriter(string path, List<Entry> entries, string folderPath)
        {
            _entries = entries;
            _filePath = path;
            _folderPath = folderPath;
        }

        //public string ReturnZazzleFormat()
        //{
        //    StringBuilder  csvBuilder = new StringBuilder();    
        //    foreach (Entry entry in _entries)
        //    {
        //        int count = 1;
        //        foreach (Tag tag in entry.Tags)
        //        {
        //            if (count < entry.Tags.Count)
        //            {
        //                csvBuilder.AppendFormat("\"{0} {1}\" ", tag.KeyWord.KeyWord, tag.Category.Name);
        //                csvBuilder.AppendFormat("\"{0} {1}\" ", tag.KeyWord.Adjective, tag.Category.Name);
        //            }
        //            else
        //            {
        //                csvBuilder.AppendFormat("\"{0} {1}\" ", tag.KeyWord.KeyWord, tag.Category.Name);
        //                csvBuilder.AppendFormat("\"{0} {1}\" ", tag.KeyWord.Adjective, tag.Category.Name);
        //            }
        //            count ++;
        //        }
        //        csvBuilder.Append(Environment.NewLine);
        //    }
        //    return csvBuilder.ToString();
        //}

        public static string ReturnZazzleFormat(string text)
        {
            StringBuilder csvBuilder = new StringBuilder();
            string[] entryArray = text.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var entry in entryArray)
            {
                csvBuilder.AppendFormat("\"{0}\" ", entry);                             
            }
            csvBuilder.Append(Environment.NewLine);
            return csvBuilder.ToString();
        }

        public static string ReturnCafePressFormat(string text)
        {
            StringBuilder csvBuilder = new StringBuilder();
            string[] entryArray = text.Split(",".ToArray(), StringSplitOptions.RemoveEmptyEntries);

            foreach (var entry in entryArray)
            {
                if (entryArray.ToList().Last() == entry)
                    csvBuilder.AppendFormat("{0} ", entry);
                else
                    csvBuilder.AppendFormat("{0}, ", entry);
            }
            csvBuilder.Append(Environment.NewLine);
            return csvBuilder.ToString();
        }
        //public string ReturnCafePressFormat()
        //{
        //    StringBuilder csvBuilder = new StringBuilder();
        //    foreach (Entry entry in _entries)
        //    {
        //        int count = 1;
        //        foreach (Tag tag in entry.Tags)
        //        {
        //            if (count < entry.Tags.Count)
        //            {
        //                csvBuilder.AppendFormat("{0} {1}, ", tag.KeyWord.KeyWord, tag.Category.Name);
        //                csvBuilder.AppendFormat("{0} {1}, ", tag.KeyWord.Adjective, tag.Category.Name);
        //            }
        //            else
        //            {
        //                csvBuilder.AppendFormat("{0} {1}, ", tag.KeyWord.KeyWord, tag.Category.Name);
        //                csvBuilder.AppendFormat("{0} {1} ", tag.KeyWord.Adjective, tag.Category.Name);
        //            }
        //            count++;
        //        }
        //        csvBuilder.Append(Environment.NewLine);
        //    }
        //    return csvBuilder.ToString();
        //}

    
        public void WriteToCsv(string text)
        {         
            using (StreamWriter swOutputFile = new StreamWriter(new FileStream(_filePath, FileMode.Append, FileAccess.Write, FileShare.Read)))
            {
                swOutputFile.WriteLine(text);
                swOutputFile.Flush();
            }
        }
    }
}
