using Chain.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chain.Application.Contract.Models
{
    public record CommentDto(
        string WriterAlias,
        string Title, 
        string Description, 
        string Gmail, 
        DateTimeOffset DateTimeCommented, 
        bool Suggest, 
        int VoteUps, 
        int VoteDowns, 
        RateNumber RateNumber);
}
