﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp2.BusinessLayer.EntityModel;

public class AddPolicyRequestEntity
{
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PolicyId { get; set; }
    public int PolicyTenure { get; set; }
    public string PolicyName { get; set; }
}
