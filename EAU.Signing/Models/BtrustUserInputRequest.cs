using System;
using System.Collections.Generic;
using System.Text;

namespace EAU.Signing.Models
{
    public class BtrustUserInputRequest
    {
        public string Input { get; set; }

        public BtrustUserInputTypes? InputType { get; set; }

        public string Otp { get; set; }
    }

    public enum BtrustUserInputTypes
    {
        EGN = 0,

        LNCH = 1,

        PROFILE = 2,

        PHONE = 3,

        EMAIL = 4
    }
}
