using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using System.IO;

namespace ATMO.mko.Logging.Attr
{
    /// <summary>
    /// mko, 28.6.2018
    /// Writes Attributes to a File. If file exists, attributes will be appended. If not, 
    /// a new file will be created and attributes appended.
    /// </summary>
    public class AttrFileWriter : IAttrFileWriter
    {
        StreamWriter csvWriter;
        bool removeExistingFile;

        string Separator;
        bool append = false;

        public AttrFileWriter(string filePath, string Separator="=", bool removeExistingFile = false)
        {
            this.removeExistingFile = removeExistingFile;
            this.Separator = Separator;

            try
            {
                if (File.Exists(filePath) && (removeExistingFile))
                {
                    File.Delete(filePath);
                }

                var dirpath = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(dirpath) && !Directory.Exists(dirpath))
                {
                    Directory.CreateDirectory(dirpath);
                }

                append = File.Exists(filePath);
                csvWriter = new StreamWriter(filePath, append, Encoding.Default);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Dispose()
        {
            csvWriter.Flush();
            csvWriter.Close();
        }


        public void Write(string AttributeName, string AttributeValue)
        {
            try
            {
                csvWriter.WriteLine($"{AttributeName}{Separator}{AttributeValue}");             
            }
            catch (Exception ex)
            {
                TraceHlp.ThrowArgEx($"Write {AttributeName}{Separator}{AttributeValue}", ex);
            }         
        }

        public void Write(string AttributeName, string Separator, string AttributeValue)
        {
            try
            {
                csvWriter.WriteLine($"{AttributeName}{Separator}{AttributeValue}");         
            }
            catch (Exception ex)
            {
                TraceHlp.ThrowArgEx($"Write {AttributeName}{Separator}{AttributeValue}", ex);
            }            
        }
    }
}
