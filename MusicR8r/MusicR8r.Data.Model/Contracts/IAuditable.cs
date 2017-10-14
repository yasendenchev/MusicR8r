using System;

namespace MusicR8r.Data.Model.Contracts
{
    public interface IAuditable
    {
        DateTime? CreatedOn { get; set; }

        DateTime? DeletedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
