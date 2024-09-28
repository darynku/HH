using FluentResults;
using HH.Domain.Common;
using HH.Domain.Entitties.ValueObjects;
using HH.Domain.Shared.Files;
namespace HH.Domain.Entitties;

public class Vacancy : BaseEntity
{
    private Vacancy() { }

    public string Title { get; init; } = string.Empty; // Название
    public string Description { get; init; } = string.Empty; // Описание
    public SalaryRange Salary { get; init; } = null!; // Зп в формате 1000-2000
    public DateTime PostedDate { get; init; } = DateTime.UtcNow; // Когда была создана
    public Guid UserId { get; init; }
    public User User { get; init; } = null!; 

    private List<ItemFile> _files = [];
    public IReadOnlyList<ItemFile> Files => _files; // Файл для вакансии

    public string Region { get; init; } = string.Empty; //Регион где будет работа
    public int Views { get; private set; } = 0; //Просмотры
    public string Position { get; init; } = string.Empty; // Должность
    public int WorkExperience { get; init; } // Опыт работы в годах
    public DateTime ExpirationDate { get; init; } // Дата истечения вакансии
    public int ApplicationsCount { get; private set; } = 0; // Количество откликов

    private Vacancy(
        Guid id,
        IEnumerable<ItemFile> files,
        string title,
        string description,
        SalaryRange salary,
        DateTime postedDate,
        Guid userId,
        string region,
        string position,
        int workExperience,
        DateTime expirationDate) : base(id)
    {
        _files = files.ToList() ?? new List<ItemFile>();
        Title = title;
        Description = description;
        Salary = salary;
        PostedDate = postedDate;
        UserId = userId;
        Region = region;
        Position = position;
        WorkExperience = workExperience;
        ExpirationDate = expirationDate;
    }

    public static Result<Vacancy> Create(
        Guid id,
        IEnumerable<ItemFile> files,
        string title,
        string description,
        SalaryRange salary,
        DateTime postedDate,
        Guid userId,
        string region,
        string position,
        int workExperience,
        DateTime expirationDate)
    {
        if (string.IsNullOrEmpty(title))
            return Result.Fail("Поле заголовка не должно быть пустым.");
        if (userId == Guid.Empty)
            return Result.Fail(nameof(userId));
        if (workExperience < 0)
            return Result.Fail("Опыт работы не может быть отрицательным.");
        if (expirationDate <= DateTime.UtcNow)
            return Result.Fail("Срок истечения вакансии должен быть в будущем.");

        var vacancy = new Vacancy(
            Guid.NewGuid(),
            files,
            title,
            description,
            salary,
            postedDate,
            userId,
            region,
            position,
            workExperience,
            expirationDate);

        return Result.Ok(vacancy);
    }

    public void UpdateFile(List<ItemFile> files)
    {
        _files = files;
    }
    // Увеличить количество просмотров
    public void IncrementViews()
    {
        Views++;
    }

    // Добавить отклик
    public void AddApplication()
    {
        ApplicationsCount++;
    }
}
