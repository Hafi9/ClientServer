using System;
using System.Collections.Generic;
using System.Linq;
using API.Contracts;
using API.DTOs.Employees;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;

namespace API.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IEducationRepository _educationRepository;
        private readonly IUniversityRepository _universityRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public EmployeeService(IEmployeeRepository employeeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository)
        {
            _employeeRepository = employeeRepository;
            _educationRepository = educationRepository;
            _universityRepository = universityRepository;
        }
        public IEnumerable<EmployeeDetailDto> GetAllEmployeeDetail()
        {
            var employees = _employeeRepository.GetAll();
            if (!employees.Any())
            {
                return Enumerable.Empty<EmployeeDetailDto>();
            }

            var employeesDetailDto = new List<EmployeeDetailDto>();

            foreach (var emp in employees)
            {
                var education = _educationRepository.GetByGuid(emp.Guid);
                var university = _universityRepository.GetByGuid(education.UniversityGuid);

                EmployeeDetailDto employeeDetail = new EmployeeDetailDto
                {
                    EmployeeGuid = emp.Guid,
                    Nik = emp.NIK,
                    FullName = emp.FirstName + " " + emp.LastName,
                    BirthDate = emp.BirthDate,
                    Gender = emp.Gender,
                    HiringDate = emp.HiringDate,
                    Email = emp.Email,
                    PhoneNumber = emp.PhoneNumber,
                    Major = education.Major,
                    Degree = education.Degree,
                    Gpa = education.GPA,
                    UniversityName = university.Name
                };

                employeesDetailDto.Add(employeeDetail);
            }

            return employeesDetailDto;
        }

        public EmployeeDetailDto? GetEmployeeDetailByGuid(Guid guid)
        {
            var employees = _employeeRepository.GetByGuid(guid);
            if (employees is null)
            {
                return null;
            }
            var education = _educationRepository.GetByGuid(employees.Guid);
            var university = _universityRepository.GetByGuid(education.UniversityGuid);

            return new EmployeeDetailDto
            {
                EmployeeGuid = employees.Guid,
                Nik = employees.NIK,
                FullName = employees.FirstName + " " + employees.LastName,
                BirthDate = employees.BirthDate,
                Gender = employees.Gender,
                HiringDate = employees.HiringDate,
                Email = employees.Email,
                PhoneNumber = employees.PhoneNumber,
                Major = education.Major,
                Degree = education.Degree,
                Gpa = education.GPA,
                UniversityName = university.Name
            };
        }
        public IEnumerable<EmployeeDto> GetAll()
        {
            var employees = _employeeRepository.GetAll();
            if (!employees.Any())
            {
                return Enumerable.Empty<EmployeeDto>(); // Employee is null or not found;
            }

            var employeeDtos = new List<EmployeeDto>();
            foreach (var employee in employees)
            {
                employeeDtos.Add((EmployeeDto)employee);
            }

            return employeeDtos; // Employee is found;
        }

        public EmployeeDto? GetByGuid(Guid guid)
        {
            var employee = _employeeRepository.GetByGuid(guid);
            if (employee is null)
            {
                return null; // Employee is null or not found;
            }

            return (EmployeeDto)employee; // Employee is found;
        }

        public EmployeeDto? Create(NewEmployeeDto newEmployeeDto)
        {
            Employee empNIK = newEmployeeDto;
            empNIK.NIK = GenerateHandler.Nik(_employeeRepository.Getlastnik());
            var employee = _employeeRepository.Create(empNIK);
            if (employee is null)
            {
                return null; // Employee is null or not found;
            }

            return (EmployeeDto)employee; // Employee is found;
        }

        public int Update(EmployeeDto employeeDto)
        {
            var employee = _employeeRepository.GetByGuid(employeeDto.Guid);
            if (employee is null)
            {
                return -1; // Employee is null or not found;
            }

            Employee toUpdate = employeeDto;
            // You may update other properties here if needed
            var result = _employeeRepository.Update(toUpdate);

            return result ? 1 // Employee is updated;
                : 0; // Employee failed to update;
        }

        public int Delete(Guid guid)
        {
            var employee = _employeeRepository.GetByGuid(guid);
            if (employee is null)
            {
                return -1; // Employee is null or not found;
            }

            var result = _employeeRepository.Delete(employee);

            return result ? 1 // Employee is deleted;
                : 0; // Employee failed to delete;
        }
    }
}
