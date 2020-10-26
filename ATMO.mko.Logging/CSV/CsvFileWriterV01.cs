using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using System.IO;

namespace ATMO.mko.Logging.CSV
{
    /// <summary>
    /// mko, 28.6.2018
    /// </summary>
    public class CsvFileWriterV01 : ICsvWriter
    {
        StreamWriter csvWriter;
        bool removeExistingFile;

        bool append = false;

        public CsvFileWriterV01(string filePath, bool removeExistingFile = false)
        {
            this.removeExistingFile = removeExistingFile;

            try
            {
                if (File.Exists(filePath) && (removeExistingFile))
                {
                    File.Delete(filePath);
                }

                var dirpath = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(dirpath))
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

        public IRCV2 WriteLine(char Separator, params string[] fields)
        {
            var ret = RCV2.Failed();
            try
            {
                csvWriter.WriteLine(string.Join(Separator.ToString(), fields));
                ret = RCV2.Ok();
            }
            catch (Exception ex)
            {
                ret = RCV2.Failed(ex);
            }
            return ret;
        }

        public IRCV2 WriteLine(params string[] fields)
        {
            var ret = RCV2.Failed();
            try
            {
                csvWriter.WriteLine(string.Join(",", fields));
                ret = RCV2.Ok();
            }
            catch (Exception ex)
            {
                ret = RCV2.Failed(ex);
            }
            return ret;

        }

        public IRCV2 WriteLine(char Separator, char NewLine, params string[] fields)
        {
            var ret = RCV2.Failed();
            try
            {
                csvWriter.Write($"{string.Join(Separator.ToString(), fields)}{NewLine}");
                ret = RCV2.Ok();
            }
            catch (Exception ex)
            {
                ret = RCV2.Failed(ex);
            }
            return ret;
        }
    }
}
