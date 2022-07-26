﻿using NotesMarketplace.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace NotesMarketplace.EmailTemplates
{
    public class SellerPublishedNoteEmail
    {
        public static void SellerPublishedNoteNotifyEmail(string supportEmail, string emailPassword, User sellerUser, string NoteTitle, string emails)
        {
            foreach (string email in emails.Split(','))
            {

                var fromEmail = new MailAddress(supportEmail, "Notes Marketplace"); //need system email
                var toEmail = new MailAddress(email);
                var fromEmailPassword = emailPassword; // Replace with actual password
                string subject = "" + sellerUser.FirstName + " " + sellerUser.LastName + " - sent his note for review";
                string body = "Hello Admins,";
                body += "<br/>We would like to inform you that,<b> " + sellerUser.FirstName + " " + sellerUser.LastName + "</b>" +
                    " sent his note <b> " + NoteTitle + "</b>" + " for review.Please look at the notes and take required actions.";
                body += "<br/><br/>Regards,<br/>";
                body += "Notes Marketplace";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromEmail.Address, fromEmailPassword)
                };

                using (var message = new MailMessage(fromEmail, toEmail)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                    smtp.Send(message);
            }
        }
    }
}