using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace AppBoxPro
{
    public class FileOper
    {
        public static void delfile(string filepath)
        {
            try
            {
                if (Directory.Exists(filepath))
                {
                    //获取指定路径下所有文件夹
                    string[] folderPaths = Directory.GetDirectories(filepath);

                    foreach (string folderPath in folderPaths)
                        Directory.Delete(folderPath, true);
                    //获取指定路径下所有文件
                    string[] filePaths = Directory.GetFiles(filepath);

                    foreach (string filePath in filePaths)
                    {
                        FileInfo fi = new FileInfo(filePath);
                        if (fi.CreationTime <= DateTime.Now.AddDays(-1))
                        {
                            File.Delete(filePath);
                        }
                    }
                       
                }
            }
            catch(Exception ee)
            {
               throw new Exception(ee.ToString());
            }
        }

        public static void createFile(string filepath,string content)
        {
            try
            {
                FileStream fs = new FileStream(filepath,FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(content);
                sw.Close();
                fs.Close();

            }
            catch(Exception ee)
            {
                throw new Exception(ee.ToString());
            }
        }
        public static string getFileContent(string filepath)
        {
            StringBuilder htmltext = new StringBuilder();
            try
            {

                using (StreamReader sr = new StreamReader(filepath))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        htmltext.Append(line);
                    }
                    sr.Close();
                }

            }
            catch (Exception ee)
            {
                throw new Exception(ee.ToString());
            }
            return htmltext.ToString();
        }
        public static void writeLog(string content)
        {
            try
            {
                if(System.Configuration.ConfigurationManager.AppSettings["wlog"].ToString().ToLower()=="false")
                {
                    return;
                }
                FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/logs/log.txt"), FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine(content);
                sw.Close();
                fs.Close();

            }
            catch (Exception ee)
            {
                throw new Exception(ee.ToString());
            }
        }
    }
}