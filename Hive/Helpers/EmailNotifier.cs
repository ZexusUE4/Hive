using Hive.DAL;
using Hive.Models;
using System;
using System.Net.Mail;

namespace Hive.Helpers
{
    public class EmailNotifier
    {
        private static string serverHostName = "http://localhost:7777";

        private static SmtpClient SmtpClient = new SmtpClient("smtp.gmail.com", 587)
        {
            Credentials = new System.Net.NetworkCredential("come.reply.to.me@gmail.com", "password123456"),
            EnableSsl = true,
        };

        private static HiveContext db = new HiveContext();

        public static void SendWelcomeMessage(string id)
        {
            HiveUser user = db.Users.Find(id);

            MailMessage message = new MailMessage()
            {
                Subject = "Hive | Welcome " + user.FullName,
                Body = "Dear, " + user.FirstName + "\nWe are very happy to have you on board, start managing your projects now !!",
                From = new MailAddress("come.reply.to.me@gmail.com"),
            };

            message.To.Add(new MailAddress(user.Email));

            SendEmail(message);
        }

        public static void SendTeamInvitation(int teamID, string email)
        {
            Team team = db.Teams.Find(teamID);

            string url = serverHostName + "/Team/AcceptInvitation?teamID=" + teamID.ToString();

            MailMessage message = new MailMessage()
            {
                Subject = "Hive | Team Invitation ",
                Body = "Hello there, "  + "\nYou were invited to join the team:\n" + team.Name +"\nClick here to accept the invitation:\t" + url,
                From = new MailAddress("come.reply.to.me@gmail.com"),
            };

            message.To.Add(new MailAddress(email));

            SendEmail(message);
        }

        private static bool SendEmail(MailMessage message)
        {
            try
            {
                SmtpClient.Send(message);
                return true;
            }
            catch (Exception ex)
            {
                string expMessage = ex.Message;
                return false;
            }
        }
    }
}