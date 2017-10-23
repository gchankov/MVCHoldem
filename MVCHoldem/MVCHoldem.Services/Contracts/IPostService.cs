namespace MVCHoldem.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using MVCHoldem.Data.Models;

    public interface IPostService
    {
        IEnumerable<Post> GetMostRecent();

        Post GetById(Guid id);

        IEnumerable<Post> AllWithoutDeleted(string searchPattern = "");

        IEnumerable<Post> AllIncludingDeleted();

        Post AddNewPost(string title, string description, string content, string author);

        void Update(Post post);

        void Delete(Post post);

        void HardDelete(Post post);
    }
}