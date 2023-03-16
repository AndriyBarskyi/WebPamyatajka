using WebPamyatajka.Models;

namespace WebPamyatajka.Services;

public interface IStudyLogsService
{
    List<StudyLog> GetAll();
    StudyLog GetById(int id);
    StudyLog Create(StudyLog studyLog);
    void Update(StudyLog studyLog);
    void Delete(StudyLog studyLog);
}