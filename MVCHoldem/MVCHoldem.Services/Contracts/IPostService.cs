namespace MVCHoldem.Services.Contracts
{
    using System.Collections.Generic;
    using MVCHoldem.Data.Models;

    public interface IPostService
    {
        IEnumerable<Post> GetMostRecent();
    }
}