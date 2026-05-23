namespace Cms.Api.Models.ContetnItems;

public class UpdateContentItemRequest
{
    public List<UpdateFiledValueRequest> Values { get; set; } = new();
}

public class UpdateFiledValueRequest
{
    public string FieldName { get; set; } = default!;
    public string? Values { get; set; }

}