using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chain.Domain.Entities;
using Chain.Domain.Enums;
using Chain.Domain.Models;

namespace Chain.Domain.Services
{
    public class RateService
    {
        public RateService() { }

        public double GetAverageRate(List<Comment> comments)
        {
            var one = comments.Count(c => c.RateNumber == RateNumber.One);
            var two = comments.Count(c => c.RateNumber == RateNumber.Two);
            var three = comments.Count(c => c.RateNumber == RateNumber.Three);
            var four = comments.Count(c => c.RateNumber == RateNumber.Four);
            var five = comments.Count(c => c.RateNumber == RateNumber.Five);

            return (double)(1 * one + 2 * two + 3 * three + 4 * four + 5 * five)
                 / (double)(one + two + three + four + five);
        }

        public Rate GetPercentRateNumbers(List<Comment> comments)
        {
            var one = comments.Count(c => c.RateNumber == RateNumber.One);
            var two = comments.Count(c => c.RateNumber == RateNumber.Two);
            var three = comments.Count(c => c.RateNumber == RateNumber.Three);
            var four = comments.Count(c => c.RateNumber == RateNumber.Four);
            var five = comments.Count(c => c.RateNumber == RateNumber.Five);

            return new Rate
            {
                One =
                    one * 1 * 100
                    / (1 * (int)one + 2 * (int)two + 3 * (int)three + 4 * four + 5 * five),
                Two =
                    two * 2 * 100
                    / (1 * (int)one + 2 * (int)two + 3 * (int)three + 4 * four + 5 * five),
                Three =
                    three * 3 * 100
                    / (1 * (int)one + 2 * (int)two + 3 * (int)three + 4 * four + 5 * five),
                Four =
                    four * 4 * 100
                    / (1 * (int)one + 2 * (int)two + 3 * (int)three + 4 * four + 5 * five),
                Five =
                    five * 5 * 100
                    / (1 * (int)one + 2 * (int)two + 3 * (int)three + 4 * four + 5 * five)
            };
        }
    }
}
