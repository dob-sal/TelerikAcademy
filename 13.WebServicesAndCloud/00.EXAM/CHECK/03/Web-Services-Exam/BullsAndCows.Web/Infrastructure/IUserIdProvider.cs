﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BullsAndCows.Web.Infrastructure
{
    public interface IUserIdProvider
    {
        string GetUserId();
    }
}