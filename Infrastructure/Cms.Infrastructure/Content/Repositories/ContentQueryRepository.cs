using Cms.Application.Content.DTOs;
using Cms.Application.Content.Interfaces;
using Cms.Domain.Content.ContentItems;
using Cms.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Cms.Infrastructure.Content.Repositories;

public class ContentQueryRepository : IContentQueryRepository
{
    private readonly ApplicationDbContext _context;

    public ContentQueryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ContentItemDto>> GetListAsync(string contentType, int page, int pageSize)
    {
        var query =
            from item in _context.ContentItems
                .Include(i => i.Versions)
                .Include(i => i.ContentType)
            where item.ContentType.Slug == contentType
            let publishedVersion = item.Versions
                .Where(v => v.IsPublished)
                .OrderByDescending(v => v.PublishedAt)
                .FirstOrDefault()
            where publishedVersion != null
            select new ContentItemDto
            {
                Id = item.Id,
                ContentType = item.ContentType.Slug,
                Slug = item.Slug,
                PublishedAt = publishedVersion.PublishedAt!.Value,
                Fields = publishedVersion.Values
                    .Select(f => new ContentFieldDto
                    {
                        Name = f.FieldName,
                        Value = f.Value
                    }).ToList()
            };

        return await query
            .OrderByDescending(x => x.PublishedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<ContentItemDto?> GetByIdAsync(string contentType, Guid id)
    {
        var query =
            from item in _context.ContentItems
                .Include(i => i.Versions)
                .Include(i => i.ContentType)
            where item.Id == id && item.ContentType.Slug == contentType
            let publishedVersion = item.Versions
                .Where(v => v.IsPublished)
                .OrderByDescending(v => v.PublishedAt)
                .FirstOrDefault()
            where publishedVersion != null
            select new ContentItemDto
            {
                Id = item.Id,
                ContentType = item.ContentType.Slug,
                Slug = item.Slug,
                PublishedAt = publishedVersion.PublishedAt!.Value,
                Fields = publishedVersion.Values
                    .Select(f => new ContentFieldDto
                    {
                        Name = f.FieldName,
                        Value = f.Value
                    }).ToList()
            };

        return await query.FirstOrDefaultAsync();
    }

    public async Task<ContentItemDto?> GetBySlugAsync(string contentType, string slug)
    {
        var query =
            from item in _context.ContentItems
                .Include(i => i.Versions)
                .Include(i => i.ContentType)
            where item.Slug == slug && item.ContentType.Slug == contentType
            let publishedVersion = item.Versions
                .Where(v => v.IsPublished)
                .OrderByDescending(v => v.PublishedAt)
                .FirstOrDefault()
            where publishedVersion != null
            select new ContentItemDto
            {
                Id = item.Id,
                ContentType = item.ContentType.Slug,
                Slug = item.Slug,
                PublishedAt = publishedVersion.PublishedAt!.Value,
                Fields = publishedVersion.Values
                    .Select(f => new ContentFieldDto
                    {
                        Name = f.FieldName,
                        Value = f.Value
                    }).ToList()
            };

        return await query.FirstOrDefaultAsync();
    }
}
