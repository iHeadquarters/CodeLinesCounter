using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace CodeLinesCounter
{
    class CountThread
    {
        public CountThread(string dragPath, Queue<FileSystemInfo> strQueue)
        {
            // TODO: Complete member initialization
            this.dragPath = dragPath;
            this.strQueue = strQueue;
        }

        /// <summary>
        /// count lines of directory or file with a suffix of .vb
        /// </summary>
        /// <returns></returns>
        private int countLines()
        {
            //if this is a file
            if (File.Exists(dragPath))
            {
                //MessageBox.Show(new FileInfo(dragPath).FullName);
                strQueue.Enqueue(new FileInfo(dragPath));
            }
            else if (Directory.Exists(dragPath))//if this is a directory
            {
                strQueue.Enqueue(new DirectoryInfo(dragPath));
            }
            else
            {
                new MessageDancer("countLines() ").showInformation("Please drag file or directory!");
                return 0;
            }

            lineNumbers = 0;
            FileSystemInfo tmp = null;

            while (strQueue.Count > 0)
            {
                tmp = strQueue.Dequeue();
                if (tmp is FileInfo)
                {
                    if (tmp.FullName.EndsWith(".vb"))
                    {
                        lineNumbers += getLines((FileInfo)tmp);
                    }
                }
                else if (tmp is DirectoryInfo)
                {
                    FileSystemInfo[] fileSysInfo = ((DirectoryInfo)tmp).GetFileSystemInfos();
                    foreach (FileSystemInfo f in fileSysInfo)
                    {
                        if (f is FileInfo)
                        {
                            if (f.FullName.EndsWith(".vb"))
                            {
                                strQueue.Enqueue((FileInfo)f);
                            }
                        }
                        else
                        {
                            strQueue.Enqueue((DirectoryInfo)f);
                        }
                    }

                }
            }

            return lineNumbers;

        }

        /// <summary>
        /// get lines of no-empty file
        /// </summary>
        /// <param name="fileInfo"></param>
        /// <returns></returns>
        private int getLines(FileInfo fileInfo)
        {
            int lines = 0;
            FileStream fileStream = null;
            StreamReader streamReader = null;
            String txtLine = null;
            //file not exist
            if (!File.Exists(fileInfo.FullName))
            {
                return 0;
            }

            try
            {
                fileStream = new FileStream(fileInfo.FullName, FileMode.Open);
                streamReader = new StreamReader(fileStream);

                while (!(streamReader.EndOfStream))
                {
                    txtLine = streamReader.ReadLine();
                    lines++;
                }
            }
            catch (Exception e)
            {
                new MessageDancer("getLines").showException(e);
            }
            finally
            {
                streamReader.Close();
                fileStream.Close();
            }

            return lines;
        }

        #region 'make this class be a Thread'
        Thread thread = null;
        public void run()
        {
            new MessageDancer("").showInformation("All code selected are " + countLines().ToString() + " lines.");
        }

        public void start()
        {
            if (thread == null)
                thread = new Thread(run);
            thread.Start();
        }
        #endregion  

        private int lineNumbers;
        private String dragPath=null;
        private Queue<FileSystemInfo> strQueue=new Queue<FileSystemInfo>();
    }
}
