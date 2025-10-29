﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTos.ProductModule
{
    public record TypeResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
