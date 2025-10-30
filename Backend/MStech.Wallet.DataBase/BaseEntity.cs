using System;

namespace Entity.Base;

public class BaseEntity<Tkey>
{
    public Tkey Id { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime? ModifyDate { get; set; }
    public string? CreatorId { get; set; }
    public string? ModifierId { get; set; }
    public bool IsDeleted { get; set; } = false;
}