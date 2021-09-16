using CrossCutting.ApiModel;
using System;

namespace FileManger.Utils.Interface
{
    public interface ISendEmails
    {
        public Boolean SendEmailConfig(EmailAM emailData);
    }
}
