﻿using Bloggy.web.Models.Domain;

namespace Bloggy.web.Repository
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>>  GetAllAsync();
        Task<Tag> GetAsync(Guid id);
        Task<Tag> AddAsync(Tag tag);
        Task<Tag?> UpdateAsync(Tag tag);
        Task<Tag?> DeleteAsync(Guid id);

    }
}
