﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStore.Core.Models.Request
{
    public class PurchaseRequestModel
    {

        public PurchaseRequestModel(){
            PurchaseDateTime = DateTime.Now;
            PurchaseNumber = Guid.NewGuid();
        }
        public Guid PurchaseNumber { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public decimal? TotalPrice { get; set; }
        
        public DateTime? PurchaseDateTime { get; set; }

    }
}
