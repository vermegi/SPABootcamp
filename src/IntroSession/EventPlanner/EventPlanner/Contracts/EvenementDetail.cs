﻿using System;
using System.Collections.Generic;
using EventPlanner.Models;

namespace EventPlanner.Contracts
{
    public class EvenementDetail
    {
        public Evenement Evenement { get; set; }
        public string Message { get; set; }
        public IEnumerable<DateTime> ReservatieData { get; set; }
    }
}