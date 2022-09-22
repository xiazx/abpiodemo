using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Abp.Demo.Shared.Utils
{
    public class SendEmailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        public static async Task SendEmailAsync(SendEmailInput input)
        {
            var from = new MailAddress(input.SenderEmailAddress, input.SenderName);
            var to = new MailAddress(input.ReceiverEmailAddress, input.ReceiverName);
            var oMail = new MailMessage(from, to);
            oMail.Subject = input.Subject; //邮件标题 
            oMail.Body = input.Content; //邮件内容
            oMail.IsBodyHtml = true; //指定邮件格式,支持HTML格式 
            oMail.BodyEncoding = Encoding.UTF8;
            oMail.Priority = MailPriority.Normal;//设置邮件的优先级为高

            //发送邮件服务器
            var client = new SmtpClient();
            client.Host = input.EmailServer; //指定邮件服务器
            client.EnableSsl = false;
            client.Port = 587;
            client.Credentials = new NetworkCredential(input.SenderEmailAddress, input.SenderEmailPwd);//指定服务器邮件,及密码
            await client.SendMailAsync(oMail); //发送邮件
            oMail.Dispose(); //释放资源
        }
    }
    public class SendEmailInput
    {
        /// <summary>
        /// 邮箱服务器
        /// </summary>
        public string EmailServer { get; set; }

        /// <summary>
        /// 发送者邮箱地址
        /// </summary>
        public string SenderEmailAddress { get; set; }

        /// <summary>
        /// 发送者邮箱密码
        /// </summary>
        public string SenderEmailPwd { get; set; }

        /// <summary>
        /// 发送者姓名
        /// </summary>
        public string SenderName { get; set; }

        /// <summary>
        /// 接收者邮箱
        /// </summary>
        public string ReceiverEmailAddress { get; set; }

        /// <summary>
        /// 接收者姓名
        /// </summary>
        public string ReceiverName { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

    }
}
