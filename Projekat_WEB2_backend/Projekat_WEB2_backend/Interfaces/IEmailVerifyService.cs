﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projekat_WEB2_backend.Interfaces
{
    public interface IEmailVerifyService
    {
        void SendVerificationMail(string prodavacMail, string statusVerifikacije);
    }
}
