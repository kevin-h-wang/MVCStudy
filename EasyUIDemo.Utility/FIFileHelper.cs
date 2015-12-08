using System;
using System.IO;
using System.Text;
using System.Web;

namespace EasyUIDemo.Utility
{
    public static class FIFileHelper
    {
        #region 下载附件

        /// <summary>
        ///     下载附件
        /// </summary>
        /// <param name="filepath">包含完整路径</param>
        /// <param name="filename">文件名</param>
        public static void Download(String filepath, String filename)
        {
            using (var fileStream = new FileStream(filepath, FileMode.Open))
            {
                long fileSize = fileStream.Length;
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.AddHeader("content-disposition",
                    String.Format("attachment;filename={0}", HttpContext.Current.Server.UrlEncode(filename)));
                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("utf-8");
                HttpContext.Current.Response.ContentType = ContentType.excel;
                HttpContext.Current.Response.AddHeader("content-length", fileSize.ToString());
                //page.EnableViewState = false;
                var fileBuffer = new byte[fileSize];
                fileStream.Read(fileBuffer, 0, (int)fileSize);
                HttpContext.Current.Response.BinaryWrite(fileBuffer);
                HttpContext.Current.Response.End();
            }
        }

        private class ContentType
        {
            /// <summary>
            ///     excel
            /// </summary>
            public const string excel = "application/ms-excel";
        }

        #endregion

        #region Upload

        public const string FileType =
            ".gif,.jpg,.png,.bmp,.jpeg,.xls,.xlsx,.doc,.docx,.pdf,.pptx,.ppt,.rar,.zip,.txt,.msg,.tfi";

        public const int FileBtyesLength = 10485760;

        /// <summary>
        ///     检查文件大小
        /// </summary>
        /// <param name="postedFile">文件</param>
        /// <param name="fileLength">文件长度</param>
        /// <returns>bool值</returns>
        public static bool CheckFileSize(HttpPostedFile postedFile, int fileLength = 10485760)
        {
            string msg = string.Empty;
            if (!string.IsNullOrEmpty(postedFile.FileName) && (postedFile.ContentLength > fileLength))
            {
                //msg = Message.Common.SizeOverFlow;
                return false;
            }
            return true;
        }

        /// <summary>
        ///     获取文件名称
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>FullFileName</returns>
        public static string PathToName(string path)
        {
            return Path.GetFileName(path);
        }


        /// <summary>
        ///     上传附件
        /// </summary>
        /// <param name="postedFile">fileupload控件</param>
        /// <param name="msg">提示信息</param>
        /// <param name="guid">文件重命名guid</param>
        /// <param name="savePath">文件保存路径</param>
        /// <param name="fileLength">文件大小限制(4M:4194304)</param>
        /// <param name="filesType">文件类型限制，默认".gif,.jpg,.png,.xls,.xlsx,.doc,.docx,.pdf"</param>
        /// <returns></returns>
        public static FIReturnInfo UploadFile(HttpPostedFile postedFile, out string msg, out string guid,
            out string savePath,
            string filesType = FileType, int fileLength = FileBtyesLength)
        {
            msg = string.Empty;
            guid = Guid.NewGuid().ToString();
            savePath = string.Empty;
            string[] strArray = filesType.ToLower().Split(',');
            if (!string.IsNullOrEmpty(postedFile.FileName))
            {
                //文件类型,带".",如:.jpg,.doc
                string extension = Path.GetExtension(postedFile.FileName).ToLower();
                if (strArray.Length != 0)
                {
                    if (!Array.Exists(strArray, element => element.Equals(extension)))
                    {
                        string error = string.Join("/", strArray);
                        var returnInfo = new FIReturnInfo(false, "只允许上传" + error + "的文件");
                        return returnInfo;
                    }
                }
                if (postedFile.ContentLength > fileLength)
                {
                    //msg = Message.Common.SizeOverFlow + fileLength + "KB(" + fileLength/(1024*1024) + "M)";
                    var returnInfo = new FIReturnInfo(false, "上传文件大小过大，允许上传的文件最大为" + FileBtyesLength / (1024 * 1024) + "M");
                    return returnInfo;
                }

                savePath = savePath + "\\" + DateTime.Now.ToString("yyyyMM");
                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);
                savePath = string.Format("{0}\\{1}_{2}{3}", savePath,
                    Path.GetFileNameWithoutExtension(postedFile.FileName), guid, extension);
                //postedFile.SaveAs(savePath);
                FileStream stream = new FileStream(savePath, FileMode.Create);

                int length = 1024;
                byte[] b = new byte[length];
                try
                {
                    int size = postedFile.InputStream.Read(b, 0, length);

                    while (size > 0)
                    {
                        stream.Write(b, 0, size);
                        size = postedFile.InputStream.Read(b, 0, length);
                    }
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Flush();
                        stream.Close();
                    }
                }

                var fiReturnInfo = new FIReturnInfo(true);
                return fiReturnInfo;
            }
            return new FIReturnInfo(false, "没有选择上传文件");
        }

        #endregion

        #region IO控制

        /// <summary>
        /// 判断一个路径是否是绝对路径
        /// </summary>
        /// <param name="pathStr"></param>
        /// <returns></returns>
        public static bool IsAbsolutePath(this string pathStr)
        {
            pathStr = pathStr.ToUpper();
            string diskStr = "ABCDEFGHIJK";  //ABC等磁盘
            if (pathStr.Length >= 4)   //C:\File  长度至少要有4个
            {
                //.取第一个，看是否是盘符
                string fristChar = pathStr.Substring(0, 1);
                if (diskStr.IndexOf(fristChar) >= 0)
                {
                    //..然后再看后面是不是 :\ 这样的
                    string lastStr = pathStr.Substring(1, 2);
                    if (lastStr == ":\\")
                    {
                        return true;
                    }
                }
            }
            //增加如果是 共享文件夹时
            if (pathStr.StartsWith("\\\\"))
            {
                return true;
            }
            return false;
        }

        #endregion
    }
}