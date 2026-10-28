namespace SGCAE.Persistence.Seeds;

// Un "sembrador" de datos iniciales. Order define en qué orden se ejecuta (los que dependen de otros, después).
public interface IDataSeeder
{
    int Order { get; }

    Task SeedAsync(CancellationToken cancellationToken = default);
}
