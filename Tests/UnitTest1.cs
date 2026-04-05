using Xunit;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Controllers;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using API.Controllers; // for ResourceNotFoundException

public class StudentControllerTests
{
    private readonly Mock<IStudentService> _mockService;
    private readonly StudentController _controller;

    public StudentControllerTests()
    {
        _mockService = new Mock<IStudentService>();
        _controller = new StudentController(_mockService.Object);
    }

    // ✅ GET ALL STUDENTS
    [Fact]
    public async Task GetAllStudents_ReturnsList()
    {
        var students = new List<Student>
        {
            new Student { Id = 1, Name = "Test", Email = "test@gmail.com" }
        };

        _mockService.Setup(s => s.GetAllStudentsAsync())
                    .ReturnsAsync(students);

        var result = await _controller.GetAllStudents();

        Assert.NotNull(result);
        Assert.Single(result);
    }

    // ✅ GET STUDENT BY ID
    [Fact]
    public async Task GetStudent_ReturnsStudent_WhenExists()
    {
        var student = new Student { Id = 1, Name = "Test" };

        _mockService.Setup(s => s.GetStudentByIdAsync(1))
                    .ReturnsAsync(student);

        var result = await _controller.GetStudent(1);

        Assert.NotNull(result);
        Assert.Equal(1, result.Id);
    }

    // ❌ GET STUDENT - 404
    [Fact]
    public async Task GetStudent_ThrowsNotFound_WhenNotExists()
    {
        _mockService.Setup(s => s.GetStudentByIdAsync(1))
                    .ReturnsAsync((Student)null);

        await Assert.ThrowsAsync<ResourceNotFoundException>(() => _controller.GetStudent(1));
    }

    // ✅ ADD STUDENT
    [Fact]
    public async Task AddStudent_ReturnsCreatedStudent()
    {
        var student = new Student { Id = 1, Name = "New Student" };

        _mockService.Setup(s => s.AddStudentAsync(It.IsAny<Student>()))
                    .ReturnsAsync(student);

        var result = await _controller.AddStudent(student);

        Assert.NotNull(result);
        Assert.Equal("New Student", result.Name);
    }

    // ✅ UPDATE STUDENT
    [Fact]
    public async Task UpdateStudent_ReturnsUpdatedStudent()
    {
        var student = new Student { Id = 1, Name = "Updated" };

        _mockService.Setup(s => s.UpdateStudentAsync(It.IsAny<Student>()))
                    .ReturnsAsync(student);

        var result = await _controller.UpdateStudent(1, student);

        Assert.NotNull(result);
        Assert.Equal("Updated", result.Name);
    }

    // ❌ UPDATE STUDENT - 404
    [Fact]
public async Task UpdateStudent_ThrowsNotFound_WhenNotExists()
{
    _mockService.Setup(s => s.UpdateStudentAsync(It.IsAny<Student>()))
                .ReturnsAsync((Student)null);

    await Assert.ThrowsAsync<ResourceNotFoundException>(() => _controller.UpdateStudent(1, new Student()));
}
   

    // ✅ DELETE STUDENT
    [Fact]
    public async Task DeleteStudent_ReturnsTrue_WhenDeleted()
    {
        _mockService.Setup(s => s.DeleteStudentAsync(1))
                    .ReturnsAsync(true);

        var result = await _controller.DeleteStudent(1);

        Assert.True(result);
    }

    // ❌ DELETE STUDENT - 404
 [Fact]
public async Task DeleteStudent_ThrowsNotFound_WhenNotExists()
{
    _mockService.Setup(s => s.DeleteStudentAsync(1))
                .ReturnsAsync(false);

    await Assert.ThrowsAsync<ResourceNotFoundException>(() => _controller.DeleteStudent(1));
}
}