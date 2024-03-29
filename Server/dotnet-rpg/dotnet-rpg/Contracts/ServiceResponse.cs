﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Contracts
{
    public class ServiceResponse<T>
    {
        public T Data { get; set; }
        public bool success { get; set; } = true;
        public string Message { get; set; } = "Success!";

    }
}
