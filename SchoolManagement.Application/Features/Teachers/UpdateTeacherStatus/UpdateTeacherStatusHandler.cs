using SchoolManagement.Application.Abstractions.Persistence;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Teachers.UpdateTeacherStatus;

public class UpdateTeacherStatusHandler
{
    private readonly ITeacherRepository _teacherRepository;

    public UpdateTeacherStatusHandler(ITeacherRepository teacherRepository)
    {
        _teacherRepository = teacherRepository;
    }

    private async Task<Teacher?> GetTeacherAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _teacherRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<bool> VerifyEmailAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var teacher = await GetTeacherAsync(id, cancellationToken);

        if (teacher is null)
        {
            return false;
        }

        teacher.VerifyEmail();

        await _teacherRepository.UpdateAsync(teacher, cancellationToken);

        return true;
    }

    public async Task<bool> ActivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var teacher = await GetTeacherAsync(id, cancellationToken);

        if (teacher is null)
        {
            return false;
        }

        teacher.ActivateTeacher();

        await _teacherRepository.UpdateAsync(teacher, cancellationToken);

        return true;
    }

    public async Task<bool> DeactivateAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var teacher = await GetTeacherAsync(id, cancellationToken);

        if (teacher is null)
        {
            return false;
        }

        teacher.DeactivateTeacher();

        await _teacherRepository.UpdateAsync(teacher, cancellationToken);

        return true;
    }

    public async Task<bool> PutOnLeaveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var teacher = await GetTeacherAsync(id, cancellationToken);

        if (teacher is null)
        {
            return false;
        }

        teacher.PutTeacherOnLeave();

        await _teacherRepository.UpdateAsync(teacher, cancellationToken);

        return true;
    }
}
