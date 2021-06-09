
using Spire.Pdf;
using System;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace 打印pdf_文件
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            ConvertPDFToPDF(textBox1.Text);
        }

   
        private static void ConvertPDFToPDF(string url)
        {


            string strUrlFilePath = string.Empty;

          

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);

            request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.2; zh-CN; rv:1.8.1.8) Gecko/20071008 Firefox/2.0.0.8";

            request.Accept = "text/xml,application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5";

            request.AllowAutoRedirect = true;

            request.Headers.Add(HttpRequestHeader.AcceptCharset, "gb2312,utf-8;q=0.7,*;q=0.7");

            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");

            request.Headers.Add(HttpRequestHeader.Cookie, "id58=05dz8VMG5yk8RCViPEsFAg==; city=wz; 58home=wz; ipcity=wz|%u6E29%u5DDE; myfeet_tooltip=end; __utma=253535702.1542940914.1392961328.1392961328.1392961328.1; __utmc=253535702; __utmz=253535702.1392961328.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none)");

            request.Timeout = 30000;

            HttpWebResponse httpresponse = (HttpWebResponse)request.GetResponse();

            Stream stream = httpresponse.GetResponseStream();

            if (stream != null)
            {
                strUrlFilePath = httpresponse.ResponseUri.ToString();//拿到跳转后的地址
            }

            httpresponse.Close();



            WebClient wc = new WebClient();

            String pdf_path = Application.StartupPath + @"\Resources\Invoice_Pdf\JDDQ\2016525\00a0e3c0-92e4-4457-b594-0a1463713c5a.pdf";

            if (!System.IO.Directory.Exists(Application.StartupPath + @"\Resources\Invoice_Pdf\JDDQ\2016525\"))

            {

                System.IO.Directory.CreateDirectory(Application.StartupPath + @"\Resources\Invoice_Pdf\JDDQ\2016525\");//不存在就创建目录 

            }

            var ddddd= wc.DownloadData(strUrlFilePath);


            //创建pdf文档
            using (PdfDocument doc = new PdfDocument()) 
            {
                //加载文件
                doc.LoadFromBytes(ddddd);
                //指示页面是横向打印还是纵向打印
                doc.PrintSettings.Landscape = false;
                //设置打印页范围
                doc.PrintSettings.SelectPageRange(10, 10);
                // 选择一页到一纸布局
                doc.PrintSettings.SelectSinglePageLayout(Spire.Pdf.Print.PdfSinglePageScalingMode.CustomScale, true, 100);
                //设定纸张的页边距，以百分之一英寸为单位
                doc.PrintSettings.SetPaperMargins(10, 10, 10, 10);
                //静默打印PDF文档
                doc.PrintSettings.PrintController = new StandardPrintController();
                //打印文件
                doc.Print();
                //关闭文档
                doc.Close();
            }
                



            
        }
    }
}
