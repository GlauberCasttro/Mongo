﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public abstract class Entity
    {
        public string Id { get; set; }
        public DateTime Datacadastro { get; set; }
    }
}
