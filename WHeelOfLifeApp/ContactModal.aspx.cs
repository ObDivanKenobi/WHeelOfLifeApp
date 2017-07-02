using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

//public partial class ContactModal : System.Web.UI.Page
//{
//    protected void Page_Load(object sender, EventArgs e)
//    {
//        txtName.Attributes.Add("title", "Your name is required.");
//        txtEmail.Attributes.Add("title", "You need an email");
//        txtEmail.Attributes.Add("type", "email");
//        txtMessage.Attributes.Add("title", "Message");
//    }

//    protected void btnSubmit_Click(object sender, EventArgs e)
//    {
//        MailMessage newMail = new MailMessage();
//        newMail.From = new MailAddress(txtEmail.Text, txtName.Text);
//        newMail.To.Add("your@email.com");
//        newMail.Subject = "From l3ny.com";
//        newMail.Body = txtMessage.Text;
//        SmtpClient SmtpSender = new SmtpClient();
//        SmtpSender.Port = 25;
//        SmtpSender.Host = "mail.l3ny.com";
//        SmtpSender.Send(newMail);
//        Response.Redirect("~/");
//    }

//    protected void Reset(object s, EventArgs e)
//    {
//        txtName.Text = "";
//        txtEmail.Text = "";
//        txtMessage.Text = "";
//    }
//}