﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Payloads
{
    public class RegistrationPayload
    {
        public string DisplayName {  get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
