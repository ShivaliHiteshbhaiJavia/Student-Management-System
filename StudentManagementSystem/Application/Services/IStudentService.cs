using Domain.Entities;

public interface IStudentService
{
    Task<IEnumerable<Student>> GetAll();
}
