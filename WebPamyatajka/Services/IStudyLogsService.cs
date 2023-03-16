using WebPamyatajka.Models;

namespace WebPamyatajka.Services;

public interface IStudyLogsService
{
    List<StudyLog> GetAllByUserId(int userId);
    List<StudyLog> GetAllByCardId(int cardId);
    StudyLog GetById(int id);
    StudyLog Create(StudyLog studyLog);
    void Update(StudyLog studyLog);
    void DeleteById(int studyLogId);
}