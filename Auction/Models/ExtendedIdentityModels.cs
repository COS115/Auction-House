﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auction.Models
{
    public class ExtendedIdentityModels:RegisterViewModel
    {
        public HttpPostedFileBase UserProfilePicture { get; set; }
    }
}