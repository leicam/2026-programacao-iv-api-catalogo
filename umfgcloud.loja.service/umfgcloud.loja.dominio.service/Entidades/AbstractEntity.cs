namespace umfgcloud.loja.dominio.service.Entidades;

public abstract class AbstractEntity
{    
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string CreatedByUserId { get; private set; } = string.Empty;
    public string CreatedByEmail { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public string UpdatedByUserId { get; private set; } = string.Empty;
    public string UpdatedByEmail { get; private set; } = string.Empty;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;    
    public bool IsActive { get; private set; } = true;

    protected AbstractEntity() { }

    protected AbstractEntity(string userId, string userEmail)
    {
        CreatedByUserId = userId ?? throw new ArgumentNullException(nameof(userId));
        CreatedByEmail = userEmail ?? throw new ArgumentNullException(nameof(userEmail));
        UpdatedByUserId = userId ?? throw new ArgumentNullException(nameof(userId));
        UpdatedByEmail = userEmail ?? throw new ArgumentNullException(nameof(userEmail));
    }

    public virtual void Activate() => IsActive = true;
    public virtual void Inactivate() => IsActive = false;

    public void Update(string userId, string userEmail)
    {
        UpdatedByUserId = userId ?? throw new ArgumentNullException(nameof(userId));
        UpdatedByEmail = userEmail ?? throw new ArgumentNullException(nameof(userEmail));
        UpdatedAt = DateTime.UtcNow;
    }    
}