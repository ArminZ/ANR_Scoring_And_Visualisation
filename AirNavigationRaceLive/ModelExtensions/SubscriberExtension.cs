using AirNavigationRaceLive.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirNavigationRaceLive.ModelExtensions
{
    [NotMapped]
    class SubscriberExtension : SubscriberSet
    {
        public string LastNameFirstName
        {
            get; set;
        }
    }
}
