using AutoMapper;
using FamilyHistorySystem.AutoMapperProfiles;
using FamilyHistorySystem.DataAccess;
using FamilyHistorySystem.Models.DTOs;
using FamilyHistorySystem.Models.Entities;
using FamilyHistorySystem.Services.services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FamilyHistorySystem.Test
{
    public class StudentServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<DbSet<Student>> _mockSet;
        private readonly Mock<DBContexto> _mockContext;
        private readonly StudentService _service;

        public StudentServiceTests()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            _mapper = config.CreateMapper();

            _mockSet = new Mock<DbSet<Student>>();
            _mockContext = new Mock<DBContexto>(new DbContextOptions<DBContexto>());
            _mockContext.Setup(m => m.Students).Returns(_mockSet.Object);

            _service = new StudentService(_mockContext.Object, _mapper);
        }

        [Fact]
        public async Task CreateAsync_Should_Add_New_Student()
        {
            //arrange
            var dto = new StudentRequestDTO
            {
                NationalId = "12345678",
                FirstName = "Juan",
                LastName = "Pérez",
                Gender = Gender.Male,
                DateOfBirth = new DateTime(2010, 1, 1),
                MotherNationalId = "87654321",
                FatherNationalId = "87654322"
            };

            // Act
            var result = await _service.CreateAsync(dto);

            // Assert
            _mockSet.Verify(m => m.AddAsync(It.IsAny<Student>(), default), Times.Once());
            _mockContext.Verify(m => m.SaveChangesAsync(default), Times.Once());
            Assert.Equal(dto.NationalId, result.NationalId);
        }

    }
}