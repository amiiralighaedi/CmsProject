using Cms.Application.Content.DTOs;
using Cms.Application.Content.Interfaces;
using Cms.Application.Content.Queries;
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
        return await BuildBaseQuery(contentType)
            .OrderByDescending(x => x.PublishedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<ContentItemDto?> GetByIdAsync(string contentType, Guid id)
    {
        return await BuildBaseQuery(contentType)
            .Where(x => x.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<ContentItemDto?> GetBySlugAsync(string contentType, string slug)
    {
        return await BuildBaseQuery(contentType)
            .Where(x => x.Slug == slug)
            .FirstOrDefaultAsync();
    }

    public async Task<QueryResponse<object>> QueryAsync(
        string contentType,
        Dictionary<string, string>? filters,
        string? sort,
        bool desc,
        int page,
        int pageSize)
    {
        var query = BuildBaseQuery(contentType);

        // 🔵 فیلترها
        if (filters != null)
        {
            foreach (var f in filters)
            {
                query = query.Where(x =>
                    x.Fields.Any(v =>
                        v.Name == f.Key &&
                        v.Value.ToLower().Contains(f.Value.ToLower())
                    )
                );
            }
        }

        if (!string.IsNullOrWhiteSpace(sort))
        {
            if (sort == "publishedAt")
                query = desc ? query.OrderByDescending(x => x.PublishedAt) : query.OrderBy(x => x.PublishedAt);
            else
                query = desc
                    ? query.OrderByDescending(x => x.Fields.FirstOrDefault(f => f.Name == sort)!.Value)
                    : query.OrderBy(x => x.Fields.FirstOrDefault(f => f.Name == sort)!.Value);
        }

        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new QueryResponse<object>
        {
            Total = total,
            Page = page,
            PageSize = pageSize,
            Items = items
        };
    }

    private IQueryable<ContentItemDto> BuildBaseQuery(string contentType)
    {
        return
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
    }
}
