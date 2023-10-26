using Chain.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chain.Application.Contract.Models
{
    public record RateModel(double AverageRate,Rate RatesPercent);
}
