using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AppServices;
using Repositories;
using Common;
using System.Net.Mail;
using System.Net;

namespace DeliverySystem.Controllers
{

    public class ClientController : ApiBaseController
    {
        IClientService clientService;
        public ClientController(IClientService clientService)
        {
            this.clientService = clientService;

        }


        //מחזיר רשימת כל הלקוחות
        [HttpGet("getClientList")]
        public List<ClientViewModel> GetAll()
        {
            return clientService.GetList();
        }

        //[HttpGet("SendEmail")]
        //public string sendMail()
        //{
        //    //try
        //    //{
        //        var senderEmail = new MailAddress("hgproject2021@gmail.com");
        //        var recieverEmail = new MailAddress("y0549365861@gmail.com", "Michal Chaumi");
        //        var password = "hodayagi";
        //        var sub = "hkjk";
        //        var body = "lkl";
        //        var smtp = new SmtpClient
        //        {

        //            Host = "smtp.gmail.com",
        //            Port = 587,
        //            EnableSsl = true,
        //            DeliveryMethod = SmtpDeliveryMethod.Network,
        //            UseDefaultCredentials = true,
        //            Credentials = new NetworkCredential(senderEmail.Address, password)
        //        };
        //        using (var mess = new MailMessage(senderEmail, recieverEmail)
        //        {
        //            IsBodyHtml = true,
        //            Subject = sub,
        //            Body = "<div style = 'font-family:font-family:Segoe UI Light, Helvetica, sans-serif'> Dear   : " + "משרשרת שם כל שהוא " + "  To View your request click <br /> <br /><br /><br />Thank you <br />F28 IS Team </div>"

        //        })
        //        {
        //            smtp.Send(mess);
        //        }
        //        return "ok";
        //   // }
        //  //  catch (Exception ex)
        //   // {

        //   // }
        //   // return "ERROR";


    //} }
    }
}
